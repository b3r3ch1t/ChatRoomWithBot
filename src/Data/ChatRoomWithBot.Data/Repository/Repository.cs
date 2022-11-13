using ChatRoomWithBot.Data.Context;
using ChatRoomWithBot.Domain.Entities;
using ChatRoomWithBot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using ChatRoomWithBot.Domain.Interfaces.Repositories;

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

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {

                if (DbSet.Any())
                {
                    var responseBd = DbSet.AsEnumerable();

                    return Task.FromResult(responseBd);
                } 

            }
            catch (Exception e)
            {
                _error.Error(e);
            }

            var result= (IEnumerable<TEntity>)new List<TEntity>();

            return Task.FromResult(result );
        }

        public async  Task<TEntity> GetByIdAsync(Guid id)
        {
            try
            {
                var responseBd = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
                return responseBd;
            }
            catch (Exception e)
            {
                _error.Error(e);


            }
           
            
            return  (TEntity) null  ;

        }


        public void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);

        }
    }
}
