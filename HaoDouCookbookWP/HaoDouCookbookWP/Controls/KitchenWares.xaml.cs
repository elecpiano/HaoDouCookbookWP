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
    public partial class KitchenWares : UserControl
    {
        public KitchenWares()
        {
            InitializeComponent();
        }

        public void Hit(bool fromRight)
        {
            if (fromRight)
            {
                this.StoryHitFromRight.Begin();
            }
            else
            {
                this.StoryHitFromLeft.Begin();
            }
        }

        double previousRotation = 0d;
        double targetRotation = 0d;
        const double ROTATION_THRESHOLD = 2d;
        double rotationDuration = 200d;

        private void NextRotation()
        {
            if (Math.Abs(targetRotation) < ROTATION_THRESHOLD)
            {
                targetRotation = 0d;
            }
            RotateAnimation.RotateFromTo(this.image1, previousRotation, targetRotation, TimeSpan.FromMilliseconds(rotationDuration), fe => NextRotation());
            previousRotation = targetRotation;
            targetRotation *= -0.8d;
            rotationDuration *= 0.8d;
        }
    }
}
