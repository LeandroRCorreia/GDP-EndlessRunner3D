using System;
using UnityEngine;

public abstract class Pickup : MonoBehaviour, ICollideableReact
{   
    [SerializeField] private AudioClip pickupAudio;
    [SerializeField] private GameObject model;
    private bool IsPickedUp = false;

    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null 
    ? audioSource = GetComponentInChildren<AudioSource>()
    : audioSource;

    public void PlayCollisionFeedBack(in IPlayerInfo playerInfo)
    {
        OnPickup(in playerInfo);
    }

    private void OnPickup(in IPlayerInfo playerInfo)
    {
        if(!IsPickedUp)
        {
            IsPickedUp = true;
            AudioSource.PlayAudioCue(pickupAudio);
            model.SetActive(false);
            ExecutePickupBehaviour(in playerInfo);
            Destroy(gameObject, pickupAudio.length);
        }

    }

    protected abstract void ExecutePickupBehaviour(in IPlayerInfo playerInfo);

}
