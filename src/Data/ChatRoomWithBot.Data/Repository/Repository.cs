using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChatRoomWithBot.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected readonly ChatRoomWithBotContext Context;
        protected readonly DbSet<TEntity> DbSet;
        private readonly IError _error;


        public Repository(ChatRoomWithBotContext context, IError error)
        {
            Context = context;
            _error = error;
            DbSet = Context.Set<TEntity>();

        }

         


        public async Task AddAsync(TEntity obj, string password)
        {
            try
            {
                await DbSet.AddAsync(obj); 
            }
            catch (Exception e)
            {
                _error.Error(e); 
            }
        }

        public async  Task<List<TEntity>> GetAllAsync()
        {
            try
            {

                if (DbSet.Any())
                {
                    var responseBd = DbSet.Where(x => x.Valid).ToList();

                    return responseBd;
                }




            }
            catch (Exception e)
            {
                _error.Error(e);
            }

            return new List<TEntity>();
        }


        public void Dispose()
        {
           Context.Dispose();
           GC.SuppressFinalize(this);

        }
    }
}
