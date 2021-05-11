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

        private float cooldownTickAmount = .016f; // 60fps??

        public void Render()
        {
            GUIStyle clonedStyle = new GUIStyle(GUI.skin.box);
            clonedStyle.imagePosition = ImagePosition.ImageAbove;
            GUI.Toolbar(new Rect(xPos, yPos, width, height), 0, contents: slots, clonedStyle);
        }

        public void SetAbility(Ability ability, int i)
        {
            barAbilities[i] = ability;
            slots[i].image = ability.Icon;
        }

        public void CastSlot(int i, Player player)
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

        public void ActivatePassiveAbilities ()
        {
            foreach (Ability ability in barAbilities)
            {
                if (ability.Type == AbilityType.Passive) ability.ApplyPassives(Player.m_localPlayer);
            }
        }

        public void TickCooldowns()
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    Ability ability = barAbilities[i];
                    GUIContent slot = slots[i];
                    if (!ability.Equals(null) && !slot.Equals(null))
                    {
                        if (ability.CooldownRemaining != 0 && ability.CooldownRemaining < cooldownTickAmount)
                        {
                            ability.RemoveCooldown();
                            slot.text = null;
                        }
                        else if (ability.CooldownRemaining > cooldownTickAmount)
                        {
                            ability.ReduceCooldown(cooldownTickAmount);
                            slot.text = Math.Round(ability.CooldownRemaining, 0).ToString();
                        }
                    }
                }
            }
            catch (Exception err){ /* do nothing */ }
        }
    }
}