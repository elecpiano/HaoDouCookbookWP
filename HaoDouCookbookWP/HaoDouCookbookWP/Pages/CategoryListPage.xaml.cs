using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using HaoDouCookbookWP.Models;
using HaoDouCookbookWP.Utility;
using System.Collections.ObjectModel;
using HaoDouCookbookWP.Animations;
using HaoDouCookbookWP.Controls;

namespace HaoDouCookbookWP.Pages
{
    public partial class CategoryListPage : PhoneApplicationPage
    {
        #region Properties

        private string ActiveCategoryID = string.Empty;

        #endregion

        #region Lifecycle

        public CategoryListPage()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ActiveCategoryID = NavigationContext.QueryString[NaviParam.CATEGORY_ID];

            this.topbar.Delay = 300d;
            this.topbar.ShowTitle("好豆菜谱");
            //kitchenWares.Hit();

            LoadData_Test();
        }

        #endregion

        #region Tab Headers

        ObservableCollection<Category> categories = new ObservableCollection<Category>();
        CategoryTabHeader previousActiveHeader = null;
        CategoryTabHeader previousShadowHeaderT = null;
        CategoryTabHeader previousShadowHeaderB = null;
        List<CategoryTabHeader> headers = new List<CategoryTabHeader>();
        private void PopulateCategories()
        {
            foreach (var category in categories)
            {
                CategoryTabHeader header;
                header = new CategoryTabHeader();
                header.DataContext = category;
                //header.Init(category.Title, category.Color);
                if (category.ID == ActiveCategoryID)
                {
                    header.Activate(() => PopulateSubCategories(category.Color, 32));
                    previousActiveHeader = header;
                }

                header.Tap += header_Tap;
                categoryPanel.Children.Add(header);
                headers.Add(header);
            }
        }

        void header_Tap(object sender, GestureEventArgs e)
        {
            CategoryTabHeader header = sender as CategoryTabHeader;
            if (previousActiveHeader == header)
            {
                return;
            }

            Category category = sender.GetDataContext<Category>();

            if (header != null)
            {
                if (previousShadowHeaderT != null)
                {
                    previousShadowHeaderT.HideVerticalShadow();
                }
                if (previousShadowHeaderB != null)
                {
                    previousShadowHeaderB.HideVerticalShadow();
                }

                previousShadowHeaderT = null;
                previousShadowHeaderB = null;

                header.Activate(() => PopulateSubCategories(category.Color, 32));
                int index = headers.IndexOf(header);
                if (index > 0)
                {
                    previousShadowHeaderT = headers[index - 1];
                    previousShadowHeaderT.ShowVerticalShadow(false);
                }
                if (index < (headers.Count - 1))
                {
                    previousShadowHeaderB = headers[index + 1];
                    previousShadowHeaderB.ShowVerticalShadow(true);
                }
            }
            previousActiveHeader.Deactivate(true);
            previousActiveHeader = header;
        }

        #endregion

        #region SubCategory

        GridLength SubCategoryRowHeight = new GridLength(110d);

        private void PopulateSubCategories(string colorHex, int itemCount)
        {
            subCategoryListPanel.Children.Clear();
            subCategoryListPanel.RowDefinitions.Clear();
            subCategoryListPanel.UpdateLayout();

            int rowCount = itemCount / 3;
            if ((itemCount % 3) != 0)
            {
                rowCount += 1;
            }

            for (int i = 0; i < rowCount; i++)
            {
                RowDefinition rowDef = new RowDefinition() { Height = SubCategoryRowHeight };
                subCategoryListPanel.RowDefinitions.Add(rowDef);
            }

            subCategoryListPanel.UpdateLayout();
            subCategoryScrollViewer.ScrollToVerticalOffset(0d);

            double delay = 0d;
            double delayIncremental = 60d;

            for (int i = 0; i < itemCount; i++)
            {
                int row = i / 3;
                int col = i % 3;
                SubCategoryButton button = new SubCategoryButton();
                button.SetValue(Grid.RowProperty, row);
                button.SetValue(Grid.ColumnProperty, col);
                subCategoryListPanel.Children.Add(button);
                //button.Init(i.ToString(), colorHex);
                button.DataContext = new Category() { Title = i.ToString(), Color = colorHex };
                delayIncremental -= 3d;
                if (delayIncremental < 0d)
                {
                    delayIncremental = 0d;
                }
                delay += delayIncremental;//Global.RANDOM.NextDouble() * 300d;
                button.Show(delay, 300d);
            }
        }

        #endregion

        #region Test

        bool dataLoaded = false;
        private void LoadData_Test()
        {
            if (dataLoaded)
            {
                return;
            }

            categories.Add(new Category() { ID = "1", Title = "菜式菜品", Color = "#FF1BA1E2" });
            categories.Add(new Category() { ID = "2", Title = "菜系", Color = "#FFA4C400" });
            categories.Add(new Category() { ID = "3", Title = "人群功效", Color = "#FFC72D57" });
            categories.Add(new Category() { ID = "4", Title = "食材", Color = "#FFF0A30A" });
            categories.Add(new Category() { ID = "5", Title = "场景", Color = "#FF76608A" });

            PopulateCategories();
        }

        #endregion

        #region App Bar

        ApplicationBarIconButton appBarSearch;
        ApplicationBarIconButton appBarRefresh;
        ApplicationBarMenuItem appBarSetting;

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

            appBarSetting = new ApplicationBarMenuItem("设置");
            appBarSetting.Click += appBarSetting_Click;
            ApplicationBar.MenuItems.Add(appBarSetting);
        }

        void appBarRefresh_Click(object sender, EventArgs e)
        {
        }

        void appBarSearch_Click(object sender, EventArgs e)
        {
        }


        void appBarSetting_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SettingsPage.xaml", UriKind.Relative));
        }

        private void ClearAppBar()
        {
            ApplicationBar.Buttons.Clear();
            ApplicationBar.MenuItems.Clear();
        }

        #endregion
    }
}