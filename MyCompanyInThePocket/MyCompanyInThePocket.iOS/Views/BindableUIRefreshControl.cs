using System;
using System.Windows.Input;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views
{
	/// <summary>
	/// Mvx user interface refresh control.
	/// </summary>
	public class BindableUIRefreshControl : UIRefreshControl
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="MvxUIRefreshControl"/> class.
		/// </summary>
		public BindableUIRefreshControl()
		{
            this.ValueChanged += Handle_ValueChanged;
		}

        void Handle_ValueChanged(object sender, EventArgs e)
        {
			var command = RefreshCommand;
			if (command == null)
				return;
			command.Execute(null);    
        }

		private string _message;
		/// <summary>
		/// Gets or sets the message to display
		/// </summary>
		/// <value>The message.</value>
		public string Message
		{
			get { return _message; }
			set
			{
				_message = value ?? string.Empty;
				this.AttributedTitle = new Foundation.NSAttributedString(_message);
			}
		}

		private bool _isRefreshing;
		/// <summary>
		/// Gets or sets a value indicating whether this instance is refreshing.
		/// </summary>
		/// <value><c>true</c> if this instance is refreshing; otherwise, <c>false</c>.</value>
		public bool IsRefreshing
		{
			get { return _isRefreshing; }
			set
			{
				_isRefreshing = value;
				if (_isRefreshing)
					BeginRefreshing();
				else
					EndRefreshing();
			}
		}

		/// <summary>
		/// Gets or sets the refresh command.
		/// </summary>
		/// <value>The refresh command.</value>
		public ICommand RefreshCommand { get; set; }
	}

}
