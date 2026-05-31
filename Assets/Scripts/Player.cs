using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using static Player;

public class Player : MonoBehaviour , IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnselectedCounterChangedEventArgs> OnselectedCounterChanged;
    public class OnselectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }



    [SerializeField] private float moveSpeed = 10f; //to make this option editable in the unity engine we use [SerializeField]
    [SerializeField] private float rotationSpeed = 20f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayermask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking;
    private Vector3 lastInteractDirection;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than one player Instane");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }
    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteraction()
    {
        Vector2 inputvector = gameInput.GetMovementVectorNoramalized();
        Vector3 moveDir = new Vector3(inputvector.x, 0f, inputvector.y);
        float interactDistance = 2f;

        if (moveDir != Vector3.zero)
        {
            lastInteractDirection = moveDir;
        }

        if(Physics.Raycast(transform.position, lastInteractDirection,out RaycastHit raycastHit, interactDistance,countersLayermask)){
           if( raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //Has Clear Counter
                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }              
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }
    private void HandleMovement()
    {
        Vector2 inputvector = gameInput.GetMovementVectorNoramalized();
        Vector3 moveDir = new Vector3(inputvector.x, 0f, inputvector.y);

        float moveDistance = Time.deltaTime * moveSpeed;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        //CHECKING FOR DIAGONAL MOVEMENT

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized; //Check for X direction 
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;  // Check for Z direction if not X direction.

                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance; //To make movement speed independent of fps.
        }

        isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnselectedCounterChanged?.Invoke(this, new OnselectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        }); 
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return (kitchenObject != null);
    }
}
