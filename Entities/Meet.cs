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
    
    public partial class Meet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meet()
        {
            this.Activity = new HashSet<Activity>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public int Number_of_days { get; set; }
        public int IDCity { get; set; }
        public byte[] Photo { get; set; }
        public Nullable<int> IDWinner { get; set; }
        public Nullable<int> IDDirection { get; set; }
        public string PhotoPath { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activity { get; set; }
        public virtual City City { get; set; }
        public virtual Direction Direction { get; set; }
        public virtual User User { get; set; }
    }
}
