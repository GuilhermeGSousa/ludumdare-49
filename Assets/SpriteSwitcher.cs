using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite sadSprite;
    [SerializeField] private Sprite angrySprite;

    public void SetSprite(bool isSad)
    {
        if(isSad) image.sprite = sadSprite;
        else image.sprite = angrySprite;
    }
}
