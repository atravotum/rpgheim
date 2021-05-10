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
                
                ApplyCosts();
                ResetCooldown();
            }
        }

        public void ApplyDamages () { }
        public void ApplyHeals () { }
        public void ApplyPassives (Player player) {
            if (PassiveEffectTarget == AbilityTarget.Self)
            {
                Console.print("Ok I am applying the '" + PassiveEffect + "' effect for '" + Name +"'");
                player.m_seman.AddStatusEffect(PassiveEffect);
            }
                
        }

        public void ApplyCosts ()
        {

        }

        public float GetRemainingCooldown () { return CooldownRemaining; }
        public void ResetCooldown () { CooldownRemaining = CooldownMax; }
        public void AlertPlayer (string text) { MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, text); }
    }
}
