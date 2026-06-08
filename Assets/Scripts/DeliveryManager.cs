using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnrecipeSpawned;
    public event EventHandler OnrecipeCompleted;
    public event EventHandler OnrecipeSuccess;
    public event EventHandler OnrecipeFailed;

    public static DeliveryManager Instance {  get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMAX = 4f;
    private int waitingRecipeMAX = 4;
    private int successfullRecipesAmount;


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
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnrecipeSpawned?.Invoke(this, EventArgs.Empty);
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
                    waitingRecipeSOList.RemoveAt(i);

                    successfullRecipesAmount++;

                    OnrecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnrecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //No Matches Found
        //Player did not deliver a correct recipe
        OnrecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }


    public int GetSuccessfullRecipesAmount()
    {

        return successfullRecipesAmount;
    }
}
