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
    
    public partial class Department
    {
        public int ID { get; set; }
        public string DepartmentName { get; set; }
        public int Floor { get; set; }
        public int RoomNumbers { get; set; }
        public int IdDoctor { get; set; }
    
        public virtual Doctor Doctor { get; set; }
    }
}
