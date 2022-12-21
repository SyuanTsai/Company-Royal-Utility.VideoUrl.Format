# 視訊套件

## Build 
dotnet build --configuration Release

## 推送指令

dotnet nuget push Utility.VideoUrl.Format.6.0.7.nupkg --api-key thisisapikey --source https://nuget.royal-test.com/api/v3/index.json

## 目前僅維護20221018後的新增功能
1. 抽換底層CommonVideo
2. 根據日期轉換格式
3. 驗證3.1 5.0 6.0 皆可以作業