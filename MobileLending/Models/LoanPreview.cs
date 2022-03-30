using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileLending.Models
{
    public class LoanPreview
    {
        public DateTime ApplicationDate { get; set; }
        public DateTime EffectDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalLoan { get; set; }
        public double InterestAmount { get; set; }
        public double Installment { get; set; }

    }
    public class LoanSchedule
    {
        public DateTime DueDate { get; set; }
        public double GrossAmount { get; set; }
        public double ClosingBalance { get; set; }
    }
}