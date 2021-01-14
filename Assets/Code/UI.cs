using UnityEngine;

public class UI : MonoBehaviour
{
    private static UI instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }    
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayDing()
    {
        instance.audioSource.Play();
    }

    public void Pause()
    {

    }
}