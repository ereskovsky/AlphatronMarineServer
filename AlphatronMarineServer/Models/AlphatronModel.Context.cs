﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlphatronMarineServer.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AlphatronMarineEntities : DbContext
    {
        public AlphatronMarineEntities()
            : base("name=AlphatronMarineEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Auth> Auth { get; set; }
        public virtual DbSet<BusinessLocation> BusinessLocation { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentTemplates> EquipmentTemplates { get; set; }
        public virtual DbSet<General> General { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductBulletFact> ProductBulletFact { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vessel> Vessel { get; set; }
        public virtual DbSet<VesselAccess> VesselAccess { get; set; }
    }
}
