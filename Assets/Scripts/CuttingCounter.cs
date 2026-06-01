using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //Counter is Empty
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                if (HasRecipewithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
            else
            {
                //Player is not carrying anything
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipewithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            KitchenObjectsSO outputKitchenObjectSO = GetOutputforInput(GetKitchenObject().GetKitchenObjectSO());

            GetKitchenObject().DestroySelf();

            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private bool HasRecipewithInput(KitchenObjectsSO inputkitchenobjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputkitchenobjectSO)
            {
                return true;
            }
        }
        return false;
    }

    private KitchenObjectsSO GetOutputforInput(KitchenObjectsSO inputkitchenobjectSO)
    {
        foreach( CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input == inputkitchenobjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}
