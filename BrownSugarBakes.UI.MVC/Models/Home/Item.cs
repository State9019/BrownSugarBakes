using System;
using BrownSugarBakes.TTR.DATA.EF;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrownSugarBakes.UI.MVC.Models.Home
{
    public class Item
    {
        public Tbl_Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Location
    {
        public Tbl_ShippingDetails ShipInfo { get; set; }
      
    }
}