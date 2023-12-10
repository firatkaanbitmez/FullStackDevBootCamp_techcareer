// Models Product.cs örneği


using System.ComponentModel.DataAnnotations;


public class Product
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    // Diğer özellikler  (stok, resim yolu, vb.). EklEEEEEEE

    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
