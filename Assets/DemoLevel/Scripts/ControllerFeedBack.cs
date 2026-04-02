using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ControllerFeedBack : MonoBehaviour
{
   public IEnumerator VibrateController(float intensity, float duration)
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(intensity, intensity);
            yield return new WaitForSeconds(duration);
            gamepad.ResetHaptics();
        }
    }
}
