using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerControls controls;
    private PlayerMovement playerMovement;
    private Player player;
    private Pickup pickup;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
        pickup = GetComponent<Pickup>();

        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => playerMovement.move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => playerMovement.move = Vector2.zero;

        controls.Gameplay.Look.performed += ctx => playerMovement.look = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => playerMovement.look = Vector2.zero;

        controls.Gameplay.Jump.performed += ctx => playerMovement.Jump();

        controls.Gameplay.Pickup.performed += ctx => pickup.DropPickup();

        //controls.Gameplay.Use.performed += ctx => Interaction.Go();

        controls.UI.Exit.performed += ctx => Application.Quit();

        controls.UI.Exit.Enable();

        controls.Gameplay.Enable();
        controls.UI.Enable();
    }

}