namespace ShopDomain.Exceptions;

public class NotExistUserNameException(string userName) : 
    Exception($"UserName{userName} Not Found")
{
}