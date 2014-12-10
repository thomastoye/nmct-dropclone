using nmct.ssa.dropbox.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using nmct.ssa.dropbox.common;

namespace nmct.ssa.dropbox.DataAccess
{
    public class DAFileLogging
    {
        private const string CONNECTIONSTRING = "DefaultConnection";

        public static int SaveLog(int fileId, string downloadBy)
        {
            string sql = "INSERT INTO FileDownload VALUES(@DownloadTime, @DownloadBy, @FileId)";

            DbParameter parTime = Database.AddParameter(CONNECTIONSTRING, "@DownloadTime", DateTime.Now);
            DbParameter parBy = Database.AddParameter(CONNECTIONSTRING, "@DownloadBy", downloadBy);
            DbParameter parId = Database.AddParameter(CONNECTIONSTRING, "@FileId", fileId);

            return Database.InsertData(CONNECTIONSTRING, sql, parTime, parBy, parId);
        }

        public static List<FileLog> GetLogs()
        {
            string sql = "SELECT * FROM FileDownload";
            List<FileLog> list = new List<FileLog>();

            DbDataReader reader = Database.GetData(CONNECTIONSTRING, sql);

            while (reader.Read())
                list.Add(Create(reader));

            return list;

        }

        private static FileLog Create(IDataRecord rec)
        {
            return new FileLog
            {
                Id = Int32.Parse(rec["Id"].ToString()),
                FileId = Int32.Parse(rec["FileId"].ToString()),
                DownloadBy = rec["DownloadBy"].ToString(),
                TimeDownloaded = DateTime.Parse(rec["DownloadTime"].ToString())
            };
        }
    }
}