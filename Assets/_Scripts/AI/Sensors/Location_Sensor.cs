using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Location_Sensor : MonoBehaviour
{
    private Agent_Base _agent;
    private int _sensorRadius;

    private float _sensorDelay;
    private float _sensorTimer = 0;

    private LayerMask _layerMask;
    private string _tag;

    public Action<bool> toggleCallback;

    // Start is called before the first frame update
    public void Init(Agent_Base agent, int sensorRadius, float sensorDelay, string tag, Action<bool> toggleFunc)
    {
        _agent = agent;
        _sensorRadius = sensorRadius;
        _sensorDelay = sensorDelay;

        _layerMask = LayerMask.NameToLayer("AI_Interactable");
        _tag = tag;

        toggleCallback = toggleFunc;
    }

    // Update is called once per frame
    void Update()
    {
        _sensorTimer += Time.deltaTime;

        if(_sensorTimer >= _sensorDelay)
        {
            Scan();
            _sensorTimer = 0;
        }
    }

    void Scan()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _sensorRadius);

        if(hitColliders.Length == 0)
        {
            toggleCallback(false);
            return;
        }

        bool isConfirmedTrue = false;

        foreach(Collider collider in hitColliders)
        {
            if(collider.gameObject.tag == _tag)
            {
                isConfirmedTrue = true;
                toggleCallback(true);
            }
        }

        if(isConfirmedTrue == false)
        {
            toggleCallback(false);
        }
    }
}
