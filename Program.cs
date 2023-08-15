using BattleBitAPI;
using BattleBitAPI.Common;
using BattleBitAPI.Server;
using CommunityServerAPI;
using Microsoft.Extensions.Configuration;
using System.Net;
using static System.Net.WebRequestMethods;

class Program
{
    static void Main(string[] args)
    {

        //Use var from secrets.json
        var config = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();


#if DEBUG
        string testMessage = Console.ReadLine();

        Task task = HttpPacketSender.sendPostDataTest(testMessage, config["bbltestendpoint"]);

#endif



        // Battlebit Server starts here
        int listeningPort = 29294;
        var listener = new ServerListener<MyPlayer, MyGameServer>();
        listener.Start(listeningPort);
        Console.Out.WriteLineAsync("Server API Started. Listening on port: " + listeningPort);
        Thread.Sleep(-1);
    }

}
class MyPlayer : Player<MyPlayer>
{

}
class MyGameServer : GameServer<MyPlayer>
{
    public List<ulong> temporaryWhiteList = new List<ulong>()
    {
        76561198071790300,
    };

    public List<ulong> temporaryTeamA = new List<ulong>()
    {
        76561198071790300,
    };

    public List<ulong> temporaryTeamB = new List<ulong>()
    {
        76561198071790301,
    };
    public List<string> temporaryLoadoutBanList = new List<string>
    { 
        //weapons
        "Kriss Vector",
        "FAL",
        "ScorpionEVO",

        //gadgets
        "C4",
        "Claymore",
        "AntiPersonnelMine",
        "Rpg7HeatExplosive",
        "RiotShield",
        "Rpg7 Pgo7 Tandem",
        "Rpg7 Pgo7 Heat Explosive",
        "Rpg7 Pgo7 Fragmentation",
        "Rpg7 Fragmentation",
        "SuicideC4",
    };

    public List<Attachment> temporaryLoadoutAttachmentBanList = new List<Attachment>()
    {

    };


    public bool enforceSpecificLoadout = false;

    public async Task forceTeamSwapOn64List(List<ulong> playerListTeamA, List<ulong> playerListTeamB, MyPlayer player)
    {
        if (playerListTeamA.Contains(player.SteamID))
        {
            player.Team = Team.TeamA;
        }
        else if (playerListTeamB.Contains(player.SteamID))
        {
            player.Team = Team.TeamB;
        }
    }

    public override async Task OnRoundStarted()
    {

    }
    public override async Task OnRoundEnded()
    {
        //send data to webserver

    }

    public override async Task OnPlayerConnected(MyPlayer player)
    {
        if (await checkIfPlayerIsOnWhiteList(player) == false)
        {
            player.Kick("You are not a whitelisted player for this match.");
        }

        await forceTeamSwapOn64List(temporaryTeamA, temporaryTeamB, player);
    }

    public override async Task OnPlayerSpawned(MyPlayer player)
    {
        if (enforceSpecificLoadout)
        {

        }

        if (await checkIfPlayerLoadoutIsLegal(player, temporaryLoadoutBanList, temporaryLoadoutAttachmentBanList) == false)
        {
            player.Kill();
            player.WarnPlayer("You have an illegal loadout. You have been force killed.");
        }
    }

    public async Task<bool> checkIfPlayerIsOnWhiteList(MyPlayer player)
    {
        if (temporaryWhiteList.Contains(player.SteamID))
        {
            return true;
        }
        return false;
    }

    public async Task <bool> checkIfPlayerLoadoutIsLegal(MyPlayer player, List<String> bannedWeaponsList, List<Attachment> theAttachmentList)
    {

        //Weapons Check
        if (bannedWeaponsList.Contains(player.CurrentLoadout.PrimaryWeapon.ToString()))
        {
            return false;
        }

        if (bannedWeaponsList.Contains(player.CurrentLoadout.SecondaryWeapon.ToString()))
        {
            return false;
        }

        //Gadgets Check
        if (bannedWeaponsList.Contains(player.CurrentLoadout.HeavyGadgetName))
        {
            return false;
        }

        if (bannedWeaponsList.Contains(player.CurrentLoadout.LightGadgetName))
        {
            return false;
        }

        //Throwables Check
        if (bannedWeaponsList.Contains(player.CurrentLoadout.ThrowableName))
        {
            return false;
        }

        //Attachments Check, WIP
        foreach (Attachment theAttachment in theAttachmentList)
        {
            if (player.CurrentLoadout.PrimaryWeapon.HasAttachment(theAttachment))
            {
                return false;
            }

            if (player.CurrentLoadout.SecondaryWeapon.HasAttachment(theAttachment))
            {
                return false;
            }

        }

        return true;
    }



    public override async Task OnConnected()
    {
        await Console.Out.WriteLineAsync("Current state: " + RoundSettings.State);

    }
    public override async Task OnGameStateChanged(GameState oldState, GameState newState)
    {
        await Console.Out.WriteLineAsync("State changed to -> " + newState);
    }

}
