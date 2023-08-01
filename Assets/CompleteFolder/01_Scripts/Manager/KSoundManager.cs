using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class KSoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    public AudioSource _bgm_AudioSource;
    public AudioClip[] _bgm_AudioClips;
    public short _BGMCount;
    public AudioClip _titleBGM;
    public KUIManager _uiManager;
    private WaitForSeconds _time = new WaitForSeconds(1);
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
        
        StartCoroutine(PlayBGMList());
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
    private IEnumerator PlayBGMList()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "00_TitleScene":
                _bgm_AudioSource.clip = _titleBGM;
                _bgm_AudioSource.loop = true;
                _bgm_AudioSource.volume = 0.1f;
                _bgm_AudioSource.Play();
                yield break;
            case "01_GameScene":
                if(_bgm_AudioSource.isPlaying)
                {
                    _bgm_AudioSource.Stop();
                }
                while (true)
                {
                    yield return _time;
                    if (_bgm_AudioSource.isPlaying) continue;
                    _BGMCount++;
                    if (_BGMCount >= _bgm_AudioClips.Length)
                    {
                        _BGMCount = 0;
                    }

                    _bgm_AudioSource.clip = _bgm_AudioClips[_BGMCount];
                    _bgm_AudioSource.volume = 0.3f;
                    _bgm_AudioSource.Play();
                }
                yield break;
            default:
                yield break;
        }
    }
}
