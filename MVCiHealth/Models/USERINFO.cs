using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCiHealth.Models
{
    public class USERINFO
    {
        public int USER_ID { get; set; }
        public int GROUP_ID { get; set; }
        public string USER_PWD { get; set; }
    }
}