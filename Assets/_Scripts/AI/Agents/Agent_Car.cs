using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Agent_Car : Agent_Base
{

    [SerializeField]
    private Direction_Sensor _waypointSensor;
    [SerializeField]
    private Direction_Sensor _parkingSensor;

    [SerializeField]
    private AI_SpawnPoint customerSpawnpoint;

    private AI_WaypointPath _path;
    private AI_Waypoint _currentWaypoint;
    private AI_Waypoint _queuedWaypoint;
    private ParkingSpace _parkingSpace;

    private bool _isAtWaypoint;
    private bool _isParking;
    private bool _isLeaving;
    private bool _isReversing;

    private new void Start()
    {
        base.Start();

        spawnPoint.isActive = false;
        spawnPoint.isUsed = false;

        _path = new AI_WaypointPath(GameObject.Find("Anti-Clockwise Road Path").gameObject.GetComponent<PathPlan>().GetPath()); 

        _currentWaypoint = GetNextWaypoint();
   
        _waypointSensor.Init(this, 3, 0.1f, transform.forward, _currentWaypoint.gameObject, HasReachedWaypoint);
        _parkingSensor.Init(this, 1f, 0.1f, transform.forward, "Parking Space", IsParking);

        seeker.SetSpeed(5f);
        seeker.SetPath(_currentWaypoint.transform.position);
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
        _waypointSensor.Init(this, 3, 0.1f, transform.forward, _currentWaypoint.gameObject, HasReachedWaypoint);

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
            Debug.Log("Path finished");
            return null;
        }

        if (next.IsSwitch == true)
        {
            Debug.Log("Switch");

            List<AI_Waypoint> newPath = next.gameObject.GetComponent<AI_SwitchWaypoint>().GetNextPlan();

            if (newPath != null)
            {
                Debug.Log("Newplan");
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
    public void HasReachedWaypoint(bool val)
    {
        _isAtWaypoint = val;
    }
    public void IsParking(bool val)
    {
        _isParking = val;
    }
}
