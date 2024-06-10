using UnityEngine;

public class NewPlayerController : MonoBehaviour
{

    private GameInput playerInput;
    private CharacterController controller;
    [SerializeField][Range(0.0f, 10.0f)] private float playerSpeed = 3f;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        playerInput = new GameInput();
        controller = GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player
        Vector2 movementInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementInput.x, 0f, movementInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
            transform.forward = move;
    }
}
