using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {   
        [Required(ErrorMessage ="Vui lòng nhập tên")]
        [Display(Name="Họ và tên")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập địa chỉ đầu tiên")]
        [Display(Name="Địa chỉ 1")]
        public string Line1 { get; set; }
        [Display(Name = "Địa chỉ 2")]
        public string Line2 { get; set; }
        [Display(Name = "Địa chỉ 3")]
        public string Line3 { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên Tỉnh/Thành Phố")]
        [Display(Name = "Tỉnh/Thành phố")]
        public string City { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên Quận/Huyện")]
        [Display(Name = "Quận/Huyện")]
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}