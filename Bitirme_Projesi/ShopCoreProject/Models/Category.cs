//Models Category.cs

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    // Diğer özellikler eklenebilir.

    public ICollection<Product> Products { get; set; }
}
