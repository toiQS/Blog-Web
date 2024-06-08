using System;

namespace Blog_Model
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Result { get; set; }

        public ServiceResult() { }


        public ServiceResult(T? result)
        {
            Result = result;
            Success = true;
        }


        public ServiceResult(string message)
        {
            Message = message; 
            Success = false;
        }


        public static ServiceResult<T> SuccessResult(T? result)
        {
            return new ServiceResult<T>(result);
        }


        public static ServiceResult<T> FailedResult(string message)
        {
            return new ServiceResult<T>(message);
        }
    }
}
