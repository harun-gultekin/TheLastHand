using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    public void ResetGame()
    {
        LevelStateManager.Instance.currentState = LevelState.Started;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}