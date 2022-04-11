using System;
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
    public class OrderDetailController : ApiController
    {
        QLThuChiEntities db = new QLThuChiEntities();
        ResponseMessge resp = new ResponseMessge();
        // GET: Order
        public HttpResponseMessage GetOrderDetail()
        {
            return resp.responseMessObject(db.Orders.ToList(), Request);

        }
        public HttpResponseMessage GetOrderDetailByID(int id)
        {
            OrderDetail od = db.OrderDetails.Where(s => s.OrderDetailID == id).Select(s => s).FirstOrDefault();
            if (od != null)
            {
                return resp.responseMessObject(od, Request);
            }
            else
            {
                JsonReturnModel jsonResult = new JsonReturnModel();
                jsonResult.message = "Did not found any record matching ID";
                jsonResult.statusCode = "404";
                return resp.responseMessNotFound(jsonResult, Request);
            }

        }
        public HttpResponseMessage CreateOrderDetail(OrderDetail od)
        {
            JsonReturnModel jsonReturn = new JsonReturnModel();
            db.OrderDetails.Add(od);
            int result = db.SaveChanges();
            if (result == 1)
            {
                jsonReturn.message = "OK";
                jsonReturn.statusCode = "200";
                return resp.responseMessOK(jsonReturn, Request);
            }
            else
            {
                jsonReturn.message = "Error at server side";
                jsonReturn.statusCode = "404";
                return resp.responseMessNotFound(jsonReturn, Request);
            }
        }

    }
}