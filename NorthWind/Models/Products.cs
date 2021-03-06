//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NorthWind.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(MetaProducts))]

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            this.Order_Details = new HashSet<Order_Details>();
        }

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
    
        public virtual Categories Categories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
        public virtual Suppliers Suppliers { get; set; }
    }
}
