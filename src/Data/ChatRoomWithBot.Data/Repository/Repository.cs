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

        protected Repository(IDependencyResolver dependencyResolver)
        {
            Context = dependencyResolver.Resolve<ChatRoomWithBotContext>();
            DbSet = Context.Set<TEntity>();
            _error = dependencyResolver.Resolve<IError>();

        }

        public async  Task<TEntity> AddAsync(TEntity obj)
        {
            try
            {
                await DbSet.AddAsync(obj);

                return obj;
            }
            catch (Exception e)
            {
                _error.Error(e);
                return null;
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
