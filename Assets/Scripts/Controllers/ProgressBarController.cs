using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{

    // tableau d'image pour les progress bar
    public Image[] ForgroundProgressBar;

    // min and max values for the progress bar
    public float min = 0.05f;
    public float max = 1f;

    public void Start()
    {
        // set the min and max values for the progress bar
        for (int i = 0; i < ForgroundProgressBar.Length; i++)
        {
            ForgroundProgressBar[i].fillAmount = min;
        }
    }

    public void UpdateFillAmount(float amount, int index)
    {
        if (amount < 0)
        {
            amount = Mathf.Abs(amount);
            if (ForgroundProgressBar[index].fillAmount - amount < min)
                ForgroundProgressBar[index].fillAmount = min;
            else
                ForgroundProgressBar[index].fillAmount -= amount;
        }
        else{
            if (ForgroundProgressBar[index].fillAmount + amount > max)
                ForgroundProgressBar[index].fillAmount = max;
            else
                ForgroundProgressBar[index].fillAmount += amount;
        }
    }

    public void AddFillAmount(float amount, int index)
    {
        ForgroundProgressBar[index].fillAmount += amount;
    }

    public void RemoveFillAmount(float amount, int index)
    {
        ForgroundProgressBar[index].fillAmount -= amount;
    }

    public float getFillAmount(int index)
    {
        return ForgroundProgressBar[index].fillAmount;
    }

    // public void AddFillAmountVampire(float amount)
    // {
    //     fillAmount[0] += amount;
    //     Update();
    // }

    // public void AddFillAmountCops(float amount)
    // {
    //     fillAmount[1] += amount;
    //     Update();
    // }

    // public void AddFillAmountPirate(float amount)
    // {
    //     fillAmount[2] += amount;
    //     Update();
    // }

    // public void RemoveFillAmountVampire(float amount)
    // {
    //     fillAmount[0] -= amount;
    //     Update();
    // }

    // public void RemoveFillAmountCops(float amount)
    // {
    //     fillAmount[1] -= amount;
    //     Update();
    // }

    // public void RemoveFillAmountPirate(float amount)
    // {
    //     fillAmount[2] -= amount;
    //     Update();
    // }

}
