using System;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;

namespace ComfyUtils.VRC
{
    public class VRCUtils
    {
        public static ApiWorld GetWorld() { return RoomManager.field_Internal_Static_ApiWorld_0; }
        public static ApiWorldInstance GetInstance() { return RoomManager.field_Internal_Static_ApiWorldInstance_0; }
        public static bool WorldValid() { return RoomManager.field_Internal_Static_ApiWorld_0 != null && RoomManager.field_Internal_Static_ApiWorldInstance_0 != null; }
        public static string GetInstanceID() { return $"{RoomManager.field_Internal_Static_ApiWorld_0.id}:{RoomManager.field_Internal_Static_ApiWorldInstance_0.idWithTags}"; }
        public static string GetTrustRank(Player player)
        {
            APIUser user = player.field_Private_APIUser_0;
            if (user.tags.Contains("admin_moderator")) { return "Admin"; }
            else if (user.tags.Contains("system_legend")) { return "Legendary User"; }
            else if (user.tags.Contains("system_trust_legend")) { return "Veteran User"; }
            else if (user.tags.Contains("system_trust_veteran")) { return "Trusted User"; }
            else if (user.tags.Contains("system_trust_trusted")) { return "Known User"; }
            else if (user.tags.Contains("system_trust_known")) { return "User"; }
            else if (user.tags.Contains("system_trust_basic")) { return "New User"; }
            else if (user.tags.Contains("system_probable_troll") || user.tags.Contains("system_troll")) { return "Nuisance"; }
            else return "Visitor";
        }
        public static Color GetTrustColor(Player player)
        {
            APIUser user = player.field_Private_APIUser_0;
            Color32 color = new Color32 { a = 255, r = 204, g = 204, b = 204 };
            if (user.tags.Contains("admin_moderator")) { color.r = 255; color.g = 38; color.b = 38; }
            else if (user.tags.Contains("system_legend")) { color.r = 255; color.g = 105; color.b = 180; }
            else if (user.tags.Contains("system_trust_legend")) { color.r = 255; color.g = 208; color.b = 0; }
            else if (user.tags.Contains("system_trust_veteran")) { color.r = 177; color.g = 143; color.b = 255; }
            else if (user.tags.Contains("system_trust_trusted")) { color.r = 255; color.g = 123; color.b = 66; }
            else if (user.tags.Contains("system_trust_known")) { color.r = 43; color.g = 207; color.b = 92; }
            else if (user.tags.Contains("system_trust_basic")) { color.r = 23; color.g = 120; color.b = 255; }
            else if (user.tags.Contains("system_probable_troll") || user.tags.Contains("system_troll"))
            { color.r = 120; color.g = 47; color.b = 47; }
            return color;
        }
        public static APIUser GetUser(string id)
        {
            APIUser user = new APIUser();
            APIUser.FetchUser(id, (Action<APIUser>)delegate (APIUser found) { user = found; }, null);
            return user;
        }
        public static QuickMenu GetQM () { return QuickMenu.prop_QuickMenu_0; }
        public static APIUser GetSelectedUser() { return QuickMenu.prop_QuickMenu_0.field_Private_APIUser_0; }
        public static Il2CppSystem.Collections.Generic.List<Player> GetPlayerList()
        { return PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0; }
        public static void JoinInstance(string worldID, string instanceID)
        { new PortalInternal().Method_Private_Void_String_String_PDM_0(worldID, instanceID); }
    }
    public static class VRCExtensions
    {
        public static Player GetPlayer(this APIUser user)
        {
            foreach (Player player in PlayerManager.field_Private_Static_PlayerManager_0.field_Private_List_1_Player_0)
            {
                if (player.field_Private_APIUser_0.id == user.id) { return player; }
            }
            return null;
        }
    }
}
