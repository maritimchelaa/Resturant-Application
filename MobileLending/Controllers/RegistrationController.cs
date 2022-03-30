using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MobileLending.Models;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http.Description;

namespace MobileLending.Controllers
{
    [RoutePrefix("Orders")]
    public class RegistrationController : ApiController
    {

        [Route("getProductCategories"), HttpGet]
        public IHttpActionResult getProductCategories(int categoryId)
        {
            productCategoryResponse pcResponse = new productCategoryResponse();
            pcResponse.IsSuccess = false;
            pcResponse.ErrorDescription = "";

            //validation
            if (categoryId == null)
            {
                categoryId = 0;

            }
            try
            {
                using (MyConnDataContext db = new MyConnDataContext())
                {
                    var dbPCategories = db.proc_getproductCategories(categoryId).ToList();
                    pcResponse.IsSuccess = true;
                    pcResponse.ErrorDescription = "";
                    pcResponse.ProductCategories = dbPCategories;
                    return Ok(pcResponse);
                }
            }
            catch (Exception ex)
            {
                pcResponse.IsSuccess = false;
                pcResponse.ErrorDescription = ex.Message;
                return Ok(pcResponse);
            }
        }

        [Route("getProducts"), HttpGet]
        public IHttpActionResult getProducts(int productId)
        {
            productResponse pResponse = new productResponse();
            pResponse.IsSuccess = false;
            pResponse.ErrorDescription = "";

            //validation
            if (productId == null)
            {
                productId = 0;

            }
            try
            {
                using (MyConnDataContext db = new MyConnDataContext())
                {
                    var dbProducts = db.proc_getproducts(productId).ToList();
                    pResponse.IsSuccess = true;
                    pResponse.ErrorDescription = "";
                    pResponse.Products = dbProducts;
                    return Ok(pResponse);
                }
            }
            catch (Exception ex)
            {
                pResponse.IsSuccess = false;
                pResponse.ErrorDescription = ex.Message;
                return Ok(pResponse);
            }
        }

        [Route ("getOutlets"), HttpGet]
        public IHttpActionResult getOutlets(int outLetId)
        {
            OutLetResponse oResponse = new OutLetResponse();
            oResponse.IsSuccess = false;
            oResponse.ErrorDescription = "";

            //validation
            if (outLetId == null)
            {
                outLetId = 0;

            }
            try
            {
                using (MyConnDataContext db = new MyConnDataContext())
                {
                    var dboutlets = db.Proc_getAllOutlets(outLetId).ToList();

                    oResponse.IsSuccess = true;
                    oResponse.ErrorDescription = "";
                    oResponse.OutletsDetails = dboutlets;
                    
                    return Ok(oResponse);
                }
            }
            catch (Exception ex)
            {
                oResponse.IsSuccess = false;
                oResponse.ErrorDescription = ex.Message;
                return Ok(oResponse);
            }
        }

        [Route("getRegions"), HttpGet]
        public IHttpActionResult getRegions(int regionId)
        {
            RegionsResponse rResponse = new RegionsResponse();
            rResponse.IsSuccess = false;
            rResponse.ErrorDescription = "";

            //validation
            if (regionId == null)
            {
                regionId = 0;

            }
            try
            {
                using (MyConnDataContext db = new MyConnDataContext())
                {
                    var dbregions = db.proc_getregions(regionId).ToList();
                    var dbAreas = db.proc_getAreasByRegionId(regionId).ToList();
                    rResponse.IsSuccess = true;
                    rResponse.ErrorDescription = "";
                    rResponse.RegionDetails = dbregions;
                    rResponse.AreaDetails=dbAreas;
                    return Ok(rResponse);
                }
            }
            catch (Exception ex)
            {
                rResponse.IsSuccess = false;
                rResponse.ErrorDescription = ex.Message;
                return Ok(rResponse);
            }
        }
       
        [Route("getOrdersList"), HttpGet]
        public IHttpActionResult getOrdersList(int Clientid)
        {
            OrderListResponse oResponse = new OrderListResponse();
            oResponse.IsSuccess = false;
            oResponse.ErrorDescription = "";

            //validation
            if (Clientid == null)
            {
                Clientid = 0;

            }
            try
            {
                using (MyConnDataContext db = new MyConnDataContext())
                {
                  var dborder = db.proc_getOrders(Clientid).ToList();
                    List<Proc_getOrderdetailsResult> orderDetailslist = new List<Proc_getOrderdetailsResult>();
                    foreach (var a in dborder)
                    {
                        
                        var dbodetails = db.Proc_getOrderdetails(a.OrderId).ToList();
                      
                    }
                    
                    oResponse.IsSuccess = true;
                    oResponse.ErrorDescription = "";
                    oResponse.Ordelist = dborder;
                    
                    return Ok(oResponse);
                }
            }
            catch (Exception ex)
            {
                oResponse.IsSuccess = false;
                oResponse.ErrorDescription = ex.Message;
                return Ok(oResponse);
            }
        }
        [Route("getOrderDetails"), HttpGet]
        public IHttpActionResult getOrderDetails(int OrderId)
        {
            OrderdEtailsResponse oResponse = new OrderdEtailsResponse();
            oResponse.IsSuccess = false;
            oResponse.ErrorDescription = "";

            //validation
            if (OrderId == null)
            {
                OrderId = 0;

            }
            try
            {
                using (MyConnDataContext db = new MyConnDataContext())
                {
                    var dborder = db.Proc_getOrderdetails(OrderId).ToList();
                   

                    oResponse.IsSuccess = true;
                    oResponse.ErrorDescription = "";
                    oResponse.OrderDetailsList = dborder;

                    return Ok(oResponse);
                }
            }
            catch (Exception ex)
            {
                oResponse.IsSuccess = false;
                oResponse.ErrorDescription = ex.Message;
                return Ok(oResponse);
            }
        }
        [Route("AddNewClient"), HttpPost]
        public IHttpActionResult AddNewClient(int? ClientId, string CfirstName, string CLastName, string PrimaryPhoneNo, string SecondPhoneNo, string EmailAdress, string Password, int? RegionId, int? AreaId, string Remarks, string CreatedBy, bool? Delete)
        {
            NewClient Nclient = new NewClient();
            Nclient.IsSuccess = false;
            Nclient.ErrorDescription = "";


            if (CfirstName == "" || CfirstName == null)
            {
                Nclient.ErrorDescription = "First Name is compulsory";
                return Ok(Nclient);
            }

            if (CLastName == "" || CLastName == null)
            {
                Nclient.ErrorDescription = "Last Name is compulsory";
                return Ok(Nclient);
            }

            if (EmailAdress != "" && EmailAdress != "undefined" && !emailIsValid(EmailAdress))

            {
                Nclient.ErrorDescription = "Invalid Email format";
                return Ok(Nclient);
            }
            if (PrimaryPhoneNo == "" || PrimaryPhoneNo == null)
            {
                Nclient.ErrorDescription = "Primary PhoneNo is required";
                return Ok(Nclient);
            }
            try
            {
                using (MyConnDataContext db = new MyConnDataContext())
                {
                    if (ClientId> 0)
                    {
                        var checkIfExist = (from a in db.TblClients where a.ClientId == ClientId select a).FirstOrDefault();
                        if(checkIfExist ==null)
                        {
                            Nclient.IsSuccess = false;
                            Nclient.ErrorDescription = "Account details was not found";
                            return Ok(Nclient);
                        }
                    }

                    var existPhone = (from a in db.TblClients where a.PrimaryPhoneNo == PrimaryPhoneNo  select a).FirstOrDefault();
                    if (existPhone != null)
                    {
                        Nclient.IsSuccess = false;
                        Nclient.ErrorDescription = "Account already exist Please login!";
                        return Ok(Nclient);                       
                    }
                    

                    var dbResult = db.proc_addEditClient(ClientId, CfirstName, CLastName, PrimaryPhoneNo, SecondPhoneNo, EmailAdress,Encrypt(Password), RegionId, AreaId, Remarks, CreatedBy, Delete).FirstOrDefault();
                    if (dbResult == null)
                    {
                        Nclient.ErrorDescription = "Process failed.please try again";
                        return Ok(Nclient);
                    }

                    Nclient.IsSuccess = true;
                    Nclient.ErrorDescription = "";
                    Nclient.ClientDetails = dbResult;

                    return Ok(Nclient);
                }
            }
            catch (Exception ex)
            {
                Nclient.IsSuccess = false;
                Nclient.ErrorDescription = ex.Message;
                return Ok(Nclient);
            }



        }
        [Route("EditClientDetails"), HttpPut]
        public IHttpActionResult EditClientDetails(int? ClientId, string CfirstName, string CLastName, string PrimaryPhoneNo, string SecondPhoneNo, string EmailAdress, string Password, int? RegionId, int? AreaId, string Remarks, string CreatedBy, bool? Delete)
        {
            NewClient Editclient = new NewClient();
            Editclient.IsSuccess = false;
            Editclient.ErrorDescription = "";


            if (CfirstName == "" || CfirstName == null)
            {
                Editclient.ErrorDescription = "First Name is required";
                return Ok(Editclient);
            }

            if (CLastName == "" || CLastName == null)
            {
                Editclient.ErrorDescription = "Last Name is required";
                return Ok(Editclient);
            }

            if (EmailAdress != "" && EmailAdress != "undefined" && !emailIsValid(EmailAdress))

            {
                Editclient.ErrorDescription = "Invalid Email format";
                return Ok(Editclient);
            }
            if (PrimaryPhoneNo == "" || PrimaryPhoneNo == null)
            {
                Editclient.ErrorDescription = "Primary PhoneNo is required";
                return Ok(Editclient);
            }
            try
            {
                using (MyConnDataContext db = new MyConnDataContext())
                {
                    if (ClientId > 0)
                    {
                        var checkIfExist = (from a in db.TblClients where a.ClientId == ClientId select a).FirstOrDefault();
                        if (checkIfExist == null)
                        {
                            Editclient.IsSuccess = false;
                            Editclient.ErrorDescription = "Account details was not found";
                            return Ok(Editclient);
                        }
                    }
                    var existPhone = (from a in db.TblClients where a.PrimaryPhoneNo == PrimaryPhoneNo select a).FirstOrDefault();
                    if (existPhone != null)
                    {
                        Editclient.IsSuccess = false;
                        Editclient.ErrorDescription = "Account already exist Please login!";
                        return Ok(Editclient);
                    }

                    var dbResult = db.proc_addEditClient(ClientId, CfirstName, CLastName, PrimaryPhoneNo, SecondPhoneNo, EmailAdress, Encrypt(Password), RegionId, AreaId, Remarks, CreatedBy, Delete).FirstOrDefault();
                    if (dbResult == null)
                    {
                        Editclient.ErrorDescription = "Process failed.please try again";
                        return Ok(Editclient);
                    }

                    Editclient.IsSuccess = true;
                    Editclient.ErrorDescription = "";
                    Editclient.ClientDetails = dbResult;

                    return Ok(Editclient);
                }
            }
            catch (Exception ex)
            {
                Editclient.IsSuccess = false;
                Editclient.ErrorDescription = ex.Message;
                return Ok(Editclient);
            }



        }
        [Route("ClientLogin"), HttpGet]
        public async Task<IHttpActionResult> ClientLogin(string UserName, string Password)
        {
            clientLoginResponse nlogin = new clientLoginResponse();
            nlogin.IsSuccess = false;
            nlogin.ErrorDescription = "";
            try
            {
                if (UserName == null || UserName.ToString().Trim() == "")
                {
                    nlogin.ErrorDescription = "Invalid username";
                    return Ok(nlogin);
                }
                if (Password == null || Password.ToString().Trim() == "")
                {
                    nlogin.ErrorDescription = "Invalid password";
                    return Ok(nlogin);
                }
                //Password = Password.ToString().Trim();
                //var passwd = Encrypt(Password);

                using (MyConnDataContext db = new MyConnDataContext())
                {
                    var pswd = Encrypt(Password);
                    var dbUserDetails = (from a in db.TblClients where a.PrimaryPhoneNo == UserName.ToString().Trim() && a.Password == pswd select a).FirstOrDefault();
                    if (dbUserDetails == null)
                    {
                        nlogin.IsSuccess = false;
                        nlogin.ErrorDescription = "Invalid Login credentials";
                        return Ok(nlogin);
                    }

                    nlogin.ClientDetails = dbUserDetails;
                    nlogin.IsSuccess = true;
                    nlogin.ErrorDescription = "";
                    return Ok(nlogin);
                }

            }
            catch (Exception ex)
            {
                nlogin.IsSuccess = false;
                nlogin.ErrorDescription = ex.ToString();
                return Ok(nlogin);
            }
        }
        [Route("saveOrderDetails"), HttpPost]
        public IHttpActionResult saveDetailOrder(List <TblOrderDetail> orderDetails)
        {
            OrdersDetailsReponse odResponse = new OrdersDetailsReponse();
            odResponse.IsSuccess = false;
            odResponse.ErrorDescription = "";

            //validation
            if (orderDetails == null)
            {
                odResponse.ErrorDescription = "Invalid order details";
                return Ok(odResponse);
            }
            try
            {
                using (MyConnDataContext dbConn = new MyConnDataContext())
                {
                    foreach(var a in orderDetails)
                    {
                        var dbAddOrder = dbConn.proc_AddEditOrderDetails(0,a.OrderId, a.ProductId, a.Quantity, a.Price, a.Subtotal,a.CreatedBy,false ).FirstOrDefault();
                    }
                    var firstOrderRecord = orderDetails.FirstOrDefault();
                    var checkIfItinserted = (from a in dbConn.TblOrderDetails where a.OrderId == firstOrderRecord.OrderId select a).ToList();
                    if (checkIfItinserted == null)
                    {
                        odResponse.IsSuccess = false;
                        odResponse.ErrorDescription = "Saving details for order "+ firstOrderRecord.OrderId.ToString() + "Failed";
                        return Ok(odResponse);
                    }
                    odResponse.IsSuccess = true;
                    odResponse.ErrorDescription = "";
                    odResponse.OrderDetailslist = checkIfItinserted;
                    return Ok(odResponse);
                }
            }
            catch (Exception ex)
            {
                odResponse.IsSuccess = false;
                odResponse.ErrorDescription = ex.Message;
                return Ok(odResponse);
            }
        }
        [Route("saveClientOrder"), HttpPost]
        public IHttpActionResult saveClientOrder(TblOrder order)
        {
            NewOrder oResponse = new NewOrder();
            oResponse.IsSuccess = false;
            oResponse.ErrorDescription = "";

            //validation
            
            if (order.ClientId == null || order.ClientId == 0)
                {
                    oResponse.ErrorDescription = "Client  is required";
                    return Ok(oResponse);
                }
            if (order.OutletId == null || order.OutletId == 0)
            {
                oResponse.ErrorDescription = "Outlet Id is required";
                return Ok(oResponse);
            }
            if (order.NoOfItems == null || order.NoOfItems == 0)
            {
                oResponse.ErrorDescription = "NoOfItems is required";
                return Ok(oResponse);
            }
            if (order.Subtotal == null || order.Subtotal == 0)
            {
                oResponse.ErrorDescription = "Subtotal is required";
                return Ok(oResponse);
            }
            if (order.DeliveryFee == null || order.DeliveryFee == 0)
            {
                oResponse.ErrorDescription = "DeliveryFee is required";
                return Ok(oResponse);
            }
            if (order.TotalPrice == null || order.TotalPrice == 0)
            {
                oResponse.ErrorDescription = "TotalPrice is required";
                return Ok(oResponse);
            }
            if (order.OrderPaymentBalance == null)
            {
                oResponse.ErrorDescription = "OrderPaymentBalance is required";
                return Ok(oResponse);
            }
            if (order.OrderStatus == null || order.OrderStatus == "")
            {
                oResponse.ErrorDescription = "OrderStatus is required";
                return Ok(oResponse);
            }
            if (order.CreatedBy == null || order.CreatedBy == "")
            {
                oResponse.ErrorDescription = "CreatedBy is required";
                return Ok(oResponse);
            }
            try
            {
                using (MyConnDataContext dbConn = new MyConnDataContext())
                {
                    var dbAddOrder = dbConn.proc_AddEditOrders (0, order.ClientId, order.OutletId, order.OrderNo, order.NoOfItems, order.Subtotal,order.DeliveryFee, order.TotalPrice, order.OrderPaymentBalance, order.OrderStatus, order.CreatedBy).FirstOrDefault();

                    if (dbAddOrder == null)
                    {
                        oResponse.IsSuccess = false;
                        oResponse.ErrorDescription = "Saving Order No. " + order.OrderNo + " failed.";
                        return Ok(oResponse);
                    }
                    oResponse.IsSuccess = true;
                    oResponse.ErrorDescription = "";
                    oResponse.OrderDetails = dbAddOrder;
                    return Ok(oResponse);
                }
            }
            catch (Exception ex)
            {
                oResponse.IsSuccess = false;
                oResponse.ErrorDescription = ex.Message;
                return Ok(oResponse);
            }
        }
        private bool emailIsValid(string email)
        {
            string expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        public static string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }
            return Convert.ToBase64String(cipherTextBytes);
        }
        public static string Decrypt(string encryptedText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

    }
                

}
