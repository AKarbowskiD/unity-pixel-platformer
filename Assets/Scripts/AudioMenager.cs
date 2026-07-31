using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource sfxSource;

    public AudioClip grassWalkSound;
    public AudioClip nonGrassWalkSound;
    public AudioClip jumpSound;
    public AudioClip fuseboxRepair;


    void Awake()
    {
        Instance = this;
    }

    public void PlayGrassWalkSound()
    {
        if (sfxSource.isPlaying) return;

        sfxSource.pitch = 0.8f;
        sfxSource.PlayOneShot(grassWalkSound);
    }

    public void PlayNonGrassWalkSound()
    {
        if (sfxSource.isPlaying) return;

        sfxSource.pitch = 0.8f;
        sfxSource.PlayOneShot(nonGrassWalkSound);
    }

    public void PlayjumpSound()
    {

        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(jumpSound);
        if (sfxSource.isPlaying) return;

    }

    public void PlayfuseboxRepair()
    {
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(fuseboxRepair);
    }
}