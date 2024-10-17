/*
 * Branch: BenH (Higham, Ben)
 * Commit: 56f110d603535bc1d5ee8186f94c86515526ae0f
 * 
 * Branch: Hossein (Soroor, Hossein)
 * Commit: 057afddb335e248e8a0c986f6117e1e702cddf11
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: c5c64a33b28ef4617eae3f6b5dcc3374872a0938, b19f37c3e27817dba491819664ea3e95d924333e
 */

using System.Collections.Generic;
using UnityEngine;

public class LitterDropper : MonoBehaviour
{
    [SerializeField] 
    private float litterTimerMin;
    [SerializeField] 
    private float litterTimerMax;

    private float _litterTimer;

    // Start is called before the first frame update
    void Start()
    {
        ResetLitterTimer();
    }

    // Update is called once per frame
    void Update()
    {
        // Decrease the timer by the time it has been between the last frame and this frame
        _litterTimer -= Time.deltaTime;

        // If the litter timer has elapsed
        if (_litterTimer <= 0)
        {
            // Drop a new piece of litter
            DropLitter();
        }
    }

    // Function to spawn litter
    void DropLitter()
    {
        //Creates a new Litter object
        GameObject litterObject = new GameObject();

        Litter instance = litterObject.AddComponent<Litter>();
        instance.gameObject.layer = LayerMask.NameToLayer("Interactable");
        instance.transform.position = transform.position;

        instance.Init(GameManager.GetLitterManager().GetRandomLitterData(), litterObject);

        // Adds the litter item to the LitterManager
        GameManager.GetLitterManager().AddLitter(instance.GetComponent<Litter>());

        ResetLitterTimer();
    }

    // Set the litterTimer variable to a random number between litterTimerMin and litterTimerMax
    void ResetLitterTimer()
    {
        _litterTimer = Random.Range(litterTimerMin, litterTimerMax);
    }
}
