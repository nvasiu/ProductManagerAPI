using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductManagerAPI.Models;

namespace ProductManagerAPI.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        ProductInvEntities db = new ProductInvEntities();
        Response response = new Response();

        [HttpGet, Route("get")]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, db.Users.ToList());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost,Route("add")]
        public HttpResponseMessage Add([FromBody] User user)
        {
            try 
            {
                db.Users.Add(user);
                db.SaveChanges();
                response.Message = "User Added Successfully.";
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost, Route("update")]
        public HttpResponseMessage Update([FromBody] User user)
        {
            try
            {
                User userUpd = db.Users.Find(user.ID);
                if (userUpd == null) 
                {
                    response.Message = "User ID does not exist.";
                    return Request.CreateResponse(HttpStatusCode.OK,response);
                }

                userUpd.Username = user.Username;
                userUpd.Name = user.Name;
                userUpd.Password = user.Password;
                userUpd.Phone = user.Phone;

                db.Entry(userUpd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                response.Message = "User Updated Successfully.";
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost, Route("delete/{DelId}")]
        public HttpResponseMessage Delete(int DelId)
        {
            try
            {
                User userDel = db.Users.Find(DelId);
                if (userDel == null)
                {
                    response.Message = "User ID does not exist.";
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                db.Users.Remove(userDel);
                db.SaveChanges();
                response.Message = "User Deleted Successfully.";
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
