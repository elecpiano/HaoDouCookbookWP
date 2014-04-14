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

namespace HaoDouCookbookWP.Pages
{
    public partial class HomePage : PhoneApplicationPage
    {
        #region Lifecycle

        public HomePage()
        {
            InitializeComponent();
            recommendationListBox.ItemsSource = recommendationList;
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
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
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

        #endregion

    }
}