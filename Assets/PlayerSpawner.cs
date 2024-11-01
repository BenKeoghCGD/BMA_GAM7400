using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Ashkan Soroor (HS)
public class PlayerSpawner : MonoBehaviour
{
    
    public List<Transform> spawnPoints = new List<Transform>();


    private void Update()
    {
        if (GameManager.GetPlayerScript().respawn)
        {
            print("Respawn");
            GameManager.GetPlayerScript().CanMove = false;
            GameManager.GetPlayerScript().gameObject.transform.position = spawnPoints[0].position;
            GameManager.GetPlayerScript().PlayerAnimator.SetBool("isDied", true);
            var playerInput = GameManager.GetPlayerScript().GetComponent<PlayerInput>();
            StartCoroutine(moveAgain());
            GameManager.GetPlayerScript().respawn = false;


        }
        
    }

    IEnumerator moveAgain()
    {
        yield return new WaitForSeconds(8f);
        GameManager.GetPlayerScript().CanMove = true;
        GameManager.GetPlayerScript().PlayerAnimator.SetBool("isDied", false);
    }
}
