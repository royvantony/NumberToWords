using System.ComponentModel.DataAnnotations;

namespace NumberToWords.Models
{
    public class NumToWords
    {
        [Required(ErrorMessage ="Amount is required")]
        public string number { get; set; } = string.Empty;
        public string words { get; set; } = string.Empty;

        public static string ConvertNumToWords(string num )
        {

            string numInWords = string.Empty;
            long wholeNumber = 0;
            string fraction = string.Empty;
            double number = 0;
            bool negativeVal = false;

            try
            {
                //Validate input
                if (num == null || num.Trim().Length == 0 )
                {
                    return "";
                }

                if (!IsNumeric(num, out number))
                {
                    return "Not a valid amount.";
                }

                //Maximum amount validation
                if (number >= 1000000000 | number <= -1000000000)
                {
                    return "Maximum limit restricted to less than a Billion.";
                }

                //Split wholenumber and fractions
                string[] numberSplit = num.Trim().Split('.');
                if (numberSplit.Length > 0)
                {
                    //Check if the input is negative
                    if (numberSplit[0].StartsWith('-'))
                    {
                        negativeVal = true;
                        numberSplit[0] = numberSplit[0].Remove(0, 1);
                    }

                    wholeNumber = numberSplit[0].Trim() != "" ? Convert.ToInt64(numberSplit[0]) : 0;

                    if (numberSplit.Length > 1)
                    {
                        if (numberSplit[1].Length > 2)
                        {
                            return "Incorrect CENTS entered.";
                        }
                        fraction = IsNumeric(numberSplit[1]) ? numberSplit[1].PadRight(2, '0').Substring(0, 2) : "";
                    }
                }

                //Convert to words
                if (number == 0)
                    return "ZERO DOLLARS";

                if (Convert.ToInt64(wholeNumber) > 0)
                {
                    numInWords = $"{ConvertToWords(wholeNumber)} DOLLARS";
                }

                if (fraction.Length > 0 && long.Parse(fraction) > 0)
                {
                    numInWords += $"{(numInWords.Length > 0 ? " AND " : "")}{ConvertToWords(long.Parse(fraction))} CENTS";
                }

                if (negativeVal)
                    numInWords = $"MINUS {numInWords}";

            }
            catch (Exception ex)
            {
                return $"Error : {ex.Message}";
            }

            return numInWords.ToUpper();

        }

        public static bool IsNumeric(string value, out double number)
        {
            return double.TryParse(value, out number);
        }
        public static bool IsNumeric(string value)
        {
            return double.TryParse(value, out double result);
        }

        /// <summary>
        /// This method is being recursively called to generate the string representation of a numeric
        /// </summary>
        /// <param name="numValue"></param>
        /// <returns></returns>
        private static String ConvertToWords(long numValue)
        {
            //String arrays to keep string equivalents.
            string[] ones = new string[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = new string[] { "", "Ten", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
            string words = "";
            try
            {

                if ((numValue / 1000000) > 0)
                {
                    words += ConvertToWords(numValue / 1000000) + " Million ";
                    numValue %= 1000000;
                }

                if ((numValue / 1000) > 0)
                {
                    words += ConvertToWords(numValue / 1000) + " Thousand ";
                    numValue %= 1000;
                }

                if ((numValue / 100) > 0)
                {
                    words += ConvertToWords(numValue / 100) + " Hundred ";
                    numValue %= 100;
                }

                if (numValue > 0)
                {
                    if (words.Length > 0)
                    {
                        words += "AND ";
                    }

                    if (numValue < 20)
                    {
                        words += ones[numValue];
                    }
                    else
                    {
                        words += tens[numValue / 10];
                        if ((numValue % 10) > 0)
                        {
                            words += "-" + ones[numValue % 10];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error : {ex.Message}";
            }
            return words.Trim();
        }

    }

}