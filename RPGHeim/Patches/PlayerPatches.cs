using HarmonyLib;
using RPGHeim.Managers;

namespace RPGHeim
{
    // invoke various neccasary methods to prep the player for the RPGHeim mod/systems
    [HarmonyPatch(typeof(Player), "OnSpawned")]
    public static class RPGHeim_Player_Awake_Patch
    {
        private static void Postfix(ref Player __instance)
        {
            // check that we found a player and prep it for it's class
            if (__instance)
            {
                // Create the hotbar when we start the game.
                RPGHeimMain.UIHotBarManager.CreateHotBar();

                // Fighter prep
                float fighterLV = __instance.GetRPGHeimSkillFactor(SkillsManager.RPGHeimSkill.Fighter);
                if (fighterLV > 0) RPGHeimFighterClass.InitializePlayer(__instance, fighterLV);
            }
        }
    }

    // intercepts hotbar item use and cancels if alt key is held (alt + 1-5 reserved for skills bar)
    [HarmonyPatch(typeof(Player), "UseHotbarItem")]
    public static class UseHotbarItemPrefix
    {
        public static void Prefix(ref int index)
        {
            //Jotunn.Logger.LogMessage($"UseHotbarItem - altkey? {InputManager.AltKeyPressed} - {index}");
            if (InputManager.AltKeyPressed)
            {
                // Allow me to mod it? -- this is how we can restrict the main hotbar from getting hit.
                index = 0;
                //Jotunn.Logger.LogMessage($"UseHotbarItem restricted - {index}");
            }
        }
    }

    // Harmony patch to check when our mod's items are used so we can trigger effects
    [HarmonyPatch(typeof(Player), "ConsumeItem")]
    public static class RPGHeim_Player_ConsumeItem_Patch
    {
        private static void Postfix(ref Inventory inventory, ref ItemDrop.ItemData item, ref Player __instance)
        {
            if (item.IsRPGHeimItem())
            {
                RPGHeimItemsSystem.itemUsed(item, __instance);
            }
        }
    }
}
