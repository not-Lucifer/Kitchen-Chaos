using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI countdownText;


    private void Start()
    {
        KitchenChaosGameManager.Instance.OnStateChnaged += KitchenChaosGameManager_OnStateChnaged;
        Hide();
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(KitchenChaosGameManager.Instance.GetCountdownToStartTimer()).ToString();
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
