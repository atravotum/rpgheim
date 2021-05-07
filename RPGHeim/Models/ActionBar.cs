using UnityEngine;

namespace RPGHeim
{
    public class ActionBar : MonoBehaviour
    {
        private GUIContent[] slots = {
            new GUIContent { text = "1", image = null },
            new GUIContent { text = "2", image = null  },
            new GUIContent { text = "3", image = null  },
            new GUIContent { text = "4", image = null  },
            new GUIContent { text = "5", image = null  },
        };

        public int xPos;
        public int yPos;
        public int width;
        public int height;

        public void Render()
        {
            GUI.Toolbar(new Rect(xPos, yPos, width, height), 0, contents: slots);
        }

        public void UpdateIconImg(int i, Texture icon)
        {
            Console.print("\n\n\n\n\n\n\n\n WHHSDFHASDFHASDHFSDFHASDFHAFSD \n\n\n\n\n\n\n\n");
            slots[i].image = icon;
        }
    }
}