using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpowner : MonoBehaviour
{
    
    [SerializeField] private int _spawmTime;
    [SerializeField] private List<GameObject> _powerUps = new List<GameObject>();
    private int _RandomPU;
    private GameObject _PUObject;
    private GameObject lastHit;
    public Vector3 collisionPoint = Vector3.zero;
    public LayerMask layerMask;
    public Vector3 raysource;    
    public Vector3 rayDestination;    

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private void Update()
    {
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collisionPoint,0.2f);
    }

    public IEnumerator Spawner()
    {
        yield return new WaitForSeconds(_spawmTime);
        rayDestination = new Vector3(raysource.x,raysource.y * -1 ,raysource.z);
        
        var ray = new Ray(GameManager.GetPlayerScript().transform.position + raysource ,rayDestination);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit ,100))
        {
            lastHit = hit.transform.gameObject;
            collisionPoint = hit.point;

            if (lastHit.gameObject.layer == 7)
            {
                _RandomPU = Random.Range(0, _powerUps.Count);
                _PUObject = Instantiate(_powerUps[_RandomPU],collisionPoint, Quaternion.identity);
            }
            StartCoroutine(Spawner());

        }

    }

    private void RandomSpawnPosition()
    {
        raysource.x = Random.Range(-7, 7f);
        if (raysource.x == -1 || raysource.x == 0 || raysource.x == 1)
        {
            raysource.x = Random.Range(-7, 7f);
        }
    }

}
