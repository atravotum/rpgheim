using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGHeim
{
    public enum AbilityType { Active, Passive };
    public enum AbilityTarget { Self, CursorAny, CursorAlly, NearbyAllies, CursorEnemy, NearbyEnemies };
    public class Ability
    {
        public string Name;
        public string Tooltip;
        public Sprite Icon;

        public AbilityType Type;

        // Private member with a Public Get/Set specific in set.
        private float _cooldownMax;
        public float CooldownMax
        {
            get
            {
                return _cooldownMax;
            }
            set 
            {
                _cooldownMax = value;
                LastUsedAt = Time.time - _cooldownMax; // Let them use it on load.
            }
        }

        public float CooldownRemaining { get { return (LastUsedAt + CooldownMax) - Time.time; } }
        public float LastUsedAt { get; private set; }

        public float StaminaCost;

        public HitData.DamageType? DamageType = null;
        public AbilityTarget? DamageTarget = null;
        public float? DamageValue = null;
        public string DamageStatusEffect = null;

        public AbilityTarget? HealTarget = null;
        public float? HealValue = null;
        public string HealStatusEffect = null;

        public AbilityTarget? PassiveEffectTarget;
        public string PassiveEffect;

        public void CastAbility()
        {
            Player player = Player.m_localPlayer;
            float calculatedCost = CalculateStaminaCost();
            if (Type == AbilityType.Passive) AlertPlayer("This ability is passive and does not need to be cast.");
            else if (LastUsedAt + CooldownMax > Time.time) AlertPlayer($"This ability is still on cooldown. {CooldownRemaining}/s");
            else if (player.m_stamina < StaminaCost) AlertPlayer("Not enough stamina to use this ability.");
            else
            {
                if (DamageType.HasValue && DamageTarget.HasValue && DamageValue.HasValue)
                    ApplyDamages();
                if (HealValue.HasValue && HealTarget.HasValue)
                    ApplyHeals();
                if (PassiveEffectTarget.HasValue && !string.IsNullOrEmpty(PassiveEffect))
                    ApplyPassive(player);

                // Apply stamina cost and reset cooldown
                player.UseStamina(calculatedCost);
                // Record the time we used the ability.
                LastUsedAt = Time.time;
            }
        }

        private float CalculateStaminaCost()
        {
            if (StaminaCost <= 0)
                return 0;

            if (StaminaCost < 1)
            {
                // Percentage cost.
                return Player.m_localPlayer.m_maxStamina * StaminaCost;
            }
            // Flat cost.
            return StaminaCost;
        }

        public void ApplyDamages() { }
        public void ApplyHeals() { }
        public void ApplyPassive(Player player)
        {
            switch (PassiveEffectTarget)
            {
                case AbilityTarget.Self:
                    player.m_seman.AddStatusEffect(PassiveEffect);
                    break;

                case AbilityTarget.NearbyAllies:
                    List<Player> nearbyPlayers = new List<Player>();
                    Player.GetPlayersInRange(player.transform.position, 25f, nearbyPlayers);
                    foreach (Player nearbyPlayer in nearbyPlayers)
                    {
                        nearbyPlayer.m_seman.AddStatusEffect(PassiveEffect);
                    }
                    break;
            }
        }

        public void RemovePassive(Player player)
        {
            switch (PassiveEffectTarget)
            {
                case AbilityTarget.Self:
                    player.m_seman.RemoveStatusEffect(PassiveEffect);
                    break;

                case AbilityTarget.NearbyAllies:
                    List<Player> nearbyPlayers = new List<Player>();
                    Player.GetPlayersInRange(player.transform.position, 25f, nearbyPlayers);
                    foreach (Player nearbyPlayer in nearbyPlayers)
                    {
                        nearbyPlayer.m_seman.RemoveStatusEffect(PassiveEffect);
                    }
                    break;
            }
        }

        public void AlertPlayer(string text) { MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, text); }
    }
}
