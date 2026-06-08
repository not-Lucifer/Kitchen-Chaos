using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectThrashed;

    new public static void ResetStaticData()
    {
        OnAnyObjectThrashed = null;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectThrashed?.Invoke(this , EventArgs.Empty);
        }
    }
   
}
