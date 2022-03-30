using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileLending.Models
{
    
    public class productCategoryResponse
    {
        public bool? IsSuccess { get; set; }
        public string ErrorDescription { get; set; }
        public List<proc_getproductCategoriesResult> ProductCategories { get; set; }
    }

    public class productResponse
    {
        public bool? IsSuccess { get; set; }
        public string ErrorDescription { get; set; }
        public List<proc_getproductsResult> Products { get; set; }
    }
    public class NewClient
    {
        public bool? IsSuccess { get; set; }
        public string ErrorDescription { get; set; }
        public proc_addEditClientResult ClientDetails { get; set; }

    }
    public class clientLoginResponse
    {
        public bool? IsSuccess { get; set; }
        public string ErrorDescription { get; set; }
        public TblClient ClientDetails { get; set; }

    }
    public class RegionsResponse
    {
        public bool? IsSuccess { get; set; }
        public string ErrorDescription { get; set; }
        public List<proc_getregionsResult> RegionDetails { get; set; }
        public List<proc_getAreasByRegionIdResult> AreaDetails  { get; set; }

    }
    public class NewOrder
    {
        public bool? IsSuccess { get; set; }
        public string ErrorDescription { get; set; }
        public proc_AddEditOrdersResult OrderDetails { get; set; }
    }

public class OrdersDetailsReponse
    {
        public bool? IsSuccess { get; set; }
        public string ErrorDescription { get; set; }
        public List <TblOrderDetail> OrderDetailslist { get; set; }
    }
    public class OutLetResponse
    {
        public bool? IsSuccess { get; set; }
        public string ErrorDescription { get; set; }
       public List <Proc_getAllOutletsResult> OutletsDetails { get; set; }

    }
    public class OrderListResponse
    {
        public bool? IsSuccess { get; set; }
        public string ErrorDescription { get; set; }
        public List<proc_getOrdersResult> Ordelist { get; set; }
       

    }
    public class OrderdEtailsResponse
    {
        public bool? IsSuccess { get; set; }
        public string ErrorDescription { get; set; }
        
        public List<Proc_getOrderdetailsResult> OrderDetailsList { get; set; }

    }
}
