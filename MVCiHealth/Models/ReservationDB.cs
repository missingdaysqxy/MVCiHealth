using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCiHealth.Models
{
    public class ReservationDB
    {
        public int RESERVATION_ID { get; set; }
        public int DOCTOR_ID { get; set; }
        public int PATIENT_ID { get; set; }
        public DateTime TIME_START { get; set; }
        public DateTime TIME_FINISH { get; set; }
        public char CONFIRMED { get; set; }
        public char VALID { get; set; }
        public string COMMENT { get; set; }
        public DateTime INSDATE { get; set; }
        
    }

    
    
}