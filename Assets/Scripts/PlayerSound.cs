using UnityEngine;

public class PlayerSound : MonoBehaviour
{


    private Player player;
    private float footstepTimer;
    private float footstepTimerMAX = 0.1f; 

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if ( footstepTimer < 0f )
        {
            footstepTimer = footstepTimerMAX;
            if (player.IsWalking())
            {
                float volume = 1f;

                SoundManager.Instance.PlayFootstepSound(player.transform.position, volume);
            }
            
        }
    }
}
