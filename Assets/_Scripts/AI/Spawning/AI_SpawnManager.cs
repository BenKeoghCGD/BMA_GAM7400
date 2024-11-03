using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class AI_SpawnManager
{
    private GameObject _customer;
    private GameObject _pedestrian;
    private GameObject _car;

    private List<AI_SpawnPoint> _customerSpawnPoints;
    private List<AI_SpawnPoint> _pedestrianSpawnPoints;
    private List<AI_SpawnPoint> _carSpawnPoints;

    private LayerMask _layerMask;

    private int _pedestrianCount;
    private int _maxPedestrians;

    private int _carCount;
    private int _maxCars;

    private float _spawnTimer;

    private bool _isReady;
    public AI_SpawnManager()
    {
        _customerSpawnPoints = new List<AI_SpawnPoint>();
        _pedestrianSpawnPoints = new List<AI_SpawnPoint>();
        _carSpawnPoints = new List<AI_SpawnPoint>();
    }

    public void Init()
    {
        ReferenceManager manager = GameManager.GetReferenceManager();

        _layerMask = manager.GetLayerMask(referenceLayers.AI);
        _maxPedestrians = manager.MaxPedestrianCount;
        _maxCars = manager.MaxCarCount;

        _customer = manager.CustomerPrefab;
        _pedestrian = manager.PedestrianPrefab;
        _car = manager.CarPrefab;

        _isReady = true;    
    }
    public void Update(float deltaTime)
    {
        if(_isReady == false)
        {
            return;
        }

        _spawnTimer += deltaTime;

        if(_spawnTimer > 3)
        {
            SpawnAgent((SpawnPointType)UnityEngine.Random.Range(0, 3));
            _spawnTimer = 0;
        }
    }
    public void SpawnAgent(SpawnPointType type)
    {
        List<AI_SpawnPoint> availableSpawnPoints = null;

        switch (type)
        {
            case SpawnPointType.CUSTOMER:
                availableSpawnPoints = _customerSpawnPoints.Where(s => s.isActive == true).Where(s => s.isUsed == false).ToList();

                if (availableSpawnPoints == null || availableSpawnPoints.Count == 0)
                {
                    if (_carCount >= _maxCars)
                    {
                        break;
                    }

                    type = SpawnPointType.CAR;
                    availableSpawnPoints = _carSpawnPoints;
                }

                break;

            case SpawnPointType.CAR:
                if (_carCount >= _maxCars)
                {
                    break;
                }

                availableSpawnPoints = _carSpawnPoints;
                break;
            case SpawnPointType.PEDESTRIAN:
                if (_pedestrianCount >= _maxPedestrians)
                {
                    break;
                }

                availableSpawnPoints = _pedestrianSpawnPoints;
                break;
        }

        if(availableSpawnPoints == null || availableSpawnPoints.Count == 0)
        {
            //Debug.Log("Cannot spawn Agent of type " + type.ToString());
            return;
        }

        AI_SpawnPoint location = availableSpawnPoints[UnityEngine.Random.Range(0, availableSpawnPoints.Count)];

        switch (type)
        {
            case SpawnPointType.CUSTOMER:

                Agent_Car car = location.gameObject.GetComponentInParent<Agent_Car>();
                location.SpawnCustomer(_customer, car);

                break;
            case SpawnPointType.CAR:

                Collider[] hitData = Physics.OverlapSphere(location.transform.position, 5, _layerMask);

                if (hitData.Length > 0)
                {
                    //Debug.Log("No space");
                    return;
                }

                location.SpawnAgent(_car);
                _carCount += 1;
                break;
            case SpawnPointType.PEDESTRIAN:
                location.SpawnAgent(_pedestrian);

                _pedestrianCount += 1;
                break;

        }
    }

    public void AddSpawnPoint(AI_SpawnPoint spawnPoint, SpawnPointType type)
    {
        switch(type)
        {
            case SpawnPointType.CUSTOMER:
                _customerSpawnPoints.Add(spawnPoint);
                break;
            case SpawnPointType.CAR:
                _carSpawnPoints.Add(spawnPoint);
                break;
            case SpawnPointType.PEDESTRIAN:
                _pedestrianSpawnPoints.Add(spawnPoint);
                break;
        }
    }

    public void Decrement(SpawnPointType type)
    {
        //Debug.Log(_carCount);
        switch (type)
        {
            case SpawnPointType.CUSTOMER:
                break;
            case SpawnPointType.CAR:
                _carCount -= 1;
                break;
            case SpawnPointType.PEDESTRIAN:
                _pedestrianCount -= 1;
                break;
        }
        //Debug.Log(_carCount);
    }
}
