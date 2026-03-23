using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CancelButton : MonoBehaviour
{
    public GameObject cancelbutton;

    private void OnEnable()
    {
        //clear previous button selection
        EventSystem.current.SetSelectedGameObject(null);
        //selects cancel button when options window active
        EventSystem.current.SetSelectedGameObject(cancelbutton);
    }
}
