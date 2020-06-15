using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject optionMenu;
    //BaseMenu
    public void OnStartButtonClick()    //GameScene로 Scene 전환
    {
        SceneMgr.Instance.LoadScene("GameScene");
    }
    public void OnOptionButtonClick()   //Option 버튼에 해당되는 기능 실행
    {
        optionMenu.SetActive(true);
    }
    public void OnExitButtonClick()     //Application 종료
    {
        Application.Quit();
    }
    //OptionMenu
    public void OnReturnButtonClick()
    {
        optionMenu.SetActive(false);

        PlayerPrefs.SetFloat("MasterVolume", SoundMgr.Instance.MasterVolume);
        PlayerPrefs.SetFloat("SEVolume", SoundMgr.Instance.SEVolume);
        PlayerPrefs.SetFloat("BGMVolume", SoundMgr.Instance.BGMVolume);
    }
}
