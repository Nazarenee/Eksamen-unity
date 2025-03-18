using UnityEngine;

public class Arrow : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource on the arrow
    public AudioClip arrowHitsWoodClip; // Sound when arrow hits wood
    public AudioClip arrowFallsOnGroundClip; // Sound when arrow falls on the ground

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the arrow hits an object tagged "wood"
        if (collision.gameObject.CompareTag("Upgrade"))
        {
            // Play the arrow hits wood sound
            PlaySound(arrowHitsWoodClip);
        }

        // Destroy the arrow after the collision
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // Play the fall sound when the arrow is destroyed
        PlaySound(arrowFallsOnGroundClip);
    }

    // Method to play a sound using a temporary AudioSource
    private void PlaySound(AudioClip clip)
    {
        // Create a new GameObject to attach the AudioSource
        GameObject soundObject = new GameObject("SoundObject");
        AudioSource soundSource = soundObject.AddComponent<AudioSource>();
        
        // Set the clip and play the sound
        soundSource.clip = clip;
        soundSource.Play();

        // Destroy the soundObject after the sound finishes playing
        Destroy(soundObject, clip.length);
    }
}