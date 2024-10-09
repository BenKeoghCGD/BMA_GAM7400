/*
 * Branch: Hossein (Soroor, Hossein)
 * Commit: 057afddb335e248e8a0c986f6117e1e702cddf11
 * 
 * Cleaned 9/10/24 (Keogh, Ben)
 * Branch: Main, Stable (Keogh, Ben)
 * Commit: b19f37c3e27817dba491819664ea3e95d924333e
 */

using System.Collections;
using UnityEngine;

public class BombPowerUp : PowerUpBase
{
   [SerializeField] int collectedTrash = 0;

   public override IEnumerator Effect(float time)
   {
      if(_player == null) yield return null;
      
      OnBombExplode();
   }

   void OnBombExplode()
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
