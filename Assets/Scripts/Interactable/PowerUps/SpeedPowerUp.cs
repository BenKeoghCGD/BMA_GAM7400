using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Example Powerup
public class SpeedPowerUp : PowerUpBase
{
    [SerializeField]
    private float speedMultiplier;
    public override IEnumerator Effect(float time)
    {
        if(player == null)
        {
            yield return null;
        }

        StartEffect();
        yield return new WaitForSeconds(time);
        EndEffect();
    }

    protected override void StartEffect()
    {
        base.StartEffect();
        player.MultiplyBaseSpeed(speedMultiplier);
    }
    protected override void EndEffect()
    {
        player.ResetMovementSpeed();
        base.EndEffect();
    }
}
