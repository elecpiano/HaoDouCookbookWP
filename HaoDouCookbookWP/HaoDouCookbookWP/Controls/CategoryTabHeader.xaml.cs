using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using HaoDouCookbookWP.Utility;

namespace HaoDouCookbookWP.Controls
{
    public partial class CategoryTabHeader : UserControl
    {
        private bool IsActive = false;
        private Action OnActivated = null;

        public CategoryTabHeader()
        {
            InitializeComponent();
            Deactivate(false);
            shadowT.Opacity = 0d;
            shadowB.Opacity = 0d;
        }

        //public void Init(string title, string colorHex)
        //{
        //    this.titleTextBlock.Text = titleTextBlockDark.Text = title;
        //    this.colorBubble.Fill = this.tileBase.Fill = new SolidColorBrush(ColorConverter.Convert(colorHex));
        //}

        public void Activate(Action complete)
        {
            IsActive = true;
            OnActivated = complete;
            VisualStateManager.GoToState(this, "Active", true);
        }

        public void Deactivate(bool transition)
        {
            IsActive = false;
            VisualStateManager.GoToState(this, "Inactive", transition);
        }

        public void ShowVerticalShadow(bool top)
        {
            (top ? shadowT : shadowB).Opacity = 1d;
        }

        public void HideVerticalShadow()
        {
            shadowT.Opacity = 0d;
            shadowB.Opacity = 0d;
        }

        private void Activate_Completed(object sender, EventArgs e)
        {
            if (OnActivated!=null)
            {
                OnActivated();
                OnActivated = null;
            }
        }
    }
}
