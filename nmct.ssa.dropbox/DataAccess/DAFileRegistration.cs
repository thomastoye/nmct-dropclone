using System;
using System.Collections.Generic;
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
    }
}