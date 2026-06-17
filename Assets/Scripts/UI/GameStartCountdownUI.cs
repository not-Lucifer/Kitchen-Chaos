using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{

    private const string NUMBER_POPUP = "NumberPopup";


    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountdownNumber;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Start()
    {
        KitchenChaosGameManager.Instance.OnStateChanged += KitchenChaosGameManager_OnStateChnaged;
        Hide();
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(KitchenChaosGameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();


        if(previousCountdownNumber != countdownNumber)
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
    }

    private void KitchenChaosGameManager_OnStateChnaged(object sender, System.EventArgs e)
    {
        if (KitchenChaosGameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }

    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
