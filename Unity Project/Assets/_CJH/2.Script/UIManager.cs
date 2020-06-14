using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static public UIManager instance;
    private void  Awake() => instance = this;
    public Text score;
    public Text highScore;
    private GameObject warning;
    private TextMeshPro notice;
    private Slider bossHP;
    
    private int intScore;
    public int IntScore
    {
        get { return intScore; }
        set { this.intScore = value; }
    }
    public int intHighScore;

    private bool bossSpawn;
    public bool BossSpawn
    {
        set { bossSpawn = value; }
    }
    private bool gameOver;
    public bool GameOver
    {
        set { gameOver = value; }
    }
    public float BossHp
    {
        get { return bossHP.value; }
        set { bossHP.value = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        warning = transform.GetChild(2).gameObject;
        notice = transform.GetChild(3).GetComponent<TextMeshPro>();
        bossHP = transform.GetChild(4).GetComponent<Slider>();
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
        BossSpawn = false;
        GameOver = false;
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
    public void AddScore(int _score)
    {
        intScore+= _score;
        score.text = "점수 : " + intScore.ToString();
        if (intScore >= intHighScore)
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

    public void Alert()
    {
        EnemyManager.instance.BossSpawn = true;
        bossHP.gameObject.SetActive(true);
        //warning.gameObject.SetActive(true);
        //StartCoroutine(Warning());
    }
    public IEnumerator Warning()
    {
        bool isMax = true;
        int count = 0;
        while(true)
        {
            if(count == 30)
            {
                warning.gameObject.SetActive(false);
                EnemyManager.instance.BossSpawn = true;
                StopCoroutine(Warning());
            }
            if(isMax)
            {
                warning.GetComponent<TextMeshPro>().faceColor 
                    = new Color(warning.GetComponent<TextMeshPro>().faceColor.r - 10, 0, 0);
                if(warning.GetComponent<TextMeshPro>().faceColor.r <= 0) { isMax = false; }
                count++;
                BossHp += 30;
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                warning.GetComponent<TextMeshPro>().faceColor
                    = new Color(warning.GetComponent<TextMeshPro>().faceColor.r + 10, 0, 0);
                if(warning.GetComponent<TextMeshPro>().faceColor.r >= 250) { isMax = true; }
                count++;
                BossHp += 30;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
