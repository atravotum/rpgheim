using BepInEx;
using Jotunn.Configs;
using Jotunn.Managers;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace RPGHeim
{
    [BepInPlugin("github.atravotum.rpgheim.fighterClass", "RPGHeim", "1.0.0")]
    [BepInDependency(Jotunn.Main.ModGuid)]
    internal class RPGHeimFighterClass : BaseUnityPlugin
    {
        private void Awake()
        {
            AddNewSkills();
        }

        // function for converting a read embedded resource stream into an 8bit array or something like that
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        // load up our new skills
        private void AddNewSkills()
        {
            // create the fighter skill
            var iconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RPGHeim.AssetsEmbedded.fighterSkillIcon.png");
            var iconByteArray = ReadFully(iconStream);
            Texture2D iconAsTexture = new Texture2D(2, 2);
            iconAsTexture.LoadImage(iconByteArray);
            Sprite iconAsSprite = Sprite.Create(iconAsTexture, new Rect(0f, 0f, iconAsTexture.width, iconAsTexture.height), Vector2.zero);
            SkillManager.Instance.AddSkill(new SkillConfig
            {
                Identifier = "github.atravotum.rpgheim.skills.fighter",
                Name = "Fighter",
                Description = "Your current level as a fighter.",
                Icon = iconAsSprite,
                IncreaseStep = 1f
            });
        }

        public static void PrepActionBar(ActionBar actionBar)
        {
            actionBar.AddGUIContentSlot(new GUIContent { text = "1" }, 0);
            actionBar.AddGUIContentSlot(new GUIContent {text = "2" }, 1);
            actionBar.AddGUIContentSlot(new GUIContent {text = "3" }, 2);
            actionBar.AddGUIContentSlot(new GUIContent {text = "4" }, 3);
            actionBar.AddGUIContentSlot(new GUIContent {text = "5" }, 4);
            actionBar.Enable();
        }
    }
}