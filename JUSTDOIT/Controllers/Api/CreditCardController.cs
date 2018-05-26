using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace viacinema.Controllers.Api
{
    [Route("api/creditcard")]
    public class CreditCardController : Controller
    {
        // Checks if credit card number is valid
        // checks that all characters are digits (0-9)
        // makes checksum for credit card numbers, so use a real credit card number (test ones)
        [HttpPost("validate")]
        public bool Validate([FromBody] string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            string ccValue = value as string;
            if (ccValue == null)
            {
                return false;
            }
            ccValue = ccValue.Replace("-", "");
            ccValue = ccValue.Replace(" ", "");

            int checksum = 0;
            bool evenDigit = false;

            // http://www.beachnet.com/~hstiles/cardtype.html
            foreach (char digit in ccValue.Reverse())
            {
                if (digit < '0' || digit > '9')
                {
                    return false;
                }

                int digitValue = (digit - '0') * (evenDigit ? 2 : 1);
                evenDigit = !evenDigit;

                while (digitValue > 0)
                {
                    checksum += digitValue % 10;
                    digitValue /= 10;
                }
            }

            return (checksum % 10) == 0;
        }
    }
}