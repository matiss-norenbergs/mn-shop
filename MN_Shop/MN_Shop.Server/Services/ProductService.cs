using DBreeze;
using DBreeze.Objects;
using DBreeze.Utils;
using MN_Shop.Server.Models;

namespace MN_Shop.Server.Services
{
    public class ProductService
    {
        private static string _productTable = "Products";

        private static byte _primaryIndex = 1;

        readonly DBreezeEngine? productEngine = null;

        public ProductService()
        {
            CustomSerializator.ByteArraySerializator = Helper.SerializeProtobuf;
            CustomSerializator.ByteArrayDeSerializator = Helper.DeserializeProtobuf;

            productEngine ??= new DBreezeEngine($"{Environment.CurrentDirectory}/Databases/Products");
        }

        public List<ProductData>? GetProductList()
        {
            try
            {
                var productList = new List<ProductData>();

                using (var tran = productEngine.GetTransaction())
                {
                    foreach (var row in tran.SelectForward<byte[], byte[]>(_productTable))
                    {
                        var obj = row.ObjectGet<ProductData>()?.Entity;
                        if (obj != null)
                            productList.Add(obj);
                    }
                }

                return productList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public Dictionary<long, ProductData> GetProductCollectionById()
        {
            try
            {
                var productList = GetProductList() ?? [];

                return productList.ToDictionary(x => x.Id, x => x);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return [];
            }
        }

        public ProductData? GetProductData(long productId)
        {
            try
            {
                var product = new ProductData();

                using (var tran = productEngine.GetTransaction())
                {
                    var row = tran.Select<byte[], byte[]>(_productTable, _primaryIndex.ToIndex(productId));
                    if (row.Exists)
                        product = row.ObjectGet<ProductData>().Entity;
                }

                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public long SetProductData(ProductData productData)
        {
            try
            {
                using (var tran = productEngine.GetTransaction())
                {
                    bool newEntity = productData.Id <= 0;
                    if (newEntity)
                        productData.Id = tran.ObjectGetNewIdentity<long>(_productTable);

                    var dbObject = new DBreezeObject<ProductData>
                    {
                        NewEntity = newEntity,
                        Entity = productData,
                        Indexes = new List<DBreezeIndex>
                        {
                            new DBreezeIndex(_primaryIndex, productData.Id) { PrimaryIndex = true }
                        }
                    };

                    tran.ObjectInsert(_productTable, dbObject);

                    tran.Commit();
                }

                return productData.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public bool DeleteProduct(long productId)
        {
            try
            {
                using (var tran = productEngine.GetTransaction())
                {
                    tran.ObjectRemove(_productTable, _primaryIndex.ToIndex(productId));

                    tran.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
