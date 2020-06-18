using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public int enemy1Value = 10;
    public int enemy2Value = 5;
    private int currentScore = 0;
    private Text textScore;
    private Rigidbody2D player;

    void Awake()
    {
        textScore = GameObject.FindGameObjectWithTag(Tags.SCORE).GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.ENEMY2)
        {
            AddScoreFromEnemy2();
        }
        else if(collision.gameObject.tag == Tags.ENEMY)
        {
            AddScoreFromEnemy1();
        }
    }

    public void AddScoreFromEnemy1()
    {
        currentScore += enemy1Value;
        UpdateTextScore();
    }

    public void AddScoreFromEnemy2()
    {
        currentScore += enemy2Value;
        UpdateTextScore();
    }

    public void UpdateTextScore()
    {
        textScore.text = "Score : " + currentScore;
    }
}
