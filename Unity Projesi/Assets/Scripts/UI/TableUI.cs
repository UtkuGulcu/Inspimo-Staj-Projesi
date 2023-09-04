using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TableUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private TMP_Text recipeNameText;

    private IHasProgress hasProgress;
    private Table table;

    private void Awake()
    {
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        table = hasProgressGameObject.GetComponent<Table>();
    }

    private void Start()
    {
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        table.OnStateChanged += Table_OnStateChanged;
        table.OnRecipeOrdered += Table_OnRecipeOrdered;
            
        slider.value = 1f;
        Hide();
    }

    private void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        slider.value = e.progressNormalized;

        if (e.progressNormalized == 0f || e.progressNormalized == 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
    
    private void Table_OnStateChanged(object sender, Table.OnStateChangedEventArgs e)
    {
        switch (e.newState)
        {
            case Table.State.Idle:
                Hide();
                break;
            case Table.State.WaitingToOrder:
                recipeNameText.text = "?";
                Show();
                break;
            case Table.State.WaitingOrder:
                Show();
                break;
            case Table.State.Eating:
                Hide();
                break;
        }
    }

    private void Table_OnRecipeOrdered(object sender, Table.OnRecipeOrderedEventArgs e)
    {
        recipeNameText.text = e.recipeSO.name;
    }
    
    

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
