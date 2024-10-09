/*
 * Branch: BenH (Higham, Ben)
 * Commit: 56f110d603535bc1d5ee8186f94c86515526ae0f
 *
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: 
 */

using System.Collections;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour, IInteractable
{
    [SerializeField] protected float duration;
    protected PlayerController _player;
    protected bool _effectStarted = false;

    public virtual IEnumerator Effect(float time)
    {
        yield return new WaitForSeconds(time);
    }

    protected virtual void StartEffect()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    protected virtual void EndEffect()
    {
        Destroy(gameObject);
    }

    // Starts the effect couroutine
    public void OnInteract(PlayerController player)
    {
        // make sure player exists
        if(player == null) return;

        _player = player;
        StartCoroutine(Effect(duration));
    }
}
