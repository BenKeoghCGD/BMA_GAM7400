using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

public class Agent_Pedestrian : Agent_Base
{
    [Header("Linger Variables")]
    [SerializeField]
    private int lingerTimeMin;
    [SerializeField]
    private int lingerTimeMax;
    [SerializeField]
    private int delayBetweenLinger;

    private Location_Sensor _waypointSensor;
    private Location_Sensor _streetCornerSensor;
    private Location_Sensor _crossingSensor;

    [Header("Sensor Variables")]
    [SerializeField]
    private int streetCornerSensorDelay;
    [SerializeField]
    private string streetCornerSensorTag;
    [SerializeField]
    private int _waypointSensorStrength;
    [SerializeField]
    private string waypointTag;
    [SerializeField]
    private int _crossingSensorStrength;
    [SerializeField]
    private string _crossingSensorTag;

    private float _lingerTimer = 0;
    private float _lingerDelayTimer = 0;

    public bool isInStreetCorner = false;
    public bool isCrossing = false;

    private float _crossingCooldown;

    private CrossingPoint _crossingPoint;
    private List<GameObject> _waypoints;
    private AI_Waypoint currentWaypoint;

    private new void Start()
    {
        base.Start();

        _streetCornerSensor = gameObject.AddComponent<Location_Sensor>();
        _streetCornerSensor.Init(this, 10, 1, streetCornerSensorTag, 0, SetStreetCornerBool);

        _waypointSensor = gameObject.AddComponent<Location_Sensor>();
        _waypointSensor.Init(this, _waypointSensorStrength, 1f, waypointTag, 2, UpdateWaypoints);

        _crossingSensor = gameObject.AddComponent<Location_Sensor>();
        _crossingSensor.Init(this, _crossingSensorStrength, 0.1f, _crossingSensorTag, 0, SetIsCrossing);

        _lingerDelayTimer = delayBetweenLinger;
    }

    private void Update()
    {
        _crossingCooldown += Time.deltaTime;

        if(seeker.HasPath == true && seeker.DistanceRemaining > 1f)
        {
            if (isCrossing == true && _crossingPoint != null)
            {
                transform.LookAt(_crossingPoint.GetCrossingPosition());

                if (_crossingPoint.CanCross == false && _crossingCooldown > 5.0f)
                {
                    StartIdle();
                    seeker.ToggleStop(true);
                    return;
                }

                if (_crossingPoint.CanCross == true)
                {
                    EndIdle();
                    _crossingCooldown = 0;
                    seeker.ToggleStop(false);
                }
            }

            return;
        }

        if(_lingerDelayTimer > 0)
        {
            _lingerDelayTimer -= Time.deltaTime;

            if (isInStreetCorner == true && _lingerTimer <= 0)
            {
                StartIdle();
                _lingerTimer = Random.Range(lingerTimeMin, lingerTimeMax);
            }
        }

        if(_lingerTimer > 0)
        {
            _lingerTimer -= Time.deltaTime;

            if(_lingerTimer <= 0)
            {
                EndIdle();
                EndLinger();
            }

            return;
        }

        SetRandomWaypoint();
    }

    private void StartIdle()
    {

    }
    private void EndIdle()
    {

    }
    private void EndLinger()
    {
        litterDropper.DropLitter();
        seeker.SetRandomPath(pathSearchRadius);

        _lingerDelayTimer = delayBetweenLinger;
    }

    private void SetRandomWaypoint()
    {
        _crossingPoint = null;

        if(_waypoints == null)
        {
            return;
        }

        if (currentWaypoint != null && currentWaypoint.IsFinalWaypoint)
        {
            Debug.Log("Final waypoint");
            Destroy(SpawnPointType.PEDESTRIAN);
            return;
        }


        if (_waypoints.Count == 0)
        {
            Debug.LogError("No Waypoints");
            transform.Rotate(new Vector3(0, Random.Range(-10f, 10f), 0));
            return;
        }

        AI_Waypoint next = _waypoints[Random.Range(0, _waypoints.Count)].GetComponent<AI_Waypoint>();

        if(next == null)
        {
            //Debug.LogError(next.gameObject.name + " Waypoint component is null");
            return;
        }

        currentWaypoint = next;
        seeker.SetPath(currentWaypoint.transform.position);
    }
    public void SetStreetCornerBool(bool val)
    {
        isInStreetCorner = val;
    }
    public void SetIsCrossing(bool val)
    {
        if(isCrossing == val)
        {
            return;
        }

        if(val == true)
        {
            if(_crossingSensor._hitData.Count == 0)
            {
                Debug.LogError("Crossing, but crossing point not stored");
            }
            else
            {
                _crossingPoint = _crossingSensor._hitData[0].GetComponent<CrossingPoint>();
            }
        }

        isCrossing = val;
    }
    public void UpdateWaypoints(bool val)
    {
        if (val == false)
        {
            //Debug.LogError("No available waypoints");
            return;
        }
        //Debug.LogError("Waypoints");
        _waypoints = _waypointSensor._hitData;
    }
}
