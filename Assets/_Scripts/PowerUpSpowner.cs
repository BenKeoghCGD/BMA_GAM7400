using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpowner : MonoBehaviour
{
    
    [SerializeField] private int _spawmTime; //The wait time for the next power-up to spawn
    [SerializeField] private List<GameObject> _powerUps = new List<GameObject>(); //powerup prefabs
    [SerializeField] private Vector3 _Raysource;    

    private int _RandomPU; //random value for selecting one of the powerups from list
    private GameObject _PUObject;
    private GameObject _LastHit;
    private Vector3 _CollisionPoint = Vector3.zero;
    private Vector3 _RayDestination;
    private int[] _RandomNumber;
    private void Start()
    {
        StartCoroutine(Spawner());
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_CollisionPoint,0.2f);
    }

    //powerup spawner
    //Spawner works in a way that a point is randomly determined within a maximum and minimum distance from the player that we specify
    //Then, using raycast, it checks whether the point is on the ground or not. This detection is done through layer indexing,
    //where in this case, we've assigned the **ground** layer to the terrain, and its index is set to 7.
    public IEnumerator Spawner()
    {
        yield return new WaitForSeconds(_spawmTime);
        
        RandomSpawnPosition();
        
        _RayDestination = new Vector3(_Raysource.x,_Raysource.y * -1 ,_Raysource.z);
        
        var ray = new Ray(GameManager.GetPlayerScript().transform.position + _Raysource ,_RayDestination);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit ,100))
        {
            _LastHit = hit.transform.gameObject;
           _CollisionPoint = new Vector3(hit.point.x,hit.point.y + 0.5f,hit.point.z);

           print(_LastHit);
            if (_LastHit.gameObject.layer == 7)
            {
                _RandomPU = Random.Range(0, _powerUps.Count);
                _PUObject = Instantiate(_powerUps[_RandomPU],_CollisionPoint, Quaternion.identity);
                print("spawner");

            }

        }
        StartCoroutine(Spawner());
    }

    //This function gives us two random values for the X and Z coordinates of the point where we want to spawn the power-up.
    private void RandomSpawnPosition()
    {
        _RandomNumber = new int[3];
        _RandomNumber[0] = Random.Range(-6, -1);
        _RandomNumber[1] = Random.Range(1, 6);
        _RandomNumber[2] = Random.Range(0, 2);
        _Raysource.x = _RandomNumber[_RandomNumber[2]];
        _RandomNumber[0] = Random.Range(-6, -1);
        _RandomNumber[1] = Random.Range(1, 6);
        _RandomNumber[2] = Random.Range(0, 2);
        _Raysource.z = _RandomNumber[_RandomNumber[2]];
    }

}
