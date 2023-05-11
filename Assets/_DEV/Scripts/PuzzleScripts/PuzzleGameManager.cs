using LastHand;
using UnityEngine;

public class PuzzleGameManager : MonoBehaviour
{
    public GameObject BlockHolder;
    public GameObject[] Blocks;

    [SerializeField]
    int totalBlock = 0;
    [SerializeField]
    int correctedBlocks = 0;

    public GameObject WinText;

    void Start()
    {
        WinText.SetActive(false);
        totalBlock = BlockHolder.transform.childCount;
        Debug.Log(totalBlock);
        Blocks = new GameObject[totalBlock];

        for (int i = 0; i < Blocks.Length; i++)
        {
            Blocks[i] = BlockHolder.transform.GetChild(i).gameObject;
        }
    }

    public void CorrectMove()
    {
        correctedBlocks += 1;
        
        if(correctedBlocks == totalBlock)
        {
            Events.GamePlay.OnPuzzleWin.Call();

            Debug.Log("You win!");
            WinText.SetActive(true);
        }
    }

    public void WrongMove()
    {
        correctedBlocks -= 1;
    }
}