using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class KursKayit
    {
        [Key]
        public int KayitId { get; set; }
        public int CustomerId { get; set; }
        public int KursId { get; set; }
        public DateTime KayitTarihi { get; set; }
    }
}