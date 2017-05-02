using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using MyCompanyInThePocket.Core.Services.Interface;
using MyCompanyInThePocket.Core.Models;
using System.Threading.Tasks;
using EventKit;
using Plugin.Settings;
using MvvmCross.Platform.iOS;
using Nito.AsyncEx;
using CoreGraphics;
using MvvmCross.Platform;
using System.Diagnostics;
using MyCompanyInThePocket.Core.Services;

namespace MyCompanyInThePocket.iOS.Services
{
    public class iOSNativeCalendarIntegrationService : INativeCalendarIntegrationService
    {
        private EKEventStore _eventStore;

        public static string AcraCalendarIdentifier
        {
            get { return CrossSettings.Current.GetValueOrDefault<string>(nameof(AcraCalendarIdentifier), ""); }
            set { CrossSettings.Current.AddOrUpdateValue(nameof(AcraCalendarIdentifier), value); }
        }

        public iOSNativeCalendarIntegrationService()
        {
            _eventStore = new EKEventStore();
        }

        private AsyncLock _PushMeetingsToCalendarAsyncLock = new AsyncLock();

		public async Task AddReminder(string title, string notes, DateTime date)
		{
			try
			{
				var result = await _eventStore.RequestAccessAsync(EKEntityType.Reminder);
				if (result.Item1)
				{
					EKReminder reminder = EKReminder.Create(_eventStore);
					reminder.Title = title;
					EKAlarm timeToRing = new EKAlarm();
					timeToRing.AbsoluteDate = ConvertDateTimeToNSDate(date);
        			reminder.AddAlarm(timeToRing);
					reminder.Calendar = _eventStore.DefaultCalendarForNewReminders;
					reminder.Notes = notes;
					NSError error;
					_eventStore.SaveReminder(reminder, true, out error);

					Debug.WriteLine(error?.Description);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

        public async Task PushMeetingsToCalendarAsync(List<Meeting> meetings)
        {
            try
            {
                using (await _PushMeetingsToCalendarAsyncLock.LockAsync())
                {
                    if (!(await _eventStore.RequestAccessAsync(EKEntityType.Event)).Item1)
                    {
                        return;
                    }

                    var calendar = _eventStore.GetCalendar(AcraCalendarIdentifier);
                    CGColor colorToUse = null;
                    if (calendar != null)
                    {
                        colorToUse = calendar.CGColor;
                        NSError errorToDelete; ;
                        _eventStore.RemoveCalendar(calendar, true, out errorToDelete);
                    }

                    // now recreate a calendar !
                    calendar = EKCalendar.FromEventStore(_eventStore);
                    calendar.Title = "ACRA du " + DateTime.Now.ToString("g");
                    if (colorToUse != null)
                    {
                        calendar.CGColor = colorToUse;
                    }

                    var sourceToSet = _eventStore.Sources
                        .FirstOrDefault(s =>
                       (s.SourceType == EKSourceType.CalDav && s.Title.Equals("iCloud", StringComparison.InvariantCultureIgnoreCase))
                       || s.SourceType == EKSourceType.Local); ;

                    if (sourceToSet == null)
                    {
                        sourceToSet = _eventStore.DefaultCalendarForNewEvents.Source;
                    }

                    calendar.Source = sourceToSet;

                    NSError error;
                    _eventStore.SaveCalendar(calendar, true, out error);
                    AcraCalendarIdentifier = calendar.CalendarIdentifier;

                    var toSaves = meetings.OrderByDescending(ap => ap.StartDate).ToArray();

                    var howMuch = toSaves.Length;
                    for (int index = 0; index < howMuch; index++)
                    {
                        var appointData = toSaves[index];

                        var toSave = EKEvent.FromStore(_eventStore);

                        //if (!string.IsNullOrEmpty(appointData.))
                        //{
                        //    toSave.Location = appointData.Location;
                        //}
                        toSave.StartDate = ConvertDateTimeToNSDate(appointData.StartDate);
                        toSave.AllDay = appointData.AllDayEvent;

                        if (appointData.IsRecurrent && appointData.AllDayEvent)
                        {
                            var end = EKRecurrenceEnd.FromEndDate(ConvertDateTimeToNSDate(appointData.EndDate));
                            var rule = new EKRecurrenceRule(EKRecurrenceFrequency.Weekly, 1, end);
                            toSave.RecurrenceRules = new[] { rule };
                        }
                        else
                        {
                            toSave.EndDate = ConvertDateTimeToNSDate(appointData.EndDate);
                        }

                        // If set to AllDay and given an EndDate of 12am the next day, EventKit
                        // assumes that the event consumes two full days.
                        // (whereas WinPhone/Android consider that one day, and thus so do we)
                        //
                        if (appointData.AllDayEvent)
                        {
                            toSave.EndDate = ConvertDateTimeToNSDate(appointData.EndDate.AddDays(-1));
                        }

						if (!appointData.IsHoliday)
                        {
                            if (appointData.Duration.TotalDays > 1 && !appointData.IsRecurrent)
                            {
                                toSave.Title = ((int)appointData.Duration.TotalDays + 1) + " " + appointData.Title;
                            }
                            else
                            {
                                toSave.Title = appointData.Title;
                            }
                        }
                        else
                        {
                            toSave.Title = "[FERIE] " + appointData.Title;
                        }

                        toSave.Notes = appointData.Type.ToString();

                        toSave.Calendar = calendar;

                        NSError errorEvent;
                        _eventStore.SaveEvent(toSave, EKSpan.ThisEvent, out errorEvent);
                    }
                }
            }
            catch (Exception e)
            {
				//TODO : localisation
                Mvx.Resolve<IMessageService>().ShowErrorToastAsync(e, "Impossible de renseigner votre calendrier iOS");
            }
        }

        public NSDate ConvertDateTimeToNSDate(DateTime date)
        {
            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
            return date.ToNSDate();
        }

        public async Task DeleteCalendarAsync()
        {
            try
            {
                using (await _PushMeetingsToCalendarAsyncLock.LockAsync())
                {
                    // pas de r�sultat
                    if (!(await _eventStore.RequestAccessAsync(EKEntityType.Event)).Item1)
                    {
                        return;
                    }

                    var calendar = _eventStore.GetCalendar(AcraCalendarIdentifier);
                    CGColor colorToUse = null;
                    if (calendar != null)
                    {
                        colorToUse = calendar.CGColor;
                        NSError errorToDelete; ;
                        _eventStore.RemoveCalendar(calendar, true, out errorToDelete);
                    }
                }
            }
            catch (Exception e)
            {
                Mvx.Resolve<IMessageService>().ShowErrorToastAsync(e, "Impossible de renseigner votre calendrier iOS");
            }
        }
    }
}