namespace Client.API.Models
{
    public abstract class BaseResponse<T>
    {
        public T Result { get; }
        public bool IsSuccess { get; }
        public string[] Message { get; }

        public BaseResponse(T result)
        {
            Result = result;
            IsSuccess = true;
        }

        public BaseResponse(string[] message)
        {
            Result = default;
            IsSuccess = false;
            Message = message;
        }
    }
}
