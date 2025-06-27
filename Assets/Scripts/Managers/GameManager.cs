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
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}
