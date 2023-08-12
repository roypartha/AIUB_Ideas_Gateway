﻿using AIUB_Ideas_Gateway.AuthFilters;
using AIUB_Ideas_Gateway.Models;
using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AIUB_Ideas_Gateway.Controllers
{
    [LoggedIn]

    public class PostController : ApiController
    {
        // Only login user can access individual post
        [HttpPost]
        [Route("api/post/{id}")]
        public HttpResponseMessage Post(int id)
        {
            try
            {
                var data = PostServices.Post(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        // Only login user can create a post
        [HttpPost]
        [Route("api/post/create")]
        public HttpResponseMessage Create(PostDTO obj)
        {
            if (obj.Title != null && obj.Content != null)
            {
                try
                {
                    var token = Request.Headers.Authorization.ToString();
                    var userId = AuthServices.GetUserID(token);
                    obj.CreatedAt = DateTime.Now;
                    obj.UpdatedAt = null;
                    obj.UserID = userId;

                    var data = PostServices.CreatePost(obj);
                    if (data == true)
                        return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Post Created!" });
                    else
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Something went wrong in Creation of post" });
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Msg = "Invalid Post object" });
            }
        }

        // update post
        [HttpPost]
        [Route("api/post/update")]

        public HttpResponseMessage Update(PostDTO obj)
        {
            try
            {
                // getting the current user token
                var token = Request.Headers.Authorization.ToString();
                var userId = AuthServices.GetUserID(token);
                if (obj.UserID == userId)
                {
                    var res = PostServices.UpdatePost(obj);
                    if (res == true)
                        return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Post updated" });
                    else
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Something wen wrong in post update" });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, new { Msg = "You don't have permission to update this post!" });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }

        }

        // delete post
        [HttpPost]
        [Route("api/post/delete")]
        public HttpResponseMessage Delete(PostDTO obj)
        {
            try
            {
                var token = Request.Headers.Authorization.ToString();
                var userId = AuthServices.GetUserID(token);
                if (obj.UserID == userId)
                {
                    var res = PostServices.DeletePost(obj.PostID);
                    if (res == true) return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Post Delete" });
                    else return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Something went wrong in post delete" });

                }
                else return Request.CreateResponse(HttpStatusCode.Forbidden, new { Msg = "You don't have the permission to delete this post!" });

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
    }
}
