using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Image ForgroundProgressBar;
    public float fillAmount;

    public float FillAmount
    {
        get { return ForgroundProgressBar.fillAmount; }
        set { fillAmount = value; }
    }

    void Start()
    {
        fillAmount = 0.05F;
    }

    void Update()
    {
        ForgroundProgressBar.fillAmount = fillAmount;
    }

    public void SetFillAmount(float amount)
    {
        fillAmount = amount;
        Update();
    }

    public void ResetFillAmount()
    {
        fillAmount = 0.05F;
        Update();
    }

    public void AddFillAmount(float amount)
    {
        fillAmount += amount;
        Update();
    }

    public void RemoveFillAmount(float amount)
    {
        fillAmount -= amount;
        Update();
    }
}
