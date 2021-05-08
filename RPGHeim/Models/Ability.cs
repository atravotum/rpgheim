using UnityEngine;

namespace RPGHeim
{
    class Ability
    {
        public string name;
        public string tooltip;
        public string type;
        public float ticksForCooldown;
        public float ticksUntilCooldown;
        public float staminaCost;
        public Texture icon;
        public StatusEffect passiveEffect;
    }
}
