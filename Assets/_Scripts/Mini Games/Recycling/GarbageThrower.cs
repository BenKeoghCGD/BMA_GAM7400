using System.Collections.Generic;
using UnityEngine;

public class GarbageThrower : MonoBehaviour
{
    public GameObject[] litterPrefabs;
    public Transform[] spawnPoints;
    public float throwForce = 300f;
    int garbageCount = 5;


    List<GameObject> instantiatedGarbage = new List<GameObject>();

    private void Start()
    {
        garbageCount = spawnPoints.Length;

        // Spawn garbage objects at random points inside the collider
        for (int i = 0; i < garbageCount; i++)
        {
            int randomIndex = Random.Range(0, litterPrefabs.Length);

            // Pick a random garbage prefab
            GameObject garbagePrefab = litterPrefabs[randomIndex];

            // Find a random position within the collider bounds


            // Instantiate the garbage object at the random position
            instantiatedGarbage.Add(Instantiate(garbagePrefab, spawnPoints[i].transform.position, Quaternion.identity));
        }

        // Throw the instantiated garbage
        ThrowGarbage();
    }

    // Function to get a random point within a collider


    void ThrowGarbage()
    {
        foreach (var litter in instantiatedGarbage)
        {
            Rigidbody rb = litter.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Apply the force to the garbage object
                rb.AddForce(Vector3.forward * throwForce, ForceMode.Impulse);
            }
        }
        // Get the Rigidbody component of the garbage object

    }
}
