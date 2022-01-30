using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager inst;

    public List<AudioClip> songList;
    public List<AudioClip> sfxList;

    public AudioSource songSource;
    public AudioSource sfxSource;
    private float startVol;
    public int currSong = 0;

    private void Awake()
    {
        if (inst != null)
        {
            Destroy(this);
        }
        else
        {
            inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    void Start()
    {
        inst = this;
        startVol = songSource.volume;
        songSource.clip = songList[currSong];
        songSource.Play();
    }

    public IEnumerator FadeSongOut(float duration, float targetVolume, int song)
    {
        float currentTime = 0;
        float start = songSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            songSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        StartCoroutine(FadeSongIn(1f, start, song));
        yield break;
    }

    public IEnumerator FadeSongIn(float duration, float targetVolume, int song)
    {
        float currentTime = 0;
        float start = songSource.volume;
        songSource.clip = songList[song];
        songSource.Play();

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            songSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    public void PlaySuccess(int sfxNumber)
    {
        sfxSource.clip = sfxList[sfxNumber];
        if (!sfxSource.isPlaying)
            sfxSource.Play();
    }
}
