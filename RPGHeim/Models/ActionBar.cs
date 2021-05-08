using UnityEngine;

namespace RPGHeim
{
    class ActionBar
    {
        private GUIContent[] slots = {
            new GUIContent { image = null },
            new GUIContent { image = null },
            new GUIContent { image = null },
            new GUIContent { image = null },
            new GUIContent { image = null },
        };
        private Ability[] abilities;

        public int xPos;
        public int yPos;
        public int width;
        public int height;

        public void Render()
        {
            GUI.Toolbar(new Rect(xPos, yPos, width, height), 0, contents: slots, GUI.skin.box);
        }

        public void SetAbility(Ability ability, int i)
        {
            abilities[i] = ability;
            slots[i].image = ability.icon;
        }
    }
}