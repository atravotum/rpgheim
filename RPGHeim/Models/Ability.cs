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
        private float CooldownRemaining;
        public float StaminaCost;

        public HitData.DamageType DamageType;
        public AbilityTarget DamageTarget;
        public float DamageValue;
        public string DamageStatusEffect;

        public AbilityTarget HealTarget;
        public float HealValue;
        public string HealStatusEffect;

        public void CastAbility(Player castingPlayer)
        {
            if (Type == AbilityType.Passive) AlertPlayer("This ability is passive and does not need to be cast.");
            else if (CooldownRemaining > 0) AlertPlayer("This ability is still on cooldown.");
            else if (castingPlayer.m_stamina < StaminaCost) AlertPlayer("Not enough stamina to use this ability.");
            else
            {
              ResetCooldown();
            }
        }

        public float GetRemainingCooldown() { return CooldownRemaining; }
        public void ResetCooldown() { CooldownRemaining = CooldownMax; }
        private void AlertPlayer(string text) { MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, text); }
    }
}
