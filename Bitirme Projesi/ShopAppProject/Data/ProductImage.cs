//Data/ProductImage.cs

using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string ImagePath { get; set; }

        public string FullImagePath => Path.Combine("/images", Path.GetFileName(ImagePath));

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }


}
