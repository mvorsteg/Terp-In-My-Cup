using UnityEngine;

public class Button : MonoBehaviour
{
    public ButtonReceiver receiver;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        receiver.SetButton(true);
        audioSource.Play();

    }

    private void OnCollisionExit(Collision other)
    {
        Debug.Log("exit");
        receiver.SetButton(false);
    }
}