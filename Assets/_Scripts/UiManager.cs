using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Updated name to capitalised UI, previous was UiManager. (BH)
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMP_Text lifeAmountText;
    public TMP_Text LitterAmountText;

    public GameObject recyclingCanvas;
    public GameObject joystickCanvas;
    public GameObject mainGameCanvas;

    public GameObject triggerUICanvas;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }


}
