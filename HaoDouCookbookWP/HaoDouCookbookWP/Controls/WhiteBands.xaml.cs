using System;
using System.Windows;
using System.Windows.Controls;
using HaoDouCookbookWP.Animations;

namespace HaoDouCookbookWP.Controls
{
    public partial class WhiteBands : UserControl
    {
        private const double DURATION_MIN = 1000d;
        private TimeSpan ShowDuration = TimeSpan.FromMilliseconds(1800);
        private TimeSpan HideDuration = TimeSpan.FromMilliseconds(100);

        public WhiteBands()
        {
            InitializeComponent();
            this.Opacity = 0d;
            Animate();
        }

        public void Animate()
        {
            Animate(band1);
            Animate(band2);
            Animate(band3);
            Animate(band4);
            Animate(band5);
            Animate(band6);
        }

        public void Show()
        {
            FadeAnimation.Fade(this, 0d, 0d, ShowDuration, fe=>FadeAnimation.Fade(fe,0d,1d,ShowDuration,null));
        }

        public void Hide()
        {
            FadeAnimation.Fade(this, 1d, 0d, HideDuration, null);
        }

        private void Animate(FrameworkElement fe)
        {
            double fromY = (Global.RANDOM.NextDouble() - 0.5d) * 1000d;
            double toY = (Global.RANDOM.NextDouble() - 0.5d) * 1000d;

            double duration = Math.Abs(fromY - toY) * 15d;
            if (duration < DURATION_MIN)
            {
                duration = DURATION_MIN;
            }
            MoveAnimation.MoveFromTo(fe, 0d, fromY, 0d, toY, TimeSpan.FromMilliseconds(duration), fe1 => Animate(fe1));
            FadeAnimation.Fade(fe, 0d, 1d, TimeSpan.FromMilliseconds(duration * 0.5d), fe2 =>
                FadeAnimation.Fade(fe2, 1d, 0d, TimeSpan.FromMilliseconds(duration * 0.5d), null));
        }
    }
}
