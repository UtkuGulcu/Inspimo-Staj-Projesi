using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIcon : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetupVisual(Sprite sprite)
    {
        image.sprite = sprite;
        gameObject.SetActive(true);
    }
}
