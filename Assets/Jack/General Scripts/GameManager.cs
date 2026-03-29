using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ControlTerminal controlTerminal;
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [Header("Stats")]
    public int totalPipes = 0;
    public int correctedPipes = 0;

    void Start()
    {
        totalPipes = PipesHolder.transform.childCount;
        
        Pipes = new GameObject[totalPipes]; 
        for(int i = 0; i < totalPipes; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes++;
        CheckWinCondition();
    }

    public void wrongMove()
    {
        correctedPipes--;
        
        if (correctedPipes < 0) correctedPipes = 0;
    }

    private void CheckWinCondition()
    {
        if(correctedPipes == totalPipes)
        {
            Debug.Log("Puzzle Solved!");
            controlTerminal.MiniGameOverUI();
        }
    }
}