using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;


    private void Start()
    {
        KitchenChaosGameManager.Instance.OnStateChnaged += KitchenChaosGameManager_OnStateChnaged;
        Hide();
    }

  

    private void KitchenChaosGameManager_OnStateChnaged(object sender, System.EventArgs e)
    {
        if (KitchenChaosGameManager.Instance.IsGameOver())
        {
            Show();

            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfullRecipesAmount().ToString();
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
