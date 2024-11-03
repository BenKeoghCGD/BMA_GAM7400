using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.UIElements;
using UnityEngine;

public enum referenceLayers
{
    INTERACTABLE,
    AI,
    CAR
}
public class ReferenceManager : MonoBehaviour
{
    [SerializeField]
    private LitterDataList _data;
    public LitterDataList LitterData => _data;

    [SerializeField]
    private GameObject _customerPrefab;
    public GameObject CustomerPrefab => _customerPrefab;

    [SerializeField]
    private GameObject _pedestrianPrefab;
    public GameObject PedestrianPrefab => _pedestrianPrefab;

    [SerializeField]
    private GameObject _carPrefab;
    public GameObject CarPrefab => _carPrefab;

    [SerializeField]
    private LayerMask _aiLayer;
    [SerializeField]
    private LayerMask _interactableLayer;
    private Dictionary<referenceLayers, LayerMask> _layerMasks;

    [SerializeField]
    private int _maxCarCount;
    public int MaxCarCount => _maxCarCount;

    [SerializeField]
    private int _maxPedestrianCount;
    public int MaxPedestrianCount => _maxPedestrianCount;

    [SerializeField]
    private int _maxLitterAmount;
    public int MaxLitterAmount => _maxLitterAmount;
    public void Init()
    {
        _layerMasks = new Dictionary<referenceLayers, LayerMask>();
        _layerMasks.Add(referenceLayers.AI, _aiLayer);
        _layerMasks.Add(referenceLayers.INTERACTABLE, _interactableLayer);
    }
    public LayerMask GetLayerMask(referenceLayers tag)
    {
        if(_layerMasks.ContainsKey(tag) == false)
        {
            Debug.LogError("Reference manager missing layermask for " +  tag.ToString());
            return 0;
        }

        return _layerMasks[tag];
    }
    public int GetLayerFromMask(referenceLayers tag)
    {
        if (_layerMasks.ContainsKey(tag) == false)
        {
            Debug.LogError("Reference manager missing layermask for " + tag.ToString());
            return 0;
        }

        return Mathf.RoundToInt(Mathf.Log(_layerMasks[tag].value, 2));
    }
}
