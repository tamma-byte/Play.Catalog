using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service.DataTransferObjects
{
    public record ItemDataTransferObject(Guid Id, string Name, string Description, decimal Price, DateTimeOffset DateCreated);

    public record CreateItemDataTransferObject([Required] string Name, string Description, [Range(0, 1000)] decimal Price);

    public record UpdateItemDataTransferObject([Required] string Name, string Description, [Range(0, 1000)] decimal Price);


}