using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public string Name;
    public List<KitchenObjectSO> kitchenObjectSOList;
    public int price;
}
