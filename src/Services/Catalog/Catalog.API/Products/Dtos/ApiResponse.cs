using System.Net;

namespace Catalog.API.Products.Dtos
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = default!;
        public HttpStatusCode StatusCode { get; set; }
        public T Result { get; set; } = default!;
    }
}
