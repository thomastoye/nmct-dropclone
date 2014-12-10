using nmct.ssa.dropbox.common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ssa.dropbox.DataAccess
{
    public class DAExternalUser
    {
        private const string CONNECTIONSTRING = "DefaultConnection";

        private static ExternalUser Create(IDataRecord rec)
        {
            return new ExternalUser{
                Id = Int32.Parse(rec["Id"].ToString()),
                Blocked = Boolean.Parse(rec["blocked"].ToString()),
                UserName = rec["UserName"].ToString(),
                Password = rec["Password"].ToString()
            };
        }

        public static List<ExternalUser> GetExternalUsers()
        {
            List<ExternalUser> list = new List<ExternalUser>();

            string sql = "SELECT * FROM ExternalUser";

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
                list.Add(Create(reader));

            return list;
        }

        public static ExternalUser GetById(int id)
        {
            string sql = "SELECT * FROM ExternalUser WHERE Id=@Id";

            DbParameter parId = Database.AddParameter(CONNECTIONSTRING, "@Id", id);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, parId);

            ExternalUser res;
            if (!reader.Read())
                res = null;
            else
                res = Create(reader);

            reader.Close();

            return res;


        }

        public static void Update(ExternalUser ext)
        {
            string sql = "UPDATE ExternalUser SET UserName=@UserName,Password=@Password,Blocked=@Blocked WHERE Id=@Id";

            DbParameter parId = Database.AddParameter(CONNECTIONSTRING, "@Id", ext.Id);
            DbParameter parUser = Database.AddParameter(CONNECTIONSTRING, "@UserName", ext.UserName);
            DbParameter parPass = Database.AddParameter(CONNECTIONSTRING, "@Password", ext.Password);
            DbParameter parBlocked = Database.AddParameter(CONNECTIONSTRING, "@Blocked", ext.Blocked);

            Database.ModifyData(CONNECTIONSTRING, sql, parId, parUser, parPass, parBlocked);
        }

        public static int New(ExternalUser ext)
        {
            string sql = "INSERT INTO ExternalUser VALUES(@UserName,@Password,@Blocked)";

            DbParameter parUser = Database.AddParameter(CONNECTIONSTRING, "@UserName", ext.UserName);
            DbParameter parPass = Database.AddParameter(CONNECTIONSTRING, "@Password", ext.Password);
            DbParameter parBlocked = Database.AddParameter(CONNECTIONSTRING, "@Blocked", ext.Blocked);

            return Database.InsertData(CONNECTIONSTRING, sql, parUser, parPass, parBlocked);
        }

        public static ExternalUser TryLogin(string userName, string password)
        {
            string sql = "SELECT * FROM ExternalUser WHERE UserName=@UserName AND Password=@Password";

            DbParameter parUser = Database.AddParameter(CONNECTIONSTRING, "@UserName", userName);
            DbParameter parPass = Database.AddParameter(CONNECTIONSTRING, "@Password", password);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, parUser, parPass);

            ExternalUser res;

            if (!reader.Read())
                res = null;
            else
                res = Create(reader);

            reader.Close();
            return res;
        }
    }
}