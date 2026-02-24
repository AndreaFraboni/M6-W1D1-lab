using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [System.Serializable]
    public class AudioSetData
    {        
        public float masterVolValue;
        public float musicVolValue;
        public float sfxVolValue;
    }
    AudioSetData mAudioSettings = new AudioSetData();

    private void OnEnable()
    {
        AudioManager.Instance.SetSliderValue(_masterSlider, "Master");
        AudioManager.Instance.SetSliderValue(_musicSlider, "Music");
        AudioManager.Instance.SetSliderValue(_sfxSlider, "SFX");

        mAudioSettings.masterVolValue = _masterSlider.value;
        mAudioSettings.musicVolValue = _musicSlider.value;
        mAudioSettings.sfxVolValue = _sfxSlider.value;
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.Instance.SetVolume(value, "Master");
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetVolume(value, "Music");
    }

    public void SetSFXVolume(float value)
    {
        AudioManager.Instance.SetVolume(value, "SFX");
    }


    //**************************************************************************************************//
    public void LoadAudioSettings()
    {
        string saveFile = Application.persistentDataPath + "/audiosettings.json";

        if (File.Exists(saveFile))
        {
            Debug.Log("FILE EXISTS !!!");
            string AudioData = File.ReadAllText(saveFile);
            mAudioSettings = JsonUtility.FromJson<AudioSetData>(AudioData);

            AudioManager.Instance.SetMasterVolume(mAudioSettings.masterVolValue);
            AudioManager.Instance.SetMusicVolume(mAudioSettings.musicVolValue);
            AudioManager.Instance.SetSFXVolume(mAudioSettings.sfxVolValue);

            AudioManager.Instance.SetSliderValue(_masterSlider, "Master");
            AudioManager.Instance.SetSliderValue(_musicSlider, "Music");
            AudioManager.Instance.SetSliderValue(_sfxSlider, "SFX");
        }
        else
        {
            Debug.Log("FILE Audio Settings DOES NOT EXISTS !!!");
        }



    }

    public void SaveAudioSettings()
    {
        string saveFile = Application.persistentDataPath + "/audiosettings.json";
        string json = JsonUtility.ToJson(mAudioSettings);
        File.WriteAllText(saveFile, json);
    }



}
