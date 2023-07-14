using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using DG.Tweening;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public static SoundManager instance;
    public AudioSource bgSound;
    public AudioClip[] bglist;
    private float bgValue;
    private float sfValue;
    // Start is called before the first frame update
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bglist.Length; i++)
        {
            if (arg0.name == bglist[i].name)
                BgSoundPlay(bglist[i]);
        }
    }
    public void SFXSoundVolume(float val)
    {
        if (val > 0)
        {
            mixer.SetFloat("SFXVolume", Mathf.Log10(val) * 20); 
        }
        else
        {
            mixer.SetFloat("SFXVolume", -80);
        }
        sfValue = val;
    }
    public void BGSoundVolume(float val)
    {
        if (val > 0)
        {
            mixer.SetFloat("BGMVolume", Mathf.Log10(val) * 20); 
        }
        else
        {
            mixer.SetFloat("BGMVolume", -80);
        }
        bgValue = val;
    }
    public void SFXplay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);
    }
    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGSound")[0];
        bgSound.volume = 0.1f;
        bgSound.Play();
    }
    
}
