using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IKazanCore.Api.Infrastructures.Utils
{
    public class EncryptionUtil
    {
		public static string ToBase64Encode(string text)
		{
			if (String.IsNullOrEmpty(text))
			{
				return text;
			}

			byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text);
			return Convert.ToBase64String(textBytes);
		}

		public static string ToBase64Decode(string base64EncodedText)
		{
			if (String.IsNullOrEmpty(base64EncodedText))
			{
				return base64EncodedText;
			}

			byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedText);
			return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
		}
	}
}
