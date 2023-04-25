using UnityEngine;
using UnityEngine.EventSystems;
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

    private float vampireValue;
    private float copsValue;
    private float pirateValue;

    private ProgressBarController progressBarController;


    void Awake()
    {
        progressBarController = FindObjectOfType<ProgressBarController>();
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.color = defaultColor;
    }

    public float GetHeight()
    {
        return textMesh.rectTransform.sizeDelta.y * textMesh.rectTransform.localScale.y;
    }

    public void Setup(ChooseScene.ChooseLabel label, ChoiceController controller, float y)
    {
        scene = label.nextScene;
        isAnimated = label.fade;
        textMesh.text = label.label;
        this.controller = controller;
        Vector3 position = textMesh.rectTransform.localPosition;
        position.y = y;
        textMesh.rectTransform.localPosition = position;

        vampireValue = label.vampireValue;
        copsValue = label.copsValue;
        pirateValue = label.pirateValue;
        // Debug.Log("label: " + label.label + "Pirate: " + pirateValue + " Vampire: " + vampireValue + " Cops: " + copsValue);
}

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.PerformChoice(scene, isAnimated: isAnimated, pirateValue: pirateValue, vampireValue: vampireValue, copsValue: copsValue);
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
