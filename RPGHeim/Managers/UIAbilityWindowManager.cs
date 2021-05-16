using Jotunn.Managers;
using RPGHeim;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIAbilityWindowManager
{
    public GameObject Window { get; set; }

    [SerializeField]
    public GameObject AbilitySlotPrefab;

    // bgWindow
    public Image BackgroundImage { get; set; }
    // fgVikingPanel
    public Image ForegroundImage { get; set; }

    public GameObject ContentHolder { get; set; }

    public bool IsActive { get; set; } = false;

    public List<AbilitySlot> AbilitySlots { get; set; } = new List<AbilitySlot>()
    {
        new AbilitySlot()
        {
            Name = "Battle Cry",
            Description = $"Increases damage of the Fighter and allies around him by 20% for 15 seconds, costing 20 stamina. 20 second cooldown.",
            ClassType = "Warrior (Lv 1)",
            SkillLevel = "Lv 34.32"
        },
        new AbilitySlot()
        {
            Name = "Rally Cry",
            Description = $"Increases stamina regeneration of the Fighter and allies around him by 20% for 5 seconds, costing 20 stamina. 10 second cooldown.",
            ClassType = "Warrior (Lv 2)",
            SkillLevel = "Lv 12.67"
        },
        new AbilitySlot()
        {
            Name = "Enhance Block",
            Description = $"Increases block and parry power of the Fighter for 30 seconds. 45 second cooldown.",
            ClassType = "Warrior (Lv 3)",
            SkillLevel = "Lv 5.91"
        },
        new AbilitySlot()
        {
            Name = "Dual Wielding",
            Description = $"Dual Wielding w/Parry Bonus (for player only).",
            ClassType = "Warrior (Lv 4)",
            SkillLevel = "Lv 0"
        },
        new AbilitySlot()
        {
            Name = "Combat Expertise",
            Description = $"Instantly increases all combat skills to 100 for 30 seconds, at no cost. 60 second cooldown.",
            ClassType = "Warrior (Lv 5)",
            SkillLevel = "Lv 0"
        }
    };

    public void SetAvailableAbilities(List<AbilitySlot> abilities)
    {
        AbilitySlots = abilities;
        Window = null;
        var isWindowOpen = IsActive;
        Toggle(shutWindow: true);
    }

    private bool ApplyToggle()
    {
        IsActive = !IsActive;
        Window.SetActive(IsActive);
        return IsActive;
    }

    public void Toggle(bool shutWindow = false, bool openWindow = false)
    {
        if (Window == null)
        {
            var uiPrefabs = AssetManager.AssetBundles.FirstOrDefault(i => i.AssetBundleName.Equals("ui"));
            var skillMenu = uiPrefabs.Prefabs.FirstOrDefault(i => i.AssetPath.Contains("SkillWindow")).LoadedPrefab;
            AbilitySlotPrefab = uiPrefabs.Prefabs.FirstOrDefault(i => i.AssetPath.Contains("AbilitySlot")).LoadedPrefab;
            Window = (GameObject)RPGHeimMain.Instantiate(skillMenu, GUIManager.PixelFix.transform);

            try
            {
                var windowRectTrans = Window.GetComponent<RectTransform>();
                if (windowRectTrans != null)
                {
                    // The window is positioned in the center of the screen --
                    // So we are calculating half the screens.. and subtracting that.. so we are at the bottom corner.
                    // The Offsets are how much we push off from the bottom left of the screen.
                    var bottomLeftCorner = new Vector2((-1 * (Screen.width / 2)), (-1 * (Screen.height / 2)));
                    var skillWindowSize = new Vector2(430, 500);
                    windowRectTrans.anchoredPosition = new Vector2(-(skillWindowSize.x / 2), bottomLeftCorner.y + skillWindowSize.y / 2 + 200);

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
            }

            foreach (Transform child in Window.transform)
            {
                if (child.name == "bgWindow")
                {
                    BackgroundImage = child.GetComponent<Image>();
                }

                if (child.name == "fgVikingPanel")
                {
                    ForegroundImage = child.GetComponent<Image>();
                }
            }

            var gridLayout = Window.GetComponentInChildren<GridLayoutGroup>();

            if (gridLayout != null)
            {
                // Do not touch the child transform traversing.. its specific to the game object.
                ContentHolder = gridLayout.gameObject;
                for (int i = 0; i < AbilitySlots.Count; i++)
                {
                    var abilitySlot = AbilitySlots[i];
                    abilitySlot.gameObject = (GameObject)RPGHeimMain.Instantiate(AbilitySlotPrefab, ContentHolder.transform);
                    abilitySlot.gameObject.name = $"{abilitySlot.Name}_Ability";
                    if (abilitySlot.Elements == null)
                    {
                        abilitySlot.Elements = new AbilitySlotElements();
                    }

                    // foreach child transform in the ability slot we just created - Were going to find things in it.. and assign our object.
                    foreach (Transform child in abilitySlot.gameObject.transform)
                    {
                        if (child.name == "AbilityName")
                        {
                            abilitySlot.Elements.txtAbilityName = child.GetComponent<Text>();
                            if (abilitySlot.Elements.txtAbilityName != null)
                            {
                                abilitySlot.Elements.txtAbilityName.text = abilitySlot.Name;
                            }
                        }

                        if (child.name == "AbilityLevel")
                        {
                            abilitySlot.Elements.txtAbilityLevel = child.GetComponent<Text>();
                            if (abilitySlot.Elements.txtAbilityLevel != null)
                            {
                                abilitySlot.Elements.txtAbilityLevel.text = abilitySlot.SkillLevel;
                            }
                        }

                        if (child.name == "AbilityDescription")
                        {
                            abilitySlot.Elements.txtAbilityDescription = child.GetComponent<Text>();
                            if (abilitySlot.Elements.txtAbilityDescription != null)
                            {
                                abilitySlot.Elements.txtAbilityDescription.text = abilitySlot.Description;
                            }
                        }

                        if (child.name == "AbilityClass")
                        {
                            abilitySlot.Elements.txtAbilityClass = child.GetComponent<Text>();
                            if (abilitySlot.Elements.txtAbilityClass != null)
                            {
                                abilitySlot.Elements.txtAbilityClass.text = abilitySlot.ClassType;
                            }
                        }

                        if (child.name == "Button")
                        {
                            abilitySlot.Elements.btnAbility = child.GetComponent<Button>();
                        }

                        if(child.name == "ObtainAbility")
                        {
                            abilitySlot.ObtainAbilityElements = new ObtainAbilityElements();
                            abilitySlot.ObtainAbilityElements.Populate(child);
                        }

                        if (child.name == "AbilityDrag")
                        {
                            var hasDragComponent = child.GetComponent<UIDragSlot>();
                            if (hasDragComponent == null)
                            {
                                var uiDrag = child.gameObject.AddComponent<UIDragSlot>();
                                // This is an ability slot, so we only copy and not swap like hotbar does.
                                uiDrag.CopyOnDropOnly = true;
                            }
                            child.name = $"{abilitySlot.Name}_Draggable";
                            foreach (Transform childOfChild in child.transform)
                            {
                                if (childOfChild.name == "fgImage")
                                {
                                    abilitySlot.Elements.fgImage = childOfChild.GetComponentInChildren<Image>();
                                }

                                if (childOfChild.name == "bgImage")
                                {
                                    abilitySlot.Elements.bgImage = childOfChild.GetComponentInChildren<Image>();
                                }
                            }
                        }

                        if (child.name == "AbilityIcon")
                        {
                            abilitySlot.Elements.imgAbilityIcon = child.GetComponentInChildren<Image>();
                            Debug.Log($"found imgAbilityIcon  {abilitySlot.Elements.imgAbilityIcon != null}");
                        }
                    }
                }
            }

            // Touchable - if needed.
            try
            {
                foreach (var abilitySlot in AbilitySlots)
                {
                    // Assigning values of those slots loaded.
                    // I do set some values above as well - prob should be moved down.
                    if (abilitySlot.Icon != null)
                    {
                        abilitySlot.Elements.fgImage.sprite = abilitySlot.Icon;
                        abilitySlot.Elements.bgImage.sprite = abilitySlot.Icon;
                    }
                    else if (!string.IsNullOrEmpty(abilitySlot.IconName))
                    {
                        var sprite = AssetManager.GetResourceSprite(abilitySlot.IconName);
                        abilitySlot.Elements.fgImage.sprite = sprite;
                        abilitySlot.Elements.bgImage.sprite = sprite;
                    }

                    abilitySlot.Elements.fgImage.fillAmount = 1;
                    abilitySlot.Elements.txtAbilityDescription.text = abilitySlot.Description;
                    abilitySlot.Elements.txtAbilityName.text = abilitySlot.Name;
                    abilitySlot.Elements.txtAbilityClass.text = abilitySlot.ClassType;
                    abilitySlot.Elements.txtAbilityLevel.text = abilitySlot.SkillLevel;

                }

            }
            catch (Exception ex)
            {
                Debug.Log($"failed to set on click listener to ability window btn");
            }
        }

        if (shutWindow)
        {
            IsActive = false;
            Window.SetActive(IsActive);
            return;
        }
        if (openWindow)
        {
            IsActive = true;
            Window.SetActive(IsActive);
            return;
        }
        ApplyToggle();
    }
}


[Serializable]
public class AbilitySlot
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ClassType { get; set; }
    public string SkillLevel { get; set; }

    public Sprite Icon { get; set; }
    public string IconName { get; set; }

    public GameObject gameObject { get; set; }

    // These are created when the window = null.
    public AbilitySlotElements Elements { get; set; }
    public ObtainAbilityElements ObtainAbilityElements { get; set; }
    public Ability Ability { get; set; }
}


[Serializable]
public class ObtainAbilityElements
{
    public void AbilityObtained()
    {
        obtainAbilityHolder.SetActive(false);
    }

    public void Populate(Transform parent)
    {
        obtainAbilityHolder = parent.gameObject;
        foreach (Transform child in parent.transform)
        {
            if(child.name == "ObtainAbilityButton")
            {
                btnObtainAbility = child.GetComponent<Button>();
                // Get the text from the child of this.
                txtObtainAbilityButtonText = btnObtainAbility.GetComponentInChildren<Text>();
                btnObtainAbility.onClick.AddListener((UnityEngine.Events.UnityAction)(() =>
                {
                    // Determine cost.
                    // Check if player has cost.
                    // Allow this or not.
                    AbilityObtained();
                }));
            }

            if(child.name.Equals("CostText"))
            {
                txtObtainCost = child.GetComponent<Text>();
            }
        }
    }

    public GameObject obtainAbilityHolder { get; set; }
    public Button btnObtainAbility { get; set; }
    public Text txtObtainAbilityButtonText { get; set; }
    public Text txtObtainCost { get; set; } // Just text atm.
}

[Serializable]
public class AbilitySlotElements
{
    public Text txtAbilityName { get; set; }
    public Text txtAbilityDescription { get; set; }
    public Text txtAbilityLevel { get; set; }
    public Text txtAbilityClass { get; set; }
    public Button btnAbility { get; set; }
    public Image imgAbilityIcon { get; set; }

    public Image bgImage { get; set; }
    public Image fgImage { get; set; }

}