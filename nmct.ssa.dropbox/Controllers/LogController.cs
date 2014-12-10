using System.Collections.Generic;
using nmct.ssa.dropbox.DataAccess;
using nmct.ssa.dropbox.Models;
using System.Web.Http;

namespace nmct.ssa.dropbox.Controllers
{
    [Authorize]
    public class LogController : ApiController
    {
        public IEnumerable<FileLog> Get()
        {
            return DAFileLogging.GetLogs();
        }
    }
}