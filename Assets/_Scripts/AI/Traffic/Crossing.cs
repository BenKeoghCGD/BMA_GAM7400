using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossing : MonoBehaviour
{
    [SerializeField]
    private List<TrafficLight> _trafficLights;
    [SerializeField]
    private float _timeToChange;

    private int _lightIndex = 0;
    private float _timer = 0;

    private void Start()
    {
        if(_trafficLights.Count == 0)
        {
            Debug.LogError("Cross has no lights listed");
            return;
        }

        _trafficLights[_lightIndex].ToggleLight();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _timeToChange)
        {
            ToggleLights();
            _timer = 0;
        }
    }

    private void ToggleLights()
    {
        _trafficLights[_lightIndex].ToggleLight();

        _lightIndex += 1;

        if(_lightIndex > _trafficLights.Count - 1)
        {
            _lightIndex = 0;
        }

        _trafficLights[_lightIndex].ToggleLight();
    }
}
