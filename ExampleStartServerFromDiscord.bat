@echo off
setlocal

rem Execute the updateorinstall script and wait for it to complete
call "UpdateServer.bat"

set "Name=Test Server123"
set "Password=thePassword"
set "AntiCheat=EAC"
set "Hz=60"
set "Port=29294"
set "MaxPing=300"
set "LocalIP=0.0.0.0"
set "VoxelMode=false"
set "ConfigPath="
set "FixedSize=true"
set "FirstSize=small"
set "MaxSize=small"
set "FirstGamemode=TDM"
set "FirstMap=Azagor"
set "Region=Europe_Central"

set "battlebit_args=-batchmode -nographics -startserver -Name=%Name% -Password=%Password% -AntiCheat=%AntiCheat% -Hz=%Hz% -Port=%Port% -MaxPing=%MaxPing% -LocalIP=%LocalIP% -VoxelMode=%VoxelMode% -ConfigPath=%ConfigPath% -FixedSize=%FixedSize% -FirstSize=%FirstSize% -MaxSize=%MaxSize% -FirstGamemode=%FirstGamemode% -FirstMap=%FirstMap%" -Region=%Region%

echo Launching the BattleBit game server...

rem Start the BattleBit game server
Start "" "C:\BattleBit\BattleBit.exe" %battlebit_args%

endlocal