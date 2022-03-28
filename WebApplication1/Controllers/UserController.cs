using GoogleApi.Entities.Places.Search.Text.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    
    public class UserController : ApiController
    {
       
        QLThuChiEntities db = new QLThuChiEntities();
        public IEnumerable<Login> GetUser()
        {
            List<Login> listResult = db.Logins.ToList();
            if (listResult.Count == 0)
            {
                return null;
            }
            else
            {
                return db.Logins.ToList();
            }
            
        }
        public IHttpActionResult GetUser(int id)
        {
            Login result = new Login();
            result = db.Logins.Select(s => s).Where(s => s.ID == id).FirstOrDefault();
            if (result != null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent)); //NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}