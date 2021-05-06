using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPGHeim
{
    public class ActionBar : MonoBehaviour
    {
        private List<GameObject> slots = new List<GameObject>();

        public void CreateSlot ()
        {
            // prep stuff needed to render the canvas
            GameObject m_gameObject;
            GameObject m_text_gameObject;
            Canvas m_canvas;
            Text m_text;
            RectTransform m_rectTransform;

            // initialize the canvas props/component
            m_gameObject = new GameObject();
            m_gameObject.name = "TestCanvas";
            m_gameObject.AddComponent<Canvas>();

            // configure the canvas
            m_canvas = m_gameObject.GetComponent<Canvas>();
            m_canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            m_gameObject.AddComponent<CanvasScaler>();
            m_gameObject.AddComponent<GraphicRaycaster>();

            // initialize the game object for the text, set it's parent and give it a name
            m_text_gameObject = new GameObject();
            m_text_gameObject.transform.parent = m_gameObject.transform;
            m_text_gameObject.name = "SlotText";

            // Add a text component to the text game object and set it's font/text/size
            m_text = m_text_gameObject.AddComponent<Text>();
            m_text.font = (Font)Resources.Load("MyFont");
            m_text.text = "wobble";
            m_text.fontSize = 100;

            // position the text
            m_rectTransform = m_text.GetComponent<RectTransform>();
            m_rectTransform.localPosition = new Vector3(0, 0, 0);
            m_rectTransform.sizeDelta = new Vector2(400, 200);

            m_canvas.enabled = true;

            Console.print("\n\n\n\nHmmm ok so: " + slots + " " + slots.Count);
            slots.Add(m_gameObject);
            Console.print("\n\n\n\n\nHmmm ok so: " + slots + " " + slots.Count);
        }
    }
}