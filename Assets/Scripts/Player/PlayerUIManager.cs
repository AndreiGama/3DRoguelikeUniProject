using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField]Camera fpsCamera;
    [SerializeField] UIDocument playerUI;
    public VisualElement rootUI;
    public VisualElement ve_Tooltip;
    public Label l_TooltipName;
    public Label l_TooltipDescription;
    Transform lastHitTooltipObject;
    public LayerMask tooltipLayer;

    public VisualElement Healthbar;
    public VisualElement ve_HPBarFill;
    public Label l_HPText;

    PlayerCombatManager playerCombatManager;
    VisualElement itemsTab;
    public VisualTreeAsset crystalItemTemplate;
    bool isItemTabActive;
    private void Awake() {
        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerUI = GetComponentInChildren<UIDocument>();
        rootUI = playerUI.rootVisualElement;
        ve_Tooltip = rootUI.Q("Tooltip");
        l_TooltipName = ve_Tooltip.Q<Label>("L_NameTooltip");
        l_TooltipDescription = ve_Tooltip.Q<Label>("L_DescriptionTooltip");
        Healthbar = rootUI.Q<VisualElement>("Healthbar");
        ve_HPBarFill = Healthbar.Q<VisualElement>("VE_Fill");
        l_HPText = Healthbar.Q<Label>("L_HP");
        itemsTab = rootUI.Q<VisualElement>("ItemsTab");
    }
    private void Start() {
        
        StartCoroutine(LookForTooltip());
        StartCoroutine(CrystalListUpdate());
    }
    IEnumerator LookForTooltip() {
        RaycastHit[] hits = Physics.RaycastAll(fpsCamera.transform.position, fpsCamera.transform.forward, 5f, tooltipLayer, QueryTriggerInteraction.Collide);
        Array.Sort(hits, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

        foreach (RaycastHit hit in hits) {
            ITooltip tooltip = hit.transform.GetComponent<ITooltip>();
            if (tooltip != null && lastHitTooltipObject != hit.transform) {
                    lastHitTooltipObject = hit.transform;
                    tooltip.Tooltip(this);
                    FadeIn();
            }
            else if (lastHitTooltipObject == hit.transform) {
                Debug.Log("it's active do nothing");
            } else {
                Debug.Log("No object");
                FadeOut();
            }
            tooltip = null;
        }
        if(hits.Length == 0) {
            FadeOut();
        }
        yield return new WaitForSeconds(1f);
        Debug.Log("RestartingCoroutine");
        StartCoroutine(LookForTooltip());
    }

    public void SetTooltip(string name, string description) {
        l_TooltipName.text = name;
        l_TooltipDescription.text = description;
    }

    public void FadeIn() {
        ve_Tooltip.AddToClassList("opacityIn");
        ve_Tooltip.RemoveFromClassList("opacityOut");
    }

    public void FadeOut() {
        ve_Tooltip.AddToClassList("opacityOut");
        ve_Tooltip.RemoveFromClassList("opacityIn");
    }

    public void setHPText() {
        l_HPText.text = "HP: " + Mathf.Clamp(playerCombatManager.health, 0, playerCombatManager.maxHealth) + " / " + playerCombatManager.maxHealth;
    }
    
    public void SetHPBar() {
        ve_HPBarFill.style.width = Length.Percent(Mathf.Clamp(playerCombatManager.health / playerCombatManager.maxHealth, 0, playerCombatManager.maxHealth) * 100);
    }

    private void Update() {
        SetHPBar();
        setHPText();
    }

    public void CrystalTabUi() {
        
        if(!isItemTabActive) {
            isItemTabActive = true;
            itemsTab.style.display = DisplayStyle.Flex;
        } else {
            isItemTabActive = false;
            itemsTab.style.display = DisplayStyle.None;
        }
    }

    IEnumerator CrystalListUpdate() {
        itemsTab.Clear();
        foreach (ItemList i in playerCombatManager.items) {
            TemplateContainer crystalItem = crystalItemTemplate.Instantiate();
            crystalItem.style.width = new StyleLength(128);
            crystalItem.style.height = new StyleLength(128);
            crystalItem.style.marginBottom = new StyleLength(25);
            crystalItem.style.marginLeft = new StyleLength(15);
            crystalItem.style.marginRight = new StyleLength(15);
            crystalItem.style.marginTop = new StyleLength(25);
            itemsTab.Add(crystalItem);
            crystalItem.Q<Label>("ItemStacks").text = i.stacks.ToString();
            crystalItem.style.backgroundImage = new StyleBackground(i.item.GiveSprite());
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(CrystalListUpdate());
    }
}
