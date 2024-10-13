using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickUp : PowerUpBase
{
    private bool _hasExecuted = false;
    public override IEnumerator Effect(float time)
    {
        if(_player == null) yield return null;
        AddLife();
    }

    public void AddLife()
    {
        if (GameManager.instance.LitterInstantiated != null && !_hasExecuted)
        {
            GameManager.instance.PlayerLife += 1;
            UiManager.instance.LifeAmountText.text = GameManager.instance.PlayerLife + "";
            Destroy(gameObject);
            _hasExecuted = true;
        }
    }
}
