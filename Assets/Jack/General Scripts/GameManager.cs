using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ControlTerminal controlTerminal;
    public GameObject PipesHolder;
    public GameObject[] Pipes;

    [Header("Stats")]
    public int totalPipes = 0;
    public int correctedPipes = 0;

    [Header("Vibration Settings - Win")]
    public float winIntensity = 0.8f;
    public float winDuration = 1.2f;

    [Header("Vibration Settings - Single Correct")]
    public float clickIntensity = 0.2f;
    public float clickDuration = 0.1f;

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
        
        if (correctedPipes < totalPipes)
        {
            StartCoroutine(VibrateController(clickIntensity, clickDuration));
        }
        
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


            StartCoroutine(VibrateController(winIntensity, winDuration));
        }
    }

    IEnumerator VibrateController(float intensity, float duration)
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(intensity, intensity);
            yield return new WaitForSeconds(duration);
            gamepad.ResetHaptics();
        }
    }

    private void OnDisable()
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.ResetHaptics();
        }
    }
}
