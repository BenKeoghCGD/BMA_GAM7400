using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Updated name to capitalised UI, previous was UiManager. (BH)
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMP_Text lifeAmountText;
    public TMP_Text LitterAmountText;
    public TMP_Text binnedAmountText;
    public TMP_Text LitterCounterText;

    public GameObject recyclingCanvas;
    public GameObject joystickCanvas;
    public GameObject mainGameCanvas;

    public GameObject triggerUICanvas;
    public Image recyclingFadeInPanel;

    public Slider LitterAmountSlider;

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
        
        LitterCounterText.text = GameManager.GetLitterManager().litterHolder.Count.ToString();
    }

    private void Update()
    {
        LitterCounterText.text = GameManager.GetLitterManager()._worldLitter.Count.ToString();
        
        LitterAmountSlider.value = Mathf.InverseLerp(0,GameManager.instance.MaxLitterAmount, GameManager.GetLitterManager()._worldLitter.Count);
    }
}
