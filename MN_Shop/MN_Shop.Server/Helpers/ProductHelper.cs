using MN_Shop.Server.Models;
using MN_Shop.Server.ModelsJson;
using System.Globalization;

namespace MN_Shop.Server.Helpers
{
    public class ProductHelper
    {
        internal static ProductDataJson GetProductDataJson(ProductData data, UserData user)
        {
            var jsonData = new ProductDataJson
            {
                Id = data.Id.ToString(),
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                PriceString = data.Price?.ToString("0.00", new CultureInfo(user.LanguageId)) ?? string.Empty
            };

            return jsonData;
        }

        internal static List<ProductDataJson> GetProductDataJson(List<ProductData> data, UserData user)
        {
            var jsonData = new List<ProductDataJson>();

            foreach (var item in data)
                jsonData.Add(GetProductDataJson(item, user));

            return jsonData;
        }
    }
}
