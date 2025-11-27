using UnityEngine;

public class AudioManager : SingletonePattern <AudioManager>
{
    private AudioSource sfxSource;

    public AudioClip heroHitSoundEffect;
    public AudioClip enemyHitSoundEffect;
    private void Start()
    {
        sfxSource = GetComponent<AudioSource>();
    }

    public void PlayHeroHitSound()
    {
        sfxSource.PlayOneShot(heroHitSoundEffect);
    }
    public void PlayEnemyHitSound()
    {
        sfxSource.PlayOneShot(enemyHitSoundEffect);
    }
}
