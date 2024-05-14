using UnityEngine;
using UnityEngine.UI;

public class MusicaVolumeController : MonoBehaviour { 

    private Slider slider;

    public void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = GameManager.instance.musicaVolume;
    }

    public void setMusicaVolume(System.Single volume)
    { 
        GameManager.instance.musicaVolume = volume;
    }
}
