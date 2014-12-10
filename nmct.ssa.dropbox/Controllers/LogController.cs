using System.Collections.Generic;
using nmct.ssa.dropbox.DataAccess;
using nmct.ssa.dropbox.Models;
using System.Web.Http;
using nmct.ssa.dropbox.common;

namespace nmct.ssa.dropbox.Controllers
{
    
    public class LogController : ApiController
    {
        public IEnumerable<FileLog> Get()
        {
            return DAFileLogging.GetLogs();
        }
    }
}