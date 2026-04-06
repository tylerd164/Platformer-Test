using UnityEngine;
using System.Linq;

public class PipeRotationScript : MonoBehaviour
{
    public float[] correctRotations;
    private bool isPlaced = false;
    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 4) * 90);
        CheckRotation();
    }

    private void OnMouseDown()
    {
        transform.Rotate(0, 0, 90);
        CheckRotation();
    }

    private void CheckRotation()
    {
        float currentZ = Mathf.Round(Mathf.Repeat(transform.eulerAngles.z, 360));

        bool nowCorrect = correctRotations.Any(angle => Mathf.Abs(currentZ - angle) < 0.1f);

        if (nowCorrect != isPlaced)
        {
            isPlaced = nowCorrect;
            if (isPlaced) gameManager.correctMove(); else gameManager.wrongMove();
        }
    }
}