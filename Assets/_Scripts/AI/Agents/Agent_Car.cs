using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Agent_Car : Agent_Base
{
    [Header("Traffic Sensors")]
    [SerializeField]
    private Direction_Sensor _trafficSensor;
    [SerializeField]
    private Direction_Sensor _frTrafficSensor;
    [SerializeField]
    private Direction_Sensor _flTrafficSensor;
    [SerializeField]
    private Direction_Sensor _fraTrafficSensor;
    [SerializeField]
    private Direction_Sensor _flaTrafficSensor;
    [SerializeField]
    private List<string> _trafficTags;
    [SerializeField]
    private float _trafficSensorStrength;

    [Header("Waypoint Sensor")]
    [SerializeField]
    private Direction_Sensor _waypointSensor;
    [SerializeField]
    private float _waypointSensorStrength;

    [Header("Parking Sensor")]
    [SerializeField]
    private Direction_Sensor _parkingSensor;
    [SerializeField]
    private string _parkingSpaceTag;
    [SerializeField]
    private float _parkingSensorStrength;

    [SerializeField]
    private AI_SpawnPoint customerSpawnpoint;

    private AI_WaypointPath _path;
    private AI_Waypoint _currentWaypoint;
    private AI_Waypoint _queuedWaypoint;
    private ParkingSpace _parkingSpace;

    private bool _fStop;
    private bool _frStop;
    private bool _flStop;
    private bool _fraStop;
    private bool _flaStop;

    private bool _isAtWaypoint;
    private bool _isParking;
    private bool _isLeaving;
    private bool _isReversing;
    

    private new void Start()
    {
        base.Start();

        spawnPoint.isActive = false;
        spawnPoint.isUsed = false;

        _currentWaypoint = GetNextWaypoint();

        _trafficSensor.Init(this, _trafficSensorStrength, 0.1f, transform.forward, _trafficTags, FStop);
        _frTrafficSensor.Init(this, _trafficSensorStrength, 0.1f, transform.forward, _trafficTags, FRStop);
        _flTrafficSensor.Init(this, _trafficSensorStrength, 0.1f, transform.forward, _trafficTags, FLStop);
        _fraTrafficSensor.Init(this, _trafficSensorStrength, 0.1f, transform.forward, _trafficTags, FRAStop);
        _flaTrafficSensor.Init(this, _trafficSensorStrength, 0.1f, transform.forward, _trafficTags, FLAStop);
        _waypointSensor.Init(this, _waypointSensorStrength, 0.1f, transform.forward, _currentWaypoint.gameObject, HasReachedWaypoint);
        _parkingSensor.Init(this, _parkingSensorStrength, 0.1f, transform.forward, _parkingSpaceTag, IsParking);

        seeker.SetSpeed(5f);
        seeker.SetPath(_currentWaypoint.transform.position);
    }
    public void InitPath(PathPlan path)
    {
        _path = new AI_WaypointPath(path.GetPath());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isLeaving == true)
        {
            if (_isAtWaypoint == true)
            {
                UpdateWaypoint();
                return;
            }

            if (_isReversing == true && seeker.HasPath == false)
            {
                ToggleSensorPause(false);

                _parkingSpace.Reset();

                seeker.SetSpeed(5f);
                seeker.SetPath(_currentWaypoint.transform.position);
                _isReversing = false;
            }

            return;
        }

        if (_isParking == true)
        {
            if(_parkingSpace == null)
            {
                InitParkingSpace();

                return;
            }
        }

        if(_parkingSpace != null)
        {
            if(_parkingSpace.OwnerIsParked == true)
            {
                ToggleSensorPause(true);
                customerSpawnpoint.isActive = true;
                return;
            }

            seeker.SetPath(_parkingSpace.GetGuidePosition());
            return;
        }

        if (_isAtWaypoint == true)
        {
            UpdateWaypoint();
        }
    }

    private void ToggleSensorPause(bool val)
    {
        _parkingSensor.SetPauseSensor(val);
        _trafficSensor.SetPauseSensor(val);
        _frTrafficSensor.SetPauseSensor(val);
        _fraTrafficSensor.SetPauseSensor(val);
        _flTrafficSensor.SetPauseSensor(val);
        _flaTrafficSensor.SetPauseSensor(val);
        _waypointSensor.SetPauseSensor(val);
    }
    private void UpdateWaypoint()
    {
        _isAtWaypoint = false;

        AI_Waypoint next = GetNextWaypoint();

        if(next == null)
        {
            Destroy(gameObject);
            return;
        }

        _currentWaypoint = next;
        _waypointSensor.Init(this, _waypointSensorStrength, 0.1f, transform.forward, _currentWaypoint.gameObject, HasReachedWaypoint);

        seeker.SetPath(_currentWaypoint.transform.position);
    }

    private AI_Waypoint GetNextWaypoint()
    {
        AI_Waypoint next;

        if (_queuedWaypoint != null)
        {
            next = _queuedWaypoint;
            _queuedWaypoint = null;

            return next;
        }

        next = _path.GetNextWaypoint();

        if (next == null)
        {
            return null;
        }

        if (next.IsSwitch == true)
        {
            List<AI_Waypoint> newPath = next.gameObject.GetComponent<AI_SwitchWaypoint>().GetNextPlan();

            if (newPath != null)
            {
                _path.SetNewPath(newPath);
                _queuedWaypoint = _path.GetNextWaypoint();
            }
        }

        return next;
    }
    private void InitParkingSpace()
    {
        _parkingSpace = _parkingSensor.GetHitData().collider.gameObject.GetComponent<ParkingSpace>();

        if(_parkingSpace == null || _parkingSpace.IsOwned == true)
        {
            _parkingSpace = null;
            _isParking = false;
            return;
        }

        seeker.SetSpeed(3f);
        seeker.EndPath();

        _parkingSpace.SetOwner(this);
    }
    public void LeaveCarPark()
    {
        seeker.SetSpeed(2f);
        seeker.Reverse(_parkingSpace.GetGuidePosition());

        _isLeaving = true;
        _isReversing = true;
    }
    public void FStop(bool val)
    {
        if (val == _fStop)
        {
            return;
        }

        _fStop = val;
        NeedsToStop();
    }
    public void FRStop(bool val)
    {
        if (val == _frStop)
        {
            return;
        }

        _frStop = val;
        NeedsToStop();
    }
    public void FLStop(bool val)
    {
        if(val == _flStop)
        {
            return;
        }

        _flStop = val;
        NeedsToStop();
    }
    public void FRAStop(bool val)
    {
        if (val == _fraStop)
        {
            return;
        }

        _fraStop = val;
        NeedsToStop();
    }
    public void FLAStop(bool val)
    {
        if (val == _flaStop)
        {
            return;
        }

        _flaStop = val;
        NeedsToStop();
    }
    public void NeedsToStop()
    {
        if(_fStop == true || _frStop == true || _flStop == true || _fraStop == true || _flaStop == true)
        {
            seeker.ToggleStop(true);
            return;
        }

        seeker.ToggleStop(false);
    }
    public void HasReachedWaypoint(bool val)
    {
        _isAtWaypoint = val;
    }
    public void IsParking(bool val)
    {
        _isParking = val;
    }
}
