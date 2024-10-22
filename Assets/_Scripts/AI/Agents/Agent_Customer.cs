using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Agent_Customer : Agent_Base
{
    [SerializeField]
    private string storeEntranceTag;

    private Location_Sensor _customerSensor;

    private float _shoppingTimer;
    private float _dropLitterTimer;

    public bool _isAtTargetLocation;
    private bool _hasDroppedLitter;
    private bool _isInShop;

    private new void Start()
    {
        base.Start();

        _customerSensor = gameObject.AddComponent<Location_Sensor>();
        _customerSensor.Init(this, 1, 1, storeEntranceTag, SetTargetBool);

        seeker.SetPath(FindStoreEntrance().transform.position);
      
        litterDropper.DropLitter();
    }

    private void Update()
    {
        if(_isInShop == false)
        {
            ToShopLogic();
            return;
        }

        if(_shoppingTimer > 0)
        {
            _shoppingTimer -= Time.deltaTime;

            if(_shoppingTimer <= 0)
            {
                ExitStore();
            }

            return;
        }

        LeaveShopLogic();
    }

    //Temp functions for testing, will probably have some form of TagManager (or whatever) in future to minimise Find function calls. (BH) 
    private GameObject FindStoreEntrance()
    {
        GameObject entrance = GameObject.FindGameObjectWithTag(storeEntranceTag);

        if(entrance == null)
        {
            Debug.LogError("No Store Entrance found");
            return null;
        }

        return entrance;

    }
    // See previous comment. (BH)
    private GameObject FindStoreExit()
    {
        GameObject exit = GameObject.FindGameObjectWithTag("Store Exit");

        if (exit == null)
        {
            Debug.LogError("No Store Exit found");
            return null;
        }

        return exit;

    }
    private void EnterStore()
    {
        _isInShop = true;
        _isAtTargetLocation = false;

        gameObject.
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;

        transform.position = FindStoreExit().transform.position;
    }
    private void ExitStore()
    {
        _customerSensor.Init(this, 1, 1, spawnPoint.transform.position, SetTargetBool);

        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;

        _hasDroppedLitter = false;
        _dropLitterTimer = 0;

        seeker.SetPath(spawnPoint.transform.position);
    }
    public void SetTargetBool(bool val)
    {
        _isAtTargetLocation = val;
    }

    void DropLitterTimer()
    {
        _dropLitterTimer += Time.deltaTime;

        if(_dropLitterTimer > 3f)
        {
            litterDropper.DropLitter();
            _hasDroppedLitter = true;
        }
    }
    // Below functions can be separted into states in future, setup simply for testing. (BH)
    void ToShopLogic()
    {
        if (_hasDroppedLitter == false)
        {
            DropLitterTimer();
        }

        if (_isAtTargetLocation == true)
        {
            EnterStore();
            _shoppingTimer = 5.0f;
        }
    }

    void LeaveShopLogic()
    {
        if (_hasDroppedLitter == false)
        {
            DropLitterTimer();
        }

        if (_isAtTargetLocation == true)
        {
            spawnPoint.isUsed = false;
            Destroy(gameObject);
        }
    }
}
