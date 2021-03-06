using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication1.Custom_Model;

namespace WebApplication1.Ultis
{
    public class ResponseMessge : ApiController
    {
        public HttpResponseMessage responseMessNotFound(JsonReturnModel json, HttpRequestMessage request)
        {
            var resp = request.CreateResponse<JsonReturnModel>(HttpStatusCode.NotFound, json);
            return resp;
        }
        public HttpResponseMessage responseMessOK(JsonReturnModel json, HttpRequestMessage request)
        {
            var resp = request.CreateResponse<JsonReturnModel>(HttpStatusCode.OK, json);
            return resp;
        }
        public HttpResponseMessage responseMessCategory(Category cat, HttpRequestMessage request)
        {
            var resp = request.CreateResponse<Category>(HttpStatusCode.OK, cat);
            return resp;
        }
        public HttpResponseMessage responseMessItem(Item item, HttpRequestMessage request)
        {
            var resp = request.CreateResponse<Item>(HttpStatusCode.OK, item);
            return resp;
        }
        public HttpResponseMessage responseMessItem(List<Item> listItem, HttpRequestMessage request)
        {
            var resp = request.CreateResponse<List<Item>>(HttpStatusCode.OK, listItem);
            return resp;
        }
        public HttpResponseMessage responseMessObject(object listItem, HttpRequestMessage request)
        {
            var resp = request.CreateResponse<object>(HttpStatusCode.OK, listItem);
            return resp;
        }
        public HttpResponseMessage responseOK(HttpRequestMessage request)
        {
            var resp = request.CreateResponse(HttpStatusCode.OK);
            return resp;
        }
    }
}