using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BNSegurosMAUI.Utils
{
	public static class TextUtils
	{
		public static bool IsValidEmail(string email)
		{
			var pattern = "^([0-9a-zA-Z]([-\\.\\w]*[+]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
			return Regex.IsMatch(email, pattern);
		}

		public static string FormatAmount(decimal amount, string currency = "")
		{
			return String.Format("{0:N2} {1}", Math.Round(amount, 2, MidpointRounding.AwayFromZero), currency);
		}

		public static bool IsValidDecimal(string amountText, out decimal amount)
		{
			if (decimal.TryParse(amountText, out amount))
			{
				return true;
			}
			return false;
		}

		public static bool IsNumeric(string text)
		{
			return text.All(c => Char.IsDigit(c));
		}

		public static bool IsLetters(string text) ///^[a-z][a-z\s]*$/
		{
			var pattern = "^[a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð ,.'-]+$";
			return Regex.IsMatch(text, pattern);
		}

		public static bool IsAmount(string amount)
		{
			var pattern = "^[0-9][0-9,\\.]+$";
			return Regex.IsMatch(amount, pattern);
		}
	}
}

