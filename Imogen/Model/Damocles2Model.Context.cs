﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Imogen.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Damocles2Entities : DbContext
    {
        public Damocles2Entities()
            : base("name=Damocles2Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EUReported> EUReporteds { get; set; }
        public virtual DbSet<JurisdictionNote> JurisdictionNotes { get; set; }
        public virtual DbSet<Jurisidction> Jurisidctions { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<RankNote> RankNotes { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<StatusNote> StatusNotes { get; set; }
        public virtual DbSet<UserNote> UserNotes { get; set; }
        public virtual DbSet<UserRank> UserRanks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSex> UserSexes { get; set; }
        public virtual DbSet<UserSexNote> UserSexNotes { get; set; }
        public virtual DbSet<UsersSession> UsersSessions { get; set; }
        public virtual DbSet<UserJurisdiction> UserJurisdictions { get; set; }
    }
}
