namespace ShopDomain.Exceptions;

public class InvalidPasswordException(string password) : 
    Exception($"Password{password} not valid")
{
}