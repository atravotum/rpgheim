using Jotunn.Managers;
using RPGHeim;
using RPGHeim.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIHotBarManager
{
    public bool _isOverlayActive = true;

    public bool IsOverlayActive
    {
        get
        {
            return _isOverlayActive;
        }

        set
        {
            _isOverlayActive = value;
            if (!value)
            {
                ManaUI.SetToVisible();
            }
            else
            {
                ManaUI.SetToHidden();
            }
            //Jotunn.Logger.LogMessage($"HotbarDeactiveOverlay - {_isOverlayActive}");
            if (HotbarDeactiveOverlay == null) return;
            HotbarDeactiveOverlay.SetActive(_isOverlayActive);
        }
    }

    private bool _isWindowActive = false;
    public void Toggle(bool? overrideActive = null)
    {
        if (overrideActive.HasValue)
        {
            _isWindowActive = overrideActive.Value;
        }
        if (Window == null) return;
        Window.SetActive(_isWindowActive);
    }

    public void Deactivate()
    {
        _isWindowActive = false;
        if (Window == null) return;
        Window.SetActive(false);
    }

    public ManaBubbleUI ManaUI { get; set; } = new ManaBubbleUI();
    public HotbarSelection HotbarSelectionUI { get; set; } = new HotbarSelection();

    public GameObject Window;
    public GameObject HotbarDeactiveOverlay;

    private Image BackgroundImage { get; set; }
    private Button btnAbilityWindow { get; set; }

    public List<AbilityButton> AbilityButtons = new List<AbilityButton>();
    private static List<Ability> activePassives = new List<Ability>();

    private string _lastAbilitySet = "";
    private Text txtAbilitySelected;
    private Ability _activeAbility = null;

    public Ability GetActiveAbility()
    {
        return _activeAbility;
    }

    public void SetActiveSlot(Ability ability)
    {
        _lastAbilitySet = ability.Name;
        txtAbilitySelected.text = _lastAbilitySet;
        UpdateActiveSlot();
    }

    public void UpdateActiveSlot()
    {
        var selectedWasFound = false;
        foreach (var slot in AbilityButtons)
        {
            if (slot.ability == null || string.IsNullOrEmpty(slot.ability.Name))
            {
                slot.SetSelected(false);
                continue;
            }

            if (slot.ability.Name.Equals(_lastAbilitySet))
            {
                selectedWasFound = true;
                slot.SetSelected(true);
                _activeAbility = slot.ability;
            }
            else
            {
                slot.SetSelected(false);
            }
        }

        if (!selectedWasFound)
        {
            _lastAbilitySet = "";
            txtAbilitySelected.text = _lastAbilitySet;
            _activeAbility = new Ability();
        }
    }

    public void UpdatePassives()
    {
        Debug.Log($"updating passive.. in method");
        var checkedActivePassives = AbilityButtons
            .Where(i =>
                i.ability != null &&
                i.ability.Type == AbilityType.Passive &&
                !string.IsNullOrEmpty(i.ability.Name)
            )
            .Select(i => i.ability)
            .ToList();

        Debug.Log($"Got Active passives... {checkedActivePassives.Count}");
        var passivesRemovedFromHotbar = activePassives
            .Where(i =>
                !string.IsNullOrEmpty(i.Name) &&
                // Any names that are missing from the passive statuses.
                !checkedActivePassives.Any(t => t.Name.Equals(i.Name))
            )
            .ToList();

        Debug.Log($"passives active: {string.Join(", ", checkedActivePassives.Select(i => i.Name))}");
        Debug.Log($"passives to remove: {string.Join(", ", passivesRemovedFromHotbar.Select(i => i.Name))}");

        var actuallyActiveStatuses = Player.m_localPlayer.m_seman.GetStatusEffects();
        var actuallyActiveStatusNames = actuallyActiveStatuses.Select(i => i.m_name).ToList();
        Debug.Log($"Active passive statuses(current!!): {string.Join(", ", actuallyActiveStatusNames.Select(i => i))}");

        foreach (var statusToRemove in passivesRemovedFromHotbar)
        {
            if (!actuallyActiveStatusNames.Contains(statusToRemove.Name)) continue;
            statusToRemove.RemovePassive(Player.m_localPlayer);
        }

        foreach (var ability in checkedActivePassives)
        {
            ability.ApplyPassive(Player.m_localPlayer);
        }
        activePassives = checkedActivePassives;
    }

    public void CreateHotBar(int? index = null)
    {
        if (Window != null) return;

        var uiPrefabs = AssetManager.AssetBundles.FirstOrDefault(i => i.AssetBundleName.Equals("ui"));
        var abilityBarPrefab = uiPrefabs.Prefabs.FirstOrDefault(i => i.AssetPath.Contains("AbilityHotBar")).LoadedPrefab;
        Window = (GameObject)RPGHeimMain.Instantiate(abilityBarPrefab, GUIManager.PixelFix.transform);

        try
        {
            var windowRectTrans = Window.GetComponent<RectTransform>();
            if (windowRectTrans != null)
            {
                // The window is positioned in the center of the screen --
                // So we are calculating half the screens.. and subtracting that.. so we are at the bottom corner.
                // The Offsets are how much we push off from the bottom left of the screen.
                var bottomLeftCorner = new Vector2((-1 * (Screen.width / 2)), (-1 * (Screen.height / 2)));
                var hotbarSize = 483;
                windowRectTrans.anchoredPosition = new Vector2(bottomLeftCorner.x + hotbarSize + 100, bottomLeftCorner.y + 120);

                //Window.transform.localEulerAngles = new Vector3(0, 0, -90f);
                Debug.Log($"The anchor position was set. - {windowRectTrans.anchoredPosition.x}, {windowRectTrans.anchoredPosition.y}");
            }
            else
            {
                Debug.Log($"No component found - ");
            }

        }
        catch (Exception ex)
        {
            // Don't do it otherwise.
            Debug.Log($"Failed to set anchor position -- 1");
        }

        foreach (Transform child in Window.transform)
        {
            if (index.HasValue)
            {
                HotbarSelectionUI.Setup(child, index.Value);
            }

            ManaUI.Setup(child);

            if (child.name == "HotbarDeactiveOverlay")
            {
                HotbarDeactiveOverlay = child.gameObject;
            }

            if (child.name == "Image")
            {
                BackgroundImage = child.GetComponent<Image>();
            }

            if (child.name == "txtSelectedProjectile")
            {
                txtAbilitySelected = child.GetComponent<Text>();
                txtAbilitySelected.text = ""; // Nothing.
            }

            if (child.name == "btnAbilityWindow")
            {
                btnAbilityWindow = child.GetComponent<Button>();
                btnAbilityWindow.onClick.AddListener((UnityEngine.Events.UnityAction)(() =>
                {
                    RPGHeimMain.UIAbilityWindowManager.Toggle();
                }));
            }

            if (child.name.Contains("Draggables"))
            {
                foreach (Transform childOfChild in child.transform)
                {
                    Debug.Log(childOfChild.name);

                    if (!childOfChild.name.Contains("AbilitySlot")) continue;

                    var newAbilityButton = new AbilityButton();
                    newAbilityButton.dragParent = childOfChild.gameObject;
                    newAbilityButton.AddUIDraggable();
                    newAbilityButton.dragSlot.IsEmpty = true;
                    newAbilityButton.dragSlot.canvasGroup.alpha = 0;

                    AbilityButtons.Add(newAbilityButton);
                }
            }
        }

        foreach (var abilityButton in AbilityButtons)
        {
            try
            {
                abilityButton.abilityIcons.cdTimer.text = "";
                abilityButton.abilityIcons.fgImage.fillAmount = 1;
            }
            catch (Exception ex)
            {
                Debug.Log($"failed to reset state of ability icons.");
            }
        }
    }

    public void TickCooldowns()
    {
        try
        {
            for (int i = 0; i < AbilityButtons.Count; i++)
            {
                var slot = AbilityButtons[i];
                Ability ability = slot.ability;
                if (ability != null && slot != null)
                {

                    if (ability.CooldownRemaining <= 0)
                    {
                        slot.abilityIcons.cdTimer.text = "";
                        slot.abilityIcons.fgImage.fillAmount = 1;
                    }
                    else
                    {
                        slot.abilityIcons.cdTimer.text = $"{RoundUp(ability.CooldownRemaining, 0)}";
                        //Jotunn.Logger.LogMessage($"cooling down - {1 - ability.CooldownRemaining / ability.CooldownMax }");
                        slot.abilityIcons.fgImage.fillAmount = 1 - (ability.CooldownRemaining / ability.CooldownMax);
                    }
                }
            }
        }
        catch (Exception err) { /* do nothing */ }
    }

    public static double RoundUp(double input, int places)
    {
        double multiplier = Math.Pow(10, Convert.ToDouble(places));
        return Math.Ceiling(input * multiplier) / multiplier;
    }

    public class ManaBubbleUI
    {
        public RectTransform ManaBubbleParent { get; set; }

        public void SetToHidden()
        {
            if (ManaBubbleParent && EndPosition)
            {
                ManaBubbleParent.position = StartPosition.position;
            }
        }

        public void SetToVisible()
        {
            if (ManaBubbleParent && EndPosition)
            {
                ManaBubbleParent.position = EndPosition.position;
            }
        }

        public Image ManaBubbleFill { get; set; }

        public RectTransform StartPosition { get; set; }
        public RectTransform EndPosition { get; set; }
        public Text txtMana { get; set; }

        public void Setup(Transform transform)
        {
            var startPositionTrans = transform.name == "Mana_StartPos";
            var endPositionTrans = transform.name == "ManaBubble_EndPos";

            if (startPositionTrans) StartPosition = (RectTransform)transform;
            if (endPositionTrans) EndPosition = (RectTransform)transform;

            if (transform.name != "ManaBubble") return;

            ManaBubbleParent = (RectTransform)transform;
            var manabubbleFillTrans = transform.Find("ManaBubble_Fill");
            if (manabubbleFillTrans)
            {
                ManaBubbleFill = manabubbleFillTrans.gameObject.GetComponent<Image>();
                Jotunn.Logger.LogMessage($"Found mana bubble fill");
            }

            var txtManaTrans = transform.Find("txtMana");
            if (txtManaTrans)
            {
                txtMana = txtManaTrans.gameObject.GetComponent<Text>();
                Jotunn.Logger.LogMessage($"Found mana bubble fill");
            }
        }
    }

    public class HotbarSelection
    {
        public RectTransform HotbarSelectionParent { get; set; }
        public GameObject Active { get; set; }
        public GameObject Disabled { get; set; }

        public void Setup(Transform transform, int index)
        {
            if (transform.name != "HotbarSelection") return;

            var nameToFind = $"Hotbar_{index + 1}";
            var foundHotbarselection = transform.Find(nameToFind);
            if (foundHotbarselection == null) return;

            Jotunn.Logger.LogMessage($"Found hotbar selection {index}");
            HotbarSelectionParent = (RectTransform)foundHotbarselection;
            var activeTrans = foundHotbarselection.Find("Active");
            if (activeTrans)
            {
                Active = activeTrans.gameObject;
                Active.SetActive(true);
            }

            var disabledTrans = foundHotbarselection.Find("Disabled");
            if (disabledTrans)
            {
                Disabled = disabledTrans.gameObject;
                Disabled.SetActive(false);
            }
        }
    }

    [Serializable]
    public class AbilityIcons
    {
        public Image fgImage;
        public Image bgImage;
        public Text cdTimer;
        // If the Ability is selected (projectile for instance)
        public GameObject selectedBorderImage;
    }


    [Serializable]
    public class AbilityButton
    {
        public GameObject dragParent;
        public AbilityIcons abilityIcons;
        public Ability ability;
        public UIDragSlot dragSlot;

        public void SetSelected(bool selected)
        {
            if (selected)
            {
                abilityIcons.selectedBorderImage.SetActive(true);
            }
            else
            {
                abilityIcons.selectedBorderImage.SetActive(false);
            }
        }

        public void ClearSlot()
        {
            ability = new Ability();
            dragSlot.IsEmpty = true;
            //// -- Remove sprites. -- Don't do it.. makes white square..
            //abilityIcons.bgImage.sprite = null;
            //abilityIcons.fgImage.sprite = null;
            // Hide graphics.
            dragSlot.canvasGroup.alpha = 0;
        }

        public void Set(Ability ability, Sprite icon)
        {
            dragSlot.canvasGroup.alpha = 1;
            dragSlot.IsEmpty = false;
            this.ability = ability;
            abilityIcons.fgImage.sprite = icon;
            abilityIcons.bgImage.sprite = icon;
        }

        public void SwapWith(AbilityButton otherButton)
        {
            // Copy of the things were about to swap about.
            var OtherFgImage = otherButton.abilityIcons.fgImage.sprite;
            var otherBgImage = otherButton.abilityIcons.bgImage.sprite;
            var otherIsEmpty = otherButton.dragSlot.IsEmpty;
            var otherAbility = otherButton.ability;

            if (!dragSlot.IsEmpty)
            {
                Debug.Log($"Dropped not empty - Swap it! {otherButton.dragSlot.gameObject.name}");
                otherButton.Set(ability: ability, icon: abilityIcons.fgImage.sprite);
                //otherButton.dragSlot.swapped = true;
            }
            else
            {
                Debug.Log($"Slot Dropped from is empty. - {otherButton.dragSlot.gameObject.name}");
                // Swapping with empty.
                otherButton.ClearSlot();
            }

            if (!otherIsEmpty)
            {
                Debug.Log($"Dragged not empty - Swap it! {dragSlot.gameObject.name}");
                Set(ability: otherAbility, icon: OtherFgImage);
                dragSlot.swapped = true;
            }
            else
            {
                Debug.Log($"Slot Dropped from is empty. - {dragSlot.gameObject.name}");
                // Swapping with empty.
                ClearSlot();
            }
        }

        public void AddUIDraggable()
        {
            abilityIcons = new AbilityIcons();
            dragSlot = dragParent.GetComponent<UIDragSlot>();
            if (dragSlot == null)
            {
                dragSlot = dragParent.AddComponent<UIDragSlot>();
            }

            foreach (Transform childOfChild in dragParent.transform)
            {
                if (childOfChild.name == "fgImage")
                {
                    abilityIcons.fgImage = childOfChild.GetComponent<Image>();
                }

                if (childOfChild.name == "bgImage")
                {
                    abilityIcons.bgImage = childOfChild.GetComponent<Image>();
                }

                if (childOfChild.name == "cd_timer")
                {
                    abilityIcons.cdTimer = childOfChild.GetComponent<Text>();
                }

                if (childOfChild.name == "imgSelected")
                {
                    abilityIcons.selectedBorderImage = childOfChild.gameObject;
                    abilityIcons.selectedBorderImage.SetActive(false);
                }
            }
        }

        //public Button button { get; set; }
        //public Image fgImage { get; set; }
        //public Image bgImage { get; set; }
        //public Text cdTimer { get; set; }
    }
}