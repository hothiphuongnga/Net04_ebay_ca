

namespace ebay.Shared.Common;

public class ResponseEntity<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Content { get; set; }
    public int StatusCode { get; set; } = 200;

    public static ResponseEntity<T> Ok(T data, string? message = null)
        => new()
        {
            Success = true,
            Content = data,
            Message = message ?? "Success",
            StatusCode = 200
        };

    public static ResponseEntity<T> Fail(string message, int statusCode = 400)
        => new()
        {
            Success = false,
            Message = message,
            StatusCode = statusCode
        };


}
