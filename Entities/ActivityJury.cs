//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NatalyaYakovenko.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActivityJury
    {
        public int ID { get; set; }
        public int IDActivity { get; set; }
        public int IDUser { get; set; }
        public int Order_Number { get; set; }
    
        public virtual Activity Activity { get; set; }
        public virtual User User { get; set; }
    }
}
