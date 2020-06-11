using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;
    private void  Awake() => instance = this;
    public Text score;
    public Text highScore;

    public int intScore;
    public int intHighScore;

    public int IntScore
    {
        get { return intScore; }
        set { this.intScore = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            intHighScore = PlayerPrefs.GetInt("HighScore");
            highScore.text = "최고 점수 : " + intHighScore.ToString();
        }
        else
        {
            intHighScore = 0;
        }
        intScore = 0;
    }

    IEnumerator CheckScore()
    {
        score.text = "점수 : " + intScore.ToString();
        if(intHighScore <= intScore)
        {
            highScore.text = "최고 점수 : " + intHighScore.ToString();
        }

        yield return new WaitForSeconds(0.3f);
    }

    private void Update()
    {
        StartCoroutine(CheckScore());
    }

    public void AddScore()
    {
        intScore++;
        score.text = "점수 : " + intScore.ToString();
        if(intScore >= intHighScore)
        {
            intHighScore = intScore;
            highScore.text = "최고 점수 : " + intHighScore.ToString();
            SaveScore();
        }
    }

    public void SaveScore()
    {
        if(intScore >= intHighScore)
        {
            intHighScore = intScore;
            PlayerPrefs.SetInt("HighScore", intHighScore);
            highScore.text = "최고 점수 : " + intHighScore.ToString();
        }
    }
}
