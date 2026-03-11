using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject PipesHolder;
    public GameObject [] Pipes;
    public GameObject GameWinPanel;

    public int totalPipes = 0;

    public int correctedPipes = 0;
    void Start()
    {
        totalPipes = PipesHolder.transform.childCount;
        
        Pipes = new GameObject[totalPipes]; 

        for(int i = 0; i < Pipes.Length; i++)
        {
            Pipes[i] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes += 1;

        if(correctedPipes == totalPipes)
        {
            Debug.Log("You win!");
            GameWinPanel.SetActive(true);
        }
    }
}
