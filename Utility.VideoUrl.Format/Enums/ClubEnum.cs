
#if NET5_0
#elif NET6_0
namespace VideoUrlFormat.Enums;
#else
#error This code block does not match csproj TargetFrameworks list
#endif

public enum ClubEnum
{
    GClub = 1
  , MStar = 2
  , MClub = 3
  , XG    = 4
  , WM    = 5
}