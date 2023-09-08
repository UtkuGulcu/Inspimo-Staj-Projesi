using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : Counter, IHasProgress
{
    public enum State
    {
        Idle,
        Frying,
        Burning,
        Burned
    }

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    [SerializeField] private List<FryingRecipeSO> validFryingRecipes;

    [SerializeField] private List<BurningRecipeSO> burningRecipes;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private State state;
    private float timer;
    private float timerMax;

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                HandleFryingState();
                break;
            case State.Burning:
                HandleBurningState();
                break;
            case State.Burned:
                break;
        }
    }
    
    private void HandleFryingState()
    {
        timer += Time.deltaTime;
        InvokeProgressChangedEvent(timer / fryingRecipeSO.fryingTimerMax);
        
        if (timer > fryingRecipeSO.fryingTimerMax)
        {
            timer = 0f;
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
            ChangeState(State.Burning);
        }
    }

    private void HandleBurningState()
    {
        timer += Time.deltaTime;
        InvokeProgressChangedEvent(timer / burningRecipeSO.burningTimerMax);

        if (timer > burningRecipeSO.burningTimerMax)
        {
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
            ChangeState(State.Burned);
        }
    }

    public override void Interact(Player interactedPlayer)
    {
        switch (state)
        {
            case State.Idle:
                HandleIdleInteraction(interactedPlayer);
                break;
            case State.Frying:
                HandleFryingInteraction(interactedPlayer);
                break;
            case State.Burning:
                HandleBurningInteraction(interactedPlayer);
                break;
            case State.Burned:
                HandleBurningInteraction(interactedPlayer);
                break;
        }
    }

    private void HandleIdleInteraction(Player interactedPlayer)
    {
        KitchenObject kitchenObjectPlayer = interactedPlayer.GetKitchenObject();
        
        if (kitchenObjectPlayer == null || !HasRecipeWithInput(kitchenObjectPlayer.GetKitchenObjectSO()))
        {
            return;
        }

        fryingRecipeSO = GetFryingRecipeWithInput(kitchenObjectPlayer.GetKitchenObjectSO());
        burningRecipeSO = GetBurningRecipeWithInput(fryingRecipeSO.output);
        
        kitchenObjectPlayer.SetKitchenObjectParent(this);
        timer = 0;
        ChangeState(State.Frying);
    }
    
    private void HandleFryingInteraction(Player interactedPlayer)
    {
        KitchenObject kitchenObjectPlayer = interactedPlayer.GetKitchenObject();

        if (kitchenObjectPlayer == null)
        {
            timer = 0f;
            GetKitchenObject().SetKitchenObjectParent(interactedPlayer);
            ChangeState(State.Idle);
            InvokeProgressChangedEvent(0f);
        }
        else if (HasRecipeWithInput(kitchenObjectPlayer.GetKitchenObjectSO()))
        {
            kitchenObjectPlayer.SetParentNull();
            GetKitchenObject().SetKitchenObjectParent(interactedPlayer);
            kitchenObjectPlayer.SetKitchenObjectParent(this);
            timer = 0f;
            ChangeState(State.Idle);
            InvokeProgressChangedEvent(0f);
        }
    }

    private void HandleBurningInteraction(Player interactedPlayer)
    {
        KitchenObject kitchenObjectPlayer = interactedPlayer.GetKitchenObject();

        if (kitchenObjectPlayer == null)
        {
            return;
        }

        if (kitchenObjectPlayer.TryGetComponent(out Plate plate))
        {
            if (plate.TryToAddToPlate(GetKitchenObject().GetKitchenObjectSO()))
            {
                GetKitchenObject().DestroySelf();
                ChangeState(State.Idle);
                InvokeProgressChangedEvent(0f);
            }
        }
    }

    private FryingRecipeSO GetFryingRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (FryingRecipeSO recipe in validFryingRecipes)
        {
            if (recipe.input == kitchenObjectSO)
            {
                return recipe;
            }
        }

        return null;
    }
    
    private BurningRecipeSO GetBurningRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        foreach (BurningRecipeSO recipe in burningRecipes)
        {
            if (recipe.input == kitchenObjectSO)
            {
                return recipe;
            }
        }

        return null;
    }   

    private bool HasRecipeWithInput(KitchenObjectSO kitchenObjectSO)
    {
        FryingRecipeSO _fryingRecipeSO = GetFryingRecipeWithInput(kitchenObjectSO);
        return _fryingRecipeSO != null;
    }

    private void ChangeState(State newState)
    {
        state = newState;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
        {
            state = state
        });
    }

    private void InvokeProgressChangedEvent(float newProgressNormalized)
    {
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = newProgressNormalized
        });
    }
}
