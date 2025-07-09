using UnityEngine;

public class GameManager : MonoBehaviour
{
    public IGameState currentState { get; private set; }

    private void Start()
    {
        ChangeState(new MenuState()); // Default to menu at start
    }

    public void ChangeState(IGameState newState)
    {
        currentState?.Exit(); // Just to make sure we leave the last state when changing to the next one
        currentState = newState;
        currentState.Enter();
    }
}
