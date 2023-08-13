@echo off
setlocal

rem Execute the updateorinstall script and wait for it to complete
call "UpdateServer.bat"

set "Name=Test Server123"
set "Password=thePassword"
set "AntiCheat=EAC"
set "Hz=60"
set "Port=29999"
set "MaxPing=300"
set "LocalIP=127.0.0.1"
set "VoxelMode=false"
set "ConfigPath="
set "ApiEndpoint=0.0.0.0:30001"
set "FixedSize=true"
set "FirstSize=small"
set "MaxSize=small"
set "FirstGamemode=DOM"
set "FirstMap=Azagor"
set "Region=US_Central"

set "battlebit_args=-batchmode -nographics -startserver -Name=%Name% -Password=%Password% -AntiCheat=%AntiCheat% -Hz=%Hz% -Port=%Port% -MaxPing=%MaxPing% -LocalIP=%LocalIP% -VoxelMode=%VoxelMode% -ConfigPath=%ConfigPath% -ApiEndpoint=%ApiEndpoint% -FixedSize=%FixedSize% -FirstSize=%FirstSize% -MaxSize=%MaxSize% -FirstGamemode=%FirstGamemode% -FirstMap=%FirstMap%" -Region=%Region%

echo Launching the BattleBit game server...

rem Start the BattleBit game server
Start "" "C:\Program Files (x86)\Steam\steamapps\common\BattleBit Remastered\BattleBit.exe" %battlebit_args%

endlocal