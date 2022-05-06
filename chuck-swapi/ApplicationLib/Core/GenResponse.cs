namespace chuck_swapi.ApplicationLib.Core
{
    public class GenResponse<T>
    {
        public bool IsSuccess { get; set; } = false;
        public T Data { get; set; } = default!;
        public string Message { get; set; } = default!;

        public static GenResponse<T> Result(T value, bool successStatus, string message = "") => new GenResponse<T> { IsSuccess = successStatus, Data = value, Message = message };
    }
}
