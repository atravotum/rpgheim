using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPGHeim
{
    public class UIDragSlot : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
    {

        public CanvasGroup canvasGroup;
        private RectTransform rectTransform;
        private Canvas canvas;
        private Transform initialParent;
        public Vector2 startAnchoredPosition;
        public bool CopyOnDropOnly = false;
        public bool swapped = false;
        public float alphaSetting { get; set; }
        public bool IsEmpty = false;

        public void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
            }

            Transform testCanvasTransform = transform.parent;
            do
            {
                testCanvasTransform = testCanvasTransform.parent;
                canvas = testCanvasTransform.GetComponent<Canvas>();
            } while (canvas == null);
            initialParent = this.transform.parent;
        }

        private void Start()
        {
            startAnchoredPosition = rectTransform.anchoredPosition;
        }

        /// <summary>
        /// This method will be called on the start of the mouse drag
        /// </summary>
        /// <param name="eventData">mouse pointer event data</param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsEmpty)
            {
                return;
            }

            if (this.transform.parent != canvas)
            {
                // set the parent to the canvas.. so it can move around -- and also move it to the bottom.. so its on the most toppest ui.
                this.transform.SetParent(canvas.transform);
                this.transform.SetAsLastSibling();
            }

            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = false;
        }

        /// <summary>
        /// This method will be called during the mouse drag
        /// </summary>
        /// <param name="eventData">mouse pointer event data</param>
        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        /// <summary>
        /// This method will be called at the end of mouse drag
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (this.transform.parent != initialParent)
            {
                // set the parent to the canvas.. so it can move around -- and also move it to the bottom.. so its on the most toppest ui.
                this.transform.SetParent(initialParent);
                this.transform.SetAsLastSibling();
            }
            rectTransform.anchoredPosition = startAnchoredPosition;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            if (CopyOnDropOnly)
            {
                return;
            }
            if (swapped)
            {
                swapped = false;
                Debug.Log($"rejected! {gameObject.name}");
                return;
            }
            IsEmpty = true;
            canvasGroup.alpha = 0;
            Debug.Log($"Set alphas to {alphaSetting} {gameObject.name}");

            var slotDroppedOnAbility = RPGHeimMain.UIHotBarManager.AbilityButtons.FirstOrDefault(i => i.dragParent.name == gameObject.name);
            slotDroppedOnAbility.ability = new Ability(); // Empty it out.
            Debug.Log($"update passives -- add/remove.");
            RPGHeimMain.UIHotBarManager.UpdatePassives();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var slotDraggedFromAbility = RPGHeimMain.UIHotBarManager.AbilityButtons.FirstOrDefault(i => i.dragParent.name == eventData.pointerDrag.name);
            var slotDropTarget = eventData.pointerDrag;
            if (slotDropTarget != null)
            {
                var slotDroppedOnAbility = RPGHeimMain.UIHotBarManager.AbilityButtons.FirstOrDefault(i => i.dragParent.name == gameObject.name);
                var slotDroppedOnTarget = slotDropTarget.GetComponent<UIDragSlot>();
                var skillName = slotDroppedOnTarget.name.Split('_').FirstOrDefault();
                var abilityFromSkillsManager = RPGHeimMain.UIAbilityWindowManager.AbilitySlots.FirstOrDefault(i => i.Name == skillName);

                var abilityDuplicateHotBarCheck = RPGHeimMain.UIHotBarManager
                    .AbilityButtons
                    .FirstOrDefault(i => 
                        i.ability != null && 
                        !string.IsNullOrEmpty(i.ability.Name) && 
                        i.ability.Name.Equals(skillName) && 
                        i.dragSlot.gameObject != slotDroppedOnAbility.dragSlot.gameObject
                    );

                //Debug.Log($"Dragged From: {slotDroppedOnTarget.name}");
                //Debug.Log($"Dropped On To: {gameObject.name}");
                //Debug.Log($"Skill name: {skillName}");
                if (slotDroppedOnTarget != null || abilityFromSkillsManager != null)
                {

                    if(abilityDuplicateHotBarCheck != null)
                    {
                        Debug.Log($"found duplicate: {abilityDuplicateHotBarCheck.ability.Name}");
                        // Clear it out make it empty.
                        abilityDuplicateHotBarCheck.ClearSlot();                        
                    }

                    if (abilityFromSkillsManager != null)
                    {
                        Debug.Log($"Dropped not empty - Swap it! {slotDroppedOnTarget.gameObject.name}");
                        slotDroppedOnAbility.Set(abilityFromSkillsManager.Ability, abilityFromSkillsManager.Elements.fgImage.sprite);
                    }
                    else
                    {
                        if (slotDroppedOnAbility != null && slotDraggedFromAbility != null)
                        {
                            slotDraggedFromAbility.SwapWith(slotDroppedOnAbility);

                            //// We have to copy it over before we can swap, since one is getting overridden.

                            //var draggedFromfgImage = slotDraggedFromAbility.abilityIcons.fgImage.sprite;
                            //var draggedFrombgImage = slotDraggedFromAbility.abilityIcons.bgImage.sprite;
                            //var draggedFromIsEmpty = slotDroppedOnTarget.IsEmpty;
                            //var draggedFromAbility = slotDraggedFromAbility.ability;

                            //var droppedTofgImage = slotDroppedOnAbility.abilityIcons.fgImage.sprite;
                            //var droppedTobgImageFrom = slotDroppedOnAbility.abilityIcons.bgImage.sprite;
                            //var droppedTofromIsEmpty = IsEmpty;
                            //var droppedTofromAbility = slotDroppedOnAbility.ability;

                            //slotDraggedFromAbility.abilityIcons.fgImage.sprite = slotDroppedOnAbility.abilityIcons.fgImage.sprite;
                            //slotDraggedFromAbility.abilityIcons.bgImage.sprite = slotDroppedOnAbility.abilityIcons.bgImage.sprite;

                            //slotDroppedOnAbility.abilityIcons.fgImage.sprite = draggedFromfgImage;
                            //slotDroppedOnAbility.abilityIcons.bgImage.sprite = draggedFrombgImage;

                            //if (!droppedTofromIsEmpty)
                            //{
                            //    Debug.Log($"Dropped not empty - Swap it! {slotDroppedOnTarget.gameObject.name}");
                            //    slotDroppedOnTarget.swapped = true;
                            //    slotDroppedOnTarget.canvasGroup.alpha = 1;
                            //    slotDroppedOnTarget.IsEmpty = false;
                            //    slotDroppedOnAbility.ability = draggedFromAbility;
                            //}
                            //else
                            //{
                            //    Debug.Log($"Slot Dropped from is empty. - {slotDroppedOnTarget.gameObject.name}");
                            //    // Swapping with empty.
                            //    slotDroppedOnTarget.IsEmpty = true;
                            //    slotDroppedOnTarget.canvasGroup.alpha = 0;
                            //    slotDroppedOnAbility.ability = new Ability();
                            //}

                            //if (!draggedFromIsEmpty)
                            //{
                            //    Debug.Log($"Dragged not empty - Swap it! {gameObject.name}");
                            //    // Swapping with empty.
                            //    //swapped = true;
                            //    canvasGroup.alpha = 1;
                            //    IsEmpty = false;
                            //    slotDraggedFromAbility.ability = droppedTofromAbility;
                            //}
                            //else
                            //{
                            //    Debug.Log($"Slot Dropped from is empty. - {slotDroppedOnTarget.gameObject.name}");
                            //    // Swapping with empty.
                            //    IsEmpty = true;
                            //    canvasGroup.alpha = 0;
                            //    slotDraggedFromAbility.ability = new Ability();
                            //}
                        }
                    }

                    Debug.Log($"update passives -- add/remove.");
                    RPGHeimMain.UIHotBarManager.UpdatePassives();
                }
            }
        }
    }
}
