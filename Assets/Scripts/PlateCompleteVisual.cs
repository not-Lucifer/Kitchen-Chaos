using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectsSO kitchenObjectsSO;
        public GameObject gameObject;
    }


    [SerializeField] PlateKitchenObject plateKitchenObject;
    [SerializeField] List<KitchenObjectSO_GameObject> kitchenObjectSOgameObjectList;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (KitchenObjectSO_GameObject kitchenObjectSOgameObject in kitchenObjectSOgameObjectList)
        {
            kitchenObjectSOgameObject.gameObject.SetActive(false);            
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach(KitchenObjectSO_GameObject kitchenObjectSOgameObject in kitchenObjectSOgameObjectList)
        {
            if(kitchenObjectSOgameObject.kitchenObjectsSO == e.kitchenObjectsSO)
            {
                kitchenObjectSOgameObject.gameObject.SetActive(true);
            }
        }

        
    }
}
