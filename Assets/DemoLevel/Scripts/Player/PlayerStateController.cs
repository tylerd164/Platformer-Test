using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{

    public bool jumpPressed { get; set; }
    public bool isIdle { get; set; }
    public bool interactButtonPressed { get; set; }
    public bool pauseButtonPressed { get; set; }
    public bool exitButtonPressed { get; set; }
    public bool puzzleActive { get; set; }

    public bool submitButtonPressed { get; set; }
}
