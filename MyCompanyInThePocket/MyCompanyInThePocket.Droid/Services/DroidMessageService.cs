using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using MyCompanyInThePocket.Core.Services;
using Plugin.CurrentActivity;
using System;
using System.Threading.Tasks;

namespace MyCompanyInThePocket.Droid.Services
{
    public class DroidMessageService : IMessageService
    {
        public Task ShowErrorToastAsync(Exception exception, string message)
        {
            var context = CrossCurrentActivity.Current.Activity;

            var toast = Toast.MakeText(context,
                message
#if DEBUG
                     + ": " + exception.Message
#endif

                , Snackbar.LengthLong);
            toast.SetGravity(GravityFlags.Top | GravityFlags.CenterHorizontal, 0, 0);
            toast.Show();

            return Task.FromResult(true);
        }
    }
}