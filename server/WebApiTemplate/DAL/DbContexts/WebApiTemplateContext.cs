﻿using Microsoft.EntityFrameworkCore;
using EmeraldChameleonChat.Model.Entity;

namespace EmeraldChameleonChat.DAL.DbContexts
{
    public class EmeraldChameleonChatContext : DbContext //must implement DbContext
    {
        //setting this to =null! is just allowing us to ignore the null warning. We know this wont be null because of the DbSet/DbContext - the warning is just VS being dumb.
        public DbSet<Model.Entity.WeatherForecast> WeatherForecast { get; set; } = null!; //must create a DbSet for each Entity. DbSets are used to query & save data to the db. Linq queries against a dbset translate to queries against the db.

        public EmeraldChameleonChatContext(DbContextOptions<EmeraldChameleonChatContext> options) : base(options) 
        { 
            //by exposing this constructor, we can provide these options at the moment we register the dbcontext on the pipeline.
        }
    }
}
