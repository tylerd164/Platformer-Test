using UnityEngine;
using UnityEngine.Audio;

public class audiomanager : MonoBehaviour
{

    [Header("--- AudioSource ---")]
    [SerializeField] AudioSource musicSource;

    [Header("--- AudioClip ---")]
    public AudioClip background;

    public AudioSource audioSource;

    public static audiomanager audioInstance;

    public AudioClip damage;
    public AudioClip walking;
    public AudioClip itempickup;
    public AudioClip puzzlefail;
    public AudioClip accessterminal;
    public AudioClip clickSound;
    public AudioClip openDoor;
    public AudioClip scanFail;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

        audioSource = GetComponent<AudioSource>();

        audioInstance = this;
    }

    public void Damage()
    {
        audioSource.PlayOneShot(damage);
    }
    public void Walking()
    {
        audioSource.PlayOneShot(walking);
    }
    public void ItemPickup()
    {
        audioSource.PlayOneShot(itempickup);
    }
    public void PuzzleFail()
    {
        audioSource.PlayOneShot(puzzlefail);
    }
    public void AccessTerminal()
    {
        audioSource.PlayOneShot(accessterminal);
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void OpenDoor()
    {
        audioSource.PlayOneShot(openDoor);
    }

    public void ScanFail()
    {
        audioSource.PlayOneShot(scanFail);
    }






}
