using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPowerUp : PowerUpBase
{
   [SerializeField] int collectedTrash = 0;

   public override IEnumerator Effect(float time)
   {
      if(player == null)
      {
         yield return null;
      }
      
      OnBombExplode();
   }

   protected override void OnBombExplode()
   {
      if (GameManager.instance.LitterInstantiated != null)
      {
         int listAmount;
         listAmount = GameManager.instance.LitterInstantiated.Count;
         for (int i = 0; i < listAmount; i++)
         {
            if (GameManager.instance.LitterInstantiated[i] != null)
            {
               Destroy(GameManager.instance.LitterInstantiated[i].gameObject);
               collectedTrash++;
            }
         }
         GameManager.instance.LitterInstantiated.Clear();
      }
   }
}
