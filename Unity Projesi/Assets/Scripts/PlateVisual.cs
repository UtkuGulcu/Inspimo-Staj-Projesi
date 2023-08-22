using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateVisual : MonoBehaviour
{
    [Serializable]
    public struct GameObject_SO
    {
        public GameObject gameObject;
        public KitchenObjectSO KitchenObjectSo;
    }

    [SerializeField] private List<GameObject_SO> validIngredientsInPlateList;
    
    private Plate plate;

    private void Awake()
    {
        plate = transform.root.GetComponent<Plate>();
    }

    private void Start()
    {
        plate.OnObjectAdded += Plate_OnObjectAdded;
    }

    private void Plate_OnObjectAdded(object sender, Plate.OnObjectAddedEventArgs e)
    {
        foreach (GameObject_SO ingredient in validIngredientsInPlateList)
        {
            if (e.addedObject == ingredient.KitchenObjectSo)
            {
                ingredient.gameObject.SetActive(true);
            }   
        }
    }
}
