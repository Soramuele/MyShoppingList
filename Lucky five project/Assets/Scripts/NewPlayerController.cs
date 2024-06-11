using UnityEngine;
using Cinemachine;

public class NewPlayerController : MonoBehaviour
{

    private GameInput playerInput;
    private CharacterController controller;
    [SerializeField][Range(0.0f, 10.0f)] private float playerSpeed = 3f;
    [SerializeField][Range(0.0f, 10.0f)] private float rotSpeed = 4f;
    private Transform cameraMain;
    private Transform child;
    [SerializeField] ShowNextItem suggestion;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        playerInput = new GameInput();
        controller = GetComponent<CharacterController>();
        child = transform.GetChild(0).transform;
    }

    void Start()
    {
        cameraMain = Camera.main.transform;
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
        Vector3 move = (cameraMain.forward * movementInput.y + cameraMain.right * movementInput.x);
        move.y = 0;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
            transform.forward = move;

        if (movementInput != Vector2.zero)
        {
            Quaternion rotation = Quaternion.Euler(new Vector3(child.localEulerAngles.x, cameraMain.localEulerAngles.y, child.localEulerAngles.z));
            child.rotation = Quaternion.Lerp(child.rotation, rotation, Time.deltaTime * rotSpeed);
        }
            
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            suggestion.ShowSuggestion();
        }
    }
}
