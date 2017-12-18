using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCiHealth.Models
{
    public class ReservationIndex
    {
        public IEnumerable<SECTION_TYPE> SECTION_LIST { get; set; }
        public IList<DOCTOR> DOCTOR_LIST { get; set; }
        //public IEnumerable<RESERVATION> RESERVATION_LIST { get; set; }
    }
}