using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HaoDouCookbookWP.Animations;
using System.Windows.Media;
using HaoDouCookbookWP.Utility;

namespace HaoDouCookbookWP.Controls
{
    public partial class CategoryButton : UserControl
    {
        private bool Hidden = false;

        public CategoryButton()
        {
            InitializeComponent();
        }

        public void Init(string title, string colorHex)
        {
            this.titleTextBlock.Text = title;
            this.ellipse.Fill = new SolidColorBrush(ColorConverter.Convert(colorHex));
        }

        public void Show(double delay, double duration)
        {
            ScaleAnimation.ScaleFromTo(ellipse, 0.05d, 0.05d, 0.05d, 0.05d, TimeSpan.FromMilliseconds(delay), fe => ScaleAnimation.ScaleFromTo(ellipse, 0.05d, 0.05d, 1d, 1d, TimeSpan.FromMilliseconds(duration), null));
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
                ScaleAnimation.ScaleFromTo(ellipse, 1d, 1d, 1, 1d, TimeSpan.FromMilliseconds(delay), fe => ScaleAnimation.ScaleFromTo(ellipse, 1d, 1d, 0.05d, 0.05d, TimeSpan.FromMilliseconds(duration), null));
            }
            else
            {
                ScaleAnimation.ScaleFromTo(ellipse, 0.05d, 0.05d, 0.05d, 0.05d, TimeSpan.FromMilliseconds(0d), null);
            }
            Hidden = true;
        }
    }
}
