namespace MN_Shop.Server
{
    public static class Helper
    {
        public static T DeserializeProtobuf<T>(this byte[] data)
        {
            T ret = default(T);
            using (var ms = new MemoryStream(data))
            {
                ret = ProtoBuf.Serializer.Deserialize<T>(ms);
                ms.Close();
            }
            return ret;
        }

        public static object DeserializeProtobuf(byte[] data, Type T)
        {
            object ret = null;
            using (var ms = new MemoryStream(data))
            {
                ret = ProtoBuf.Serializer.NonGeneric.Deserialize(T, ms);
                ms.Close();
            }
            return ret;
        }

        public static byte[] SerializeProtobuf(this object data)
        {
            byte[] bt = null;
            using (var ms = new MemoryStream())
            {
                ProtoBuf.Serializer.NonGeneric.Serialize(ms, data);
                bt = ms.ToArray();
                ms.Close();
            }
            return bt;
        }
    }
}
