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
using WebApplication1.Custom_Model;

namespace WebApplication1.Controllers
{
    //[EnableCors(origins: "http://kimman.somee.com/api", headers: "*", methods: "*")]
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
            if (result == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent)); //NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
        [System.Web.Http.ActionName("GetUserByUser")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult GetUserByObject(Login user)
        {
            Login result = db.Logins.Select(s => s).Where(s => s.Username == user.Username && s.Password == user.Password).FirstOrDefault();
            if (result != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        public IHttpActionResult CreateUser(Login user)
        {
            Login userfromDB = db.Logins.Select(s => s).Where(s => s.Username == user.Username).FirstOrDefault();
            if (userfromDB == null)
            {
                db.Logins.Add(user);
                db.SaveChanges();
                return Ok(user.ID);
            }
            else
            {
                JsonReturnModel returnData = new JsonReturnModel();
                returnData.message = "User has already exist";
                returnData.statusCode = "404";
                return   Json(returnData);
            }

        }
        public IHttpActionResult DeleteUser(int id)
        {
            if (id != 0)
            {
                Login user = db.Logins.Find(id);
                db.Logins.Remove(user);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        public IHttpActionResult PutUser(Login user)
        {
            if (user.ID != 0)
            {
                Login userDB = db.Logins.Find(user.ID);
                if (user != null)
                {
                    userDB.Username = user.Username;
                    userDB.Password = user.Password;
                    db.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}