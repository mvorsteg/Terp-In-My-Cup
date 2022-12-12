using UnityEngine;
using System.Collections.Generic;

public class Button : MonoBehaviour
{
    public List<(ButtonReceiver, bool)> receivers = new List<(ButtonReceiver, bool)>();
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        foreach ((ButtonReceiver, bool) b in receivers)
        {
            b.Item1.SetButton(b.Item2);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        foreach ((ButtonReceiver, bool) b in receivers)
        {
            Debug.Log(b.Item1 + " " + !b.Item2);
            b.Item1.SetButton(!b.Item2);
        }
        audioSource.Play();

    }

    private void OnCollisionExit(Collision other)
    {
        foreach ((ButtonReceiver, bool) b in receivers)
        {
            b.Item1.SetButton(b.Item2);
        }
    }
}