using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{

    public List<ProgressBarController> bars = new List<ProgressBarController>();
    int vampireIndex = 0;
    int policeIndex = 1;
    int pirateIndex = 2;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public float GetVampireBar()
    {
        return bars[vampireIndex].fillAmount;
    }

    public float GetPoliceBar()
    {
        return bars[policeIndex].fillAmount;
    }

    public float GetPirateBar()
    {
        return bars[pirateIndex].fillAmount;
    }

    public void SetVampireBar(float value)
    {
        Debug.Log(bars[vampireIndex].fillAmount);
        bars[vampireIndex].SetFillAmount(value);
    }

    public void SetPoliceBar(float value)
    {
        Debug.Log(bars[policeIndex].fillAmount);
        bars[policeIndex].SetFillAmount(value);
    }

    public void SetPirateBar(float value)
    {
        Debug.Log(bars[pirateIndex].fillAmount);
        bars[pirateIndex].SetFillAmount(value);
    }
}
