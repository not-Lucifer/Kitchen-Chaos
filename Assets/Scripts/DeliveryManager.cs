using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance {  get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMAX = 4f;
    private int waitingRecipeMAX = 4;


    private void Awake()
    {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMAX;

            if(waitingRecipeSOList.Count < waitingRecipeMAX)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                //has same no of ingredients
                bool plateContentMatchesRecipe = true;
                 foreach(KitchenObjectsSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                 {
                    bool ingredientFound = false;
                    foreach (KitchenObjectsSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound =true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        //This Recipe ingredient was not found
                        plateContentMatchesRecipe = false;  
                    }

                 }

                 if (plateContentMatchesRecipe)
                {
                    Debug.Log("PlayerDelivered Correct Recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        //No Matches Found
        //Player did not deliver a correct recipe
        Debug.Log("Player did not deliver a correct recipe");
    }
}
