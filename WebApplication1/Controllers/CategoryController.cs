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
using WebApplication1.Ultis;

namespace WebApplication1.Controllers
{
    public class CategoryController : ApiController
    {
        QLThuChiEntities db = new QLThuChiEntities();
        ResponseMessge resp = new ResponseMessge();
        public IEnumerable<Category> GetCategory()
        {
            return db.Categories.ToList();
        }
        public IHttpActionResult GetCategoryByID(int id)
        {
            
            Category cat = new Category();
            cat = db.Categories.Select(s => s).Where(s => s.CategoryID == id).FirstOrDefault();
            return Ok(cat);
        }
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CreateCategory(Category cat)
        {
            ResponseMessge resp = new ResponseMessge();
            JsonReturnModel jsonReturn = new JsonReturnModel();
            Category catDB = db.Categories.Select(s => s).Where(s => s.CategoryName == cat.CategoryName).FirstOrDefault();
            if (catDB == null)
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                jsonReturn.statusCode = "200";
                jsonReturn.message = "OK";
                return resp.responseMessOK(jsonReturn, Request);
            }
            else
            {
                jsonReturn.message = "This Category has already exits";
                jsonReturn.statusCode = "404";
                //listResult.Add(jsonReturn);
                return resp.responseMess(jsonReturn,Request);
            }
        }
        public HttpResponseMessage putCategory(Category cat)
        {
            Category catDB = db.Categories.Find(cat.CategoryID);
            if (catDB != null)
            {
                catDB.CategoryName = cat.CategoryName;
                catDB.Description = cat.Description;
                int result =  db.SaveChanges();
                if (result == 1 || cat.CategoryName == catDB.CategoryName || cat.Description == catDB.Description)
                {
                    JsonReturnModel jsonResult = new JsonReturnModel();
                    jsonResult.message = "OK";
                    jsonResult.statusCode = "200";
                    return resp.responseMessOK(jsonResult, Request);
                }
                else
                {
                    JsonReturnModel jsonResult = new JsonReturnModel();
                    jsonResult.message = "Error DB Server Site. Contact Admin";
                    jsonResult.statusCode = "404";
                    return resp.responseMess(jsonResult, Request);
                }
            }
            else
            {
                JsonReturnModel jsonResult = new JsonReturnModel();
                jsonResult.message = "Not found this Category. Please contact Administrator";
                jsonResult.statusCode = "404";
                 return resp.responseMess(jsonResult, Request);
            }
        }
        public HttpResponseMessage DeleteCategory(int id)
        {
            Category cat = db.Categories.Select(s=>s).Where(s=>s.CategoryID == id).FirstOrDefault();
            db.Categories.Remove(cat);
            int result = db.SaveChanges();
            if (result == 1)
            {
                JsonReturnModel jsonResult = new JsonReturnModel();
                jsonResult.statusCode = "200";
                jsonResult.message = "OK";
                return resp.responseMessOK(jsonResult,Request);
            }
            else
            {
                JsonReturnModel jsonResult = new JsonReturnModel();
                jsonResult.message = "Not found this Category. Please contact Administrator";
                jsonResult.statusCode = "404";
                return resp.responseMess(jsonResult, Request);
            }
        }
     
    }
}