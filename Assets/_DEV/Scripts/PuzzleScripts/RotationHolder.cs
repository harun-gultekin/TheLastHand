using UnityEngine;

public class RotationHolder : MonoBehaviour
{
    int[] rotations = { 0,90,180,270 };

    public int[] correctRotation;
    
    [SerializeField]
    bool isPlaced = false;
    int rotationHolder;
    int PossibleRots = 1;

    PuzzleGameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("PuzzleGameManager").GetComponent<PuzzleGameManager>();
    }

    void Start()
    {
        PossibleRots = correctRotation.Length;
        int rand = Random.Range(0, rotations.Length);
        rotationHolder = rotations[rand];
        transform.rotation = Quaternion.Euler(0, 0, rotations[rand]);
        //transform.eulerAngles = new Vector3(0, 0, rotations[rand]);
        //Debug.Log("Current rotation: "+rotationHolder);
        if(PossibleRots > 1)
        {
            if (rotationHolder == correctRotation[0] || rotationHolder == correctRotation[1])
            {
                isPlaced = true;
                gameManager.CorrectMove();
            }
        }
        else
        {
            if (rotationHolder == correctRotation[0])
            {
                isPlaced = true;
                gameManager.CorrectMove();
            }
        }
    }

    private void OnMouseDown()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        rotationHolder = (rotationHolder + 90) % 360;

        if (PossibleRots > 1)
        {
            if (rotationHolder == correctRotation[0] || rotationHolder == correctRotation[1] && isPlaced == false)
            {
                isPlaced = true;
                gameManager.CorrectMove();
            }
            else if (isPlaced)
            {
                isPlaced = false;
                gameManager.WrongMove();
            }
        }
        else
        {
            if (rotationHolder == correctRotation[0] && isPlaced == false)
            {
                isPlaced = true;
                gameManager.CorrectMove();
            }
            else if (isPlaced)
            {
                isPlaced = false;
                gameManager.WrongMove();
            }
        }
    }
}