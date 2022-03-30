using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileLending.Controllers
{
    public class UGIController : Controller
    {
        // GET: UGI
        public ActionResult Index()
        {
        //    [Route("pushOnlinePayment"), HttpPost]
        //public IHttpActionResult pushOnlinePayment(tblPayment payment)
        //    {
        //        paymentResponse pResponse = new paymentResponse();
        //        pResponse.IsSuccess = false;
        //        pResponse.ErrorDescription = "";

        //        //validation
        //        if (payment.ClientName == null || payment.ClientName == "")
        //        {
        //            pResponse.ErrorDescription = "Client Name is required";
        //            return Ok(pResponse);
        //        }
        //        if (payment.ClientNationalId == null || payment.ClientNationalId == "")
        //        {
        //            pResponse.ErrorDescription = "Client National Id is required";
        //            return Ok(pResponse);
        //        }
        //        if (payment.PaymentAmount == null || payment.PaymentAmount <= 0)
        //        {
        //            pResponse.ErrorDescription = "Invalid payment amount";
        //            return Ok(pResponse);
        //        }
        //        if (payment.PaymentTime == null)
        //        {
        //            pResponse.ErrorDescription = "Payment date time is required";
        //            return Ok(pResponse);
        //        }
        //        if (payment.PaymentTime > DateTime.Now)
        //        {
        //            pResponse.ErrorDescription = "Payment date time cannot be in the future";
        //            return Ok(pResponse);
        //        }
        //        if (payment.PaymentType == null || payment.PaymentType == "")
        //        {
        //            pResponse.ErrorDescription = "Payment type is required";
        //            return Ok(pResponse);
        //        }
        //        if (payment.PaymentType != "POLICY" && payment.PaymentType != "QUOTE")
        //        {
        //            pResponse.ErrorDescription = "Invalid payment type.Please use either POLICY or QUOTE";
        //            return Ok(pResponse);
        //        }

        //        if (payment.ReferenceNo == null || payment.ReferenceNo == "")
        //        {
        //            pResponse.ErrorDescription = "Payment reference No is required";
        //            return Ok(pResponse);
        //        }
        //        if (payment.TransactionId == null || payment.TransactionId == "")
        //        {
        //            pResponse.ErrorDescription = "Payment transaction Id is required";
        //            return Ok(pResponse);
        //        }

        //        var re = HttpContext.Current.Request;
        //        var cKey = re.Headers.Get("X-UGI-CONSUMER-KEY");
        //        var cSecret = re.Headers.Get("X-UGI-CONSUMER-SECRET" + "");

        //        if (cKey == null || cKey == "" || cSecret == null || cSecret == "")
        //        {
        //            pResponse.IsSuccess = false;
        //            pResponse.ErrorDescription = "Authentication required";
        //            return Ok(pResponse);
        //        }
        //        try
        //        {
        //            using (MyConnDataContext dbConn = new MyConnDataContext())
        //            {
        //                var dbAuth = (from a in dbConn.tblApiUsers where a.ConsumerKey == cKey && a.ConsumerSecret == cSecret select a).FirstOrDefault();
        //                if (dbAuth == null)
        //                {
        //                    pResponse.IsSuccess = false;
        //                    pResponse.ErrorDescription = "Invalid Authentication details";
        //                    return Ok(pResponse);
        //                }
        //                //check if posted first
        //                var existingPaytCheck = (from a in dbConn.tblPayments where a.TransactionId == payment.TransactionId select a).FirstOrDefault();
        //                if (existingPaytCheck != null)
        //                {
        //                    pResponse.IsSuccess = false;
        //                    pResponse.ErrorDescription = "Payment already posted";
        //                    return Ok(pResponse);
        //                }
        //                //post raw payment details first
        //                var dbPostRawPayt = dbConn.proc_addRawPayment(payment.PaymentTime, payment.TransactionId, payment.PaymentAmount, payment.ClientNationalId, payment.ClientName, payment.PaymentType, payment.ReferenceNo).FirstOrDefault();
        //                if (dbPostRawPayt == null)
        //                {
        //                    pResponse.IsSuccess = false;
        //                    pResponse.ErrorDescription = "Posting payment " + payment.TransactionId + " failed.";
        //                    return Ok(pResponse);
        //                }
        //                //now proceed
        //                if (payment.PaymentType == "QUOTE")
        //                {
        //                    var dbQuoteCheck = (from a in dbConn.tblQuote1s where a.QuoteNumber == payment.ReferenceNo select a).FirstOrDefault();
        //                    if (dbQuoteCheck == null)
        //                    {
        //                        pResponse.IsSuccess = false;
        //                        pResponse.ErrorDescription = "Invalid quote number.Please confirm";
        //                        return Ok(pResponse);
        //                    }
        //                    var postQuotePayment = dbConn.proc_addQuotePayments(0, dbQuoteCheck.Id, 0, payment.PaymentAmount, "BANK", payment.ReferenceNo, "Online API", false, dbPostRawPayt.PaymentId).FirstOrDefault();
        //                    if (postQuotePayment == null)
        //                    {
        //                        pResponse.IsSuccess = false;
        //                        pResponse.ErrorDescription = "Posting payment " + payment.TransactionId + " failed.";
        //                        return Ok(pResponse);
        //                    }
        //                    pResponse.IsSuccess = true;
        //                    pResponse.ErrorDescription = "";
        //                    pResponse.PaymentDetails = dbPostRawPayt;
        //                    //pResponse.PaymentDetails.ClientName = dbPostRawPayt.ClientName;
        //                    //pResponse.PaymentDetails.ClientNationalId = dbPostRawPayt.ClientNationalId;
        //                    //pResponse.PaymentDetails.CreatedOn = dbPostRawPayt.CreatedOn;
        //                    //pResponse.PaymentDetails.PaymentAmount = dbPostRawPayt.PaymentAmount;
        //                    //pResponse.PaymentDetails.PaymentId = dbPostRawPayt.PaymentId;
        //                    //pResponse.PaymentDetails.PaymentTime = dbPostRawPayt.PaymentTime;
        //                    //pResponse.PaymentDetails.PaymentType = dbPostRawPayt.PaymentType;
        //                    //pResponse.PaymentDetails.ReferenceNo = dbPostRawPayt.ReferenceNo;
        //                    //pResponse.PaymentDetails.TransactionId = dbPostRawPayt.TransactionId;
        //                    return Ok(pResponse);
        //                }
        //                if (payment.PaymentType == "POLICY")
        //                {
        //                    var dbPolicyCheck = (from a in dbConn.tblPolicies where a.PolicyNumber == payment.ReferenceNo select a).FirstOrDefault();
        //                    if (dbPolicyCheck == null)
        //                    {
        //                        pResponse.IsSuccess = false;
        //                        pResponse.ErrorDescription = "Invalid policy number.Please confirm";
        //                        return Ok(pResponse);
        //                    }
        //                    var postPolicyPayment = dbConn.proc_addPremiumPayments(0, dbPolicyCheck.PolicyId, payment.PaymentAmount, "BANK", payment.ReferenceNo, "Online via API", dbPostRawPayt.PaymentId).FirstOrDefault();
        //                    if (postPolicyPayment == null)
        //                    {
        //                        pResponse.IsSuccess = false;
        //                        pResponse.ErrorDescription = "Posting payment " + payment.TransactionId + " failed.";
        //                        return Ok(pResponse);
        //                    }

        //                    paymentResponse pr = new paymentResponse();
        //                    pResponse.IsSuccess = true;
        //                    pResponse.ErrorDescription = "";
        //                    pResponse.PaymentDetails = dbPostRawPayt;
        //                    //pResponse.PaymentDetails.ClientName = dbPostRawPayt.ClientName;
        //                    //pResponse.PaymentDetails.ClientNationalId = dbPostRawPayt.ClientNationalId;
        //                    //pResponse.PaymentDetails.CreatedOn = dbPostRawPayt.CreatedOn;
        //                    //pResponse.PaymentDetails.PaymentAmount = dbPostRawPayt.PaymentAmount;
        //                    //pResponse.PaymentDetails.PaymentId = dbPostRawPayt.PaymentId;
        //                    //pResponse.PaymentDetails.PaymentTime = dbPostRawPayt.PaymentTime;
        //                    //pResponse.PaymentDetails.PaymentType = dbPostRawPayt.PaymentType;
        //                    //pResponse.PaymentDetails.ReferenceNo = dbPostRawPayt.ReferenceNo;
        //                    //pResponse.PaymentDetails.TransactionId = dbPostRawPayt.TransactionId;
        //                    return Ok(pResponse);
        //                }
        //                return Ok(pResponse);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            pResponse.IsSuccess = false;
        //            pResponse.ErrorDescription = ex.Message;
        //            return Ok(pResponse);
        //        }
        //    }
            return View();
        }
    }
}