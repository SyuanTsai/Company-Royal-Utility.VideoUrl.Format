#if NET5_0
#elif NET6_0
namespace VideoUrlFormat.Enums;
#else
#error This code block does not match csproj TargetFrameworks list
#endif

public enum VideoByGameTypeEnum
{
    // NewGame 新增遊戲 - 加入新的GameID
    Bacc       = 1
  , InsuBacc   = 6
  , LongHu     = 2
  , LunPan     = 3
  , PokDeng    = 7
  , FanTan     = 5
  , ShaiZi     = 4
  , SamBo      = 9
  , BullBull   = 8
  , RgRacing   = 10
  , BcBacc     = 11
  , BcLongHu   = 12
  , AndarBahar = 15
  , SeDie      = 16
  , HiLo       = 17
  , BcSdd      = 18
}