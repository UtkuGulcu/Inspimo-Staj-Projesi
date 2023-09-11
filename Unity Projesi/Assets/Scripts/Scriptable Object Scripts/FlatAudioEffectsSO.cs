using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FlatAudioEffectsSO")]
public class FlatAudioEffectsSO : ScriptableObject
{
    public GameObject customerSpawn;
    public GameObject takeOrder;
    public GameObject payOrder;
    public GameObject[] buttonClick;
    public GameObject doorOpen;
    public GameObject doorClose;
}
