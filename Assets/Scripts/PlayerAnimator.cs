using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Player player; // Add the player object to this field from unity.

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();  
    }
    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
