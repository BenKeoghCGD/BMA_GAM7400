using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class PowerUpBase : MonoBehaviour, IInteractable
{
    [SerializeField]
    protected float duration;

    protected PlayerController player;
    protected bool effectStarted = false;

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
    public void OnInteract(PlayerController _player)
    {
        if(_player == null)
        {
            return;
        }

        player = _player;
        StartCoroutine(Effect(duration));
    }
}
