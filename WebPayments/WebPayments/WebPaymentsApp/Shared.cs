using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace WebPaymentsApp
{
    public class Shared
    {
        private static Regex regex;
        public static bool isValidPhone(string phoneNumber) {
            regex = new Regex("^[0-9]+$");
            if (!regex.IsMatch(phoneNumber))
                return false;
            if (phoneNumber.Length != 9)
                return false;
            if (phoneNumber[0].ToString() != "5")
                return false;
            return true;
        }

        public static bool isValidPin(string pin) {
            regex = new Regex("^[0-9]+$");
            if (pin.Length != 11)
                return false;
            if (!regex.IsMatch(pin))
                return false;
            return true;
        }

        public static bool isValidAmount(double amount) {
            if (amount < 1 || amount > 100)
                return false;
            return true;
        }
        public static double calculateCommision(double payAmount) {
            double commisionFee = Math.Round(payAmount * 1 / 100, 2);
            if (commisionFee < 0.5)
                commisionFee = 0.5;
            return commisionFee;
        }

        internal static bool isValidIban(string iban)
        {
            regex = new Regex("^GE[0-9]{2}[A-Z]{2}[0-9]{16}");
            if (iban.Length != 22)
                return false;
            if (!regex.IsMatch(iban))
                return false;
            return true;
        }
        public static string getConf(string connectionName)
        {
            JObject appSettings = JObject.Parse(File.ReadAllText("appsettings.json"));
            return appSettings.SelectToken($"{connectionName}").ToString();
        }
    }
}
