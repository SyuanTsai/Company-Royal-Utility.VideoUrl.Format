using VideoUrlFormat.Domain.Server;

namespace Utility.VideoUrl.Format.Core31.Test.Video
{
    public static class GenData
    {
        /// <summary>
        ///     下注失敗後(TimeOut)
        ///     回Call下注成功並還錢
        /// </summary>
        public static Server GetServer()
        {
            return new Server()
            {
                ServerNo         = 20221018, HistoryVideoUrl1                 = "https://test.com/url1/"
              , HistoryVideoUrl2 = "https://test.com/url2/", HistoryVideoUrl3 = "https://test.com/url3/"
            };
        }
    }
}