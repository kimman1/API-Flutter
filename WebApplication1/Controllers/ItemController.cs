using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApplication1.Custom_Model;
using WebApplication1.Ultis;

namespace WebApplication1.Controllers
{
    public class ItemController : ApiController
    {
        QLThuChiEntities db = new QLThuChiEntities();
        ResponseMessge resp = new ResponseMessge();
        // GET: Item
        public IEnumerable GetItem()
        {
            return db.Items.ToList();
        }
        public HttpResponseMessage GetItemByID(int id)
        {
           
            Item item = db.Items.Select(s => s).Where(s => s.ItemID == id).FirstOrDefault();
            if (item != null)
            {
                return resp.responseMessItem(item, Request);
            }
            else
            {
                JsonReturnModel jsonResult = new JsonReturnModel();
                jsonResult.message = "Not found this Item";
                jsonResult.statusCode = "404";
                return resp.responseMess(jsonResult, Request);
            }
            
        }
        public HttpResponseMessage GetItemByCategoryID(int CategoryID)
        {

            List<Item> listItem = db.Items.Select(s => s).Where(s => s.CategoryID == CategoryID).ToList();
            if (listItem.Count != 0)
            {
                return resp.responseMessItem(listItem, Request);
            }
            else
            {
                JsonReturnModel jsonResult = new JsonReturnModel();
                jsonResult.message = "Not found this Item";
                return resp.responseMess(jsonResult, Request);
            }

        }
        public HttpResponseMessage PutItem(Item item)
        {
            Item itemFromDB = db.Items.Select(s => s).Where(s => s.ItemID == item.ItemID).FirstOrDefault();
            if (itemFromDB != null)
            {
                itemFromDB.CategoryID = item.CategoryID;
                itemFromDB.ItemName = item.ItemName;
                itemFromDB.UnitPrice = item.UnitPrice;
                db.SaveChanges();
                return resp.responseOK(Request);
            }
            else
            {
                JsonReturnModel jsonResult = new JsonReturnModel();
                jsonResult.message = "Did not find any matching record";
                jsonResult.statusCode = "404";
                return resp.responseMess(jsonResult, Request);
            }
        }
        public HttpResponseMessage CreateItem(Item item)
        {
            JsonReturnModel jsonResult = new JsonReturnModel();
            db.Items.Add(item);
           int result = db.SaveChanges();
            if (result == 1)
            {
                jsonResult.statusCode = "200";
                jsonResult.message = "OK";
                return resp.responseMessOK(jsonResult, Request);
            }
            else
            {
                
                jsonResult.message = "Error DB Server Site. Contact Admin";
                jsonResult.statusCode = "404";
                return resp.responseMess(jsonResult, Request);
            }
        }
    }
}