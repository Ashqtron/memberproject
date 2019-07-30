using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MemberWebApiProject.Models;

namespace MemberWebApiProject.Controllers
{
    public class ServicesController : ApiController
    {
        //Create instance of Linq-To-Sql class as db  
        DataClasses1DataContext db = new DataClasses1DataContext();



        //This action method return all members records.  
        // GET api/<controller>  
        public IEnumerable<Service> Get()
        {
            //returning all records of table tblMember.  
            return db.Services.ToList().AsEnumerable();
        }



        //This action method will fetch and filter for specific member id record  
        // GET api/<controller>/5  
        public HttpResponseMessage Get(int id)
        {
            //fetching and filter specific member id record   
            var memberdetail = (from a in db.Services where a.ServiceId == id select a).FirstOrDefault();


            //checking fetched or not with the help of NULL or NOT.  
            if (memberdetail != null)
            {
                //sending response as status code OK with memberdetail entity.  
                return Request.CreateResponse(HttpStatusCode.OK, memberdetail);
            }
            else
            {
                //sending response as error status code NOT FOUND with meaningful message.  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or Member Not Found");
            }
        }
    }
}