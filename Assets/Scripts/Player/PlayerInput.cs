using Unity.Netcode;
using UnityEngine;

public class PlayerInput : NetworkBehaviour
{
    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
    }

    private void Update()
    {
        if (!IsOwner) return; // Prevent input if not local player

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Player pressed Escape!");
            stateMachine.ChangeState(new UIState(UIManager.Instance));
        }

        // Pass update through to the current state
        stateMachine.Update();
    }
}