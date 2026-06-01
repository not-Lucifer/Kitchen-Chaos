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

            }
            else
            {
                this.GetKitchenObject().SetKitchenObjectParent(player);
            }

        }
        
    }
    
}
