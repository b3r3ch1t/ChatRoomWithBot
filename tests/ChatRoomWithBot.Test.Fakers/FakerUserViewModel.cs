 

using Bogus;
using ChatRoomWithBot.Application.ViewModel;

namespace ChatRoomWithBot.Test.Fakers
{
    public static  class FakerUserViewModel
    {
        public static IEnumerable<UserViewModel> GetListUserViewModel()
        {
            var dateCreated = DateTime.Now;
            var listUsers = new Faker<UserViewModel>()

                .RuleFor(u => u.Id, f => f.PickRandom<Guid>())
                .RuleFor(u => u.Name, f => f.Person.FullName)
                .RuleFor(u => u.DateCreated, dateCreated)
                .RuleFor(u => u.Email, f => f.Person.Email);

            var qte = Randomizer.Seed.Next(5, 100);

            var result = listUsers.Generate(qte); 

            return result;


        }
    }
}
