using ProtoBuf;

namespace MN_Shop.Server.Models
{
    [ProtoContract]
    public class ProductData
    {
        [ProtoMember(1, IsRequired = true)]
        public long Id { get; set; } = 0;

        [ProtoMember(2, IsRequired = true)]
        public string Name { get; set; } = string.Empty;

        [ProtoMember(3, IsRequired = true)]
        public string Description { get; set; } = string.Empty;

        [ProtoMember(4, IsRequired = true)]
        public decimal Price { get; set; } = 0;
    }
}
