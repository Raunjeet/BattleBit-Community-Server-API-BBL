using BattleBitAPI.Common;
using System;

namespace CommunityServerAPI
{
    class RuleEnforcerUnitTest
    {
        public static void RunUnitTest()
        {
            MyPlayer player1 = new MyPlayer();
            MyPlayer player2 = new MyPlayer();
            MyPlayer player3 = new MyPlayer();
            MyPlayer player4 = new MyPlayer();

            List<MyPlayer> playerListA = new List<MyPlayer>();
            List<MyPlayer> playerListB = new List<MyPlayer>();

            List<WeaponItem> weaponItemBanList = new List<WeaponItem>
            {
                //Weapons.ACR
            };
            List<Attachment> attachmentBanList = new List<Attachment>
            {
                Attachments.Tactical
            };
            List<Gadget> gadgetBanList = new List<Gadget>
            {
                Gadgets.AdvancedBinoculars
            };
            List<MyPlayer> whiteList = new List<MyPlayer>
            {
                player1,
                player2
            };
            List<MyPlayer> modList = new List<MyPlayer>
            { 
                player3,
                player4
            };

            bool killOnRuleBroken = true;


            RuleEnforcer ruleEnforcer = new RuleEnforcer(weaponItemBanList, attachmentBanList, gadgetBanList, whiteList, modList, killOnRuleBroken, playerListA, playerListB);

            //Test Weapon ban list


            //Test Gadget ban list
            player1.SetHeavyGadget(Gadgets.AdvancedBinoculars.Name,0);

            ruleEnforcer.EnforceGadgetBanList(player1);

            ruleEnforcer.KickIfNotOnWhitelistSteam64(player3);

        }
    }
}
