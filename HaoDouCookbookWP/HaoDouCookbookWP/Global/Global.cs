using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookbookWP
{
    public class Constants
    {
        public const string DOMAIN = "http://api.gootrip.com"; //"http://115.28.253.73";

        //recommendation
        public const string RECOMMENDATION_MODULE = "recommendation";
        public const string RECOMMENDATION_LIST_FILE_NAME = "recommendation_list.txt";

        //animation duration
        public static TimeSpan TOP_BAR_TITLE_DURATION_1 = TimeSpan.FromMilliseconds(100);
        public static TimeSpan TOP_BAR_TITLE_DURATION_2 = TimeSpan.FromMilliseconds(200);
        public static TimeSpan TOP_BAR_TITLE_DURATION_3 = TimeSpan.FromMilliseconds(300);
    }

    public class NaviParam
    {
        public const string RECIPE_ID = "recipe_id";
        public const string RECIPE_NAME = "recipe_name";

        public const string CATEGORY_ID = "category_id";
        public const string CATEGORY_NAME = "category_name";
    }

    public class Global
    {
        public static Random RANDOM = new Random();
    }
}
