using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioManager;


    public JSONHandler handler;

    private Slider volumeSlider;


    public void Start()
    {

        GameObject testSlider = GameObject.Find("Volume Slider");

        // if found volume slider means in options,
        // else set master volume to current settings
        if (testSlider)
        {
            volumeSlider = GameObject.Find("Volume Slider").GetComponent<Slider>();
            volumeSlider.value = handler.getVolume();
        } else
        {
            setVolume(handler.getVolume());
        }

    }

    public void onEnterOption()
    {
        GameObject testSlider = GameObject.Find("Volume Slider");
        if (testSlider)
        {
            volumeSlider = GameObject.Find("Volume Slider").GetComponent<Slider>();
            volumeSlider.value = handler.getVolume();
        }
    }



    public void setVolume(float sliderValue)
    {
        audioManager.SetFloat("MasterVol", Mathf.Log10(sliderValue) * 20);
    }

    public void saveVolume()
    {
        Setting newSetting = new Setting()
        {
            volume = volumeSlider.value
        };

        handler.OutputJSON(newSetting);
    }


}
