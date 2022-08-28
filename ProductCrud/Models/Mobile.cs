using System.ComponentModel.DataAnnotations;

namespace ProductCrud.Models;

public class Mobile
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Please Enter Product Name")]
    [Display(Name = "Mobile Name")]
    [StringLength(100)]
    public string? MobileName { get; set; }
    
    [Required(ErrorMessage = "Please Enter Product Price")]
    [DataType(DataType.Currency)]
    public int Price { get; set; }
    
    
    [Display(Name = "Product Image")]
    public string? MobilePhotoImage { get; set; } = null;
}