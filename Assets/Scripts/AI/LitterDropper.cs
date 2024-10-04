using System;
using System.Collections.Generic;
using UnityEngine;

public class LitterDropper : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> litterObjects;

    [SerializeField]
    private float litterTimerMin;
    [SerializeField]
    private float litterTimerMax;

    private float litterTimer;

    // Start is called before the first frame update
    void Start()
    {
        litterTimer = UnityEngine.Random.Range(litterTimerMin, litterTimerMax);
    }

    // Update is called once per frame
    void Update()
    {
        litterTimer -= Time.deltaTime;

        if (litterTimer <= 0)
        {
            DropLitter();
        }
    }
    void DropLitter()
    {
        GameObject instance = Instantiate(litterObjects[UnityEngine.Random.Range(0, litterObjects.Count)]);
        instance.transform.position = transform.position;

        litterTimer = UnityEngine.Random.Range(litterTimerMin, litterTimerMax);
    }
}
