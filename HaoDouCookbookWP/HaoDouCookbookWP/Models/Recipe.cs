using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookbookWP.Models
{
    [DataContract]
    public class Recipe
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "img")]
        public string CoverImage { get; set; }

        [DataMember(Name = "img")]
        public string Author { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "img")]
        public Ingredient[] MainIngredients { get; set; }

        [DataMember(Name = "img")]
        public Ingredient[] MinorIngredients { get; set; }

        [DataMember(Name = "img")]
        public CookStep[] Steps { get; set; }

        [DataMember(Name = "tip")]
        public string Tip { get; set; }
    }

    [DataContract]
    public class Ingredient
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "img")]
        public string Amount { get; set; }
    }

    [DataContract]
    public class CookStep
    {
        [DataMember(Name = "desc")]
        public string Description { get; set; }

        [DataMember(Name = "img")]
        public string Image { get; set; }
    }
}
