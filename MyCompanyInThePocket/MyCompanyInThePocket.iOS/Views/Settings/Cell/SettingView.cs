using System;
using Cirrious.FluentLayouts.Touch;
using MyCompanyInThePocket.Core.ViewModels.Settings;
using UIKit;

namespace MyCompanyInThePocket.iOS.Views.Settings.Cell
{
	public class SettingView : UIView
	{
       	nfloat _hmargin = 15;
		nfloat _hSeparator = 1;

		public SettingView()
		{
			BackgroundColor = ApplicationColors.CellBackgroundColor;

			Label = new UILabel();
            Label.TextColor = ApplicationColors.ForegroundSettingCellColor;
			Label.Font = UIFont.FromName(ApplicationStyle.ContentFontName, ApplicationStyle.TitleCellFontSize);

            TopSeparator = new UIView();
            TopSeparator.BackgroundColor = ApplicationColors.SeparatorColor;
          
            BottomSeparator = new UIView();
			BottomSeparator.BackgroundColor = ApplicationColors.SeparatorColor;
		
			this.AddSubviews(Label,TopSeparator, BottomSeparator);
		}

        public UIView TopSeparator { get; set; }
		public UILabel Label { get; set; }
		public UIView BottomSeparator { get; set; }
        public SettingsHorizontalAlignment HorizontalAlignment
        {
            get;
            set;
        }

        public void ApplyFirstCellStyle()
        {
           
        }

        public bool HaveFullTopSeparator
        {
            get;
            set;
        }

		public bool HaveFullBottomSeparator
		{
            get;
            set;
		}

		void ApplyTopCellStyle()
		{
            if (HaveFullTopSeparator)
            {
                this.AddConstraints(
                    TopSeparator.AtTopOf(this),
                    TopSeparator.AtLeftOf(this),
                    TopSeparator.WithSameWidth(this),
                    TopSeparator.Height().EqualTo(_hSeparator));
            }
            else
            {
				this.AddConstraints(
				   TopSeparator.AtTopOf(this),
				   TopSeparator.AtLeftOf(this, _hmargin),
				   TopSeparator.WithSameWidth(this),
				   TopSeparator.Height().EqualTo(_hSeparator));
            }
		}

		void ApplyBottomCellStyle()
		{
            if (HaveFullBottomSeparator)
            {
                BottomSeparator.Hidden = false;
                this.AddConstraints(
                    BottomSeparator.AtBottomOf(this),
                    BottomSeparator.AtLeftOf(this),
                    BottomSeparator.WithSameWidth(this),
                    BottomSeparator.Height().EqualTo(_hSeparator));
            }
            else
            {
                BottomSeparator.Hidden = true;
            }
		}

        public void CreateConstraints()
        {
			this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
 			foreach (var constraint in Constraints)
            {
                this.RemoveConstraint(constraint);
            }

			ApplyTopCellStyle();
            ApplyBottomCellStyle();
			switch (HorizontalAlignment)
            {
                case SettingsHorizontalAlignment.Left :
					this.AddConstraints(
						Label.WithSameCenterY(this),
						Label.AtLeftOf(this, _hmargin));
                    break;
                case SettingsHorizontalAlignment.Right:
					this.AddConstraints(
					    Label.WithSameCenterY(this),
                        Label.AtRightOf(this, _hmargin));
                    break;
                case SettingsHorizontalAlignment.Center:
                    Label.TextAlignment = UITextAlignment.Center;
                    this.AddConstraints(
                        Label.WithSameWidth(this),
                        Label.WithSameCenterY(this),
                        Label.WithSameCenterX(this));
                    break;
                default:
                    break;
            }
        }
	}
}
