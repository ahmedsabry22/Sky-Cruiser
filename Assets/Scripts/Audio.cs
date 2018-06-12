using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource BgMusicAS;
    [SerializeField] private AudioSource ActionsAS;

    [Header("Audio Clips")]
    public AudioClip explosion;
    public AudioClip wheels;

    [SerializeField] private Toggle soundToggle;

    public static Audio Instance;


    private bool active;
    private void Start()
    {
        if (Instance == null)
            Instance = this;

        CheckState();

        if (soundToggle)
            soundToggle.isOn = !Convert.ToBoolean(PlayerPrefs.GetInt(Constants.AUDIO_KEY, 1));
    }

    public void PlayClip(string audioClipName)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/" + audioClipName);
        ActionsAS.PlayOneShot(clip);
    }

    public void Mute(bool state)
    {
        PlayerPrefs.SetInt(Constants.AUDIO_KEY, Convert.ToInt16(!state));
        CheckState();
    }

    private void CheckState()
    {
        active = Convert.ToBoolean(PlayerPrefs.GetInt(Constants.AUDIO_KEY, 1));

        if (BgMusicAS)
        {
            BgMusicAS.enabled = active;
        }
        if (ActionsAS)
        {
            ActionsAS.enabled = active;
        }
    }
}