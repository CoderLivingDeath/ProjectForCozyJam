using System.Collections;
using UnityEngine;

public class DestroyParticalFXBehaviour : MonoBehaviour
{
    public ParticleSystem ParticleSystem;

    public void Play()
    {
        StartCoroutine(PlayCoroutine());
    }

    private IEnumerator PlayCoroutine()
    {
        if (ParticleSystem != null)
        {
            ParticleSystem.Play();

            // Wait for the duration of the particle system
            yield return new WaitForSecondsRealtime(ParticleSystem.main.startLifetime.constant);

            // Destroy the current object
            Destroy(this.gameObject);
        }
        else
        {
            Debug.LogWarning("ParticleSystem is not assigned!");
        }
    }
}
