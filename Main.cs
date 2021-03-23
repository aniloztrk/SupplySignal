using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System.Linq;
using Rocket.API;
using UnityEngine;
using Rocket.API.Collections;
using Rocket.Unturned.Chat;
using System.Net;
using System;
using System.IO;

namespace SupplySignal
{
    public class Main : RocketPlugin<Config> 
    {
        protected override void Load()
        {
            UseableThrowable.onThrowableSpawned += Throwed;
        }
        protected override void Unload()
        {
            UseableThrowable.onThrowableSpawned -= Throwed;
        }
        private void Throwed(UseableThrowable useable, GameObject throwable)
        {          
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(useable.player);
            string message = Configuration.Instance.AnnouncMessage.Replace('{', '<').Replace('}', '>').Replace("%PLAYERNAME%", player.DisplayName);
            if (player.HasPermission(Configuration.Instance.SupplyPermission))
            {
                foreach (var itemid in Configuration.Instance.SıgnalSmokes)
                {
                    if (useable.equippedThrowableAsset.id == itemid)
                    {
                        GetAirdrop(throwable.transform.localPosition);
                        if (Configuration.Instance.SupplyAnnounc)
                        {
                            ChatManager.serverSendMessage(message, Color.white, null, null, EChatMode.SAY, Configuration.Instance.AnnouncImgUrl, true);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
                UnturnedChat.Say(player, Translate("PermissionMessage"), Color.red);
            }
        }
        public void GetAirdrop(Vector3 position)
        {
            var nodes = LevelNodes.nodes.OfType<AirdropNode>().Where(k => k.id != 0).ToList();

            AirdropNode airdropNode = nodes[UnityEngine.Random.Range(0, nodes.Count())];
            LevelManager.airdrop(position, airdropNode.id, Provider.modeConfigData.Events.Airdrop_Speed);
        }
        public override TranslationList DefaultTranslations => new TranslationList
        {
            { "PermissionMessage", "You dont have a permission." },
        };        
    }
}
