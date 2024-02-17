using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObstacleDecoration : MonoBehaviour
{
    [SerializeField] private AudioClip collisionAudio;
    [SerializeField] private Animation collisionAnimation;

    private AudioSource audioSource;
    public AudioSource AudioSource => audioSource == null ? GetComponent<AudioSource>() : audioSource;

    private void Awake() 
    {
        if(collisionAnimation != null) 
        {
            collisionAnimation.playAutomatically = false;

        }

    }

    public virtual void PlayCollisionFeedBack()
    {
        AudioUtility.PlayAudioCue(AudioSource, collisionAudio);
        if(collisionAnimation != null)
        {
            collisionAnimation.Play();
        }
        
    }

}
