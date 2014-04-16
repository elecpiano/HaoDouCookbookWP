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
using System.Collections.ObjectModel;

namespace HaoDouCookbookWP.Pages
{
    public partial class CategoryPage : PhoneApplicationPage
    {
        #region Properties

        private string CategoryID = string.Empty;
        private string CategoryName = string.Empty;

        #endregion

        #region Lifecycle

        public CategoryPage()
        {
            InitializeComponent();
            BuildApplicationBar();
            categoryListBox.ItemsSource = categoryItemList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            CategoryID = NavigationContext.QueryString[NaviParam.CATEGORY_ID];
            CategoryName = NavigationContext.QueryString[NaviParam.CATEGORY_NAME];
            this.topbar.ShowTitle(CategoryName);

            LoadData_Test();//TO-DO
        }

        #endregion

        #region Category

        ObservableCollection<CategoryItem> categoryItemList = new ObservableCollection<CategoryItem>();

        private void LoadData()
        {

        }

        bool dataLoaded = false;
        private void LoadData_Test()
        {
            if (dataLoaded)
            {
                return;
            }
            categoryItemList.Clear();
            categoryItemList.Add(new CategoryItem() { Name = "爆炒菜花", Image = "/Assets/TestImages/1.jpg", Description="原料：五花肉，花菜，油，盐，酱油，蒜" });
            categoryItemList.Add(new CategoryItem() { Name = "爆炒菜花", Image = "/Assets/TestImages/2.jpg", Description = "原料：五花肉，花菜，油，盐，酱油，蒜" });
            categoryItemList.Add(new CategoryItem() { Name = "爆炒菜花", Image = "/Assets/TestImages/3.jpg", Description = "原料：五花肉，花菜，油，盐，酱油，蒜" });
            categoryItemList.Add(new CategoryItem() { Name = "爆炒菜花", Image = "/Assets/TestImages/1.jpg", Description = "原料：五花肉，花菜，油，盐，酱油，蒜" });
            categoryItemList.Add(new CategoryItem() { Name = "爆炒菜花", Image = "/Assets/TestImages/2.jpg", Description = "原料：五花肉，花菜，油，盐，酱油，蒜" });
            categoryItemList.Add(new CategoryItem() { Name = "爆炒菜花", Image = "/Assets/TestImages/3.jpg", Description = "原料：五花肉，花菜，油，盐，酱油，蒜" });
            categoryItemList.Add(new CategoryItem() { Name = "爆炒菜花", Image = "/Assets/TestImages/1.jpg", Description = "原料：五花肉，花菜，油，盐，酱油，蒜" });
            categoryItemList.Add(new CategoryItem() { Name = "爆炒菜花", Image = "/Assets/TestImages/2.jpg", Description = "原料：五花肉，花菜，油，盐，酱油，蒜" });
            categoryItemList.Add(new CategoryItem() { Name = "爆炒菜花", Image = "/Assets/TestImages/3.jpg", Description = "原料：五花肉，花菜，油，盐，酱油，蒜" });
            
            dataLoaded = true;
        }

        private void categoryItem_Tap(object sender, GestureEventArgs e)
        {
            CategoryItem item = sender.GetDataContext<CategoryItem>();
            string[] paramArray = new string[] { NaviParam.RECIPE_ID, item.ID, NaviParam.RECIPE_NAME, item.Name };
            string strUri = string.Format("/Pages/RecipeDetailPage.xaml?{0}={1}&{2}={3}", paramArray);
            NavigationService.Navigate(new Uri(strUri, UriKind.Relative));
        }

        private void CategoryItem_Tap(object sender, GestureEventArgs e)
        {

        }

        #endregion

        #region App Bar

        ApplicationBarIconButton appBarSearch;
        ApplicationBarIconButton appBarRefresh;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // search
            appBarSearch = new ApplicationBarIconButton(new Uri("/Assets/AppBar/search.png", UriKind.Relative));
            appBarSearch.Text = "搜索";
            appBarSearch.Click += appBarSearch_Click;
            ApplicationBar.Buttons.Add(appBarSearch);

            // refresh
            appBarRefresh = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.Relative));
            appBarRefresh.Text = "刷新";
            appBarRefresh.Click += appBarRefresh_Click;
            ApplicationBar.Buttons.Add(appBarRefresh);
        }

        void appBarRefresh_Click(object sender, EventArgs e)
        {
        }

        void appBarSearch_Click(object sender, EventArgs e)
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