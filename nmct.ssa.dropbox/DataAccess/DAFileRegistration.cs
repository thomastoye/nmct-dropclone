using nmct.ssa.dropbox.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace nmct.ssa.dropbox.DataAccess
{
    public class DAFileRegistration
    {
        private const string CONNECTIONSTRING = "DefaultConnection";

        public static int SaveFileRegistration(string fileName, string description, string userName)
        {
            string sql = "INSERT INTO FileRegistration VALUES(@Description, @FileName, @UploadTime, @UserName)";
            DbParameter parDesc = Database.AddParameter(CONNECTIONSTRING, "@Description", description);
            DbParameter parFileName = Database.AddParameter(CONNECTIONSTRING, "@FileName", fileName);
            DbParameter parTime = Database.AddParameter(CONNECTIONSTRING, "@UploadTime", DateTime.Now);
            DbParameter parName = Database.AddParameter(CONNECTIONSTRING, "@UserName", userName);

            return Database.InsertData(CONNECTIONSTRING, sql, parDesc, parFileName, parTime, parName);
        }

        public static int SaveDownloaders(string user, int fileId)
        {
            string sql = "INSERT INTO FileUser VALUES(@FileId, @UserName)";
            DbParameter parId = Database.AddParameter(CONNECTIONSTRING, "@FileId", fileId);
            DbParameter parUser = Database.AddParameter(CONNECTIONSTRING, "@UserName", user);

            return Database.InsertData(CONNECTIONSTRING, sql, parId, parUser);
        }

        public static List<FileRegistration> BestandenVanUser(string userName)
        {
            string sql = "SELECT FileId, Description, FileName, UploadTime, UserName FROM FileRegistration WHERE UserName=@UserName";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@UserName", userName);

            List<FileRegistration> list = new List<FileRegistration>();
            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par);
            while (reader.Read())
            {
                list.Add(CreateFileRegistration(reader));
            }
            reader.Close();
            return list;
        }

        private static FileRegistration CreateFileRegistration(IDataRecord record) {
            return new FileRegistration
            {
                Description = record["Description"].ToString(),
                FileId = Int32.Parse(record["FileId"].ToString()),
                FileName = record["FileName"].ToString(),
                UploadTime = DateTime.Parse(record["UploadTime"].ToString()),
                UserName = record["UserName"].ToString()
            };
        }

        public static List<FileRegistration> BestandenMetToegangVoor(string userName)
        {
            string sql = "SELECT FileRegistration.FileId, FileRegistration.UserName, FileRegistration.Description, FileRegistration.FileName, FileRegistration.UploadTime  FROM FileRegistration, FileUser WHERE FileRegistration.FileId = FileUser.FileId AND FileUser.UserName = @UserName";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@UserName", userName);

            List<FileRegistration> list = new List<FileRegistration>();

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par);
            while (reader.Read())
                list.Add(CreateFileRegistration(reader));

            reader.Close();
            return list;

        }

        public static FileRegistration GetFileRegistrationById(int id) {
            FileRegistration file;

            string sql = "SELECT * FROM FileRegistration WHERE FileId=@Id";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@Id", id);

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql, par);
            if(reader.Read())
                file = CreateFileRegistration(reader);
            else
                file = null;

            reader.Close();
            return file;
        }

        public static void DeleteFile(int id)
        {
            string sql = "DELETE FROM FileRegistration WHERE FileId=@Id; DELETE FROM FileUser WHERE FileId=@Id";
            DbParameter par = Database.AddParameter(CONNECTIONSTRING, "@Id", id);

            Database.ModifyData(CONNECTIONSTRING, sql, par);
        }
    }
}