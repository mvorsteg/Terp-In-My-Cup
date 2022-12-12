using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Vector2 move;
    public Vector2 look;

    public float speed = 12f;
    public float jumpHeight = 3f;
    public float lookSpeed = 100f;
    private CharacterController cc;
    public CameraController cameraController;
    private AudioSource audioSource;

    private Vector3 velocity;
    private float timeToFootstep;

    public Transform groundCheck;
    private bool isGrounded;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public AudioClip[] footsteps;


    public float footstepDelay;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        cameraController.Rotate(look.x * lookSpeed, look.y * lookSpeed);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //hitColliders = Physics.OverlapSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 inputMovement = transform.right * move.x + transform.forward * move.y;    
        //cc.Move(inputMovement * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        cc.Move(((inputMovement * speed) + velocity) * Time.deltaTime);

        timeToFootstep -= Time.deltaTime;
        if (isGrounded && cc.velocity.magnitude > 2f && timeToFootstep <= 0)
        {
            AudioClip sound = GetRandomFootstepClip();
            audioSource.clip = sound;
            audioSource.Play();
            timeToFootstep = footstepDelay;
        }
    }    

    public void Jump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private AudioClip GetRandomFootstepClip()
    { 
        return footsteps[Random.Range(0, footsteps.Length)];
    }
}