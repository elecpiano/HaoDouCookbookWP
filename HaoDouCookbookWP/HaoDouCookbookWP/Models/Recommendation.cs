using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookbookWP.Models
{
    [DataContract]
    public class Recommendation
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "img")]
        public string FoodImage { get; set; }

        [DataMember(Name = "img2")]
        public string UserImage { get; set; }
    }
}
