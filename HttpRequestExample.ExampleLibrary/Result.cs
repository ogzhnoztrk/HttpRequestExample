namespace HttpRequestExample.ExampleLibrary
{
    public class Result<T>
    {
        public Result() { }

        public Result(int _code, string _message)
        {
            code = _code;
            message = _message;
        }

        public Result(int _code, string _message, T _data)
        {
            code = _code;
            message = _message;
            data = _data;
        }
        public Result(int _code, string _message, T _data, DateTime _time)
        {
            code = _code;
            message = _message;
            data = _data;
            time = _time;
        }

        public int code { get; set; }
        public string message { get; set; }
        public DateTime time { get; private set; } = DateTime.Now;
        public T data { get; set; }
    }

    public class Result : Result<object>
    {
        public Result() : base() { }
        public Result(int _code, string _message) : base(_code, _message) { }
        public Result(int _code, string _message, object _data) : base(_code, _message, _data) { }
        public Result(int _code, string _message, object _data, DateTime _time) : base(_code, _message, _data, _time) { }
    }
}
