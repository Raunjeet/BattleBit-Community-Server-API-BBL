using BattleBitAPI;
using BattleBitAPI.Common;
using BattleBitAPI.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CommunityServerAPI
{
   class RuleEnforcer : GameServer<MyPlayer>
    {
        private List<WeaponItem> weaponbanlist { get; set; }
        private List<Attachment> attachmentbanlist { get; set; }
        private List<Gadget> gadgetbanlist { get; set; }
        private List<MyPlayer> whitlistedplayers { get; set; }
        private List<MyPlayer> moderatorssteamid { get; set; }
        private bool enforceorkillonrulebroken { get; set; }
        private List<MyPlayer> teamA {  get; set; }
        private List<MyPlayer> teamB { get; set; } 

        public RuleEnforcer(List <WeaponItem> weaponBanList, List<Attachment> attachmentBanList, List<Gadget>gadgetBanList, List<MyPlayer>whitlistedPlayers, List<MyPlayer> Moderators, bool EnforceOrKillOnRuleBroken)
        {

            weaponbanlist = weaponBanList;
            attachmentbanlist = attachmentBanList;
            gadgetbanlist = gadgetBanList;
            whitlistedplayers = whitlistedPlayers;
            moderatorssteamid = Moderators;

        }

        public RuleEnforcer(List<WeaponItem> weaponBanList, List<Attachment> attachmentBanList, List<Gadget> gadgetBanList, List<MyPlayer> whitlistedPlayers, List<MyPlayer> Moderators, bool EnforceOrKillOnRuleBroken, List<MyPlayer> teamA, List<MyPlayer> teamB)
        {

            weaponbanlist = weaponBanList;
            attachmentbanlist = attachmentBanList;
            gadgetbanlist = gadgetBanList;
            whitlistedplayers = whitlistedPlayers;
            moderatorssteamid = Moderators;

        }

        public void EnforceWeaponBanList(MyPlayer player)
        {
            if (weaponbanlist.Contains(player.CurrentLoadout.PrimaryWeapon) || weaponbanlist.Contains(player.CurrentLoadout.PrimaryWeapon))
            {
                if (enforceorkillonrulebroken)
                {
                    player.Kill();
                    player.WarnPlayer("You are using a banned weapon. You have been killed.");
                }
                else
                {
                    player.WarnPlayer("You have an illegal weapon.");
                }

            }
        }

        public void EnforceGadgetBanList(MyPlayer player)
        {

            foreach(var gadget in gadgetbanlist)
            {
                if (player.CurrentLoadout.HasGadget(gadget) == true)
                {
                    if(enforceorkillonrulebroken)
                    {
                        player.Kill();
                        player.WarnPlayer("You have an illegal gadget. You have been killed.");
                        Console.WriteLine("Enforcing Gadget ban on: " + player.Name);
                    }
                    else
                    {
                        player.WarnPlayer("You have an illegal gadget.");
                    }
                }
            }
        }

        public void KickIfNotOnWhitelistSteam64(MyPlayer player)
        {
            if(whitlistedplayers.Contains(player) == false)
            {
                player.Kick("You are not whitelisted for this game.");
            }
        }
        public void SetRole(MyPlayer player, Roles role) 
        {
            player.playerRole = (ulong)role;
        }
        public void EnforceTeam(MyPlayer player)
        {
            if(teamA.Contains(player))
            {
                player.ChangeTeam(Team.TeamA);
            }
            else if (teamB.Contains(player))
            {
                player.ChangeTeam(Team.TeamB);
            }
        }
    }
}
