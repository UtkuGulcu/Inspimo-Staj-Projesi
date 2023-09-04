using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUIManager : MonoBehaviour
{
    [SerializeField] private Plate plate;
    [SerializeField] private GameObject IconTemplate;

    private void Start()
    {
        plate.OnObjectAdded += Plate_OnObjectAdded;
    }

    private void OnDisable()
    {
        plate.OnObjectAdded -= Plate_OnObjectAdded;
    }
    
    private void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void Plate_OnObjectAdded(object sender, Plate.OnObjectAddedEventArgs e)
    {
        GameObject spawnedIcon = Instantiate(IconTemplate, transform);
        spawnedIcon.GetComponent<PlateIcon>().SetupVisual(e.addedObject.icon);
    }
}
