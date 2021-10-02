using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HeartElementUI : MonoBehaviour
{
    public HeartElementUI next;
    [SerializeField] Image fillImage;
    [Range(0, 1)] float fillAmount;

    public void SetHealth(float health)
    {
        fillAmount = health;
        fillImage.fillAmount = health;
        health--;

        if(next != null)
        {
            next.SetHealth(health);
        }
    }
}
