using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotationWithCamera : MonoBehaviour
{
    [SerializeField] private Transform playerBody;    // Assign the player model/body here
    [SerializeField] private Transform playerCamera;  // Assign the camera transform here
    [SerializeField] private float mouseSensitivity = 100f;

    private InputAction lookAction;
    private float xRotation = 0f; // Vertical rotation (pitch)

    void Start()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        if (lookAction == null)
        {
            Debug.LogError("Look action not found in InputSystem.actions");
        }
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center
    }

    void Update()
    {
        if (lookAction == null) return;

        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        // Horizontal rotation (yaw) — rotate player body left/right
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        playerBody.Rotate(Vector3.up * mouseX);

        // Vertical rotation (pitch) — rotate camera up/down with clamping
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);  // Prevent camera flipping

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}