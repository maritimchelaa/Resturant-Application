using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileLending.Models
{
    public class GlobalVariable
    {
        public static double Round(double x, bool IsRoundDown, bool IsRoundUp, double rounding = 1)
        {
            try
            {

                //rounding = Classes.MainInfo.saccoRoundCurrency;
                if (IsRoundDown)
                {
                    return ((int)(x / rounding)) * rounding;
                }
                else if (IsRoundUp)
                {
                    return (Math.Round(x / rounding)) * rounding;
                }
                else
                {
                    return Math.Round(x / rounding, MidpointRounding.AwayFromZero) * rounding;
                }
            }
            catch
            {
                return x;
            }
        }
        public static double RoundMonthlyInterest(double x, bool IsRoundDown, bool IsRoundUp, double rounding = 1)
        {

            try
            {
                /* if (rounding == 0)
                     rounding = Classes.MainInfo.saccoRoundCurrency;*/
                if (rounding == 0)
                    rounding = 0.05;

                if (IsRoundDown)
                {
                    return ((int)(x / rounding)) * rounding;
                }
                else if (IsRoundUp)
                {
                    return (Math.Round(x / rounding)) * rounding;
                }
                else
                {
                    return Math.Round(x / rounding, MidpointRounding.AwayFromZero) * rounding;
                }
            }
            catch
            {
                return x;
            }
        }
    }
}