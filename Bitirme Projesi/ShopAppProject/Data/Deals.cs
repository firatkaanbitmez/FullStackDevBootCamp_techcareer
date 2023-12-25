using System;
using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class Deals
    {
        [Key]
        public int DealsId { get; set; }

        [Display(Name = "Deals Type")]
        public DealsType DealsType { get; set; }

        [Display(Name = "Deals Name")]
        public string DealsName { get; set; }

        [Display(Name = "Discount Percentage")]
        public decimal? DiscountPercentage { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Status")]
        public DealsStatus Status
        {
            get
            {
                var now = DateTime.Now;
                return (now >= StartDate && now <= EndDate) ? DealsStatus.Active : DealsStatus.Inactive;
            }
        }
    }

    public enum DealsStatus
    {
        Active,
        Inactive
    }
    public enum DealsType
    {
        [Display(Name = "Sabit İndirim Yüzdesi")]
        DiscountPercentage,

        [Display(Name = "İndirim Kuponu")]
        DiscountCoupon,

        [Display(Name = "Ücretsiz Kargo")]
        FreeShipping,

        [Display(Name = "Hediye Ürün")]
        GiftProduct,

        [Display(Name = "Saatlik İndirimler")]
        TimeDownSale,

        [Display(Name = "Kategori İndirimleri")]
        CategoryDiscount,
    }

}
