using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, IInteractables
{
    [SerializeField] private Transform hingeTransform;

    [Header("Door Rotation")]
    [SerializeField] private Vector3 openRotation = new Vector3(0, 90, 0);
    [SerializeField] private Vector3 closedRotation = new Vector3(0, 0, 0);

    [Header("Door Timing")]
    [SerializeField] private float doorLerpTime = 0.5f; // Use this for both open/close

    private bool isOpen = false;
    private bool isAnimating = false;

    private void Start()
    {
        if (hingeTransform == null)
            Debug.LogWarning("Hinge Transform is not assigned!");
    }

    public void OnFocus(PlayerInteractor player)
    {
        Debug.Log("Door is highlighted or showing a prompt.");
        player.ShowInteractCursor(); // Swap to interact crosshair
    }

    public void OnLoseFocus(PlayerInteractor player)
    {
        Debug.Log("Stopped highlighting door.");
        player.ShowDefaultCursor(); // Swap back to default
    }

    public void OnInteract()
    {
        if (isAnimating)
            return;

        Debug.Log("OnInteract called.");
        isAnimating = true;
        isOpen = !isOpen;

        Vector3 targetRotation = isOpen ? openRotation : closedRotation;
        StartCoroutine(RotateDoor(targetRotation));
    }

    public string GetInteractionPrompt()
    {
        return isOpen ? "Press E to close the door" : "Press E to open the door";
    }

    private IEnumerator RotateDoor(Vector3 targetRotation)
    {
        Quaternion startRot = hingeTransform.localRotation;
        Quaternion endRot = Quaternion.Euler(targetRotation);
        float elapsed = 0f;

        while (elapsed < doorLerpTime)
        {
            hingeTransform.localRotation = Quaternion.Slerp(startRot, endRot, elapsed / doorLerpTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        hingeTransform.localRotation = endRot;
        isAnimating = false;
    }
}
