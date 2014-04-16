using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HaoDouCookbookWP.Models;
using System.Windows.Input;
using System.Windows.Media;
using HaoDouCookbookWP.Animations;
using HaoDouCookbookWP.Controls;
using HaoDouCookbookWP.Utility;

namespace HaoDouCookbookWP.Pages
{
    public partial class CommentPage : PhoneApplicationPage
    {
        #region Properties

        private string RecipeID = string.Empty;
        Brush RedBrush = new SolidColorBrush(Colors.Red);
        Brush BlackBrush = new SolidColorBrush(ColorConverter.Convert("#ff808080"));
        public bool IsRed = false;
        TimeSpan duration = TimeSpan.FromMilliseconds(150f);

        #endregion

        #region Lifecycle

        public CommentPage()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            RecipeID = NavigationContext.QueryString[NaviParam.RECIPE_ID];
            this.topbar.ShowTitle("写评论");
            //this.commentTextBox.Focus();
        }

        #endregion

        private void NotifyRed()
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

        #region App Bar

        ApplicationBarIconButton appBarCancel;
        ApplicationBarIconButton appBarSubmit;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            // cancel
            appBarCancel = new ApplicationBarIconButton(new Uri("/Assets/AppBar/cancel.png", UriKind.Relative));//TO-DO
            appBarCancel.Text = "comment";//TO-DO
            appBarCancel.Click += appBarCancel_Click;
            ApplicationBar.Buttons.Add(appBarCancel);

            // comment
            appBarSubmit = new ApplicationBarIconButton(new Uri("/Assets/AppBar/upload.png", UriKind.Relative));//TO-DO
            appBarSubmit.Text = "submit";//TO-DO
            appBarSubmit.Click += appBarSubmit_Click;
            ApplicationBar.Buttons.Add(appBarSubmit);
        }

        void appBarCancel_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
        private void appBarSubmit_Click(object sender, EventArgs e)
        {
        }

        private void ClearAppBar()
        {
            ApplicationBar.Buttons.Clear();
            ApplicationBar.MenuItems.Clear();
        }

        #endregion


    }
}