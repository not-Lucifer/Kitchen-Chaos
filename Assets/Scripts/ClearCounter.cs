using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
   [SerializeField]  private KitchenObjectsSO kitchenObjectsSO;
   [SerializeField]  private Transform counterTopPoint;


    private KitchenObject kitchenObject;

    public void Interact(Player player)
    {
        if(kitchenObject == null)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectsSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
        }
        else
        {
            //give the object to player
            kitchenObject.SetKitchenObjectParent(player);
        }
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return (kitchenObject != null);
    }

}
