using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Example Powerup
public class MagnetPowerup : PowerUpBase
{
    [SerializeField] private float _speed;   //litters move to player magnet with this speed
    [SerializeField] private float _distance;    //The farthest distance at which a magnet affects a piece of litter.
    public override void MagnetEffect(float time)
    {
        print("Magnet Activated");
        StartMagnetEffect();
    }

    protected override void StartMagnetEffect()
    {
        base.StartMagnetEffect();
        StartCoroutine(StartMagneticAttraction());
    }

    private IEnumerator StartMagneticAttraction()       //this function is in charge on attraction of magnet
    {
        var litterObjcts = GameManager.instance.LitterInstantiated;
        float timer = magnetDuration;  
        while (timer > 0)       //The purpose of this loop is to create a countdown timer.
        {
            for (int i = 0; i < litterObjcts.Count; i++)        //Here, the litter that was identified is measured in terms of distance from the player, and if it falls within a specified range, attraction occurs.
            {
                if (GameManager.instance.LitterInstantiated[i] != null)
                {
                    if (Vector3.Distance(player.transform.position ,litterObjcts[i].transform.position) <= _distance)
                    {
                        print("Magnet Attraction");
                        litterObjcts[i].transform.position = Vector3.MoveTowards(litterObjcts[i].transform.position,player.transform.position,
                            _speed * Time.deltaTime);
                        if (litterObjcts[i].transform.position == player.transform.position)
                        {
                            Destroy(litterObjcts[i].gameObject);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(1f);
            timer--;
        }
        print("Magnet Deactivated");
    }
    
}
