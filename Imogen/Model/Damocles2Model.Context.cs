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
    
        public virtual DbSet<A> A { get; set; }
        public virtual DbSet<C> C { get; set; }
        public virtual DbSet<EUReported> EUReporteds { get; set; }
        public virtual DbSet<GoneButNotForgottenLink> GoneButNotForgottenLinks { get; set; }
        public virtual DbSet<Jurisidction> Jurisidctions { get; set; }
        public virtual DbSet<ProcessingResult> ProcessingResults { get; set; }
        public virtual DbSet<R> R { get; set; }
        public virtual DbSet<Rank> Ranks { get; set; }
        public virtual DbSet<UserJurisdiction> UserJurisdictions { get; set; }
        public virtual DbSet<UserRank> UserRanks { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersSession> UsersSessions { get; set; }
        public virtual DbSet<FaceARC> FaceARCs { get; set; }
        public virtual DbSet<Face> Faces { get; set; }
        public virtual DbSet<Hash> Hashes { get; set; }
        public virtual DbSet<Matedata> Matedatas { get; set; }
        public virtual DbSet<CurrentReport> CurrentReports { get; set; }
    }
}
