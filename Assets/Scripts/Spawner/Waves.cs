using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Waves", menuName = "ScriptableObjects/Wavess", order =1)]
public class Waves : ScriptableObject
{
    [field: SerializeField]
    public GameObject[] gunsInWave{get; private set;}
    [field: SerializeField]
    public float timeBeforeThisWave { get; private set;}
    [field:SerializeField]
    public float NumberToSpawn { get; private set;}
}
