using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class KSoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    public AudioSource _bgm_AudioSource;
    public AudioClip _bgm_AudioClip;
    public KUIManager _uiManager;

    public static KSoundManager Instance;

    public void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        _bgm_AudioSource = GetComponent<AudioSource>();
        BGM_Play(_bgm_AudioClip);
    }

    private void Update()
    {
        if(!_uiManager._settingBG_Image.gameObject.activeSelf)
        {
            return;
        }
        /* Sound Bar */
        _mixer.SetFloat("BGM_Volume", Mathf.Log10(_uiManager._bgm_Slider.value)*20);
        _mixer.SetFloat("SFX_Volume", Mathf.Log10(_uiManager._sfx_Slider.value)*20);
    }

    /* 추후에 오브젝트 풀 만들어서 사용하기 */
    public void Sound_PlayOneTime(string soundName, AudioClip clip)
    {
        var go = new GameObject(soundName + "Sound");
        var audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = _mixer.FindMatchingGroups("SFX")[0];
        audioSource.clip = clip;
        audioSource.loop = false;
        audioSource.Play();

        Destroy(go, clip.length);
    }
    private void BGM_Play(AudioClip clip)
    {
        _bgm_AudioSource = GetComponent<AudioSource>();
        _bgm_AudioSource.clip = clip;
        _bgm_AudioSource.loop = true;
        _bgm_AudioSource.volume = 0.5f;
        _bgm_AudioSource.Play();
    }
}