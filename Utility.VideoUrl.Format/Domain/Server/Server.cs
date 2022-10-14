namespace VideoUrlFormat.Domain.Server
{
    /// <summary>
    /// 伺服器(機台)
    /// </summary>
    public partial class Server
    {
        /// <summary>
        /// 伺服器(機台)Id
        /// </summary>
        public string Id { get; set; } = null!;

        public int? ServerNo { get; set; }

        /// <summary>
        /// 遊戲Id
        /// </summary>
        public int GameNo { get; set; }

        /// <summary>
        /// 館別Id
        /// </summary>
        public int LobbyNo { get; set; }

        /// <summary>
        /// 伺服器(機台)名稱
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 是否啟用
        /// </summary>
        public byte Enable { get; set; }

        public string? HistoryVideoUrl1 { get; set; }

        public string? HistoryVideoUrl2 { get; set; }

        public string? HistoryVideoUrl3 { get; set; }
    }
}