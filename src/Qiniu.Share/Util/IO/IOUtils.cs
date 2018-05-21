namespace Qiniu.Share.IO
{
    public static class IOUtils
    {
        static IOUtils()
        {
            Api = new CommonIOApi();
        }

        public static IIOUtils Api { set; get; }
    }
}
