﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AltairScope.DomainModels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AltairScopeEntities : DbContext
    {
        public AltairScopeEntities()
            : base("name=AltairScopeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Property> Properties { get; set; }
        public DbSet<Property_Sale> Property_Sale { get; set; }
        public DbSet<Property_Change_History> Property_Change_History { get; set; }
        public DbSet<Property_Comment> Property_Comment { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Property_Evaluation> Property_Evaluation { get; set; }
        public DbSet<Neighbourhood> Neighbourhoods { get; set; }
        public DbSet<Property_FMV_Tracking> Property_FMV_Tracking { get; set; }
        public DbSet<Property_Rental_Tracking> Property_Rental_Tracking { get; set; }
    }
}
