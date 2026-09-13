namespace ShopApplication.DTOs.Product;

public record FileUpload(
    Stream Content,
    string FileName,
    string ContentType);