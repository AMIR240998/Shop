namespace ShopDomain.Exceptions;

public class DuplicateCodeException(object code) : 
    Exception($"code {code} Is Duplicated")
{
}