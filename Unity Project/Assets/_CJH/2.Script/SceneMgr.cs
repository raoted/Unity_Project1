using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    //씬매니저 싱글톤 만들기
    //씬매니저는 시작, 게임, 종료씬 모두를 관리해야 한다.
    //또한 씬매니저는 씬이 변경되도 삭제되면 안된다.
    public static SceneMgr Instance;
    private void Awake()
    {
        //씬매니저가 존재한다면
        //새로 생성되는 씬 매니저는 삭제하고 바로 빠져나와라
        if(Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        //인스턴스가 없을떄
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string value)
    {
        SceneManager.LoadScene(value);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
