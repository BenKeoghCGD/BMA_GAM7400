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
    public TMP_Text BeigeBinText;
    public TMP_Text BlackBinText;
    public TMP_Text ReadBinText;

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
