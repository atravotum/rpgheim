using System.Collections.Generic;
using UnityEngine;

namespace RPGHeim
{
    public class Ability
    {
        public enum AbilityType { Active, Passive };
        public class Effect
        {
            public enum TargetType
            {
                Self,
                NearbyAllies,
                NearbyEnemies,
                All
            };

            public float DamageValue;
            public HitData.DamageType DamageType;
            public float HealValue;
            public TargetType Target;
            public List<StatusEffect> StatusEffects = new List<StatusEffect>();

            public class StatusEffect
            {
                public string Name;
                public string Display_Name;
                public string Display_Description;
                public Dictionary<string, string> Modifiers;
                public Dictionary<string, string> ModSkills;
            }
        }

        public string Name;
        public string Tooltip;
        public AbilityType Type;
        public string Skill;

        public float CooldownMax;
        public float StaminaCost;
        public Texture Icon;

        public float CooldownRemaining;
        public List<Effect> Effects = new List<Effect>();

        public void CastAbility(Player player)
        {
            /*float calculatedCost = StaminaCost.Equals(null) || StaminaCost <= 0 ? 0 
                : StaminaCost > 0 && StaminaCost < 1 ? player.m_maxStamina * StaminaCost
                : StaminaCost;
            Console.print("Casting ability: " + Name + " should cost: " + StaminaCost);
            Console.print("So calculated cost is: " + calculatedCost + " and player stam is: " + player.m_stamina);
            if (Type == AbilityType.Passive) AlertPlayer("This ability is passive and does not need to be cast.");
            else if (CooldownRemaining > 0) AlertPlayer("This ability is still on cooldown.");
            else if (player.m_stamina < StaminaCost) AlertPlayer("Not enough stamina to use this ability.");
            else
            {
                if (DamageType != null && DamageTarget != null && DamageValue != null)
                    ApplyDamages(player);
                if (HealValue != null && HealTarget != null)
                    ApplyHeals(player);
                if (PassiveEffectTarget != null && PassiveEffect != null)
                    ApplyPassives(player);

                // apply stamina cost and reset cooldown
                player.UseStamina(calculatedCost);
                CooldownRemaining = CooldownMax;
            }*/
        }
        public void ApplyDamages (Player player) { }
        public void ApplyHeals (Player player)
        {
            /*switch (HealTarget)
            {
                case AbilityTarget.Self:
                    player.Heal(HealValue);
                    if (HealStatusEffect != null) player.m_seman.AddStatusEffect(HealStatusEffect);
                break;

                case AbilityTarget.NearbyAllies:
                    List<Player> nearbyPlayers = new List<Player>();
                    Player.GetPlayersInRange(player.transform.position, 25f, nearbyPlayers);
                    foreach (Player nearbyPlayer in nearbyPlayers)
                    {
                        nearbyPlayer.Heal(HealValue);
                        if (HealStatusEffect != null) nearbyPlayer.m_seman.AddStatusEffect(HealStatusEffect);
                    }
                break;
            }*/
        }
        public void ApplyPassives (Player player) {
            /*Console.print("Ok I'm in passive apply...");
            switch (PassiveEffectTarget)
            {
                case AbilityTarget.Self:
                    player.m_seman.AddStatusEffect(PassiveEffect, true);
                break;

                case AbilityTarget.NearbyAllies:
                    Console.print("Ok I'm in passive nearby..." + PassiveEffect);
                    List<Player> nearbyPlayers = new List<Player>();
                    Player.GetPlayersInRange(player.transform.position, 25f, nearbyPlayers);
                    foreach (Player nearbyPlayer in nearbyPlayers)
                    {
                        nearbyPlayer.m_seman.AddStatusEffect(PassiveEffect, true);
                    }
                break;
            }*/
        }
        public float GetRemainingCooldown () { return CooldownRemaining; }
        public void ReduceCooldown (float value)
        {
            float newValue = CooldownRemaining - value;
            CooldownRemaining = newValue < 1 ? 0 : newValue;
        }
        public void RemoveCooldown()
        {
            CooldownRemaining = 0;
        }
        public void AlertPlayer (string text) { MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, text); }
    }
}
