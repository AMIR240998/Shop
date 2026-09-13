namespace ShopApplication.DTOs.Product;

public record UpdateProductImageDto(
     long ProductId,
     long ImageId,
     FileUpload Image);