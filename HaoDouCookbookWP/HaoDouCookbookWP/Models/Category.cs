﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookbookWP.Models
{
    [DataContract]
    public class Category
    {
        [DataMember(Name="id")]
        public string ID { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [IgnoreDataMember]
        public string Color { get; set; }

    }
}
