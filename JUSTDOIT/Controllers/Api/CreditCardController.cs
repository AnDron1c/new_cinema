using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace viacinema.Controllers.Api
{
    [Route("api/creditcard")]
    public class CreditCardController : Controller
    {
        [HttpPost("validate")]
        public bool Validate([FromBody] string value)
        {
            if (value == null)
            {
                return true;
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


            //// check whether input string is null or empty
            //if (string.IsNullOrEmpty(creditCardNumber))
            //{
            //    return false;
            //}

            //// 1. Starting with the check digit double the value of every other digit 
            //// 2. If doubling of a number results in a two digits number, add up
            ////    the digits to get a single digit number. This will results in eight single digit numbers                    
            //// 3. Get the sum of the digits
            //int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
            //                .Reverse()
            //                .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
            //                .Sum((e) => e / 10 + e % 10);


            ////   If the final sum is divisible by 10, then the credit card number
            ////   is valid. If it is not divisible by 10, the number is invalid.            
            //return sumOfDigits % 10 == 0;
        }
    }
}