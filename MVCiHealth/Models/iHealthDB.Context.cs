﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LocalDBEntities : DbContext
    {
        public LocalDBEntities()
            : base("name=LocalDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
<<<<<<< HEAD
=======
        public virtual DbSet<USERINFO> USERINFO { get; set; }
>>>>>>> 7fed91c79409195c8e0aa609f0f93479eb490f5c
        public virtual DbSet<DISEASE_TYPE> DISEASE_TYPE { get; set; }
        public virtual DbSet<DOCTOR> DOCTOR { get; set; }
        public virtual DbSet<DOCTOR_EVALUATION> DOCTOR_EVALUATION { get; set; }
        public virtual DbSet<GROUPINFO> GROUPINFO { get; set; }
        public virtual DbSet<PATIENT> PATIENT { get; set; }
        public virtual DbSet<PATIENT_HISTORY> PATIENT_HISTORY { get; set; }
        public virtual DbSet<RESERVATION> RESERVATION { get; set; }
        public virtual DbSet<SECTION_TYPE> SECTION_TYPE { get; set; }
        public virtual DbSet<SYSLOG> SYSLOG { get; set; }
<<<<<<< HEAD
        public virtual DbSet<USERINFO> USERINFO { get; set; }
=======
>>>>>>> 7fed91c79409195c8e0aa609f0f93479eb490f5c
    }
}
