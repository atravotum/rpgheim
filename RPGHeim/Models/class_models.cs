using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGHeim.Models
{
    static class Fighter
    {
        public static void InitializePlayer()
        {
            // add the class abilities
            RPGHeimMain.SkillsBar.SetAbility(AssetManager.GetAbility("$ability_RPGHeimFightingSpirit"), 0);
            RPGHeimMain.SkillsBar.SetAbility(AssetManager.GetAbility("$ability_RPGHeimWarCry"), 1);
            RPGHeimMain.SkillsBar.SetAbility(AssetManager.GetAbility("$ability_RPGHeimTrainedReflexes"), 2);
            RPGHeimMain.SkillsBar.SetAbility(AssetManager.GetAbility("$ability_RPGHeimStrengthWielding"), 3);
            RPGHeimMain.SkillsBar.SetAbility(AssetManager.GetAbility("$ability_RPGHeimWeaponsMaster"), 4);

            // Enable the skills bar
            RPGHeimMain.SkillsBar.isEnabled = true;
        }
    }
    static class Healer
    {
        public static void InitializePlayer()
        {
            // add the class abilities
            RPGHeimMain.SkillsBar.SetAbility(AssetManager.GetAbility("$ability_RPGHeimSoothingLight"), 0);
            RPGHeimMain.SkillsBar.SetAbility(AssetManager.GetAbility("$ability_RPGHeimHealingAura"), 1);
            RPGHeimMain.SkillsBar.SetAbility(AssetManager.GetAbility("$ability_RPGHeimCleanse"), 2);
            //RPGHeimMain.SkillsBar.SetAbility(AssetManager.GetAbility("$ability_RPGHeim"), 3);
            //RPGHeimMain.SkillsBar.SetAbility(AssetManager.GetAbility("$ability_RPGHeim"), 4);

            // Enable the skills bar
            RPGHeimMain.SkillsBar.isEnabled = true;
        }
    }
    static class Rogue
    {
        public static void InitializePlayer()
        {

        }
    }
    static class Wizard
    {
        public static void InitializePlayer()
        {

        }
    }
}
