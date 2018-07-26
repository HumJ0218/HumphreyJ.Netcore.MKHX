using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HumphreyJ.NetCore.MKHX.Web.Models;
//using HumphreyJ.NetCore.MKHX.Web.Models.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HumphreyJ.NetCore.MKHX.Web
{
    public class ErrorController : Controller
    {
        [Route("error{statusCode}/{sender?}")]
        public IActionResult CustomError(string statusCode, string sender)
        {
            Exception exception = null;

            var code = Response.StatusCode;

            switch (sender)
            {
                case null:
                case "":
                    {
                        break;
                    }
                case "ExceptionHandler":
                    {
                        //if (MKHXContext.IsHealthy == false)
                        //{
                        //    exception = MKHXContext.Exception;
                        //}
                        break;
                    }
                case "StatusCodePagesWithReExecute":
                    {
                        break;
                    }
                default:
                    {
                        try
                        {
                            code = int.Parse(statusCode);
                        }
                        catch (Exception ex)
                        {
                            code = (int)HttpStatusCode.InternalServerError;
                            exception = ex;
                        }
                        break;
                    }
            }

            var status = (HttpStatusCode)code;

            ViewData["status"] = status;

            switch (status)
            {
                case HttpStatusCode.InternalServerError:
                    {
                        if (exception == null)
                        {
                            exception = HttpGlobalExceptionFilter.GetLastError();
                            if (exception?.GetType()==typeof(NeedVersionSelectedException))
                            {
                                Response.StatusCode = 200;
                                return View("Blank");
                            }
                        }
                        ViewData["exception"] = exception;
                        break;
                    }
            }

            ViewData["showDetails"] = Startup.Environment.IsDevelopment();

            return View("Error");
        }

        [Route("error/test500")]
        public IActionResult Test500()
        {
            throw new Exception(nameof(Test500));
        }

        [Route("error/test400")]
        public IActionResult Test400()
        {
            return new BadRequestResult();
        }
    }
}