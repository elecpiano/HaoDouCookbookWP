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
            InitCategories();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            topbar.ShowTitle("好豆菜谱");
            LoadRecommendations_Test();
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
            topbar.Flirt(swipeFromRight);
            parnoramaPrevoiusSelectedIndex = panorama.SelectedIndex;

            switch (panorama.SelectedIndex)
            {
                case 0:
                    HideCategories(true);
                    break;
                case 1:
                    ShowCategories();
                    break;
                case 2:
                    HideCategories(true);
                    break;
                case 3:
                    HideCategories(true);
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

        private void LoadRecommendations_Test()
        {
            recommendationList.Clear();
            recommendationList.Add(new Recommendation() { Title = "宫保鸡丁", FoodImage = "/Assets/TestImages/1.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "鱼香肉丝", FoodImage = "/Assets/TestImages/2.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "番茄鸡蛋", FoodImage = "/Assets/TestImages/3.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "宫保鸡丁", FoodImage = "/Assets/TestImages/1.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "鱼香肉丝", FoodImage = "/Assets/TestImages/2.jpg", UserImage = "/Assets/TestImages/user.jpg" });
            recommendationList.Add(new Recommendation() { Title = "番茄鸡蛋", FoodImage = "/Assets/TestImages/3.jpg", UserImage = "/Assets/TestImages/user.jpg" });
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
            category1.Show(200d, CategoryAnimationDuration);
            category2.Show(270d, CategoryAnimationDuration);
            category3.Show(340d, CategoryAnimationDuration);
            category4.Show(410d, CategoryAnimationDuration);
            category5.Show(480d, CategoryAnimationDuration);
        }

        private void HideCategories(bool withTransition)
        {
            category1.Hide(withTransition, 200d, CategoryAnimationDurationLong);
            category2.Hide(withTransition, 150d, CategoryAnimationDurationLong);
            category3.Hide(withTransition, 100d, CategoryAnimationDurationLong);
            category4.Hide(withTransition, 50d, CategoryAnimationDurationLong);
            category5.Hide(withTransition, 10d, CategoryAnimationDurationLong);
        }

        #endregion

    }
}