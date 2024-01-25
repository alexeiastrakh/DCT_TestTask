using System;
using System.Globalization;
using System.Threading.Tasks;
using TestProjectDCT.Models;

namespace TestProjectDCT.ViewModels.Services
{
    public class ConversionService
    {
        public async Task<double> ConvertAsync(Asset fromCrypto, Asset toCrypto, double amount)
        {
            if (fromCrypto == null || toCrypto == null || string.IsNullOrWhiteSpace(fromCrypto.PriceUsd) || string.IsNullOrWhiteSpace(toCrypto.PriceUsd))
            {
                Console.WriteLine("Invalid FromCrypto or ToCrypto or PriceUsd property.");
                return 0;
            }

            if (amount <= 0)
            {
                Console.WriteLine("Amount must be greater than zero.");
                return 0;
            }

            if (double.TryParse(fromCrypto.PriceUsd, NumberStyles.Any, CultureInfo.InvariantCulture, out double fromCryptoPrice) &&
                double.TryParse(toCrypto.PriceUsd, NumberStyles.Any, CultureInfo.InvariantCulture, out double toCryptoPrice))
            {
                return (amount / fromCryptoPrice) * toCryptoPrice;
            }
            else
            {
                Console.WriteLine("Error parsing PriceUsd property.");
                return 0;
            }
        }
    }
}
