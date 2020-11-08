using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using MainMusicStore.Models.DbModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MainMusicStore.Models.ViewModels
{
    public class ProductVM
    {
        public Product  Product { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }
    }
}
