using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    //BaseMenu
    public void OnStartButtonClick()    //GameScene로 Scene 전환
    {
        SceneMgr.Instance.LoadScene("GameScene");
    }
    public void OnOptionButtonClick()   //Option 버튼에 해당되는 기능 실행
    {
        
    }
    public void OnExitButtonClick()     //Application 종료
    {
        Application.Quit();
    }
    //OptionMenu
    public void OnReturnButtonClick()
    {

    }
}
