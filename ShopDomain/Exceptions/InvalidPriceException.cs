namespace ShopDomain.Exceptions;

public class InvalidPriceException() :
    Exception("The percentage price increase must not be equal to zero or less than ninety")
{
}