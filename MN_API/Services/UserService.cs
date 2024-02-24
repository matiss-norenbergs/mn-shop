using DBreeze;
using DBreeze.Objects;
using DBreeze.Utils;
using MN_API.Models;

namespace MN_API.Services
{
    public class UserService
    {
        private static string userTable = "Users";

        private static byte _primaryIndex = 1;

        readonly DBreezeEngine? userEngine = null;

        public UserService()
        {
            CustomSerializator.ByteArraySerializator = Helper.SerializeProtobuf;
            CustomSerializator.ByteArrayDeSerializator = Helper.DeserializeProtobuf;

            userEngine ??= new DBreezeEngine($"{Environment.CurrentDirectory}/Databases/Users");
        }

        public List<UserData>? GetUserList()
        {
            try
            {
                var userList = new List<UserData>();

                using (var tran = userEngine.GetTransaction())
                {
                    foreach (var row in tran.SelectForward<byte[], byte[]>(userTable))
                    {
                        var obj = row.ObjectGet<UserData>()?.Entity;
                        if (obj != null)
                            userList.Add(obj);
                    }
                }

                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public UserData GetUserData(long userId)
        {
            try
            {
                var user = new UserData();

                using (var tran = userEngine.GetTransaction())
                {
                    var row = tran.Select<byte[], byte[]>(userTable, _primaryIndex.ToIndex(userId));
                    if (row.Exists)
                        user = row.ObjectGet<UserData>().Entity;
                }

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public long SetUserData(UserData userData)
        {
            try
            {
                using (var tran = userEngine.GetTransaction())
                {
                    bool newEntity = userData.Id == 0;
                    if (newEntity)
                        userData.Id = tran.ObjectGetNewIdentity<long>(userTable);

                    var dbObject = new DBreezeObject<UserData>
                    {
                        NewEntity = newEntity,
                        Entity = userData,
                        Indexes = new List<DBreezeIndex>
                        {
                            new DBreezeIndex(_primaryIndex, userData.Id) { PrimaryIndex = true }
                        }
                    };

                    tran.ObjectInsert(userTable, dbObject);

                    tran.Commit();
                }

                return userData.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public bool DeleteUser(long userId)
        {
            try
            {
                using (var tran = userEngine.GetTransaction())
                {
                    tran.ObjectRemove(userTable, _primaryIndex.ToIndex(userId));

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
