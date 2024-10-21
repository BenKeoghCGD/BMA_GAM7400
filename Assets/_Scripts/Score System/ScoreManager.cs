
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int _BeingScore = 0;
    [SerializeField] int _BlackScore = 0;
    [SerializeField] int _CardboardScore = 0;
    [SerializeField] int _RedScore = 0;
    [SerializeField] int _FoodScore = 0;
    [SerializeField] int _GeneralScore = 0;
    [SerializeField] int _CansScore = 0;

    public bool ReadyForStore;
    
    public void LitterValuCalculator(Collider target)
    {
        if (target.GetComponent<Litter>())
        {
            var currenctLitter = target.GetComponent<Litter>();
            switch (currenctLitter.litterType)
            {
                case LitterType.Beige:
                    GameManager.instance.PlayerScore += _BeingScore;
                    Debug.Log("Beige Litter");
                    break;
                case LitterType.Black:
                    GameManager.instance.PlayerScore += _BlackScore;
                    Debug.Log("Black Litter");
                    break;
                case LitterType.Cardboard:
                    GameManager.instance.PlayerScore += _CardboardScore;
                    Debug.Log("Cardboard Litter");
                    break;
                case LitterType.Red:
                    GameManager.instance.PlayerScore += _RedScore;
                    Debug.Log("Red Litter");
                    break;
                case LitterType.FoodGarden:
                    GameManager.instance.PlayerScore += _FoodScore;
                    Debug.Log("Food Garden Litter");
                    break;
                case LitterType.GeneralWaste:
                    GameManager.instance.PlayerScore += _GeneralScore;
                    Debug.Log("General Waste Litter");
                    break;
                case LitterType.CansBottles:
                    GameManager.instance.PlayerScore += _CansScore;
                    Debug.Log("CansBottles Litter");
                    break;
            }
        }
    }

    public void StoreScore()
    {
        if (ReadyForStore)
        {
            GameManager.instance.StoredScore += GameManager.instance.PlayerScore;
            GameManager.instance.PlayerScore = 0;
        }
    }
}
