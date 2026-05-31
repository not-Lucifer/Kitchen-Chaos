using System;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectsSO kitchenObjectsSO;


    public override void Interact(Player player)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }

}
