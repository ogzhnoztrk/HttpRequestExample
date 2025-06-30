namespace HttpRequestExample.ExampleLibrary
{
    public static class Config
    {
        public static string ip { get; set; } // 10.60.60.240
        public static int port { get; set; } //4050
        public static bool usessl { get; set; } //false
        public static string startpath { get; set; } //api

        public static void Configure(string ip, int port, bool usessl, string startpath)
        {
            Config.ip = ip;
            Config.port = port;
            Config.usessl = usessl;
            Config.startpath = startpath;
        }
    }
}
