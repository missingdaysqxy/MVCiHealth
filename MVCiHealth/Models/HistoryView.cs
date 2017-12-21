using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCiHealth.Models
{
    public class HistoryView
    {
        public PATIENT_HISTORY HISTORY { get; set; }
        public DOCTOR DOCTOR { get; set; }
        public PATIENT PATIENT { get; set; }
        public string Name { get; set; }
    }
}