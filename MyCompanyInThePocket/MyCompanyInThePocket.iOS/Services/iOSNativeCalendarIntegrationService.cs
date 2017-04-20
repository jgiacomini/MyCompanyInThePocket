using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MyCompanyInThePocket.Core.Services.Interface;
using MyCompanyInThePocket.Core.Models;
using System.Threading.Tasks;
using EventKit;
using Plugin.Settings;
using MvvmCross.Platform.iOS;

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
            //_eventStore.
        }

        public async Task PushMeetingsToCalendarAsync(List<Meeting> meetings)
        {
            try
            {
                var tcs = new TaskCompletionSource<bool>();

                _eventStore.RequestAccess(EKEntityType.Event, (a, e) => { tcs.TrySetResult(a); });

                // pas de r�sultat
                if (!await tcs.Task)
                {
                    return;
                }

                var calendar = _eventStore.GetCalendar(AcraCalendarIdentifier);
                NSError errorToDelete;
                _eventStore.RemoveCalendar(calendar, true, out errorToDelete);

                calendar = null;
                if (calendar == null)
                {

                    calendar = EKCalendar.FromEventStore(_eventStore);
                    calendar.Title = "ACRA";

                    calendar.Source = _eventStore.Sources.FirstOrDefault(s => s.SourceType == EKSourceType.Subscribed || s.SourceType == EKSourceType.Local);
                    NSError error;
                    _eventStore.SaveCalendar(calendar, true, out error);
                    AcraCalendarIdentifier = calendar.CalendarIdentifier;
                }


                var toSaves = meetings.OrderByDescending(ap => ap.StartDate).ToArray();

                var howMuch = toSaves.Length;
                for (int index = 0; index < howMuch; index++)
                {
                    var appointData = toSaves[index];

                    // If set to AllDay and given an EndDate of 12am the next day, EventKit
                    // assumes that the event consumes two full days.
                    // (whereas WinPhone/Android consider that one day, and thus so do we)
                    //
                    if (appointData.AllDayEvent)
                    {
                        appointData.EndDate = appointData.EndDate.AddMilliseconds(-1);
                    }

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


                    if (appointData.Title != null && appointData.Title.IndexOf("f�ri�", StringComparison.OrdinalIgnoreCase) == -1)
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
            catch (Exception)
            {

            }
        }

        public NSDate ConvertDateTimeToNSDate(DateTime date)
        {
            return date.ToNSDate();

            date = date.ToLocalTime();
            var newDate = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(2001, 1, 1, 0, 0, 0));
            return NSDate.FromTimeIntervalSinceReferenceDate((date - newDate).TotalSeconds);
        }
    }
}