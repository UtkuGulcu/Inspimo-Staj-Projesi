using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Button closePanelButton;
    
    private PanelUIVisual panelUIVisual;

    private void Awake()
    {
        panelUIVisual = GetComponent<PanelUIVisual>();
    }

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(MusicSlider_OnValueChanged); 
        soundSlider.onValueChanged.AddListener(SoundSlider_OnValueChanged);
        closePanelButton.onClick.AddListener(ClosePanel);
        
        

        musicSlider.value = SoundManager.Instance.GetMusicVolume();
        soundSlider.value = SoundManager.Instance.GetMasterVolume();
    }

    private void MusicSlider_OnValueChanged(float value)
    {
        SoundManager.Instance.ChangeMusicVolume(value);
    }

    private void SoundSlider_OnValueChanged(float value)
    {
        SoundManager.Instance.ChangeMasterVolume(value);
    }

    private void ClosePanel()
    {
        panelUIVisual.Close();
    }
}
