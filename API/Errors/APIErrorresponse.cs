using System;

namespace API.Errors;

public class APIErrorresponse(int StatusCode, string ExceptionMessage, string Details)
{
    public int StatusCode { get; set; } = StatusCode;
    public string ExceptionMessage { get; set; } = ExceptionMessage;

    public string Details { get; set; } = Details;
}
