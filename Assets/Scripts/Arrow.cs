using UnityEngine;

public class Arrow : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip arrowHitsWoodClip;
    public AudioClip arrowFallsOnGroundClip;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Upgrade"))
        {
            PlaySound(arrowHitsWoodClip);
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        PlaySound(arrowFallsOnGroundClip);
    }

    private void PlaySound(AudioClip clip)
    {
        GameObject soundObject = new GameObject("SoundObject");
        AudioSource soundSource = soundObject.AddComponent<AudioSource>();
        
        soundSource.clip = clip;
        soundSource.Play();

        Destroy(soundObject, clip.length);
    }
}