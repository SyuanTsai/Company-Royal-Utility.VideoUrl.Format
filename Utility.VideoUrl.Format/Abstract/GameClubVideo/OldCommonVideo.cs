

using System;

#if NETCOREAPP3_1
#elif NET5_0 
#elif NET6_0
namespace VideoUrlFormat.Abstract.GameClubVideo;
#else
#error This code block does not match csproj TargetFrameworks list
#endif
[Obsolete("預計10/17切換視訊格式，調閱視訊網址+MMDD+完整輪號局號")]
public class OldCommonVideo : BaseVideo
{
    
}