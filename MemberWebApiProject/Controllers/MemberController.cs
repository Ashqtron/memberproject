using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MemberWebApiProject.Models;

namespace MemberWebApiProject.Controllers
{
    public class MemberController : ApiController
    {

        //Create instance of Linq-To-Sql class as db  
        DataClasses1DataContext db = new DataClasses1DataContext();



        //This action method return all members records.  
        // GET api/<controller>  
        public IEnumerable<tblMember> Get()
        {
            //returning all records of table tblMember.  
            return db.tblMembers.ToList().AsEnumerable();
        }



        //This action method will fetch and filter for specific member id record  
        // GET api/<controller>/5  
        public HttpResponseMessage Get(int id)
        {
            //fetching and filter specific member id record   
            var memberdetail = (from a in db.tblMembers where a.MemberID == id select a).FirstOrDefault();


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



        //Get action methods of the previous section
        public IHttpActionResult PostNewStudent(MemberViewModel member)
        {
            try
            {
                var newuser = new tblMember
                {
                    MemberID = member.MemberID,
                    MemberName = member.MemberName,
                    PhoneNumber = member.PhoneNumber
                };
                db.tblMembers.InsertOnSubmit(newuser);

                //Save the submitted record  
                db.SubmitChanges();

                //return response status as successfully created with member entity  
                var msg = Request.CreateResponse(HttpStatusCode.Created, newuser);

                //Response message with requesturi for check purpose  
                msg.Headers.Location = new Uri(Request.RequestUri + newuser.MemberID.ToString());

                /// return msg;
                 return Ok();
            }
            catch (Exception ex)
            {
                return Ok();
                //return response as bad request  with exception message.  
                // return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        /*//Get action methods of the previous section
        public IHttpActionResult PostNewMember(MemberViewModel member)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            dynamic records = from rec in db.tblMembers
                              select rec;


            var newuser = new tblMember
            {
                MemberID = member.MemberID,
                MemberName = member.MemberName,
                PhoneNumber = member.PhoneNumber
            };

            db.tblMembers.InsertOnSubmit(newuser);
            try
            {
                db.SubmitChanges();
               
            }
            catch (Exception ex)
            {
                ex.GetBaseException();
                string err = "";
                err = "<p> Couldn't register user. </p>";
               
            }

            return Ok();
        }*/


        //To add a new member record  
        // POST api/<controller>  
        public HttpResponseMessage Post([FromBody]tblMember _member)
        {
            try
            {
                //To add an new member record  
                db.tblMembers.InsertOnSubmit(_member);

                //Save the submitted record  
                db.SubmitChanges();

                //return response status as successfully created with member entity  
                var msg = Request.CreateResponse(HttpStatusCode.Created, _member);

                //Response message with requesturi for check purpose  
                msg.Headers.Location = new Uri(Request.RequestUri + _member.MemberID.ToString());

                return msg;
            }
            catch (Exception ex)
            {

                //return response as bad request  with exception message.  
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



        //To update member record  
        // PUT api/<controller>/5  
        public HttpResponseMessage Put(int id, [FromBody]tblMember _member)
        {
            //fetching and filter specific member id record   
            var memberdetail = (from a in db.tblMembers where a.MemberID == id select a).FirstOrDefault();

            //checking fetched or not with the help of NULL or NOT.  
            if (memberdetail != null)
            {
                //set received _member object properties with memberdetail  
                memberdetail.MemberName = _member.MemberName;
                memberdetail.PhoneNumber = _member.PhoneNumber;
                //save set allocation.  
                db.SubmitChanges();

                //return response status as successfully updated with member entity  
                return Request.CreateResponse(HttpStatusCode.OK, memberdetail);
            }
            else
            {
                //return response error as NOT FOUND  with message.  
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid Code or Member Not Found");
            }


        }

        // DELETE api/<controller>/5  
        public HttpResponseMessage Delete(int id)
        {

            try
            {
                //fetching and filter specific member id record   
                var _DeleteMember = (from a in db.tblMembers where a.MemberID == id select a).FirstOrDefault();

                //checking fetched or not with the help of NULL or NOT.  
                if (_DeleteMember != null)
                {

                    db.tblMembers.DeleteOnSubmit(_DeleteMember);
                    db.SubmitChanges();

                    //return response status as successfully deleted with member id  
                    return Request.CreateResponse(HttpStatusCode.OK, id);
                }
                else
                {
                    //return response error as Not Found  with exception message.  
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member Not Found or Invalid " + id.ToString());
                }
            }

            catch (Exception ex)
            {

                //return response error as bad request  with exception message.  
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}