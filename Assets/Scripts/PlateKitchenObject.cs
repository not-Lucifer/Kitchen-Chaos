using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectsSO kitchenObjectsSO;
    }


    [SerializeField] List<KitchenObjectsSO> validkitchenObjectsSOList;


    private List<KitchenObjectsSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectSO)
    {
        if (!validkitchenObjectsSOList.Contains(kitchenObjectSO)){
            //Not a valid item
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Already has this type
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{
                kitchenObjectsSO = kitchenObjectSO
            });

            return true;
        }
    }
}
