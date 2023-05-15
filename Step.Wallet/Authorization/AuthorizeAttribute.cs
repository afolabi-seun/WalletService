using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Step.Service.Model.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net.NetworkInformation;
using Newtonsoft.Json.Linq;
using Step.Utils.Cryptography;

namespace Step.Service.Authorization;

[AttributeUsage(AttributeTargets.Class)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private static IConfiguration Config()
    {
        IConfiguration _appConfig;

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

        _appConfig = builder.Build();
        return _appConfig;
    }
    private static AuthenticateResponse ValidToken(string authToken, string authUiKey, string profileId)
    {
        var response = new AuthenticateResponse();

        var _config = Config();
        var uikey = _config.GetSection("AppSettings:ApiSettings:UiKey").Value!;

        //compare uikey keys...
        if (authUiKey != uikey)
        {
            response.ProfileId = profileId;
            response.Status = "Error";
            response.StatusText = "Unauthorised access, invalid authUiKey.";
        }
        else
        {
            var data = JsonConvert.SerializeObject(new { userId = profileId, token = authToken });
            var request = new StringContent(data, Encoding.UTF8, "application/json");

            var client = new HttpClient();
            var uri = _config.GetSection("AppSettings:Integration:AuthService:Uri").Value;
            var resp = client.PostAsync(uri, request).Result;
            if (resp != null)
            {
                var json = resp.Content.ReadAsStringAsync().Result;
                if (json != "[]")
                {
                    response = JsonConvert.DeserializeObject<AuthenticateResponse>(json)!;
                }
            }
            else
            {
                response.ProfileId = profileId;
                response.Status = "Error";
                response.StatusText = "An error occured. Could not get a response from Auth API";
            }
        }

        return response;
    }

    public void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        // skip authorization if action is decorated with [AllowAnonymous] attribute
        var allowAnonymous = filterContext.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        if (filterContext != null)
        {
            var authToken = filterContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var uiKey = filterContext.HttpContext.Request.Headers["uikey"].FirstOrDefault();
            var profileId = filterContext.HttpContext.Request.Headers["profileId"].FirstOrDefault();

            if (authToken != null && uiKey != null && profileId != null)
            {
                var authenticateResponse = ValidToken(authToken, uiKey, profileId);
                if (authenticateResponse != null)
                {
                    var status = authenticateResponse.Status;
                    if (status != null)
                        switch (status.ToLower())
                        {
                            case "200":
                                filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                                filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                                filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
                                break;
                            case "400":
                            default:
                                filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                                filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");
                                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                filterContext.Result = new JsonResult("Unauthorized")
                                {
                                    Value = new
                                    {
                                        Status = authenticateResponse.Status,
                                        Message = authenticateResponse.StatusText
                                    },
                                };
                                break;
                        }
                }
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                filterContext.Result = new JsonResult("Forbidden")
                {
                    Value = new
                    {
                        Status = 405,
                        Message = "Unauthorised access, new authToken or profileId required."
                    },
                };
            }
        }
    }
}

