using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarparkManagerAnim : MonoBehaviour
{

    private Animator Bollard;
    private Animator Gate;
    private Animator Gate2;

    public float BollardDelay = 5f;
    public float GateDelay = 5f;
    private bool BollardUp = true;
    private bool GateOpen = true;
    // Start is called before the first frame update
    void Start()
    {
        Bollard = GetComponent<Animator>();
        Gate = GetComponent<Animator>();
        Gate2 = GetComponent<Animator>();
        StartCoroutine(AnimateOnTimer());
    }

    IEnumerator AnimateOnTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(BollardDelay);
            yield return new WaitForSeconds(GateDelay);
            if (BollardUp)
            {
                Bollard.SetTrigger("BollardD");
            }
            else
            {
                Bollard.SetTrigger("BollardU");
            }
            if (GateOpen)
            {
                Gate.SetTrigger("Open");
                Gate2.SetTrigger("Open");
            }
            else
            {
                Gate.SetTrigger("Close");
                Gate2.SetTrigger("Close");
            }

            BollardUp = !BollardUp;
            GateOpen = !GateOpen;
        }
    }
    
}
