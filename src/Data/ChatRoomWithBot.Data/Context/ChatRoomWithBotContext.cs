

using ChatRoomWithBot.Data.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ChatRoomWithBot.Domain.Interfaces;

namespace ChatRoomWithBot.Data.Context
{
    internal class ChatRoomWithBotContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {

        public ChatRoomWithBotContext(
            DbContextOptions<ChatRoomWithBotContext> options) : base(options)
        {

        }

        #region DbSet

        public DbSet<User> Users { get; set; }

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
            var entries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (!(entry.Entity is IEntity trackable)) continue;
                switch (entry.State)
                {
                    case EntityState.Modified:

                        trackable.ChangeDateModification(DateTime.Now);

                        break;

                    case EntityState.Added:

                        trackable.ChangeDateAdded(DateTime.Now);
                        trackable.ChangeDateModification(DateTime.Now);
                        trackable.Activate();

                        if (trackable.Id == Guid.Empty )
                        {
                            trackable.ChangeId();
                        }

                        break;

                }



            }

        }
        #endregion

    }
}
