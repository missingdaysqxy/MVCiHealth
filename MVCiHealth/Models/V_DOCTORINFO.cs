//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCiHealth.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class V_DOCTORINFO
    {
        public int DOCTOR_ID { get; set; }
        public string DOCTOR_NM { get; set; }
        public Nullable<int> GENDER { get; set; }
        public Nullable<int> AGE { get; set; }
        public string TEL { get; set; }
        public string PHOTO_URL { get; set; }
        public Nullable<int> SECTION_ID { get; set; }
        public Nullable<int> DISEASE_ID { get; set; }
        public string INTRODUCTION { get; set; }
        public Nullable<double> LEVEL { get; set; }
        public Nullable<System.DateTime> INSDATE { get; set; }
        public int PATIENT_ID { get; set; }
        public string PATIENT_NM { get; set; }
    }
}
