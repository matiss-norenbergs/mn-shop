namespace MN_Shop.Server.ModelsJson
{
    public class ProductDataJson
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double? Price { get; set; } = null;

        public string PriceString { get; set; } = string.Empty;
    }
}
