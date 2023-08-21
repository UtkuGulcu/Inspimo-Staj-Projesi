using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObjectVisual : MonoBehaviour
{
    [SerializeField] private GameObject selectedObjectVisuals;

    public void Show()
    {
        selectedObjectVisuals.SetActive(true);
    }

    public void Hide()
    {
        selectedObjectVisuals.SetActive(false);
    }
}
