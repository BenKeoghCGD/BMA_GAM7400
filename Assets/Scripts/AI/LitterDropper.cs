/*
 * Branch: BenH (Higham, Ben)
 * Commit: 56f110d603535bc1d5ee8186f94c86515526ae0f
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: 
 */

using System.Collections.Generic;
using UnityEngine;

public class LitterDropper : MonoBehaviour
{
    [SerializeField] private List<GameObject> litterObjects;
    [SerializeField] private float litterTimerMin;
    [SerializeField] private float litterTimerMax;

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
        // Generate a new gameObject (instance) using one of the items provided in the litterObjects list
        GameObject instance = Instantiate(litterObjects[Random.Range(0, litterObjects.Count)]);
        // Set the position of the litter to the position of the LitterDropper
        instance.transform.position = transform.position;

        ResetLitterTimer();
    }

    // Set the litterTimer variable to a random number between litterTimerMin and litterTimerMax
    void ResetLitterTimer() => _litterTimer = Random.Range(litterTimerMin, litterTimerMax);
}
