﻿using ChatRoomWithBot.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Data.IdentiModel;

namespace ChatRoomWithBot.Data.Context
{
    public class ChatRoomWithBotContext : IdentityDbContext<UserIdentity, IdentityRole<Guid>, Guid>
    {

        public ChatRoomWithBotContext(
            DbContextOptions<ChatRoomWithBotContext> options) : base(options)
        {

        }

        #region DbSet

        public DbSet<UserIdentity> Users { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }


        #endregion



        #region SaveChanges

        public override int SaveChanges()
        {


            UpdateData();
            var result = base.SaveChanges();


            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {


            UpdateData();

            var result = base.SaveChangesAsync(cancellationToken);


            return result;
        }

        private void UpdateData()
        {
            var entries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (!(entry.Entity is IEntity trackable)) continue;
                switch (entry.State)
                {
                     

                    case EntityState.Added:

                        trackable.ChangeDateCreated(DateTime.Now); 
                        trackable.Activate();

                        if (trackable.Id == Guid.Empty)
                        {
                            trackable.ChangeId();
                        }

                        break;

                }



            }

        }

        #endregion
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assemblyWithConfigurations = GetType().Assembly; //get whatever assembly you want
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);

        }


    }
}
