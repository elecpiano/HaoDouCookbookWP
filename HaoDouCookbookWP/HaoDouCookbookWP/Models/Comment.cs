using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookbookWP.Models
{
    [DataContract]
    public class Comment
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "submitter")]
        public string Submitter { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

    }
}
