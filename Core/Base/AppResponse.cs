using System;
namespace Core.Base
{
	public class AppResponse<T> where T : BaseEntity
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
        public string Message { set; get; } = "Success"; 
	}
}

