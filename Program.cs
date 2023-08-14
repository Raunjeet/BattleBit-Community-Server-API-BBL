using BattleBitAPI;
using BattleBitAPI.Common;
using BattleBitAPI.Server;


class Program
{
    static void Main(string[] args)
    {
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

    public bool enforceSpecificLoadout = false;

    public async Task forceTeamSwapOn64List(List<ulong> playerListTeamA, List<ulong> playerListTeamB)
    {
        //get list of all players in server steam 64

        //loop through list

        //if current player belongs to team a
        //set team a
        //else set team b
    }


    public List<Attachment> temporaryLoadoutAttachmentBanList = new List<Attachment>()
    {

    };



    public override async Task OnRoundStarted()
    {

    }
    public override async Task OnRoundEnded()
    {

    }

    public override async Task OnPlayerConnected(MyPlayer player)
    {
        if (await checkIfPlayerIsOnWhiteList(player) == false)
        {
            player.Kick("You are not a whitelisted player for this match.");
        }
    }

    public override async Task OnPlayerSpawned(MyPlayer player)
    {
        if (enforceSpecificLoadout)
        {

        }

        if (await checkIfPlayerLoadoutIsLegal(player, temporaryLoadoutBanList) == false)
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

    public async Task<bool> checkIfPlayerLoadoutIsLegal(MyPlayer player, List<String> allowedWeaponsList)
    {

        //Weapons Check
        if (allowedWeaponsList.Contains(player.CurrentLoadout.PrimaryWeapon.ToString()))
        {
            return false;
        }

        if (allowedWeaponsList.Contains(player.CurrentLoadout.SecondaryWeapon.ToString()))
        {
            return false;
        }

        //Gadgets Check
        if (allowedWeaponsList.Contains(player.CurrentLoadout.HeavyGadgetName))
        {
            return false;
        }

        if (allowedWeaponsList.Contains(player.CurrentLoadout.LightGadgetName))
        {
            return false;
        }

        //Throwables Check
        if (allowedWeaponsList.Contains(player.CurrentLoadout.ThrowableName))
        {
            return false;
        }

        //Attachments Check, WIP
        foreach (Attachment theAttachment in temporaryLoadoutAttachmentBanList)
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
