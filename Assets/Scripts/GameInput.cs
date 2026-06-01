using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;


    private PlayerInputAction playerInputActions;
    private void Awake()
    {
        //Adding the  New Input Class Constructor..
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //if (OnInteractAction != null)
        //{
        //    OnInteractAction(this, EventArgs.Empty);
        //}
        //OR
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNoramalized()
    {
        Vector2 inputvector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputvector = inputvector.normalized;

        return inputvector;
    }
}
