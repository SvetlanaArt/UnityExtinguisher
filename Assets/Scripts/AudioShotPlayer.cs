using UnityEngine;

public class AudioShotPlayer : MonoBehaviour
{
    [Header("Bolt")]
    [SerializeField] AudioClip boltGetOut;
    [SerializeField] AudioClip boltFallDown;
    [SerializeField][Range(0f, 1f)] float boltSoundVolume = 1f;

    [Header("Uchwyt")]
    [SerializeField] AudioClip handleGetDownUp;
    [SerializeField][Range(0f, 1f)] float handleSoundVolume = 1f;

     public void PlayBoltGetSound(Vector3 pos)
    {
        PlaySound(boltGetOut, boltSoundVolume, pos);
    }

    public void PlayBoltFallSound(Vector3 pos)
    {
        PlaySound(boltFallDown, boltSoundVolume, pos);
    }

    public void PlayHandleGetSound(Vector3 pos)
    {
        PlaySound(handleGetDownUp, handleSoundVolume, pos);
    }

      void PlaySound(AudioClip sound, float volume, Vector3 pos)
    {
        if (sound != null)
        {
            AudioSource.PlayClipAtPoint(sound, pos, volume);
        }
    }

}
