using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMgr : MonoBehaviour
{
    public static SoundMgr Instance;
    public Slider masterSlide;
    public Slider SESlide;
    public Slider BGMSlide;

    
    public float MasterVolume;
    public float SEVolume;
    public float BGMVolume;

    private void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        //인스턴스가 없을떄
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if(PlayerPrefs.HasKey("MasterVolume")) { MasterVolume = PlayerPrefs.GetFloat("MasterVolume"); }
        else
        {
            MasterVolume = 1.0f;
            PlayerPrefs.SetFloat("MasterVolume", MasterVolume);
        }
        if (PlayerPrefs.HasKey("BGMVolume")) { BGMVolume = PlayerPrefs.GetFloat("BGMVolume"); }
        else
        {
            BGMVolume = 1.0f;
            PlayerPrefs.SetFloat("BGMVolume", BGMVolume);
        }
        if (PlayerPrefs.HasKey("SEVolume")) { SEVolume = PlayerPrefs.GetFloat("SEVolume"); }
        else
        {
            SEVolume = 1.0f;
            PlayerPrefs.SetFloat("SEVolume", BGMVolume);
        }
    }

    private void Start()
    {
        masterSlide.value = MasterVolume;
        SESlide.value = SEVolume;
        BGMSlide.value = BGMVolume;
    }

    public void OnControllMaster()
    {
        MasterVolume = masterSlide.value;
    }

    public void OnControllSE()
    {
        SEVolume = SESlide.value;
    }

    public void OnControllBGM()
    {
        BGMVolume = BGMSlide.value;
    }
}
