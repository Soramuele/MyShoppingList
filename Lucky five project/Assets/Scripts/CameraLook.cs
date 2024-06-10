using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraLook : MonoBehaviour
{
    private CinemachineFreeLook cinemachine;
    private GameInput playerInput;
    [SerializeField][Range(0f, 10f)] private float lookSpeed = 1f;

    void Awake()
    {
        playerInput = new GameInput();
        cinemachine = GetComponent<CinemachineFreeLook>();
    }

    void OnEnable()
    {
        playerInput.Enable();
    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    void Update()
    {
        Vector2 delta = playerInput.Player.Look.ReadValue<Vector2>();
        cinemachine.m_XAxis.Value += delta.x * 200 * lookSpeed * Time.deltaTime;
    }
}
