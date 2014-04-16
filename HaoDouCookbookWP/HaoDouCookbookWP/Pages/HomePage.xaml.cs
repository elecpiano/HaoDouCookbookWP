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

namespace HaoDouCookbookWP.Pages
{
    public partial class HomePage : PhoneApplicationPage
    {
        #region Lifecycle

        public HomePage()
        {
            InitializeComponent();

            recommendationListBox.ItemsSource = recommendationList;
            topicListBox.ItemsSource = topicList;

            BuildApplicationBar();
            InitCategories();
            InitUserCenter();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.topbar.Delay = 700d;
            this.topbar.ShowTitle("好豆菜谱");
            //kitchenWares.Hit();

            LoadRecommendations_Test();
            LoadTopics_Test();
        }

        #endregion

        #region Panorama Selection

        int parnoramaPrevoiusSelectedIndex = 0;

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool swipeFromRight = false;
            if (panorama.SelectedIndex == 0 && parnoramaPrevoiusSelectedIndex == 3)
            {
                swipeFromRight = true;
            }
            else if (panorama.SelectedIndex == 3 && parnoramaPrevoiusSelectedIndex == 0)
            {
                swipeFromRight = false;
            }
            else if (panorama.SelectedIndex > parnoramaPrevoiusSelectedIndex)
            {
                swipeFromRight = true;
            }
            else
            {
                swipeFromRight = false;
            }
            topbar.Delay = 300d;
            topbar.Flirt();
            kitchenWares.Hit(swipeFromRight);
            parnoramaPrevoiusSelectedIndex = panorama.SelectedIndex;

            switch (panorama.SelectedIndex)
            {
                case 0:
                    HideCategories(true);
                    HideUserCenter(true);
                    break;
                case 1:
                    ShowCategories();
                    HideUserCenter(true);
                    break;
                case 2:
                    HideCategories(true);
                    HideUserCenter(true);
                    break;
                case 3:
                    HideCategories(true);
                    ShowUserCenter();
                    break;
                default:
                    break;
            }

        }

        #endregion

        #region Recommendation

        ObservableCollection<Recommendation> recommendationList = new ObservableCollection<Recommendation>();
        ListDataLoader<Recommendation> recommendationLoader = new ListDataLoader<Recommendation>();

        private void LoadRecommendations()
        {
            if (recommendationLoader.Loaded || recommendationLoader.Busy)
            {
                return;
            }

            //snow1.IsBusy = true;

            recommendationLoader.Load("getrecommendationlist", string.Empty, true, Constants.RECOMMENDATION_MODULE, Constants.RECOMMENDATION_LIST_FILE_NAME,
                list =>
                {
                    recommendationList.Clear();
                    foreach (var item in list)
                    {
                        recommendationList.Add(item);
                    }
                    scrollViewerRecommendation.ScrollToVerticalOffset(0);
                    //snow1.IsBusy = false;
                });
        }

        private void Recommendation_Tap(object sender, GestureEventArgs e)
        {
            Recommendation rec = sender.GetDataContext<Recommendation>();
            string[] paramArray = new string[] { NaviParam.RECIPE_ID, rec.ID, NaviParam.RECIPE_NAME, rec.Title };
            string strUri = string.Format("/Pages/RecipeDetailPage.xaml?{0}={1}&{2}={3}", paramArray);
            NavigationService.Navigate(new Uri(strUri, UriKind.Relative));
        }

        #endregion

        #region Categories

        double CategoryAnimationDuration = 400d;
        double CategoryAnimationDurationLong = 500d;
        double ShowCategoryDelay = 100d;
        double ShowCategoryDelayIncrement = 100d;

        private void InitCategories()
        {
            category1.Init("菜式菜品", "#FF1BA1E2");
            category2.Init("菜系", "#FFA4C400");
            category3.Init("人群功效", "#FFC72D57");
            category4.Init("食材", "#FFF0A30A");
            category5.Init("场景", "#FF76608A");
            HideCategories(false);
        }

        private void category1_Tap(object sender, GestureEventArgs e)
        {
            string categoryId = "1";//TO-DO
            string[] paramArray = new string[] { NaviParam.CATEGORY_ID, categoryId };
            string strUri = string.Format("/Pages/CategoryListPage.xaml?{0}={1}", paramArray);
            NavigationService.Navigate(new Uri(strUri, UriKind.Relative));
        }

        private void category2_Tap(object sender, GestureEventArgs e)
        {

        }

        private void category3_Tap(object sender, GestureEventArgs e)
        {

        }

        private void category4_Tap(object sender, GestureEventArgs e)
        {

        }

        private void category5_Tap(object sender, GestureEventArgs e)
        {

        }

        private void ShowCategories()
        {
            category1.Show(ShowCategoryDelay + ShowCategoryDelayIncrement * 0d, CategoryAnimationDuration);
            category2.Show(ShowCategoryDelay + ShowCategoryDelayIncrement * 1d, CategoryAnimationDuration);
            category3.Show(240d, CategoryAnimationDuration);
            category4.Show(310d, CategoryAnimationDuration);
            category5.Show(380d, CategoryAnimationDuration);

            whiteBands.Show();
        }

        private void HideCategories(bool withTransition)
        {
            category1.Hide(withTransition, 200d, CategoryAnimationDurationLong);
            category2.Hide(withTransition, 150d, CategoryAnimationDurationLong);
            category3.Hide(withTransition, 100d, CategoryAnimationDurationLong);
            category4.Hide(withTransition, 50d, CategoryAnimationDurationLong);
            category5.Hide(withTransition, 10d, CategoryAnimationDurationLong);

            whiteBands.Hide();
        }

        #endregion

        #region Topic

        private void Topic_Tap(object sender, GestureEventArgs e)
        {

        }

        ObservableCollection<Topic> topicList = new ObservableCollection<Topic>();


        #endregion

        #region Test


        bool recommendationLoaded = false;
        private void LoadRecommendations_Test()
        {
            if (recommendationLoaded)
            {
                return;
            }
            recommendationList.Clear();
            recommendationList.Add(new Recommendation() { Title = "荠菜鸡丝合子", FoodImage = "/Assets/TestImages/1.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "宫保鸡丁", FoodImage = "/Assets/TestImages/2.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "鱼香肉丝", FoodImage = "/Assets/TestImages/3.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "番茄鸡蛋", FoodImage = "/Assets/TestImages/1.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "荠菜鸡丝合子", FoodImage = "/Assets/TestImages/2.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "宫保鸡丁", FoodImage = "/Assets/TestImages/3.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "鱼香肉丝", FoodImage = "/Assets/TestImages/1.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "番茄鸡蛋", FoodImage = "/Assets/TestImages/2.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationLoaded = true;
        }

        bool topicLoaded = false;
        private void LoadTopics_Test()
        {
            if (topicLoaded)
            {
                return;
            }
            topicList.Clear();
            topicList.Add(new Topic() { Title = "别不把小葱当菜，春季小葱营", Image = "/Assets/TestImages/1.jpg" });
            topicList.Add(new Topic() { Title = "春季让味道香辣的蒜苗来守护", Image = "/Assets/TestImages/2.jpg" });
            topicList.Add(new Topic() { Title = "春季魔芋减肥瘦身靓容颜", Image = "/Assets/TestImages/3.jpg" });
            topicList.Add(new Topic() { Title = "别不把小葱当菜，春季小葱营", Image = "/Assets/TestImages/1.jpg" });
            topicList.Add(new Topic() { Title = "春季让味道香辣的蒜苗来守护", Image = "/Assets/TestImages/2.jpg" });
            topicList.Add(new Topic() { Title = "春季魔芋减肥瘦身靓容颜", Image = "/Assets/TestImages/3.jpg" });
            topicList.Add(new Topic() { Title = "别不把小葱当菜，春季小葱营", Image = "/Assets/TestImages/1.jpg" });
            topicList.Add(new Topic() { Title = "春季让味道香辣的蒜苗来守护", Image = "/Assets/TestImages/2.jpg" });
            topicList.Add(new Topic() { Title = "春季魔芋减肥瘦身靓容颜", Image = "/Assets/TestImages/3.jpg" });
            topicList.Add(new Topic() { Title = "别不把小葱当菜，春季小葱营", Image = "/Assets/TestImages/1.jpg" });
            topicList.Add(new Topic() { Title = "春季让味道香辣的蒜苗来守护", Image = "/Assets/TestImages/2.jpg" });
            topicList.Add(new Topic() { Title = "春季魔芋减肥瘦身靓容颜", Image = "/Assets/TestImages/3.jpg" });
            topicLoaded = true;
        }

        #endregion

        #region User Center

        private void InitUserCenter()
        {
            userCenterItem1.Init("喜欢的菜谱", "#FF1BA1E2");
            userCenterItem2.Init("关注的专辑", "#FFA4C400");
            HideUserCenter(false);
            account.DataContext = "/Assets/TestImages/user.jpg";
        }

        private void ShowUserCenter()
        {
            userCenterItem1.Show(100d, CategoryAnimationDuration);
            userCenterItem2.Show(200d, CategoryAnimationDuration);
            bubbles.IsBusy = true;
        }

        private void HideUserCenter(bool withTransition)
        {
            userCenterItem1.Hide(withTransition, 50d, CategoryAnimationDurationLong);
            userCenterItem2.Hide(withTransition, 10d, CategoryAnimationDurationLong);
            bubbles.IsBusy = false;
        }

        private void userCenterItem1_Tap(object sender, GestureEventArgs e)
        {

        }

        private void userCenterItem2_Tap(object sender, GestureEventArgs e)
        {

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