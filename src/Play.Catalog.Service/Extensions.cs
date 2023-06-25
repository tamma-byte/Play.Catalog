using Play.Catalog.Service.DataTransferObjects;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service
{
    public static class Extensions
    {
        // returns item into an entity
        public static ItemDataTransferObject AsDataTransferObject(this Item item)
        {
            return new ItemDataTransferObject(item.Id, item.Name, item.Description, item.Price, item.CreateDate);
        }
    }
}