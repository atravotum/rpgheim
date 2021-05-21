using System;
using UnityEngine;

namespace RPGHeim
{
    class ActionBar
    {
        private GUIContent[] slots = {
            new GUIContent { image = null, text = null },
            new GUIContent { image = null, text = null },
            new GUIContent { image = null, text = null },
            new GUIContent { image = null, text = null },
            new GUIContent { image = null, text = null },
        };
        private Ability[] barAbilities = new Ability[5];

        public int xPos;
        public int yPos;
        public int width;
        public int height;

        public float cooldownTickRate = .9f;
        public bool isEnabled = false;

        public void Render () {
            if (isEnabled)
            {
                GUIStyle clonedStyle = new GUIStyle(GUI.skin.box);
                clonedStyle.imagePosition = ImagePosition.ImageAbove;
                GUI.Toolbar(new Rect(xPos, yPos, width, height), 0, contents: slots, clonedStyle);
            }
        }

        public void SetAbility (Ability ability, int i)
        {
            barAbilities[i] = ability;
            slots[i].image = ability.Icon;
        }

        public void CastSlot (int i, Player player)
        {
            try
            {
                barAbilities[i].CastAbility(player);
            }
            catch (Exception err)
            {
                Console.print(err);
            }
        }

        public void TickPassives ()
        {
            if (isEnabled)
            {
                try
                {
                    foreach (Ability ability in barAbilities)
                    {
                        if (ability.Type == Ability.AbilityType.Passive) ability.ApplyPassives(Player.m_localPlayer);
                    }
                }
                catch (Exception err) { /* Do Nothing */ }
            }
        }

        public void TickCooldowns ()
        {
            if (isEnabled)
            {
                try
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Ability ability = barAbilities[i];
                        GUIContent slot = slots[i];
                        if (!ability.Equals(null) && !slot.Equals(null))
                        {
                            ability.ReduceCooldown(cooldownTickRate);
                            float newCooldown = (float)Math.Round(ability.CooldownRemaining, 0);

                            if (newCooldown == 0)
                                slot.text = null;
                            else
                                slot.text = newCooldown.ToString();
                        }
                    }
                }
                catch (Exception err) { /* do nothing */ }
            }
        }
    }
}