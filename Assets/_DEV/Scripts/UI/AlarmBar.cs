using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class AlarmBar : MonoBehaviour
{
    // Written by M.Samil Ikizoglu

    // public Image alarmBarImage;
    // public Color beginColor, endColor;    
    public GameObject alarmBar;
    public bool OnlyWhiteColor;
    public Vector3 alarmBarLevel = new Vector3(0,1,1);
    
    public AlarmBarLevel theAlarmBarLevel;
    private Vector3 alarmBarPrevLevel = new Vector3(0,1,1);
    
    public float animationTime;

    private Color alarmBarColor;


    // Vector3 alarmLevel1 = new Vector3(0.20f,1,1);
    // Vector3 alarmLevel2 = new Vector3(0.40f,1,1);
    // Vector3 alarmLevel3 = new Vector3(0.60f,1,1);
    // Vector3 alarmLevel4 = new Vector3(0.80f,1,1);
    // Vector3 alarmLevel5 = new Vector3(1,1,1);

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
            if(alarmBarLevel.x >= 1) alarmBarLevel.x = 1;
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
        //LeanTween.scaleX(alarmBar, 1, alarmBarLevel);
        LeanTween.scale(alarmBar, alarmBarLevel, animationTime);




        // ****************************** //
        // ***Renk Değişim Denemeleri *** //
        // ****************************** //



        /// LeanTween.color(alarmBar, Color.red, animationTime).setDelay(1f);  //olmadı

        /*
        LeanTween.scale(alarmBar, alarmBarLevel, animationTime).setOnUpdate( (value) => 
        {
            alarmBarImage.fillAmount = value;
            alarmBarImage.color = Color.Lerp(beginColor, endColor, value);
        });
        */



      /*
       LeanTween.value(alarmBar, 0.1f, 1, animationTime).setOnUpdate( (value) => 
       {
           alarmBarImage.color = Color.Lerp(beginColor, endColor, value);
       });

       */





        // LeanTween.color(alarmBar, alarmBarColor, animationTime);    // Animasyonlu renk değişimi   //olmadı
        // LeanTween.color(alarmBar, Color.red, animationTime);    // Animasyonlu renk değişimi   //olmadı

        //  LeanTween.value(gameObject, 0.1f, 1, animationTime).setOnUpdate( (value) => 
        //  {
        //      alarmBarImage.color = Color.Lerp(beginColor, endColor, value);
        //  });


        // LeanTween.scaleX(alarmBar, 1, alarmBarLevel).setOnComp1ete(ShowMessage);
    }


    void sHowMessage()
    {
        //timeUpMessage.SetActive(true);
    }

}
