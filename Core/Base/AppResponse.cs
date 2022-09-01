using System;
namespace Core.Base
{
	public class AppResponse<T> 
	{
		public AppResponse(T data)
        {
			Data = data; 
        }
        public AppResponse(String message)
        {
            Message = message;
        }
        public T? Data { set; get; }
        public string Message { set; get; } = "success"; 
	}
}

