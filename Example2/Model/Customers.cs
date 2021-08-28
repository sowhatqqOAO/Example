using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Example2.Model
{
    public class Customers
    {
        [Key]//主鍵
        [Required]//必要欄位=Not Null
        [DisplayName("客戶代號")]//欄位名稱
        [MaxLength(5)]//欄位長度
        public string CustomerID { get; set; }

        [Required]//必要欄位=Not Null
        [DisplayName("公司名稱")]//欄位名稱
        [MaxLength(40)]//欄位長度
        public string CompanyName { get; set; }

        [Required]//必要欄位=Not Null
        [DisplayName("聯絡人名稱")]//欄位名稱
        [MaxLength(30)]//欄位長度
        public string ContactName { get; set; }

        [Required]//必要欄位=Not Null
        [DisplayName("聯絡人抬頭")]//欄位名稱
        [MaxLength(30)]//欄位長度
        public string ContactTitle { get; set; }

        [Required]//必要欄位=Not Null
        [DisplayName("地址")]//欄位名稱
        [MaxLength(60)]//欄位長度
        public string Address { get; set; }

        [Required]//必要欄位=Not Null
        [DisplayName("城市")]//欄位名稱
        [MaxLength(15)]//欄位長度
        public string City { get; set; }

        [Required]//必要欄位=Not Null
        [DisplayName("地區")]//欄位名稱
        [MaxLength(30)]//欄位長度
        public string Region { get; set; }

        [Required]//必要欄位=Not Null
        [DisplayName("郵遞區號")]//欄位名稱
        [MaxLength(10)]//欄位長度
        public string PostalCode { get; set; }

        [Required]//必要欄位=Not Null
        [DisplayName("鄉鎮")]//欄位名稱
        [MaxLength(15)]//欄位長度
        public string Country { get; set; }

        [DisplayName("自訂功能別代號")]//欄位名稱
        [MaxLength(24)]//欄位長度
        public string Phone { get; set; }

        [DisplayName("自訂功能別代號")]//欄位名稱
        [MaxLength(24)]//欄位長度
        public string Fax { get; set; }
    }
}