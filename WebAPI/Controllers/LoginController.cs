﻿using System;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class LoginController : ApiController
    {
        //For user login  
        [Route("Api/Login/UserLogin")]
        [HttpPost]
        public Response Login(Login Lg)
        {
            WebAngularEntities1 DB = new WebAngularEntities1();
            var Obj = DB.Usp_Login(Lg.UserName, Lg.Password).ToList<Usp_Login_Result>().FirstOrDefault();
            if (Obj.Status == 0) return new Response
            {
                Status = "Invalid",
                Message = "Invalid User."
            };
            if (Obj.Status == -1) return new Response
            {
                Status = "Inactive",
                Message = "User Inactive."
            };
            else return new Response
            {
                Status = "Success",
                Message = Lg.UserName
            };
        }
        //For new user Registration  
        [Route("Api/Login/UserRegistration")]
        [HttpPost]
        public object createcontact(Registration Lvm)
        {
            try
            {
                WebAngularEntities1 db = new WebAngularEntities1();
                User Wb = new User();
                if (Wb.UserId == 0)
                {
                    Wb.UserName = Lvm.UserName;
                    Wb.LoginName = Lvm.LoginName;
                    Wb.Password = Lvm.Password;
                    Wb.Email = Lvm.Email;
                    Wb.ContactNo = Lvm.ContactNo;
                    Wb.Address = Lvm.Address;
                    Wb.IsApporved = Lvm.IsApporved;
                    Wb.Status = Lvm.Status;
                    db.Users.Add(Wb);
                    db.SaveChanges();
                    return new Response
                    {
                        Status = "Success",
                        Message = "SuccessFully Saved."
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return new Response
            {
                Status = "Error",
                Message = "Invalid Data."
            };
        }
    }
}