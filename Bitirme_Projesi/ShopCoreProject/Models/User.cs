// Models User.cs örneği

using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public string? Email { get; set; }

    // Diğer özellikler eklenebilir (şifre, adres, vb.).
}
