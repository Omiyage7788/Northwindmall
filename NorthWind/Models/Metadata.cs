using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorthWind.Models
{
    public class MetaProducts
    {
        [DisplayName("產品編號")]
        public int ProductID { get; set; }

        [DisplayName("產品名稱")]
        [Required(ErrorMessage = "產品名稱為必填")]
        [StringLength(40, ErrorMessage = "產品名稱最多40字")]
        public string ProductName { get; set; }

        [DisplayName("供應商")]
        public Nullable<int> SupplierID { get; set; }

        [DisplayName("類別")]
        public Nullable<int> CategoryID { get; set; }

        [DisplayName("產品單位")]
        [Required(ErrorMessage = "產品單位為必填")]
        [StringLength(20, ErrorMessage = "產品單位最多20字")]
        public string QuantityPerUnit { get; set; }

        [DisplayName("單價")]
        [Range(0, double.MaxValue)]
        public Nullable<decimal> UnitPrice { get; set; }

        [DisplayName("庫存量")]
        [DefaultValue(0)]
        [Range(0, short.MaxValue)]
        public Nullable<short> UnitsInStock { get; set; }

        [DisplayName("訂購量")]
        [DefaultValue(0)]
        [Range(0, short.MaxValue)]
        public Nullable<short> UnitsOnOrder { get; set; }

        [DisplayName("安全存量")]
        [Range(0, short.MaxValue)]
        public Nullable<short> ReorderLevel { get; set; }

        [DisplayName("是否已下架")]
        [Required(ErrorMessage = "是否已下架為必填")]
        public bool Discontinued { get; set; }

    }

    public class MetaCategories
    {
        [DisplayName("類別編號")]
        public int CategoryID { get; set; }

        [DisplayName("類別名稱")]
        [Required(ErrorMessage = "類別名稱為必填")]
        [StringLength(15, ErrorMessage = "類別名稱最多15字")]
        public string CategoryName { get; set; }

        [DisplayName("類別描述")]
        public string Description { get; set; }

        [DisplayName("圖片")]
        [FileExtensions(Extensions = ".jpg", ErrorMessage = "這不是jpg檔")]
        public byte[] Picture { get; set; }
    }

    public class MetaCustomers
    {
        [CheckCustomersID(ErrorMessage ="此帳號已有人使用")]
        [DisplayName("顧客編號")]
        [Required(ErrorMessage = "顧客編號為必填")]
        [RegularExpression("[A-Z]{5}", ErrorMessage = "格式有誤")]
        public string CustomerID { get; set; }

        [DisplayName("公司名稱")]
        [Required(ErrorMessage = "公司名稱為必填")]
        [StringLength(40, ErrorMessage = "公司名稱最多40字")]
        public string CompanyName { get; set; }

        [DisplayName("連絡人姓名")]
        [StringLength(30, ErrorMessage = "連絡人姓名最多30字")]
        public string ContactName { get; set; }

        [DisplayName("連絡人職稱")]
        [StringLength(30, ErrorMessage = "連絡人職稱最多30字")]
        public string ContactTitle { get; set; }

        [DisplayName("地址")]
        [StringLength(60, ErrorMessage = "地址最多60字")]
        public string Address { get; set; }

        [DisplayName("城市")]
        [StringLength(15, ErrorMessage = "城市最多15字")]
        public string City { get; set; }

        [DisplayName("區域")]
        [StringLength(15, ErrorMessage = "區域最多15字")]
        public string Region { get; set; }

        [DisplayName("郵遞區號")]
        [StringLength(10, ErrorMessage = "郵遞區號最多10字")]
        public string PostalCode { get; set; }

        [DisplayName("國家")]
        [StringLength(15, ErrorMessage = "國家最多15字")]
        public string Country { get; set; }

        [DisplayName("連絡電話")]
        [Required(ErrorMessage = "連絡電話為必填")]
        [StringLength(24, ErrorMessage = "連絡電話最多24字")]
        public string Phone { get; set; }

        [DisplayName("傳真號碼")]
        [StringLength(24, ErrorMessage = "傳真號碼最多24字")]
        public string Fax { get; set; }



        public class CheckCustomersID:ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                NorthwindEntities db = new NorthwindEntities();

                var result = db.Customers.Where(m => m.CustomerID == value.ToString()).FirstOrDefault();

                bool r = result == null ? true : false;

                return r;
            }
        }
       
        }



    }

