using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Product
{
    public class CreateProductDto : CreateOrUpdateProduct
    {
        [Required]
        public string No { get; set; }

    }
}
