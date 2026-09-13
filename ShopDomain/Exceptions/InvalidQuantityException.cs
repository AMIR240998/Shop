namespace ShopDomain.Exceptions;

public class InvalidQuantityException(object obj) : 
    Exception($"Items {obj} Quantity Must be Greater Than Zero")
{
}