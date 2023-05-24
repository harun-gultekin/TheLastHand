using System;
using UnityEngine;

public class PrefManager : MonoBehaviour
{
    public static PrefManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }
    
    public LevelState GetLevelState()
    {
        var levelState = PlayerPrefs.GetString("CurrentLevelState", LevelState.OnMenu.ToString());
        Enum.TryParse(levelState, out LevelState result);
        return result;
        
        //USAGE
        //var levelState = PrefManager.Instance.GetQualityType();
    }

    public void SetLevelState(LevelState levelState)
    {
        PlayerPrefs.SetString("CurrentLevelState", levelState.ToString());
    }
}