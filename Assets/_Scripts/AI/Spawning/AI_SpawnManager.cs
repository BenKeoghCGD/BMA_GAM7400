using System;
using System.Collections.Generic;
using System.Linq;
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

    private int _pedestrianCount;
    private int _maxPedestrians = 30;

    private int _carCount;
    private int _maxCars = 15;

    private float _spawnTimer;
    public AI_SpawnManager(GameObject customer, GameObject pedestrian, GameObject car)
    {
        _customerSpawnPoints = new List<AI_SpawnPoint>();
        _pedestrianSpawnPoints = new List<AI_SpawnPoint>();
        _carSpawnPoints = new List<AI_SpawnPoint>();

        _customer = customer;
        _pedestrian = pedestrian;
        _car = car;
    }

    public void Update(float deltaTime)
    {
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
            Debug.Log("Cannot spawn Agent of type " + type.ToString());
            return;
        }

        AI_SpawnPoint location = availableSpawnPoints[UnityEngine.Random.Range(0, availableSpawnPoints.Count)];

        switch (type)
        {
            case SpawnPointType.CUSTOMER:

                Agent_Car car = location.gameObject.GetComponentInParent<Agent_Car>();
                location.SpawnCustomer(_customer, car);

                _carCount += 1;
                break;
            case SpawnPointType.CAR:
                location.SpawnAgent(_car);
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
    }
}
