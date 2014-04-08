using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HaoDouCookbookWP.Animations;

namespace HaoDouCookbookWP.Controls
{
    public partial class TopBar : UserControl
    {
        private static Style _TitleTextStyle;
        private static Style TitleTextStyle
        {
            get
            {
                if (_TitleTextStyle == null)
                {
                    _TitleTextStyle = Application.Current.Resources["TopBar_Text_Style"] as Style;
                }
                return _TitleTextStyle;
            }
        }

        private string Title { get; set; }

        private List<TextBlock> titleTextBlocks = new List<TextBlock>();

        public TopBar()
        {
            InitializeComponent();
        }

        public void Flirt()
        {
            int index = 0;
            foreach (var tb in titleTextBlocks)
            {
                MoveAnimation.MoveFromTo(tb, 0, 0, 0, 0, TimeSpan.FromMilliseconds(index * 100 + 200),
                    fe0 => MoveAnimation.MoveFromTo(fe0, 0, 0, 0, -8, Constants.TOP_BAR_TITLE_DURATION_1,
                        fe1 => MoveAnimation.MoveFromTo(fe1, 0, -8, 0, 8, Constants.TOP_BAR_TITLE_DURATION_2,
                            fe2 => MoveAnimation.MoveFromTo(fe2, 0, 8, 0, 0, Constants.TOP_BAR_TITLE_DURATION_3, null))));
                index++;
            }
        }

        public void ShowTitle(string title)
        {
            Title = title;
            foreach (var character in Title)
            {
                TextBlock tb = new TextBlock() { Text = character.ToString(), Style = TitleTextStyle };
                this.stackPanel.Children.Add(tb);
                titleTextBlocks.Add(tb);
            }
            Flirt();
        }
    }
}
