namespace ShopDomain.Exceptions;

public class DuplicateUserNameException(string userName):
    Exception($"UserName{userName} is Duplicated")
{
}