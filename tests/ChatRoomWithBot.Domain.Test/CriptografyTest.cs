using Bogus;

namespace ChatRoomWithBot.Domain.Test
{
    public class CriptografyTest
    {
        [Fact]
        public void TestCriptografy()
        {
            var faker = new Faker("pt_BR");

            var plainText = faker.Lorem.Text();

            var cipherText = Criptografy.Encrypt(plainText);

            var plainTextDecrypted = Criptografy.Decrypt(cipherText);

            Assert.Equal(plainTextDecrypted, plainText);


        }
         
    }
}
