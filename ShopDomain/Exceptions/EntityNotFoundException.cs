namespace ShopDomain.Exceptions;

public class EntityNotFoundException(string entityName,object? id) : 
    Exception($"{entityName} with id {id} Not Found")
{
}