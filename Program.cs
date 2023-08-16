using BattleBitAPI;
using BattleBitAPI.Common;
using BattleBitAPI.Server;
using CommunityServerAPI;
using Microsoft.Extensions.Configuration;
using System;
//using static System.Net.WebRequestMethods;

class Program
{
    static void Main(string[] args)
    {
        //Use var from secrets.json
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();
        
#if DEBUG
        //string testMessage = Console.ReadLine();
        //Task task = HttpPacketSender.sendPostDataTest(testMessage, config["bbltestendpoint"]);
#endif

        // Battlebit Server starts here
        int listeningPort = 29294;
        var listener = new ServerListener<MyPlayer, MyGameServer>();
        listener.Start(listeningPort);
        Console.Out.WriteLineAsync("Server API Started. Listening on port: " + listeningPort);
        //RuleEnforcerUnitTest.RunUnitTest();
        Thread.Sleep(-1);
    }


}
class MyPlayer : Player<MyPlayer>
{
    public ulong playerRole;
    public enum Roles : ulong
    {
        None = 0,

        Admin = 1 << 0,
        Moderator = 1 << 1,
        Special = 1 << 2,
        Vip = 1 << 3,
        

        Player = 1 << 4,
        Referee = 1 << 5,
    }
   


}



class MyGameServer : GameServer<MyPlayer>
{

    public override async Task OnPlayerSpawned(MyPlayer player)
    {
        //await RuleEnforcer.EnforceGadgetBanList(player);
    }
    public override async Task OnRoundStarted()
    {

    }
    public override async Task OnRoundEnded()
    {
        //send data to webserver
    }

    public override async Task OnPlayerGivenUp(MyPlayer player)
    {
        await Console.Out.WriteLineAsync("Giveup: " + player);
    }
    public override async Task OnPlayerDied(MyPlayer player)
    {
        await Console.Out.WriteLineAsync("Died: " + player);
    }
    public override async Task OnAPlayerRevivedAnotherPlayer(MyPlayer from, MyPlayer to)
    {
        await Console.Out.WriteLineAsync(from + " revived " + to);
    }
    public override async Task OnPlayerDisconnected(MyPlayer player)
    {
        await Console.Out.WriteLineAsync("Disconnected: " + player);
    }
}


