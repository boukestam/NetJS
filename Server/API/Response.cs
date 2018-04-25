﻿using NetJS.Core.Javascript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NetJS.Server.API {
    public class Response {

        /// <summary>Writes a cookie.</summary>
        /// <param name="name">The name of the cookie (string)</param>
        /// <param name="value">The value to set (string)</param>
        /// <param name="options">Options: expires (date), path (string) and httpOnly (boolean)</param>
        /// <example><code lang="javascript">Response.setCookie("SSID", "AE3oaD8COGojttJue", {
        ///     expires: new Date(),
        ///     path: "localhost/example",
        ///     httpOnly: false
        /// });</code></example>
        public static Constant setCookie(Constant _this, Constant[] arguments, Scope scope) {
            var key = Core.Tool.GetArgument<Core.Javascript.String>(arguments, 0, "Response.setCookie").Value;
            var value = Core.Tool.GetArgument<Core.Javascript.String>(arguments, 1, "Response.setCookie").Value;

            var options = Core.Tool.GetArgument<Core.Javascript.Object>(arguments, 2, "Response.setCookie", false);

            var cookie = new HttpCookie(key, value);

            if (options != null) {
                if(options.Get("expires") is Core.Javascript.Date date) {
                    cookie.Expires = date.Value;
                }

                if (options.Get("path") is Core.Javascript.String path) {
                    cookie.Path = path.Value;
                }

                if (options.Get("httpOnly") is Core.Javascript.Boolean httpOnly) {
                    cookie.HttpOnly = httpOnly.Value;
                }
            }
            
            HttpContext.Current.Response.Cookies.Add(cookie);
            return Static.Undefined;
        }

        /// <summary>Removes a cookie.</summary>
        /// <param name="name">The name of the cookie (string)</param>
        /// <example><code lang="javascript">Response.removeCookie("SSID")</code></example>
        public static Constant removeCookie(Constant _this, Constant[] arguments, Scope scope) {
            var key = Core.Tool.GetArgument<Core.Javascript.String>(arguments, 0, "Response.removeCookie").Value;

            var cookie = new HttpCookie(key);
            cookie.Expires = DateTime.Now.AddDays(-1);
            HttpContext.Current.Response.Cookies.Add(cookie);
            return Static.Undefined;
        }

        /// <summary>Writes an header.</summary>
        /// <param name="name">The name of the header (string)</param>
        /// <param name="value">The value to set (string)</param>
        /// <example><code lang="javascript">Response.setHeader("Content-Type", "application/json");</code></example>
        public static Constant setHeader(Constant _this, Constant[] arguments, Scope scope) {
            var key = Core.Tool.GetArgument<Core.Javascript.String>(arguments, 0, "Response.setHeader").Value;
            var value = Core.Tool.GetArgument<Core.Javascript.String>(arguments, 1, "Response.setHeader").Value;

            var context = Tool.GetContext(scope, "Response.setHeader");
            context.Response.AppendHeader(key, value);
            return Static.Undefined;
        }

        /// <summary>Removes an header.</summary>
        /// <param name="name">The name of the header (string)</param>
        /// <example><code lang="javascript">Response.removeHeader("ApplicationID")</code></example>
        public static Constant removeHeader(Constant _this, Constant[] arguments, Scope scope) {
            var key = Core.Tool.GetArgument<Core.Javascript.String>(arguments, 0, "Response.removeHeader").Value;

            var context = Tool.GetContext(scope, "Response.removeHeader");
            context.Response.Headers.Remove(key);
            return Static.Undefined;
        }

        /// <summary>Sends an array of bytes and exits the execution immediately</summary>
        /// <param name="bytes">The bytes to send (Uint8Array)</param>
        /// <example><code lang="javascript">Request.sendBytes(bytes);</code></example>
        public static Constant sendBytes(Constant _this, Constant[] arguments, Scope scope) {
            var bytes = Core.Tool.GetArgument<Core.Javascript.Uint8Array>(arguments, 0, "Request.sendBytes");

            var context = Tool.GetContext(scope, "Request.sendBytes");
            context.Response.OutputStream.Write(bytes.Buffer.Data, 0, bytes.Buffer.Data.Length);
            context.Response.OutputStream.Close();
            context.Response.End();

            return Static.Undefined;
        }

        /// <summary>Sends an file and exits the execution immediately</summary>
        /// <param name="name">The name of the file (string)</param>
        /// <example><code lang="javascript">Request.sendFile("image.png");</code></example>
        public static Constant sendFile(Constant _this, Constant[] arguments, Scope scope) {
            var name = Core.Tool.GetArgument<Core.Javascript.String>(arguments, 0, "Request.sendFile");

            var application = NetJS.Tool.GetApplication(scope);

            var context = Tool.GetContext(scope, "Request.sendFile");

            var file = application.Cache.GetPath(name.Value, application, false);
            context.Response.ContentType = MimeMapping.GetMimeMapping(file);
            context.Response.TransmitFile(file);
            context.Response.End();

            return Static.Undefined;
        }

        /// <summary>Sets the response status code.</summary>
        /// <param name="statusCode">The status code (number)</param>
        /// <example><code lang="javascript">Response.setStatusCode(200);</code></example>
        public static Constant setStatusCode(Constant _this, Constant[] arguments, Scope scope) {
            var code = Core.Tool.GetArgument<Core.Javascript.Number>(arguments, 0, "Response.setStatusCode");

            var context = Tool.GetContext(scope, "Response.setHeader");
            context.Response.StatusCode = (int)code.Value;
            return Static.Undefined;
        }

        /// <summary>Sets the response status description.</summary>
        /// <param name="statusDescription">The status description (number)</param>
        /// <example><code lang="javascript">Response.setStatusDescription(200);</code></example>
        public static Constant setStatusDescription(Constant _this, Constant[] arguments, Scope scope) {
            var description = Core.Tool.GetArgument<Core.Javascript.String>(arguments, 0, "Response.setStatusDescription");

            var context = Tool.GetContext(scope, "Response.setStatusDescription");
            context.Response.StatusDescription = description.Value;
            return Static.Undefined;
        }

        /// <summary>Sets the response encoding.</summary>
        /// <param name="encoding">The web name (registered with IANA) of the encoding (string)</param>
        /// <example><code lang="javascript">Response.setEncoding("UTF-8");</code></example>
        public static Constant setEncoding(Constant _this, Constant[] arguments, Scope scope) {
            var encoding = Core.Tool.GetArgument<Core.Javascript.String>(arguments, 0, "Response.setEncoding");

            var context = Tool.GetContext(scope, "Response.setEncoding");
            context.Response.ContentEncoding = Encoding.GetEncoding(encoding.Value);
            return Static.Undefined;
        }
    }
}