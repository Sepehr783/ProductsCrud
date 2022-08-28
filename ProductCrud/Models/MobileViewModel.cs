using System.ComponentModel.DataAnnotations;

namespace ProductCrud.Models;

public class MobileViewModel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please Enter Product Name")]
    [Display(Name = "Mobile Name")]
    [StringLength(100, MinimumLength = 5)]
    public string? MobileName { get; set; } = null;

    [Required(ErrorMessage = "Please Enter Product Price")]
    [DataType(DataType.Currency)]
    public int Price { get; set; }

    [Display(Name = "Product Image")]
    public IFormFile? MobileImage { get; set; } = null;
}