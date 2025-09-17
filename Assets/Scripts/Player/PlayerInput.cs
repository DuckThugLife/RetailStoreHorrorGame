using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class PlayerInput : NetworkBehaviour
{
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
          Debug.LogError("PlayerController not found on the same GameObject!");
           
    }

    private void Update()
    {
        if (IsOwner) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Player pressed Escape!");
           
            
            if (playerController.MenuObjectHandler != null)
            {
                playerController.MenuObjectHandler.CloseMenu();
            }
            
        }
    }
}
