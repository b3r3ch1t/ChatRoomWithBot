using Bogus;
using ChatRoomWithBot.Data.IdentityModel;

namespace ChatRoomWithBot.Test.Fakers
{
    public static class FakerUserIdentity
    {
        public static IEnumerable<UserIdentity> GetListUserUserIdentity()
        {
            var dateCreated = DateTime.Now;

            var listUsers = new Faker<UserIdentity>()

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
