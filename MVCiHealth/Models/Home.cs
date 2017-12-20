using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCiHealth.Models
{
    public class Home
    {
        public IList<DOCTOR> DOCTORLIST { get; set; }
        public IList<V_EVALUATION> V_EVALUATIONLIST { get; set; }
        public IList<USERINFO> USERINFOLIST { get; set; }
    }
}