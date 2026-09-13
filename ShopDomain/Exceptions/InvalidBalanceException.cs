namespace ShopDomain.Exceptions;

public class InvalidBalanceException() : 
    Exception($"balance must be Greater than zero")
{
}