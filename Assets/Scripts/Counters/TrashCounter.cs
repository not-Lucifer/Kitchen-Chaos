using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectThrashed;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectThrashed?.Invoke(this , EventArgs.Empty);
        }
    }
   
}
