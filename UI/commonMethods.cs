using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI
{
    public class commonMethods
    {
        // Converts a string into an integer, inclusive between min and max. 
        public int convertString(string entry, int min, int max)
        {
            bool parseSuccess;
            int parsedInput;

            parseSuccess = int.TryParse(entry, out parsedInput);
            if (parseSuccess && parsedInput >= min && parsedInput <= max)
            {
                return parsedInput;
            }
            else
            {
                return -1;
            }
        }
        // same method, without a max value. 
        public int convertString(string entry, int min)
        {
            bool parseSuccess;
            int parsedInput;

            parseSuccess = int.TryParse(entry, out parsedInput);
            if (parseSuccess && parsedInput >= min)
            {
                return parsedInput;
            }
            else
            {
                return -1;
            }
        }
    }
}