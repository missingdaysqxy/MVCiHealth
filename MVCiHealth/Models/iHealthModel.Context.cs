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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class iHealthEntities : DbContext
    {
        public iHealthEntities()
            : base("name=iHealthEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DISEASE_TYPE> DISEASE_TYPE { get; set; }
        public virtual DbSet<DOCTOR> DOCTOR { get; set; }
        public virtual DbSet<DOCTOR_EVALUATION> DOCTOR_EVALUATION { get; set; }
        public virtual DbSet<GROUPINFO> GROUPINFO { get; set; }
        public virtual DbSet<PATIENT> PATIENT { get; set; }
        public virtual DbSet<PATIENT_HISTORY> PATIENT_HISTORY { get; set; }
        public virtual DbSet<RESERVATION> RESERVATION { get; set; }
        public virtual DbSet<SECTION_TYPE> SECTION_TYPE { get; set; }
        public virtual DbSet<SYSLOG> SYSLOG { get; set; }
        public virtual DbSet<USERINFO> USERINFO { get; set; }
        public virtual DbSet<V_DOCTORINFO> V_DOCTORINFO { get; set; }
        public virtual DbSet<V_RESERVATION> V_RESERVATION { get; set; }
        public virtual DbSet<V_EVALUATION> V_EVALUATION { get; set; }
    
        public virtual int VeryfyPassword(string login_nm, string password, ObjectParameter iscorrect, ObjectParameter user_id)
        {
            var login_nmParameter = login_nm != null ?
                new ObjectParameter("login_nm", login_nm) :
                new ObjectParameter("login_nm", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("VeryfyPassword", login_nmParameter, passwordParameter, iscorrect, user_id);
        }
    
        public virtual int VeryfyPassword1(string login_nm, string password, ObjectParameter iscorrect, ObjectParameter user_id)
        {
            var login_nmParameter = login_nm != null ?
                new ObjectParameter("login_nm", login_nm) :
                new ObjectParameter("login_nm", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("VeryfyPassword1", login_nmParameter, passwordParameter, iscorrect, user_id);
        }
    }
}
