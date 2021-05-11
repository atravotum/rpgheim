using System.Collections.Generic;
using UnityEngine;

namespace RPGHeim
{
    public enum AbilityType { Active, Passive };
    public enum AbilityTarget { Self, CursorAny, CursorAlly, NearbyAllies, CursorEnemy, NearbyEnemies };
    class Ability
    {
        public string Name;
        public string Tooltip;
        public Texture Icon;

        public AbilityType Type;
        public float CooldownMax;
        public float CooldownRemaining;
        public float StaminaCost;

        public HitData.DamageType DamageType;
        public AbilityTarget DamageTarget;
        public float DamageValue;
        public string DamageStatusEffect;

        public AbilityTarget HealTarget;
        public float HealValue;
        public string HealStatusEffect;

        public AbilityTarget PassiveEffectTarget;
        public string PassiveEffect;

        public void CastAbility(Player player)
        {
            float calculatedCost = StaminaCost.Equals(null) || StaminaCost <= 0 ? 0 
                : StaminaCost > 0 && StaminaCost < 1 ? player.m_maxStamina * StaminaCost
                : StaminaCost;
            if (Type == AbilityType.Passive) AlertPlayer("This ability is passive and does not need to be cast.");
            else if (CooldownRemaining > 0) AlertPlayer("This ability is still on cooldown.");
            else if (player.m_stamina < StaminaCost) AlertPlayer("Not enough stamina to use this ability.");
            else
            {
                if (DamageType != null && DamageTarget != null && DamageValue != null)
                    ApplyDamages();
                if (HealValue != null && HealTarget != null)
                    ApplyHeals();
                if (PassiveEffectTarget != null && PassiveEffect != null)
                    ApplyPassives(player);

                // apply stamina cost and reset cooldown
                player.UseStamina(calculatedCost);
                CooldownRemaining = CooldownMax;
            }
        }

        public void ApplyDamages () { }
        public void ApplyHeals () { }
        public void ApplyPassives (Player player) {
            switch (PassiveEffectTarget)
            {
                case AbilityTarget.Self:
                    player.m_seman.AddStatusEffect(PassiveEffect);
                    break;

                case AbilityTarget.NearbyAllies:
                    List<Player> nearbyPlayers = new List<Player>();
                    Player.GetPlayersInRange(player.transform.position, 15f, nearbyPlayers);
                    foreach (Player nearbyPlayer in nearbyPlayers)
                    {
                        player.m_seman.AddStatusEffect(PassiveEffect);
                    }
                break;
            }
        }

        public float GetRemainingCooldown () { return CooldownRemaining; }
        public void ReduceCooldown (float value)
        {
            float newValue = CooldownRemaining - value;
            CooldownRemaining = newValue < 0 ? 0 : newValue;
        }
        public void RemoveCooldown()
        {
            CooldownRemaining = 0;
        }
        public void AlertPlayer (string text) { MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, text); }
    }
}
