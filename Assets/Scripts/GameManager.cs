using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public List<GameObject> LitterInstantiated = new List<GameObject>();
   
   private void Awake()
   {
      if (instance != null && instance != this) Destroy(gameObject);
      else
      {
         instance = this;
      }
   }
}
