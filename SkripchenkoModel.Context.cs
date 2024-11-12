﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hospital
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HospitalEntities : DbContext
    {
        private static HospitalEntities context;

        public static HospitalEntities GetContext()
        {
            if (context == null)
                context = new HospitalEntities();

            return context;
        }

        public HospitalEntities()
            : base("name=HospitalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<MedicalHistory> MedicalHistory { get; set; }
        public virtual DbSet<Patient> Patient { get; set; }
        public virtual DbSet<TreatmentSheet> TreatmentSheet { get; set; }
        public virtual DbSet<authsmall> authsmall { get; set; }
    }
}