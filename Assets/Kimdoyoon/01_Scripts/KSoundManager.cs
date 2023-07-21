using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class KSoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    public AudioSource BGM_audioSource;
    public AudioClip BGM_AudioClip;
    public KUIManager uiManager;

    public static KSoundManager instance;

    public void Awake()
    {
        BGM_audioSource = GetComponent<AudioSource>();
        BGM_Play(BGM_AudioClip);
    }

    private void Update()
    {
        if(!uiManager.SettingBG_Image.gameObject.activeSelf)
        {
            return;
        }
        /* 사운드Bar 조정 */
        mixer.SetFloat("BGM_Volume", Mathf.Log10(uiManager.BGM_Slider.value)*20);
        mixer.SetFloat("SFX_Volume", Mathf.Log10(uiManager.SFX_Slider.value)*20);
    }

    public void Sound_PlayOneTime(string _soundName, AudioClip clip)
    {
        GameObject go = new GameObject(_soundName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("EffectSound")[0];
        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.Play();

        Destroy(go, clip.length);
    }
    public void BGM_Play(AudioClip clip)
    {
        BGM_audioSource = GetComponent<AudioSource>();
        BGM_audioSource.clip = clip;
        BGM_audioSource.loop = true;
        BGM_audioSource.volume = 0.5f;
        BGM_audioSource.Play();
    }
}