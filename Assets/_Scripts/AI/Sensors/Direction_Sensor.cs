using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction_Sensor : MonoBehaviour
{
    private Agent_Base _agent;
    private Vector3 _direction;

    private GameObject _target;

    private float _sensorStrength;
    private float _sensorDelay;
    private float _sensorTimer = 0;

    [SerializeField]
    private LayerMask _layerMask;
    private string _tag;

    private RaycastHit hit;

    private bool _checkForTarget;

    public Action<bool> toggleCallback;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(Agent_Base agent, float sensorStrength, float sensorDelay, Vector3 direction, string tag, Action<bool> toggleFunc)
    {
        _agent = agent;
        _sensorStrength = sensorStrength;
        _sensorDelay = sensorDelay;
         _tag = tag;
        _direction = direction;
        _checkForTarget = false;

        toggleCallback = toggleFunc;
    }
    public void Init(Agent_Base agent, float sensorStrength, float sensorDelay, Vector3 direction, GameObject target, Action<bool> toggleFunc)
    {
        _agent = agent;
        _sensorStrength = sensorStrength;
        _sensorDelay = sensorDelay;
        _direction = direction;

        _target = target;
        _checkForTarget = true;

        toggleCallback = toggleFunc;
    }
    void FixedUpdate()
    {
        _sensorTimer += Time.fixedDeltaTime;

        if (_sensorTimer >= _sensorDelay)
        {
            if (_checkForTarget == true)
            {
                ScanForTarget();
            }
            else
            {
                ScanForTag();
            }

            _sensorTimer = 0;
        }
    }

    private void ScanForTag()
    {
        Debug.DrawRay(transform.position, transform.forward * _sensorStrength, Color.red);

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
    private void ScanForTarget()
    {
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
}
