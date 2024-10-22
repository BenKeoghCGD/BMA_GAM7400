using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
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


    private Location_Sensor streetCornerSensor;
    [Header("Sensor Variables")]
    [SerializeField]
    private int streetCornerSensorDelay;
    [SerializeField]
    private string streetCornerSensorTag;

    private LitterDropper _litterDropper;

    private float _lingerTimer = 0;
    private float _lingerDelayTimer = 0;

    public bool isInStreetCorner = false;

    private new void Start()
    {
        base.Start();

        if(gameObject.GetComponent<LitterDropper>() != null)
        {
            _litterDropper = gameObject.GetComponent<LitterDropper>();
        }
        else
        {
            _litterDropper = gameObject.AddComponent<LitterDropper>();
        }

        streetCornerSensor = gameObject.AddComponent<Location_Sensor>();
        streetCornerSensor.Init(this, 30, 1, streetCornerSensorTag, SetStreetCornerBool);

        _lingerDelayTimer = delayBetweenLinger;
    }

    private void Update()
    {
        if(seeker.HasPath == true)
        {
            return;
        }

        if(_lingerDelayTimer > 0)
        {
            _lingerDelayTimer -= Time.deltaTime;

            if (isInStreetCorner == true && _lingerTimer <= 0)
            {
                _lingerTimer = Random.Range(lingerTimeMin, lingerTimeMax);
            }
        }

        if(_lingerTimer > 0)
        {
            _lingerTimer -= Time.deltaTime;

            if(_lingerTimer <= 0)
            {
                EndLinger();
            }

            return;
        }

        seeker.SetRandomPath(pathSearchRadius);
    }

    private void EndLinger()
    {
        _litterDropper.DropLitter();
        seeker.SetRandomPath(pathSearchRadius);

        _lingerDelayTimer = delayBetweenLinger;
    }

    public void SetStreetCornerBool(bool val)
    {
        isInStreetCorner = val;
    }
}
