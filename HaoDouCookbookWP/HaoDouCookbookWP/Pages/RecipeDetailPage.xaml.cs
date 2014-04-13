using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HaoDouCookbookWP.Pages
{
    public partial class RecipeDetailPage : PhoneApplicationPage
    {
        #region Properties

        private string RecipeID = string.Empty;
        private string RecipeName = string.Empty;

        #endregion

        #region Lifecycle

        public RecipeDetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            RecipeID = NavigationContext.QueryString[NaviParam.RECIPE_ID];
            RecipeName = NavigationContext.QueryString[NaviParam.RECIPE_NAME];

            this.topbar.ShowTitle(RecipeName);
        }

        #endregion


    }
}