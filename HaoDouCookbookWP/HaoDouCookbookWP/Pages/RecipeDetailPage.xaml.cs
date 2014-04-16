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
            PrepareScrollViewer();
            BuildApplicationBar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            RecipeID = NavigationContext.QueryString[NaviParam.RECIPE_ID];
            RecipeName = NavigationContext.QueryString[NaviParam.RECIPE_NAME];

            LoadData_Test();

            this.topbar.ShowTitle(RecipeName);
            //this.kitchenWares.Hit();

            Rendering = true;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Rendering = false;
            base.OnNavigatingFrom(e);
        }

        #endregion

        #region ScrollViewer Event

        Point POINT_ZERO = new Point(0, 0);
        bool Rendering = false;
        double NORMAL_Y = 0d;

        private void PrepareScrollViewer()
        {
            this.AddHandler(UIElement.ManipulationStartedEvent, (EventHandler<ManipulationStartedEventArgs>)OnManipulationStarted, true);
            this.AddHandler(UIElement.ManipulationDeltaEvent, (EventHandler<ManipulationDeltaEventArgs>)OnManipulationDelta, true);
            this.AddHandler(UIElement.ManipulationCompletedEvent, (EventHandler<ManipulationCompletedEventArgs>)OnManipulationCompleted, true);
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void OnManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            //CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void OnManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            scrollTransform.TranslateY += e.DeltaManipulation.Translation.Y;
        }

        private void OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            //CompositionTarget.Rendering -= CompositionTarget_Rendering;
        }

        void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            if (!Rendering)
            {
                return;
            }
            DetectPos();
        }

        private void DetectPos()
        {
            Point point = recipePanel.TransformToVisual(dragDetector).Transform(POINT_ZERO);
            //string str = point.X.ToString() + ", " + point.Y.ToString();
            //debugText.Text = str;

            if (point.Y < NORMAL_Y)
            {
                double scale = 1d + (NORMAL_Y - point.Y) * 0.0028d;
                coverImageTransform.ScaleX = scale;
                coverImageTransform.ScaleY = scale;
            }
            else
            {
                coverImageTransform.ScaleX = 1d;
                coverImageTransform.ScaleY = 1d;
            }
        }

        #endregion

        #region Panorama Selection

        int pivotPrevoiusSelectedIndex = 0;

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool swipeFromRight = false;
            if (pivot.SelectedIndex == 0 && pivotPrevoiusSelectedIndex == 2)
            {
                swipeFromRight = true;
            }
            else if (pivot.SelectedIndex == 2 && pivotPrevoiusSelectedIndex == 0)
            {
                swipeFromRight = false;
            }
            else if (pivot.SelectedIndex > pivotPrevoiusSelectedIndex)
            {
                swipeFromRight = true;
            }
            else
            {
                swipeFromRight = false;
            }

            topbar.Flirt();
            kitchenWares.Hit(swipeFromRight);
            pivotPrevoiusSelectedIndex = pivot.SelectedIndex;
        }

        #endregion

        #region Data

        Recipe recipe = null;

        private void LoadData()
        {
        }

        private void LoadData_Test()
        {
            recipe = new Recipe();
            recipe.CoverImage = "/Assets/TestImages/1.jpg";
            recipe.Author = "莫小贝";
            recipe.Date = DateTime.Today;
            recipe.MainIngredients = new Ingredient[2];
            recipe.MainIngredients[0] = new Ingredient() { Name = "排骨", Amount = "300g" };
            recipe.MainIngredients[1] = new Ingredient() { Name = "土豆", Amount = "200g" };

            recipe.MinorIngredients = new Ingredient[2];
            recipe.MinorIngredients[0] = new Ingredient() { Name = "油 ", Amount = "适量" };
            recipe.MinorIngredients[1] = new Ingredient() { Name = "盐", Amount = "适量" };
            recipe.MinorIngredients[1] = new Ingredient() { Name = "姜", Amount = "适量" };
            recipe.MinorIngredients[1] = new Ingredient() { Name = "大蒜", Amount = "适量" };

            recipe.Steps = new CookStep[3];
            recipe.Steps[0] = new CookStep() { Image = "/Assets/TestImages/1.jpg", Description = "1.包菜洗净，用手撕成差不多大小的片" };
            recipe.Steps[1] = new CookStep() { Image = "/Assets/TestImages/2.jpg", Description = "2.猪肉切片，加适量黄酒、生粉、老抽捏匀待用，然后准备小葱、姜、蒜切碎，准备豆豉" };
            recipe.Steps[2] = new CookStep() { Image = "/Assets/TestImages/3.jpg", Description = "3.浆好的肉片下锅大火翻炒至变色即可，装盘待用" };

            recipe.Tip = "炒这个菜不需要很多油。";

            this.recipePanel.DataContext = recipe;
        }

        #endregion

        #region Layout Format

        bool optimizedView = false;

        List<IngredientItem> IngredientItems = new List<IngredientItem>();

        private void Ingredients_DoubleTap(object sender, GestureEventArgs e)
        {
            optimizedView = !optimizedView;
            if (optimizedView)
            {
                Point point;
                point = mainIngredientTitle.TransformToVisual(mainIngredientTitle_Optimized).Transform(POINT_ZERO);
                MoveAnimation.MoveFromTo(mainIngredientTitle, 0d, 0d, 0 - point.X, 0d, TimeSpan.FromMilliseconds(200), null);

                point = minorIngredientTitle.TransformToVisual(minorIngredientTitle_Optimized).Transform(POINT_ZERO);
                MoveAnimation.MoveFromTo(minorIngredientTitle, 0d, 0d, 0 - point.X, 0d, TimeSpan.FromMilliseconds(200), null);

                OptimizeView();
            }
            else
            {
                MoveAnimation.MoveTo(mainIngredientTitle, 0d, 0d, TimeSpan.FromMilliseconds(400), null);
                MoveAnimation.MoveTo(minorIngredientTitle, 0d, 0d, TimeSpan.FromMilliseconds(400), null);

                RecoverView();
            }
        }

        private void IngredientItem_Loaded(object sender, RoutedEventArgs e)
        {
            IngredientItem item = sender as IngredientItem;
            if (item != null)
            {
                IngredientItems.Add(item);
            }
        }

        private void OptimizeView()
        {
            foreach (var item in IngredientItems)
            {
                item.Optimize();
            }
        }

        private void RecoverView()
        {
            foreach (var item in IngredientItems)
            {
                item.Recover();
            }
        }

        #endregion

        #region App Bar

        ApplicationBarIconButton appBarLike;
        ApplicationBarIconButton appBarComment;
        ApplicationBarIconButton appBarSubmit;

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            //ApplicationBar.Mode = ApplicationBarMode.Minimized;

            // like
            appBarLike = new ApplicationBarIconButton(new Uri("/Assets/AppBar/like.png", UriKind.Relative));
            appBarLike.Text = "like";//TO-DO
            appBarLike.Click += appBarLike_Click;

            // comment
            appBarComment = new ApplicationBarIconButton(new Uri("/Assets/AppBar/edit.png", UriKind.Relative));//TO-DO
            appBarComment.Text = "comment";//TO-DO
            appBarComment.Click += appBarComment_Click;

            // comment
            appBarSubmit = new ApplicationBarIconButton(new Uri("/Assets/AppBar/upload.png", UriKind.Relative));//TO-DO
            appBarSubmit.Text = "submit";//TO-DO
            appBarSubmit.Click += appBarSubmit_Click;

            SetAppBarDefault();
        }

        void appBarLike_Click(object sender, EventArgs e)
        {
        }

        void appBarComment_Click(object sender, EventArgs e)
        {
            //ClearAppBar();
            //SetAppBarForComment();
            //ShowCommentPopup();
            string[] paramArray = new string[]{ NaviParam.RECIPE_ID, RecipeID};
            string strUri = string.Format("/Pages/CommentPage.xaml?{0}={1}", paramArray);
            NavigationService.Navigate(new Uri(strUri, UriKind.Relative));
        }
        private void appBarSubmit_Click(object sender, EventArgs e)
        {
            bool canClosePopup = TrySubmitComment();
            if (canClosePopup)
            {
                popupContainer.Close(null);
            }
        }

        private void SetAppBarDefault()
        {
            ApplicationBar.Buttons.Add(appBarLike);
            ApplicationBar.Buttons.Add(appBarComment);
        }

        private void SetAppBarForComment()
        {
            ApplicationBar.Buttons.Add(appBarSubmit);
        }

        private void ClearAppBar()
        {
            ApplicationBar.Buttons.Clear();
            ApplicationBar.MenuItems.Clear();
        }

        #endregion

        #region Comment Popup

        private CommentControl _commentControl = null;
        private CommentControl commentControl
        {
            get
            {
                if (_commentControl == null)
                {
                    _commentControl = new CommentControl();
                }
                return _commentControl;
            }
        }

        private PopupCotainer _popupContainer = null;
        private PopupCotainer popupContainer
        {
            get
            {
                if (_popupContainer == null)
                {
                    _popupContainer = new PopupCotainer(this);
                }
                return _popupContainer;
            }
        }

        private void ShowCommentPopup()
        {
            popupContainer.OnClosing += popupContainer_OnClosing;
            popupContainer.Show(commentControl, PopupMode.Top, () => commentControl.OnShown());
        }

        private void popupContainer_OnClosing(object sender, EventArgs e)
        {
            popupContainer.OnClosing -= popupContainer_OnClosing;
            commentControl.OnClosing();
            ClearAppBar();
            SetAppBarDefault();
        }

        private bool TrySubmitComment()
        {
            if (string.IsNullOrEmpty(commentControl.CommentString))
            {
                return true;
            }

            if (commentControl.IsRed)
            {
                commentControl.NotifyRed();
                return false;
            }

            //TO-DO
            //submit comment

            return true;

        }

        #endregion

    }
}