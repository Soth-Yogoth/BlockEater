using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private LevelStateManager levelStateManager;
    [SerializeField] private GameManager gameManager;
    
    private InputAction moveAction;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
    }
    
    private void OnEnable()
    {
        moveAction.performed += OnMove;
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.performed -= OnMove;
        moveAction.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if(gameManager.IsPaused) return;
        
        Vector2 input = context.ReadValue<Vector2>();

        Vector2Int direction = new Vector2Int(
            Mathf.RoundToInt(input.x),
            Mathf.RoundToInt(input.y)
        );
        
        if (direction != Vector2.zero)
        {
            levelStateManager.HandlePlayerMove(direction);
        }
    }
}
