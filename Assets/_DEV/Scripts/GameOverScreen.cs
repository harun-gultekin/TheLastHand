using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointsText;

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        // pointsText.text = score.ToString() + "POINTS";
    }

    public void RestartButton() 
    {
        //SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("MenuScene");
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    */
}



