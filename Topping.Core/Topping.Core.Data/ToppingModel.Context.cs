﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Topping.Core.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ToppingModelContainer : DbContext
    {
        public ToppingModelContainer()
            : base("name=ToppingModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<GameConfig> GameConfigs { get; set; }
        public DbSet<GameRanking> GameRankings { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<ViewUserIdAndName> ViewUserIdAndNames { get; set; }
        public DbSet<GamesDetail> GamesDetails { get; set; }
        public DbSet<ViewGetRanking> ViewGetRankings { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<ViewGetGameDetail> ViewGetGameDetails { get; set; }
    }
}
