using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction_Sensor : MonoBehaviour
{
    enum SensorType
    {
        TAG,
        TAGSET,
        TARGET
    }
    private Agent_Car _agent;
    private Vector3 _direction;

    private GameObject _target;

    private float _sensorStrength;
    private float _sensorDelay;
    private float _sensorTimer = 0;

    [SerializeField]
    private LayerMask _layerMask;

    private List<string> _tagSet;
    private string _tag;

    private RaycastHit hit;
    private SensorType _sensorType;
    private bool _isPaused;

    public Action<bool> toggleCallback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(Agent_Car agent, float sensorStrength, float sensorDelay, Vector3 direction, string tag, Action<bool> toggleFunc)
    {
        _agent = agent;
        _sensorStrength = sensorStrength;
        _sensorDelay = sensorDelay;
         _tag = tag;
        _direction = direction;
        _sensorType = SensorType.TAG;

        toggleCallback = toggleFunc;
    }
    public void Init(Agent_Car agent, float sensorStrength, float sensorDelay, Vector3 direction, List<string> tags, Action<bool> toggleFunc)
    {
        _agent = agent;
        _sensorStrength = sensorStrength;
        _sensorDelay = sensorDelay;
        _tagSet = tags;
        _direction = direction;
        _sensorType = SensorType.TAGSET;

        toggleCallback = toggleFunc;
    }
    public void Init(Agent_Car agent, float sensorStrength, float sensorDelay, Vector3 direction, GameObject target, Action<bool> toggleFunc)
    {
        _agent = agent;
        _sensorStrength = sensorStrength;
        _sensorDelay = sensorDelay;
        _direction = direction;

        _target = target;
        _sensorType = SensorType.TARGET;

        toggleCallback = toggleFunc;
    }
    void FixedUpdate()
    {
        if(_isPaused == true)
        {
            return;
        }

        _sensorTimer += Time.fixedDeltaTime;

        if (_sensorTimer >= _sensorDelay)
        {
            switch (_sensorType)
            {
                case SensorType.TAG:
                    ScanForTag();
                    break;
                case SensorType.TAGSET:
                    ScanForTagSet();
                    break;
                case SensorType.TARGET:
                    ScanForTarget();
                    break;
            }

            _sensorTimer = 0;
        }
    }

    private void ScanForTag()
    {
        //Debug.DrawRay(transform.position, transform.forward * _sensorStrength, Color.red);

        if (Physics.Raycast(transform.position, transform.forward, out hit, _sensorStrength, _layerMask, QueryTriggerInteraction.Collide))
        {
            if (hit.collider.tag == _tag)
            {
                toggleCallback(true);
                return;
            }
        }

        toggleCallback(false);
    }
    private void ScanForTagSet()
    {
        //Debug.DrawRay(transform.position, transform.forward * _sensorStrength, Color.green);

        if (Physics.Raycast(transform.position, transform.forward , out hit, _sensorStrength, _layerMask, QueryTriggerInteraction.Collide))
        {
            if (_tagSet.Contains(hit.collider.tag) == true)
            {
                if (hit.collider.tag != "Traffic Light")
                {
                    toggleCallback(true);
                    return;
                }

                if (hit.collider.gameObject.GetComponentInParent<TrafficLight>().IsGreen == false)
                {
                    toggleCallback(true);
                    return;
                }
            }
        }

        toggleCallback(false);
    }
    private void ScanForTarget()
    {
        if(_target == null)
        {
            Debug.LogError("Target is null");
            return;
        }
        //Debug.DrawRay(transform.position, transform.forward * _sensorStrength, Color.white);

        if (Physics.Raycast(transform.position,  transform.forward, out hit, _sensorStrength, _layerMask, QueryTriggerInteraction.Collide))
        {
            if(hit.collider.gameObject == _target)
            {
                toggleCallback(true);
                return;
            }
        }

        toggleCallback(false);
    }

    public RaycastHit GetHitData()
    {
        return hit;
    }

    public void SetPauseSensor(bool val)
    {
        _isPaused = val;
    }
}
