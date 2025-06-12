using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    public AudioSource bgmSource;  // BGM 재생용
    public AudioSource sfxSource;  // 효과음 재생용

    [Header("Clips")]
    public AudioClip backmusic;
    public AudioClip complete;
    public AudioClip heart;
    public AudioClip pick;
    public AudioClip scheduleSelect;
    public AudioClip success;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else Destroy(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // DressUpScene에 들어갈 때만 BGM 플레이
        if (scene.name == "DressUpScene")
        {
            bgmSource.clip = backmusic;
            bgmSource.loop = true;
            bgmSource.Play();
        }
        else
        {
            bgmSource.Stop();
        }
    }

    // 외부에서 호출할 메서드들
    public void PlayPick() => sfxSource.PlayOneShot(pick);
    public void PlayComplete() => sfxSource.PlayOneShot(complete);
    public void PlayHeart() => sfxSource.PlayOneShot(heart);
    public void PlayScheduleSelect() => sfxSource.PlayOneShot(scheduleSelect);
    public void PlaySuccess() => sfxSource.PlayOneShot(success);
}
