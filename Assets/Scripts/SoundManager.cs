using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }



    [SerializeField] private AudioClipRefSO audioClipRefSO;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnrecipeSuccess += DeliveryManager_OnrecipeSuccess;
        DeliveryManager.Instance.OnrecipeFailed += DeliveryManager_OnrecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_ObjectPlacedHere;
        TrashCounter.OnAnyObjectThrashed += TrashCounter_OnAnyObjectThrashed;
    }

    private void TrashCounter_OnAnyObjectThrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(audioClipRefSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_ObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefSO.objectDrop, baseCounter.transform.position);
    }

    private void Player_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnrecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnrecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length-1)],position,volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position , float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootstepSound(Vector2 positon, float volume )
    {
        PlaySound(audioClipRefSO.footstep,positon,volume);
    }
}
