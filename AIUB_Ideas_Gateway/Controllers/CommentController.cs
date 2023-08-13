﻿using AIUB_Ideas_Gateway.AuthFilters;
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
    public class CommentController : ApiController
    {
        [LoggedIn]
        [HttpGet]
        [Route("api/comment/all")]
        public HttpResponseMessage All() // all comment(post and job)
        {
            try
            {
                var token = Request.Headers.Authorization.ToString();
                var commments = CommentServices.AllComments();
                return Request.CreateResponse(HttpStatusCode.OK, commments);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
            }

        }


        [LoggedIn]
        [HttpPost]
        [Route("api/comment/create")]  // Create Comment
        public HttpResponseMessage Create(CommentDTO obj)
        {
            if (obj.Text != null)
            {
                try
                {
                    var token = Request.Headers.Authorization.ToString();
                    var userId = AuthServices.GetUserID(token);
                    obj.CreatedAt = DateTime.Now;
                    obj.UserID = userId;

                    var data = CommentServices.CreateComment(obj);
                    if (data == true)
                        return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Comment Created!" });
                    else
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Something went wrong in Creation of Comment" });
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Msg = "Invalid Comment object" });
            }
        }
        [LoggedIn]
        [HttpPost]
        [Route("api/comment/{id}")]  // Find comment by comment id
        public HttpResponseMessage CommentById(int id)
        {
            try
            {
                var token = Request.Headers.Authorization.ToString();
                var data = CommentServices.CommentById(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }

        }

        [LoggedIn]
        [HttpPost]
        [Route("api/comment/delete/{id}")] // Delete comment by comment id
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var token = Request.Headers.Authorization.ToString();
                var data = CommentServices.DeleteComment(id);
                if (data == true)
                    return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Delete Successfully!" });
                else
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Something went wrong in Delete of Comment" });

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }

        }

        [LoggedIn]
        [HttpPost]
        [Route("api/comment/post/{id}")]   // Find all comments for a post by post id
        public HttpResponseMessage PostId(int id)
        {
            try
            {
                var data = CommentServices.PostId(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }

        }
        [LoggedIn]
        [HttpPost]
        [Route("api/comment/job/{id}")] // Find all comments for a Job by job id
        public HttpResponseMessage JobId(int id)
        {
            try
            {

                var data = CommentServices.JobId(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }

        }
        [LoggedIn]
        [HttpPost]
        [Route("api/comment/user")]  // Find all comments create by session User 
        public HttpResponseMessage User()
        {
            try
            {
                var token = Request.Headers.Authorization.ToString();
                var userId = AuthServices.GetUserID(token);
                var data = CommentServices.UserId(userId);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }

        }

        [LoggedIn]
        [HttpPost]
        [Route("api/comment/user/{id}")]  // Find all comments create by User (User id)
        public HttpResponseMessage UserId(int id)
        {
            try
            {

                var data = CommentServices.UserId(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }

        }

        [LoggedIn]
        [HttpGet]
        [Route("api/comment/userpost/{postId}")]
        public HttpResponseMessage GetUserPostComments(int postId)
        {
            try
            {
                var token = Request.Headers.Authorization.ToString();
                var userId = AuthServices.GetUserID(token);

                CommentServices commentServices = new CommentServices(); // Create an instance
                var comments = commentServices.GetUserPostComments(userId, postId);

                return Request.CreateResponse(HttpStatusCode.OK, comments);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [LoggedIn]
        [HttpGet]
        [Route("api/comment/userjob/{jobId}")]
        public HttpResponseMessage GetUserJobComments(int jobId)
        {
            try
            {
                var token = Request.Headers.Authorization.ToString();
                var userId = AuthServices.GetUserID(token);

                CommentServices commentServices = new CommentServices();
                var comments = commentServices.GetUserJobComments(userId, jobId);

                return Request.CreateResponse(HttpStatusCode.OK, comments);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }





        [LoggedIn]
        [HttpPost]
        [Route("api/comment/postcount/{id}")]  // Count numbers of comment for a post by PostID
        public HttpResponseMessage CountByPost(int id)
        {
            try
            {
                int count = CommentServices.CountByPost(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { Count = count });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }

        [LoggedIn]
        [HttpPost]
        [Route("api/comment/jobcount/{id}")] // Count numbers of comment for a Jod post by JobID
        public HttpResponseMessage CountByJob(int id)
        {
            try
            {

                int count = CommentServices.CountByJob(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { Count = count });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }
        [LoggedIn]
        [HttpPost]
        [Route("api/comment/update/{id}")] // Update only the comment Text
        public HttpResponseMessage UpdateComment(int id, CommentDTO updatedComment)
        {
            try
            {
                var token = Request.Headers.Authorization.ToString();
                var userId = AuthServices.GetUserID(token);

                var existingComment = CommentServices.CommentById(id);

                if (existingComment == null || existingComment.UserID != userId)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { Msg = "Comment not found or you don't have permission to update." });
                }

                existingComment.Text = updatedComment.Text;


                bool success = CommentServices.UpdateComment(existingComment);

                if (success)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { Msg = "Comment updated successfully." });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Msg = "Failed to update comment." });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message.ToString());
            }
        }



    }
}