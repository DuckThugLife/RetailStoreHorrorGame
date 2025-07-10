using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Transform raycastFromObject;   
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactLayer;

    [SerializeField] private GameObject defaultCanvasCrosshair;
    [SerializeField] private GameObject interactCanvasCrosshair;

    private IInteractables currentInteractable;

    void Update()
    {
        Ray ray = new Ray(raycastFromObject.transform.position, raycastFromObject.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(raycastFromObject.transform.position, raycastFromObject.transform.forward * interactDistance, Color.green);

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            var interactable = hit.collider.GetComponent<IInteractables>();

            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    currentInteractable?.OnLoseFocus(this);
                    currentInteractable = interactable;
                    currentInteractable.OnFocus(this);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    currentInteractable.OnInteract();
                }
            }
        }
        else
        {
            if (currentInteractable != null)
            {
                currentInteractable.OnLoseFocus(this);
                currentInteractable = null;
            }
        }
    }

    public void ShowDefaultCursor()
    {
        defaultCanvasCrosshair.SetActive(true);
        interactCanvasCrosshair.SetActive(false);
    }

    public void ShowInteractCursor()
    {
        defaultCanvasCrosshair.SetActive(false);
        interactCanvasCrosshair.SetActive(true);
    }
}
