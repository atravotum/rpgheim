using Jotunn.Entities;
using Jotunn.Managers;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RPGHeim
{
    public class Ability
    {
        public string Name;
        public string Tooltip;
        public AbilityType Type;
        public string Skill;

        public float CooldownMax;
        public float StaminaCost;
        public Texture Icon;

        public float CooldownRemaining;
        public List<Effect> Effects = new List<Effect>();

        public enum AbilityType { Active, Passive };
        public class Effect
        {
            public float DamageValue;
            public HitData.DamageType DamageType;
            public float HealValue;
            public TargetType Target;
            private Texture Icon;

            public List<StatusEffect> StatusEffects = new List<StatusEffect>();
            
            public enum TargetType
            {
                Self,
                NearbyAllies,
                NearbyEnemies,
                All
            };
            public class StatusEffect
            {
                public string Name;
                public string Display_Name;
                public string Display_Description;

                public Dictionary<string, string> Modifiers;
                public Dictionary<string, string> ModSkills;
            }

            public void Execute(ref Texture iconRef)
            {
                this.Icon = iconRef;
                if (this.DamageType.Equals(null) && !this.DamageValue.Equals(null) && this.DamageValue > 0)
                    ApplyDamages();

                if (!this.HealValue.Equals(null) && this.HealValue > 0)
                    ApplyHeals();

                if (!this.StatusEffects.Equals(null) && this.StatusEffects.Count > 0)
                    ApplyStatusEffects();
            }

            private void ApplyDamages()
            {
                Console.print("Applying damages??");
                switch (this.Target)
                {
                    case TargetType.Self:
                        damageSelf(this.DamageType, this.DamageValue);
                    break;

                    case TargetType.NearbyAllies:
                        damageAllies(this.DamageType, this.DamageValue);
                    break;

                    case TargetType.NearbyEnemies:
                        damageEnemies(this.DamageType, this.DamageValue);
                    break;

                    case TargetType.All:
                        damageAll(this.DamageType, this.DamageValue);
                    break;
                }
            }
            private void ApplyHeals()
            {
                switch (this.Target)
                {
                    case Effect.TargetType.Self:
                        healSelf(this.HealValue);
                    break;

                    case Effect.TargetType.NearbyAllies:
                        healAllies(this.HealValue);
                    break;

                    case Effect.TargetType.NearbyEnemies:
                        healEnemies(this.HealValue);
                    break;

                    case Effect.TargetType.All:
                        healAll(this.HealValue);
                    break;
                }
            }
            private void ApplyStatusEffects()
            {
                Console.print("Ok We are going to apply status effects");
                foreach (Effect.StatusEffect statusEffect in this.StatusEffects)
                {
                    switch (this.Target)
                    {
                        case Effect.TargetType.Self:
                            inflictStatusSelf(statusEffect);
                        break;

                        case Effect.TargetType.NearbyAllies:
                            inflictStatusAllies(statusEffect);
                        break;

                        case Effect.TargetType.NearbyEnemies:
                            inflictStatusEnemies(statusEffect);
                        break;

                        case Effect.TargetType.All:
                            inflictStatusAll(statusEffect);
                        break;
                    }
                }
            }

            private void damageSelf(HitData.DamageType damageType, float damageValue)
            {
                HitData.DamageTypes newDamagesTypes = new HitData.DamageTypes();
                newDamagesTypes.GetType().GetProperty(damageType.ToString()).SetValue(newDamagesTypes, damageValue, null);
                HitData newHit = new HitData { m_damage = newDamagesTypes };
                Player.m_localPlayer.Damage(newHit);
            }
            private void damageAllies(HitData.DamageType damageType, float damageValue) {}
            private void damageEnemies(HitData.DamageType damageType, float damageValue) {}
            private void damageAll(HitData.DamageType damageType, float damageValue) {}

            private void healSelf(float healValue) { Player.m_localPlayer.Heal(healValue); }
            private void healAllies(float healValue)
            {
                // heal the players around the caster
                Player player = Player.m_localPlayer;
                List<Player> nearbyPlayers = new List<Player>();
                Player.GetPlayersInRange(player.transform.position, 25f, nearbyPlayers);
                foreach (Player nearbyPlayer in nearbyPlayers)
                {
                    nearbyPlayer.Heal(healValue);
                }

                // Heal the casting player
                healSelf(healValue);
            }
            private void healEnemies(float healValue) {}
            private void healAll(float healValue) {}

            private void inflictStatusSelf(Effect.StatusEffect seConfig)
            {
                inflictStatusOnTarget(seConfig, Player.m_localPlayer);
            }
            private void inflictStatusAllies(Effect.StatusEffect statusEffect) { }
            private void inflictStatusEnemies(Effect.StatusEffect statusEffect) { }
            private void inflictStatusAll(Effect.StatusEffect statusEffect) { }
            private void inflictStatusOnTarget(Effect.StatusEffect seConfig, Player player)
            {
                Console.print("Applying: " + seConfig.Name + ", on: " + player);
                switch (seConfig.Name)
                {
                    case "SE_Stats":
                        Console.print("ok it was an se stats");
                        SE_Stats NewSEStats = ScriptableObject.CreateInstance<SE_Stats>();
                        NewSEStats.m_name = seConfig.Display_Name;
                        NewSEStats.m_tooltip = seConfig.Display_Description;
                        NewSEStats.m_icon = Sprite.Create((Texture2D)this.Icon, new Rect(0f, 0f, this.Icon.width, this.Icon.height), Vector2.zero); ;

                        // Apply Modifiers
                        if (seConfig.Modifiers != null)
                        {
                            Console.print(seConfig.Name + " ok it has modifiers!");
                            foreach (KeyValuePair<string, string> modifier in seConfig.Modifiers)
                            {
                                //PropertyInfo property = NewSEStats.GetType().GetProperty(modifier.Key);
                                //Console.print("Property is: " + property.GetValue(this, null));
                                //Type myType = typeof(SE_Stats);
                                //foreach (var property2 in NewSEStats.GetType().GetProperties())
                                //{
                                    //var type = property2;
                                    //Console.print(type);
                                //}
                                //var propertyType = property.GetType();
                                //Console.print(property.Name);
                                Console.print(modifier.Key);
                                Console.print(modifier.Value);
                                //.SetValue(NewSEStats, )
                            }
                        }

                        //ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(NewSEStats, fixReference: false));

                        // Apply Mod Skills
                        Console.print("Going to try it");
                        player.m_seman.AddStatusEffect(NewSEStats, true);
                        //player.m_seman.AddStatusEffect(NewSEStats.name, true);
                        //Player.m_localPlayer.m_seman.AddStatusEffect("Burning", true);
                        //Player.m_localPlayer.m_seman.AddStatusEffect(NewSEStats, true);
                        //Character character = (Character)Player.m_localPlayer;
                        //character.m_seman.AddStatusEffect(NewSEStats, true);
                        //Humanoid humanoid = (Humanoid)Player.m_localPlayer;
                        //humanoid.m_seman.AddStatusEffect(NewSEStats, true);
                    break;

                    case "SE_CustomModifier":
                        SE_CustomModifier NewSECustomModifier = ScriptableObject.CreateInstance<SE_CustomModifier>();
                        NewSECustomModifier.name = seConfig.Name;
                        NewSECustomModifier.m_name = seConfig.Display_Name;
                        NewSECustomModifier.m_tooltip = seConfig.Display_Description;

                        player.m_seman.AddStatusEffect(NewSECustomModifier, true);
                    break;

                    case "SE_Cleanse":
                        SE_Cleanse NewSECleanse= ScriptableObject.CreateInstance<SE_Cleanse>();
                        NewSECleanse.name = seConfig.Name;
                        NewSECleanse.m_name = seConfig.Display_Name;
                        NewSECleanse.m_tooltip = seConfig.Display_Description;

                        player.m_seman.AddStatusEffect(NewSECleanse, true);
                    break;
                }
            }
        }

        private void AlertPlayer(string text) { MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, text); }
        public void RemoveCooldown() { CooldownRemaining = 0; }
        public void ReduceCooldown(float value)
        {
            float newValue = CooldownRemaining - value;
            CooldownRemaining = newValue < 1 ? 0 : newValue;
        }

        public void CastAbility()
        {
            try
            {
                Player player = Player.m_localPlayer;
                float calculatedCost = StaminaCost.Equals(null) || StaminaCost <= 0 ? 0
                : StaminaCost > 0 && StaminaCost < 1 ? player.m_maxStamina * StaminaCost
                : StaminaCost;
                Console.print("Casting ability: " + Name + " should cost: " + StaminaCost);
                Console.print("So calculated cost is: " + calculatedCost + " and player stam is: " + player.m_stamina);
                if (Type == AbilityType.Passive) AlertPlayer("This ability is passive and does not need to be cast.");
                else if (CooldownRemaining > 0) AlertPlayer("This ability is still on cooldown.");
                else if (player.m_stamina < StaminaCost) AlertPlayer("Not enough stamina to use this ability.");
                else
                {
                    // execute each effect for this ability
                    ExecuteEffects();

                    // apply stamina cost and reset cooldown
                    player.UseStamina(calculatedCost);
                    CooldownRemaining = CooldownMax;
                }
            }
            catch (Exception err)
            {
                Console.print("Error casting ability: " + Name + " => " + err);
            }
        }
        public void ExecuteEffects()
        {
            foreach (Effect effect in Effects)
            {
                effect.Execute(ref this.Icon);
            }
        }
    }
}
/*public class StatusEffect
{
    public string Name;
    public string Display_Name;
    public string Display_Description;
    public Dictionary<string, string> Modifiers;
    public Dictionary<string, string> ModSkills;
}*/