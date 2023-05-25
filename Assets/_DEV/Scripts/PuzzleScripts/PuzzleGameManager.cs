using LastHand;
using UnityEngine;

public class PuzzleGameManager : MonoBehaviour
{
    public GameObject blockHolder;
    public GameObject[] blocks;

    private int _totalBlock = 0;
    private int _correctedBlocks = 0;

    void Start()
    {
        _totalBlock = blockHolder.transform.childCount;
        blocks = new GameObject[_totalBlock];

        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i] = blockHolder.transform.GetChild(i).gameObject;
        }
    }

    public void CorrectMove()
    {
        _correctedBlocks += 1;
        
        if(_correctedBlocks == _totalBlock)
        {
            Events.GamePlay.OnPuzzleWin.Call();
        }
    }

    public void WrongMove()
    {
        _correctedBlocks -= 1;
    }
}