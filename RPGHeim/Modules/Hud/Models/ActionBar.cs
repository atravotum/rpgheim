using UnityEngine;

namespace RPGHeim
{
    public class ActionBar : MonoBehaviour
    {
        private bool barEnabled = false;

        public int xPos;
        public int yPos;
        public int width;
        public int height;

        private GUIContent[] slots = new GUIContent[5];

        public void AddGUIContentSlot (GUIContent slotContent, int slotInt)
        {
            slots[slotInt] = slotContent;
        }

        public void Enable ()
        {
            barEnabled = true;
        }

        public void Disable()
        {
            barEnabled = false;
        }

        public void Render()
        {
            if (barEnabled) GUI.Toolbar(new Rect(xPos, yPos, width, height), 0, contents: slots);
        }
    }
}