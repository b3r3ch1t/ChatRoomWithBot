using System.Security.Cryptography;
using System.Text;

namespace ChatRoomWithBot.Domain;

public static  class Criptografy
{

    private static readonly byte[] Iv = Encoding.UTF8.GetBytes("N9#^:uTjRFTK%O+G");
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("0l$2azmfZ9ofre9)0k9|5Lwrfz?QL`Lg");

    public static string Encrypt(string plainText)
    {
        // Check arguments.
        if (plainText is not { Length: > 0 })
            throw new ArgumentNullException(nameof(plainText));

        // Create an Aes object
        // with the specified key and IV.
        using var aesAlg = Aes.Create();
        aesAlg.Key = Key;
        aesAlg.IV = Iv ;

        // Create an encryptor to perform the stream transform.
        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for encryption.
        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            //Write all data to the stream.
            swEncrypt.Write(plainText);
        }
        var encrypted = msEncrypt.ToArray();
        var result = Convert.ToBase64String(encrypted);

        return result ;

    }

    public static string Decrypt(string cipherText)
    {
        if (cipherText is not { Length: > 0 })
            throw new ArgumentNullException(nameof(cipherText));

        var inputText = Convert.FromBase64String(cipherText.Replace(" ", "+"));

          
        // Create an Aes object
        // with the specified key and IV.
        using var aesAlg = Aes.Create();
        aesAlg.Key = Key ;
        aesAlg.IV = Iv;

        // Create a decryptor to perform the stream transform.
        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        // Create the streams used for decryption.
        using var msDecrypt = new MemoryStream(inputText);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt); 


        // Read the decrypted bytes from the decrypting stream
        // and place them in a string.
        var plaintext = srDecrypt.ReadToEnd();

        return plaintext;

    }
}