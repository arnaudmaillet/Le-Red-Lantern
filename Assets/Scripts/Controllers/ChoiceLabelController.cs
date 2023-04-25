using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class ChoiceLabelController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Public
    [Header("References")]
    public Color defaultColor;
    public Color hoverColor;

    // Private
    private StoryScene scene;
    private TextMeshProUGUI textMesh;
    private ChoiceController controller;
    private bool isAnimated;

    private float pirateBar;
    private float vampireBar;
    private float policeBar;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.color = defaultColor;
    }

    public float GetHeight()
    {
        return textMesh.rectTransform.sizeDelta.y * textMesh.rectTransform.localScale.y;
    }


    public float getPirateBar()
    {
        return pirateBar;
    }

    public float getVampireBar()
    {
        return vampireBar;
    }

    public float getPoliceBar()
    {
        return policeBar;
    }

    public void Setup(ChooseScene.ChooseLabel label, ChoiceController controller, float y)
    {
        scene = label.nextScene;
        isAnimated = label.fade;
        textMesh.text = label.label;
        pirateBar = label.pirateBar;
        vampireBar = label.vampireBar;
        policeBar = label.policeBar;
        this.controller = controller;
        Vector3 position = textMesh.rectTransform.localPosition;
        position.y = y;
        textMesh.rectTransform.localPosition = position;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.PerformChoice(scene, isAnimated: isAnimated, pirateBar: pirateBar, vampireBar: vampireBar, policeBar: policeBar);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMesh.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.color = defaultColor;
    }
}
