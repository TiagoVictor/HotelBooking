﻿namespace Application.Responses
{
    public enum ErrorCode
    {
        NOT_FOUND = 1,
        COULDNOT_STORE_DATA = 2,
        INVALID_DOCUMENT = 3,
        MISSING_REQUIRED_INFORMATION = 4,
        INVALID_EMAIL = 5,
    }

    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCode ErrorCode { get; set; }
    }
}
