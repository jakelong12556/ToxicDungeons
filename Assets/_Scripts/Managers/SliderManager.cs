using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public MusicManager musicManager;

    public void setVolume(float sliderValue)
    {
        musicManager.setVolume(sliderValue);
    }

}
