using UIKit;

namespace MyCompanyInThePocket.iOS
{
	public class ToastView : UIView
	{
		public ToastView()
		{
			BackgroundColor = ApplicationColors.ToastBackgroundColor;
			TextView = new UILabel();
			TextView.TextColor = ApplicationColors.ToastForegroundColor;
			TextView.Font = UIFont.FromName(ApplicationStyle.ToastFontName, ApplicationStyle.ToastFontSize);
			TextView.LineBreakMode = UILineBreakMode.TailTruncation;
			TextView.TextAlignment = UITextAlignment.Center;
			TextView.Text = "Aucune connexion internet";
			AddSubviews(TextView);
		}

		public void Show()
		{
			this.Hidden = false;
			this.Alpha = 0;

		  	UIView.Animate(1, 0, UIViewAnimationOptions.CurveEaseIn, () => 
		    {
				this.Alpha = 0.8f;
		    }, () => UIView.Animate(5, () => 
		    {
		        this.Alpha = 1;
		    }));
		}

		public void Hide()
		{ 
			this.Alpha = 1;
		  	UIView.Animate(1, 0, UIViewAnimationOptions.CurveEaseIn, () => 
			{
				this.Alpha = 0.2f;
			}, () => UIView.Animate(5, () => 
			{
				this.Alpha = 0;
				this.Hidden = true;
			}));
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
			TextView.Frame = new CoreGraphics.CGRect(0, 0, this.Bounds.Width, this.Bounds.Height);
		}

		public UILabel TextView
		{
			get;
			set;
		}
	}
}
