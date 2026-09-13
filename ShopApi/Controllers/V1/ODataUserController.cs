using Asp.Versioning;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ShopApplication.Repositories;
using ShopDomain.Entities;

namespace ShopApi.Controllers.V1;

[ApiVersion(1.0)]

public class ODataUserController(IUserRepository repository) : ODataController
{
    [EnableQuery(MaxExpansionDepth = 2)]
    public IQueryable<User> Get()
    {
        return repository.GetAllAsQueryable();
    }
}