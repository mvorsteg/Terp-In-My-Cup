using UnityEngine;
using System.Collections;

public class Door : ButtonReceiver
{
    public Vector3 openOffset;
    public float timeToMove = 2f;

    private Vector3 closedPos;
    private Vector3 openPos;
    private AudioSource audioSource;
    private float elapsedTime;
    private bool isMoving = false;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        closedPos = transform.position;   
        openPos = closedPos + openOffset; 
    }

    protected override void Activate()
    {
        base.Activate();
        Debug.Log(transform.name + " active");
        if (!isMoving)
        {
            StartCoroutine(Move());
        }
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        if (!isMoving)
        {
            StartCoroutine(Move());
        }
    }    

    private IEnumerator Move()
    {
        isMoving = true;
        elapsedTime = activated ? 0 : timeToMove;
        audioSource.Play();
        while (elapsedTime <= timeToMove && elapsedTime >= 0)
        {
            if (activated)
            {
                elapsedTime += Time.deltaTime;
            }
            else
            {
                elapsedTime -= Time.deltaTime;
            }
            transform.position = Vector3.Lerp(closedPos, openPos, elapsedTime / timeToMove);
            yield return null;
        }
        isMoving = false;
        audioSource.Stop();
    }

}