using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    [Header("References")]
    public bool isSwitching = false;
    public Image background1;
    public Image background2;
    public Animator animator;

    public void switchBackground(Sprite sprite)
    {
        if (!isSwitching)
        {
            background2.sprite = sprite;
            animator.SetTrigger("SwitchFirst");
        } else {
            background1.sprite = sprite;
            animator.SetTrigger("SwitchSecond");
        }
        isSwitching = !isSwitching;
    }

    public void setImage(Sprite sprite)
    {
        if (!isSwitching)
        {
            background1.sprite = sprite;
        } else {
            background2.sprite = sprite;
        }
    }

}
