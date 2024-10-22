using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AI_SpawnManager
{
    private GameObject _customer;
    private GameObject _pedestrian;

    private List<AI_SpawnPoint> _customerSpawnPoints;
    private List<AI_SpawnPoint> _pedestrianSpawnPoints;

    private int _pedestrianCount;
    private int _maxPedestrians = 20;

    private float _spawnTimer;
    public AI_SpawnManager(GameObject customer, GameObject pedestrian)
    {
        _customerSpawnPoints = new List<AI_SpawnPoint>();
        _pedestrianSpawnPoints = new List<AI_SpawnPoint>();

        _customer = customer;
        _pedestrian = pedestrian;
    }

    public void Update(float deltaTime)
    {
        _spawnTimer += deltaTime;

        if(_spawnTimer > 2)
        {
            SpawnAgent((SpawnPointType)UnityEngine.Random.Range(0, 2));
            _spawnTimer = 0;
        }
    }
    public void SpawnAgent(SpawnPointType type)
    {
        List<AI_SpawnPoint> availableSpawnPoints = null;

        switch (type)
        {
            case SpawnPointType.CUSTOMER:
                availableSpawnPoints = _customerSpawnPoints.Where(s => s.isUsed == false).ToList();
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
                location.SpawnAgent(_customer);
                break;
            case SpawnPointType.PEDESTRIAN:
                location.SpawnAgent(_pedestrian);
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
            case SpawnPointType.PEDESTRIAN:
                _pedestrianSpawnPoints.Add(spawnPoint);
                break;
        }

    }
}
