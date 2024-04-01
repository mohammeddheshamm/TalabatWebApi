﻿using System;

namespace TalabatAPIS.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statusCode , string message = null)
        {
            StatusCode = statusCode ;
            Message = message ??  GetDefaultMessageForStatusCode(statusCode) ;
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You have made a bad request",
                401 => "You are not Authorized",
                404 => "Resource is not found",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. Hate Leads to career shift :D.",
                _ => null
            };
        }
    }
}
