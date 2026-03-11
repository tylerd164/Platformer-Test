using UnityEngine;
using TMPro; 
using System.Collections;

public class LifeLostFlashScript : MonoBehaviour
{   
    public GameObject flashPanel;
    public float flashSpeed = 0.2f;
    public int flashCount = 3;

    void Start()
    {
        flashPanel.SetActive(false);
    }
    
    public void TriggerFlash()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            flashPanel.SetActive(true);
            yield return new WaitForSeconds(flashSpeed);
            flashPanel.SetActive(false);
            yield return new WaitForSeconds(flashSpeed);
        }
    }
}
