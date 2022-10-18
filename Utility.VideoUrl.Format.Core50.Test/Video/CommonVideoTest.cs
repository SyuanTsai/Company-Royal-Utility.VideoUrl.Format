using FluentAssertions;
using NUnit.Framework;

namespace Utility.VideoUrl.Format.Core31.Test.Video
{
    public class CommonVideoTest
    {
        /// <summary>
        ///     正常生出視訊格式
        /// </summary>
        [Test]
        public void GenerateVideo_Susses_GenUrl()
        {
            var server = GenData.GetServer();

            var info = new GameInfo()
            {
                NoActive = "0001", NoRun = "2210189999",
            };

            var baseVideo = new CommonVideo();

            var result = baseVideo.GenerateVideo(info, server)
                                  .Result;

            result.VideoOne.Should()
                  .Be($"{server.HistoryVideoUrl1}1018/22101899990001.mp4", "格式錯誤，");

            result.VideoTwo.Should()
                  .Be($"{server.HistoryVideoUrl2}1018/22101899990001.mp4", "格式錯誤，");

            result.VideoThree.Should()
                  .Be($"{server.HistoryVideoUrl3}1018/22101899990001.mp4", "格式錯誤，");

            Assert.Pass();
        }

        /// <summary>
        ///     格式錯誤給予空字串
        /// </summary>
        [Test]
        [TestCase("0001", "9999")]
        [TestCase("", "2210189999")]
        [TestCase("", "")]
        public void GenerateVideo_Fail_stringEmpty(string noActive, string noRun)
        {
            var server = GenData.GetServer();

            var info = new GameInfo()
            {
                NoActive = noActive, NoRun = noRun
            };

            var baseVideo = new CommonVideo();

            var result = baseVideo.GenerateVideo(info, server)
                                  .Result;

            result.VideoOne.Should()
                  .Be(string.Empty, "格式錯誤，");

            result.VideoTwo.Should()
                  .Be(string.Empty, "格式錯誤，");

            result.VideoThree.Should()
                  .Be(string.Empty, "格式錯誤，");

            Assert.Pass();
        }
    }
}