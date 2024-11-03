using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Litter")]
public class LitterData : ScriptableObject
{
    public string litterName;
    public Mesh litterModel;
    public Material litterMaterial;
    public int litterSize;
    public LitterType litterType;
    public AudioClip litterSound;
}
