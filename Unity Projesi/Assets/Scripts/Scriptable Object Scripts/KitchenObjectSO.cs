using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/KitchenObjectSO")]
public class KitchenObjectSO : ScriptableObject
{
   public Sprite icon;
   public string kitchenObjectName;
   public int price;
   public GameObject prefab;
}
