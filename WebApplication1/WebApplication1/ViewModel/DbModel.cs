using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication1.ViewModel
{
    public class DbModel
    {
        [DisplayName ("編號")]
        public int Id { get; set; }
        [DisplayName("名稱")]
        public string Name { get; set; }
        [DisplayName("圖片")]
        public string Imag { get; set; }
        [DisplayName("圖片實體位置")]
        public string LImag { get; set; }
    }
}