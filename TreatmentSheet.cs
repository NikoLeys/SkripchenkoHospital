//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class TreatmentSheet
    {
        public int ID { get; set; }
        public Nullable<int> DateTratment { get; set; }
        public int IdMedicalHistory { get; set; }
        public string Medicines { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public string PatientCondition { get; set; }
    
        public virtual MedicalHistory MedicalHistory { get; set; }
    }
}
