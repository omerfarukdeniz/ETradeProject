using MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VIEWMODEL.ViewModels;

namespace MVCUI.Models.ShoppingTools
{
    public class OrderVM
    {
        public PaymentVM PaymentVM { get; set; }
        public Order Order { get; set; }

    }
}