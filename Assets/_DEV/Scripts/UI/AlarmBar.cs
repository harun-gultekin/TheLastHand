using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class AlarmBar : MonoBehaviour
{
    public GameObject alarmBar;
    public bool OnlyWhiteColor;
    public Vector3 alarmBarLevel = new Vector3(0,1,1);
    public AlarmBarLevel theAlarmBarLevel;
    private Vector3 alarmBarPrevLevel = new Vector3(0,1,1);
    public float animationTime;
    private Color alarmBarColor;

    void Start()
    {
        AnimateBar();
    }

    void Update()
    {
        alarmBarLevel = theAlarmBarLevel.alarmBarLevel;

       
        CheckLevelChanges();
    }
    
    public void CheckLevelChanges()
    {
        if(alarmBarPrevLevel.x != alarmBarLevel.x)
        {
            if (alarmBarLevel.x >= 1)
            {
                UIManager.Instance.gameOverPanel.gameObject.SetActive(true);
                LevelStateManager.Instance.currentState = LevelState.OnMenu;
                alarmBarLevel.x = 1;
            }
            else if(alarmBarLevel.x <= 0) alarmBarLevel.x = 0;
            
            if(alarmBarLevel.x <= 0.2f)         ColorUtility.TryParseHtmlString("#ffffff", out alarmBarColor);
            else if(alarmBarLevel.x <= 0.4f)    ColorUtility.TryParseHtmlString("#ffcccc", out alarmBarColor);
            else if(alarmBarLevel.x <= 0.6f)    ColorUtility.TryParseHtmlString("#ff9999", out alarmBarColor);
            else if(alarmBarLevel.x <= 0.8f)    ColorUtility.TryParseHtmlString("#ff4c4c", out alarmBarColor);
            else if(alarmBarLevel.x <= 1f)      ColorUtility.TryParseHtmlString("#ff0000", out alarmBarColor);

            AnimateBar();
            if (!OnlyWhiteColor) alarmBar.GetComponent<Image>().color = alarmBarColor;  // Animasyonsuz renk değişimi
            alarmBarPrevLevel.x = alarmBarLevel.x;
        }
    }
    
    public void AnimateBar()
    {
        LeanTween.scale(alarmBar, alarmBarLevel, animationTime);
    }
}