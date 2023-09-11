using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Table : MonoBehaviour, IInteractable, IKitchenObjectParent, IHasProgress
{
    public enum State
    {
        Idle,
        WaitingToOrder,
        WaitingOrder,
        Eating
    }

    public class OnStateChangedEventArgs : EventArgs
    {
        public State newState;
    }
    
    public class OnRecipeOrderedEventArgs : EventArgs
    {
        public RecipeSO recipeSO;
    }
    
    public class OnRecipeDoneEventArgs : EventArgs
    {
        public RecipeSO recipeSO;
    }

    public static event EventHandler OnAnyObjectPlacedHere;
    public static event EventHandler<OnRecipeOrderedEventArgs> OnAnyRecipeOrdered;
    public static event EventHandler<OnRecipeDoneEventArgs> OnAnyRecipeDone;
    
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<OnRecipeOrderedEventArgs> OnRecipeOrdered;

    [SerializeField] private SelectedObjectVisual SelectedObjectVisual;
    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private Transform kitchenObjectLocationTransform;
    [SerializeField] private Transform chairSittingLocation;
    [SerializeField] private Transform dropOffLocation;

    private KitchenObject kitchenObject;
    private RecipeSO orderedRecipe;
    private Customer customer;
    private bool isOccupied;
    private State state;
    private float timer;
    private float timerMax;
    private float difficultyMultiplier = 1f;

    private void Start()
    {
        DifficultyManager.Instance.OnDifficultyChanged += DifficultyManager_OnDifficultyChanged;
    }

    private void DifficultyManager_OnDifficultyChanged(object sender, DifficultyManager.OnDifficultyChangedEventArgs e)
    {
        difficultyMultiplier = e.newDifficulty == DifficultyManager.Difficulty.Medium ? 0.9f : 0.8f;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            
            case State.WaitingToOrder or State.WaitingOrder:
                HandleOrderTimerLogic();
                break;
            
            case State.Eating:
                HandleEatingTimerLogic();
                break;
        }
    }

    private void HandleOrderTimerLogic()
    {
        timer -= Time.deltaTime;
        InvokeOnProgressChangedEvent(timer / timerMax);

        if (timer <= 0f)
        {
            HandleFailState();
        }
    }
    
    private void HandleEatingTimerLogic()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            HandleEatingDone();
        }
    }
    
    public void Interact(Player interactedPlayer)
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.WaitingToOrder:
                HandleWaitingToOrderInteraction(interactedPlayer);
                break;
            case State.WaitingOrder:
                HandleWaitingOrderInteraction(interactedPlayer);
                break;
        }
    }

    private void HandleWaitingToOrderInteraction(Player player)
    {
        orderedRecipe = OrderManager.Instance.GetRandomRecipe();
        ChangeState(State.WaitingOrder);

        OnRecipeOrdered?.Invoke(this, new OnRecipeOrderedEventArgs
        {
            recipeSO = orderedRecipe
        });
        
        OnAnyRecipeOrdered?.Invoke(this, new OnRecipeOrderedEventArgs
        {
            recipeSO = orderedRecipe
        });
    }

    private void HandleWaitingOrderInteraction(Player player)
    {
        KitchenObject playerKitchenObject = player.GetKitchenObject();
        
        if (playerKitchenObject == null || playerKitchenObject is not Plate)
        {
            return;
        }

        Plate playerPlate = playerKitchenObject.GetComponent<Plate>();

        if (!IsValidPlate(playerPlate.GetIngredientsInPlate()))
        {
            return;
        }
        
        playerKitchenObject.SetKitchenObjectParent(this);
        ChangeState(State.Eating);
        
        InvokeOnRecipeDone(orderedRecipe);
    }

    private void ChangeState(State newState)
    {
        state = newState;
        
        switch (state)
        {
            case State.WaitingToOrder:
                timerMax = 60f * difficultyMultiplier;
                break;
            
            case State.WaitingOrder:
                timerMax = 100f * difficultyMultiplier;
                break;
            
            case State.Eating:
                timerMax = 10f;
                break;
        }
        
        timer = timerMax;
        InvokeOnStateChangedEvent(newState);
    }

    private void HandleFailState()
    {
        customer.LeaveRestaurant();
        ChangeState(State.Idle);
        isOccupied = false;
        InvokeOnRecipeDone(orderedRecipe);
    }

    private void HandleEatingDone()
    {
        GetKitchenObject().DestroySelf();
        customer.LeaveRestaurant();
        ChangeState(State.Idle);
        ResourceManager.Instance.IncreaseMoney(orderedRecipe.price);
        isOccupied = false;
    }
    
    public void StartInteracting()
    {
        SelectedObjectVisual.Show();
    }

    public void StopInteracting()
    {
        SelectedObjectVisual.Hide();
    }
    
    public Transform GetKitchenObjectLocationTransform()
    {
        return kitchenObjectLocationTransform;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        
        if (kitchenObject != null)
        {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public void SetOccupied()
    {
        isOccupied = true;
    }

    public Vector3 GetChairSittingLocation()
    {
        return chairSittingLocation.position;
    }

    private void InvokeOnProgressChangedEvent(float newProgressNormalized)
    {
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = newProgressNormalized
        });
    }
    
    private void InvokeOnStateChangedEvent(State newState)
    {
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            newState = newState,
        });
    }

    private void InvokeOnRecipeDone(RecipeSO recipeSO)
    {
        OnAnyRecipeDone?.Invoke(this, new OnRecipeDoneEventArgs
        {
            recipeSO = recipeSO
        });
    }

    private bool IsValidPlate(List<KitchenObjectSO> ingredientsInPlateList)
    {
        if (ingredientsInPlateList.Count != orderedRecipe.kitchenObjectSOList.Count)
            return false;

        Dictionary<KitchenObjectSO, int> counts = new Dictionary<KitchenObjectSO, int>();

        foreach (KitchenObjectSO ingredientInPlate in ingredientsInPlateList)
        {
            if (counts.ContainsKey(ingredientInPlate))
                counts[ingredientInPlate]++;
            else
                counts[ingredientInPlate] = 1;
        }

        foreach (KitchenObjectSO ingredientInOrderedRecipe in orderedRecipe.kitchenObjectSOList)
        {
            if (!counts.ContainsKey(ingredientInOrderedRecipe))
                return false;

            counts[ingredientInOrderedRecipe]--;

            if (counts[ingredientInOrderedRecipe] < 0)
                return false;
        }

        return true;
    }

    public void SetCustomer(Customer customer)
    {
        this.customer = customer;
        ChangeState(State.WaitingToOrder);
    }

    public Vector3 GetDropOffLocation()
    {
        return dropOffLocation.position;
    }
}