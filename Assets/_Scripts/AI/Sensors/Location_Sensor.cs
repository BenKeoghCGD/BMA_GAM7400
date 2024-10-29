using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Location_Sensor : MonoBehaviour
{
    enum SensorType
    {
        TAG,
        TARGET,
        TAGINFRONT
    }

    private Agent_Base _agent;
    private int _sensorRadius;

    private float _sensorDelay;
    private float _sensorTimer = 0;

    private LayerMask _layerMask;
    private string _tag;

    private Vector3 _target;
    private bool lookForTag;

    private SensorType _sensorType;
    public List<GameObject> _hitData;

    private bool _isPaused;
    public Action<bool> toggleCallback;

    // Start is called before the first frame update
    public void Init(Agent_Base agent, int sensorRadius, float sensorDelay, string tag, int typeIndex, Action<bool> toggleFunc)
    {
        lookForTag = true;
        _sensorType = (SensorType)typeIndex;

        _agent = agent;
        _sensorRadius = sensorRadius;
        _sensorDelay = sensorDelay;

        _layerMask = agent.layerMask;
        _tag = tag;

        toggleCallback = toggleFunc;
    }
    public void Init(Agent_Base agent, int sensorRadius, float sensorDelay, Vector3 target, Action<bool> toggleFunc)
    {
        lookForTag = false;
        _sensorType = SensorType.TARGET;

        _agent = agent;
        _sensorRadius = sensorRadius;
        _sensorDelay = sensorDelay;

        _target = target;

        toggleCallback = toggleFunc;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(_isPaused == true)
        {
            return;
        }

        _sensorTimer += Time.fixedDeltaTime;

        if(_sensorTimer >= _sensorDelay)
        {
            switch(_sensorType)
            {
                case SensorType.TARGET:
                    ScanForLocation();
                    break;
                case SensorType.TAG:
                    ScanForTag();
                    break;
                case SensorType.TAGINFRONT:
                    ScanForTagInFront();
                    break;
            }

            _sensorTimer = 0;
        }
    }

    void ScanForTag()
    {
        Collider[] hitData = Physics.OverlapSphere(transform.position, _sensorRadius, _layerMask);
        _hitData = new List<GameObject>();

        if(hitData.Length == 0)
        {
            toggleCallback(false);
            return;
        }

        bool isConfirmedTrue = false;

        foreach(Collider collider in hitData)
        {
            if(collider.gameObject.tag != _tag)
            {
                continue;
            }

            isConfirmedTrue = true;
            _hitData.Add(collider.gameObject);
            toggleCallback(true);
        }

        if(isConfirmedTrue == false)
        {
            toggleCallback(false);
        }
    }

    void ScanForLocation()
    {
        Vector3 distance = transform.position - _target;

        if (distance.magnitude <= _sensorRadius)
        {
            toggleCallback(true);
            return;
        }

        toggleCallback(false);
    }
    void ScanForTagInFront()
    {
        Collider[] hitData = Physics.OverlapSphere(transform.position, _sensorRadius, _layerMask);

        Debug.DrawRay(gameObject.transform.position, transform.forward * 10, Color.blue);

        if (hitData.Length == 0)
        {
            toggleCallback(false);
            return;
        }

        bool isConfirmedTrue = false;
        _hitData = new List<GameObject>();

        foreach (Collider collider in hitData)
        {
            if (collider.gameObject.tag != _tag)
            {
                continue;
            }

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 toCollider = collider.transform.position - transform.position;

            if( toCollider.magnitude < 10f)
            {
                continue;
            }

            toCollider = toCollider.normalized;

            if (Vector3.Dot(forward, toCollider) < 0)
            {
                continue;
            }

            _hitData.Add(collider.gameObject);
            isConfirmedTrue = true;
            toggleCallback(true);
        }

        if (isConfirmedTrue == false)
        {
            toggleCallback(false);
        }
    }
    public void SetPauseSensor(bool val)
    {
        _isPaused = val;
    }
}
