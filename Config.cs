using Rocket.API;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SupplySignal 
{
    public class Config : IRocketPluginConfiguration
    {
        public bool SupplyAnnounc;
        public string SupplyPermission, AnnouncMessage, AnnouncImgUrl;
        [XmlArrayItem("SignalIDs")]
        public List<ushort> SıgnalSmokes { get; set; } = new List<ushort>();
        public void LoadDefaults()
        {
            SupplyAnnounc = true;
            SupplyPermission = "mixy.supply";
            AnnouncMessage = "{color=yellow}%PLAYERNAME%{/color} {color=white}called air drop.{/color}";
            AnnouncImgUrl = "";
            SıgnalSmokes = new List<ushort>
            {
                261,
            };
        }
    }
}
