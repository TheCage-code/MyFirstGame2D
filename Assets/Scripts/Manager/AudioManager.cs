using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioMixer musicMixer, effectMixer;
    public AudioSource bgGroundMusicAS;
    public static AudioManager Instance;
    [Range(-80, 20)]
    public float effectVol, masterVol;

    public Slider masterSlr, effectSlr;

   


    




    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void MasterVolume()
    {
        DataManager.instance.SetMusicData(masterSlr.value);
        musicMixer.SetFloat("masterVolume",PlayerPrefs.GetFloat("MusicVolume"));
    }
    public void EffectVolume()
    {
        DataManager.instance.FXData(effectSlr.value);
        effectMixer.SetFloat("effectVolume", PlayerPrefs.GetFloat("FXVolume"));
    }
    void Start()
    {
        PlayAudio(bgGroundMusicAS);
       // masterSlr.value = masterVol;
       // effectSlr.value = effectVol;
        masterSlr.minValue = -80;
        masterSlr.maxValue = 20;
        effectSlr.minValue = -80;
        effectSlr.maxValue = 20;

        masterSlr.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
        effectSlr.value = PlayerPrefs.GetFloat("FXVolume", 0f);
    }

    
    void Update()
    {
       // MasterVolume();
        //EffectVolume();

       
    }
    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}
