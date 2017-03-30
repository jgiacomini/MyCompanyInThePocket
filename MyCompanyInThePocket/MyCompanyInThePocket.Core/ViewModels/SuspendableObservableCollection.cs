using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
namespace MyCompanyInThePocket.Core.ViewModels
{
	public class SuspendableObservableCollection<T> : ObservableCollection<T>
	{
		private bool _isPaused;


		protected override void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
			if (!_isPaused)
			{
				base.OnCollectionChanged(e); 
			}
		}

		public void PauseNotifications()
		{
			_isPaused = true;
		}

		public void ResumeNotifications()
		{
			_isPaused = false;
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}
	}
}