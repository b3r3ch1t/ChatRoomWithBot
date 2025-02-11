using System.Globalization;

namespace ChatRoomWithBot.Domain.Extensions;

public static class StringExtension
{

	 
	public static bool IsDigit(this string value)
	{
		return !value.Any(char.IsLetter);
	}

	public static decimal ConvertToDecimalInvariante(this string value)
	{
		try
		{
			var result = Convert.ToDecimal(value, CultureInfo.GetCultureInfo("pt-BR"));

			return result;
		}
		catch (Exception)
		{
			return 0;
		}

	}

	public static bool IsPresent(this string value)
	{
		return !string.IsNullOrWhiteSpace(value);
	}

	 private static string UrlCombine(string path1, string path2)
		{
			path1 = path1.TrimEnd('/') + "/";
			path2 = path2.TrimStart('/');

			return Path.Combine(path1, path2)
				.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
		}

	public static T ToEnum<T>(this string value)
	{
		return (T)Enum.Parse(typeof(T), value, true);
	}

	public static string RemoveTrailingSlash(this string url)
	{
		return url.EndsWith("/") ? url.TrimEnd('/') : url;
	}
	public static bool IsEmpty(this string value)
	{
		return string.IsNullOrWhiteSpace(value);
	}

}