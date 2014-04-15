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
using HaoDouCookbookWP.Animations;
using HaoDouCookbookWP.Utility;

namespace HaoDouCookbookWP.Controls
{
    public partial class CommentControl : UserControl
    {
        Brush RedBrush = new SolidColorBrush(Colors.Red);
        Brush BlackBrush = new SolidColorBrush(ColorConverter.Convert("#ff808080"));
        public bool IsRed = false;
        TimeSpan duration = TimeSpan.FromMilliseconds(150f);
        public string CommentString
        {
            get
            {
                return this.commentTextBox.Text;
            }
        }

        public CommentControl()
        {
            InitializeComponent();
        }

        public void OnShown()
        {
            this.commentTextBox.Focus();
        }

        public void OnClosing()
        {
            this.commentTextBox.Text = string.Empty;
        }

        public void NotifyRed()
        {
            ScaleAnimation.ScaleFromTo(countTextBlock, 1f, 1f, 1.5f, 1.5f, duration,
                fe1 => ScaleAnimation.ScaleFromTo(fe1, 1.5f, 1.5f, 1f, 1f, duration,
                    fe2 => ScaleAnimation.ScaleFromTo(fe2, 1f, 1f, 1.5f, 1.5f, duration,
                        fe3 => ScaleAnimation.ScaleFromTo(fe3, 1.5f, 1.5f, 1f, 1f, duration, null))));
        }

        private void commentTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int length = commentTextBox.Text.Length;
            countTextBlock.Text = length.ToString() + "/140";
            if (length>140 && !IsRed)
            {
                countTextBlock.Foreground = RedBrush;
                IsRed = true;
            }
            else if(length<=140 && IsRed)
            {
                countTextBlock.Foreground = BlackBrush;
                IsRed = false;
            }
        }
    }
}
