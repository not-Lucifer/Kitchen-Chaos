using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
   [SerializeField]  private KitchenObjectsSO kitchenObjectsSO;

    public override void Interact(Player player)
    {
        
    }
    
}
