using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ChatRoomWithBot.Domain;

public static class Utils
{
	public static string CaracteresEspeciais()
	{

			return "A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ’´`'-";
		}

	public static bool ExisteCaracterEspecial(string texto)
	{
			if (string.IsNullOrEmpty(texto)) return false;


			var result = !Regex.IsMatch(texto, @"^([a-zA-Z]\p{M}*)+$");
			return result;
		}

	public static bool NomeValido(string texto)
	{


			foreach (var item in texto.Trim().Split(' ').ToList())
			{
				bool existeCaracterEspecial = ExisteCaracterEspecial(item);
				if (existeCaracterEspecial)
				{
					return false;
				}
			}

			return true;

		}

	public static bool IsNumeric(this Type type)
	{
			var result = NumeriTypes.Contains(type);
			return result;
		}

	public static bool IsDateTime(this Type type)
	{
			return type == typeof(DateTime);
		}

	private static readonly HashSet<Type> NumeriTypes = new()
	{
		typeof(decimal),
		typeof(int)
	};

	public static string GetMd5Hash(string input)
	{
			using var md5Hash = MD5.Create();
			// Convert the input string to a byte array and compute the hash.
			var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			var sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data 
		// and format each one as a hexadecimal string.
			for (var i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

	public static string Base64Encode(string plainText)
	{
			var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			return Convert.ToBase64String(plainTextBytes);
		}

	public static string Base64Decode(string base64EncodedData)
	{
			var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
			return Encoding.UTF8.GetString(base64EncodedBytes);
		}


	public static int MaxTamanhoMd5Hash = 128;

	public static decimal ConvertStringToDecimal(string value)
	{
			try
			{
				var result = Convert.ToDecimal(value, CultureInfo.GetCultureInfo("pt-BR"));

				return result;
			}
			catch (Exception)
			{
				return default(decimal);
			}

		}

	public static readonly string EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

	public static DateTime Now => DateTime.UtcNow;

	public static DateTime NowBr => DateTime.UtcNow.AddHours(-3);

	public static bool IsDevelopmentEnvironment => EnvironmentName == EnvironmentNamesEnum.Development.ToString();

	public static bool IsStagingEnvironment => EnvironmentName == EnvironmentNamesEnum.Staging.ToString();

	public static bool IsProductionEnvironment =>
		EnvironmentName == EnvironmentNamesEnum.Production.ToString();

	public static string AppName { get; set; }
	 

	public static string GenerateRandomString()
	{
			var random = new Random();
			var length = random.Next(minValue:15, maxValue:20);
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var stringChars = new char[length];


			for (var i = 0; i < length; i++)
			{
				stringChars[i] = chars[random.Next(chars.Length)];
			}
			return new string(stringChars);
		}
	 
		 


	public enum EnvironmentNamesEnum
	{
		Local = 1,
		Development = 100,
		Staging = 300,
		Production = 400
	}
}