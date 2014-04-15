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

namespace HaoDouCookbookWP.Controls
{
    public partial class IngredientItem : UserControl
    {
        public IngredientItem()
        {
            InitializeComponent();
        }

        Point POINT_ZERO = new Point(0, 0);
        TimeSpan duration = TimeSpan.FromMilliseconds(300);
        public void Optimize()
        {
            Point point;
            point = textblock_1.TransformToVisual(textblock_1opt).Transform(POINT_ZERO);
            MoveAnimation.MoveFromTo(textblock_1, 0d, 0d, 0 - point.X, 0d, duration, null);

            point = textblock_2.TransformToVisual(textblock_2opt).Transform(POINT_ZERO);
            MoveAnimation.MoveFromTo(textblock_2, 0d, 0d, 0 - point.X, 0d, duration, null);

            FadeAnimation.Fade(textblock_0opt, 0d, 1d, duration, null);
        }

        public void Recover()
        {
            MoveAnimation.MoveTo(textblock_1, 0d, 0d, duration, null);
            MoveAnimation.MoveTo(textblock_2, 0d, 0d, duration, null);
            FadeAnimation.Fade(textblock_0opt, 1d, 0d, duration, null);
        }
    }
}
