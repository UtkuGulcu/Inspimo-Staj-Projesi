using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundReferences : MonoBehaviour
{
    public static SoundReferences Instance { get; private set; }
    
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private FlatAudioEffectsSO flatAudioEffectsSO;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are multiple Sound Managers!!!");
            Destroy(this);
        }
    }

    private void Start()
    {
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.OnPickedSomething += Player_OnPickedSomething;
        Counter.OnAnyObjectPlacedHere += Counter_OnAnyObjectPlacedHere;
        IngredientCounter.OnAnyIngredientInteracted += Counter_OnAnyObjectPlacedHere;
        Table.OnAnyObjectPlacedHere += Table_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectPlacedHere;
        CustomerManager.Instance.OnCustomerSpawned += CustomerManager_OnCustomerSpawned;
        Door.Instance.OnDoorOpened += Door_OnDoorOpened;
        Door.Instance.OnDoorClosed += Door_OnDoorClosed;
        Table.OnAnyRecipeOrdered += Table_OnAnyRecipeOrdered;
        Table.OnAnyOrderPaid += Table_OnAnyOrderPaid;
        Table.OnAnyOrderFailed += Table_OnAnyOrderFailed;
        IngredientItemUI.OnIngredientBought += OnBuyButtonClicked;
        UpgradePanelUI.OnUpgradeButtonClicked += OnBuyButtonClicked;
        ShopUI.OnAnyChangePanelButtonDown += OnAnyUIButtonClicked;
    }

    private void Table_OnAnyOrderFailed(object sender, EventArgs e)
    {
        GameObject spawnedSound = Instantiate(flatAudioEffectsSO.failOrder);
        Destroy(spawnedSound, 1f);
    }

    private void OnAnyUIButtonClicked(object sender, EventArgs e)
    {
        int randomIndex = Random.Range(0, flatAudioEffectsSO.buttonClick.Length);
        GameObject spawnedSound = Instantiate(flatAudioEffectsSO.buttonClick[randomIndex]);
        Destroy(spawnedSound, 1f);
    }

    private void OnBuyButtonClicked(object sender, EventArgs e)
    {
        GameObject spawnedSound = Instantiate(flatAudioEffectsSO.payOrder);
        Destroy(spawnedSound, 2f);
    }

    private void Table_OnAnyOrderPaid(object sender, EventArgs e)
    {
        GameObject spawnedSound = Instantiate(flatAudioEffectsSO.payOrder);
        Destroy(spawnedSound, 2f);
    }

    private void Table_OnAnyRecipeOrdered(object sender, Table.OnRecipeOrderedEventArgs e)
    {
        GameObject spawnedSound = Instantiate(flatAudioEffectsSO.takeOrder);
        Destroy(spawnedSound, 2f);
    }

    private void Door_OnDoorClosed(object sender, EventArgs e)
    {
        GameObject spawnedSound = Instantiate(flatAudioEffectsSO.doorClose);
        Destroy(spawnedSound, 2f);
    }

    private void Door_OnDoorOpened(object sender, EventArgs e)
    {
        GameObject spawnedSound = Instantiate(flatAudioEffectsSO.doorOpen);
        Destroy(spawnedSound, 2f);
    }

    private void CustomerManager_OnCustomerSpawned(object sender, EventArgs e)
    {
        GameObject spawnedSound = Instantiate(flatAudioEffectsSO.customerSpawn);
        Destroy(spawnedSound, 2f);
    }

    private void TrashCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        SoundManager.Instance.PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void Table_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        Table table = sender as Table;
        SoundManager.Instance.PlaySound(audioClipRefsSO.objectDrop, table.transform.position);
    }

    private void Counter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        Counter counter = sender as Counter;
        SoundManager.Instance.PlaySound(audioClipRefsSO.objectDrop, counter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e)
    {
        Player player = sender as Player;
        SoundManager.Instance.PlaySound(audioClipRefsSO.objectPickup, player.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        SoundManager.Instance.PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    public void PlayFootstepsSound(Vector3 position, float volume = 1f)
    {
        SoundManager.Instance.PlaySound(audioClipRefsSO.footstep, position, volume);
    }
}
