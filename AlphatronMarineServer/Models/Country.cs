//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlphatronMarineServer.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    public partial class Country
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Country()
        {
            this.Vessel = new HashSet<Vessel>();
        }
    
        public int ID { get; set; }
        public int enabled { get; set; }
        public string Code31 { get; set; }
        public string Code21 { get; set; }
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string Flag32 { get; set; }
        public string Flag128 { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longtitude { get; set; }
        public int Zoom { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vessel> Vessel { get; set; }
    }
}
