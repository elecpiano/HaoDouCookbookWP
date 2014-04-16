using System;
using System.Windows.Controls;
using HaoDouCookbookWP.Animations;
using System.Windows.Media;
using HaoDouCookbookWP.Utility;

namespace HaoDouCookbookWP.Controls
{
    public partial class SubCategoryButton : UserControl
    {
        private bool Hidden = false;

        public SubCategoryButton()
        {
            InitializeComponent();
        }

        //public void Init(string title, string colorHex)
        //{
        //    this.titleTextBlock.Text = title;
        //    this.ellipse.Fill = new SolidColorBrush(ColorConverter.Convert(colorHex));
        //}

        public void Show(double delay, double duration)
        {
            ScaleAnimation.ScaleFromTo(rect, 0d, 0d, 0d, 0d, TimeSpan.FromMilliseconds(delay),
                fe =>
                {
                    ScaleAnimation.ScaleFromTo(rect, 0d, 0d, 1d, 1d, TimeSpan.FromMilliseconds(duration), null);
                });

            Hidden = false;
        }

        public void Hide(bool withTransition, double delay, double duration)
        {
            if (Hidden)
            {
                return;
            }
            if (withTransition)
            {
                ScaleAnimation.ScaleFromTo(rect, 1d, 1d, 1, 1d, TimeSpan.FromMilliseconds(delay), fe => ScaleAnimation.ScaleFromTo(rect, 1d, 1d, 0d, 0d, TimeSpan.FromMilliseconds(duration), null));
            }
            else
            {
                ScaleAnimation.ScaleFromTo(rect, 0.05d, 0.05d, 0.05d, 0.05d, TimeSpan.FromMilliseconds(0d), null);
            }
            Hidden = true;
        }
    }
}
