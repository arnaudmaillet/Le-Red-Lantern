using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceController : MonoBehaviour
{

    // Public
    [Header("References")]
    public ChoiceLabelController label;
    public GameController gameController;

    // Private
    private RectTransform rectTransform;
    private Animator animator;
    private float labelHeight = -1;

    void Start()
    {
        animator = GetComponent<Animator>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetupChoice(ChooseScene scene)
    {
        DestroyLabels();
        animator.SetTrigger("Show");
        for (int i = 0; i < scene.labels.Count; i++)
        {
            ChoiceLabelController newLabel = Instantiate(label.gameObject, transform).GetComponent<ChoiceLabelController>();

            if (labelHeight == -1)
            {
                labelHeight = newLabel.GetHeight();
            }
            newLabel.Setup(scene.labels[i], this, CalculateLabelPosition(i, scene.labels.Count));
        }

        Vector2 size = rectTransform.sizeDelta;
        size.y = (scene.labels.Count + 2) * labelHeight;
        rectTransform.sizeDelta = size;
    }

    public void PerformChoice(StoryScene scene, bool isAnimated = true, float pirateBar = 0, float vampireBar = 0, float policeBar = 0)
    {
        gameController.PlayScene(scene, isAnimated: isAnimated, pirateBar: pirateBar, vampireBar: vampireBar, policeBar: policeBar);
        animator.SetTrigger("Hide");
    }

    private float CalculateLabelPosition(int index, int count)
    {
        if (count % 2 == 0)
        {
            if (index < count / 2)
            {
                return labelHeight * (count / 2 - index - 1) + labelHeight / 2;
            }
            else
            {
                return -1 * (labelHeight * (index - count / 2 ) + labelHeight / 2);
            }
        }
        else
        {
            if (index < count / 2)
            {
                return labelHeight * (count / 2 - index);
            }
            else if (index > count / 2)
            {
                return -1 * (labelHeight * (index - count / 2));
            }
            else return 0;
        }
    }

    private void DestroyLabels()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
