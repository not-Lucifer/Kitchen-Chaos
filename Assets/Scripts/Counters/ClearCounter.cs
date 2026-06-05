using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
   [SerializeField]  private KitchenObjectsSO kitchenObjectsSO;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //Counter is Empty
            if (player.HasKitchenObject())
            {
                //Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate
                   if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                   {
                        GetKitchenObject().DestroySelf();
                   }
                    
                }
                else
                {
                    //player is not carrying plate but something else

                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        //Counter is holding Plate

                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }

            }
            else
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
        
    }
    
}
