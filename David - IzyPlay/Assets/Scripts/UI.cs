using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //VARIÁVEIS DE PONTUAÇÃO
    public Text scoreText, coinText;
    public static int score, coin, currentScore;

    //VARIÁVEIS GAMEOVER BOX
    public GameObject gameOverBox;
    public static bool isGameOver, isGameWon = false;

    //VARIÁVEIS KNIFE HUD
    public Image[] knifeHud;

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        scoreText.text = score.ToString();
        coinText.text = coin.ToString();

        if (isGameOver)
        {
            StartCoroutine(EnableGameOver());
        }
        else if (isGameWon)
        {
            StartCoroutine(NextStage());
        }

        switch (currentScore)
        {
            case 0:
                knifeHud[0].color = Color.white;
                knifeHud[1].color = Color.white;
                knifeHud[2].color = Color.white;
                knifeHud[3].color = Color.white;
                knifeHud[4].color = Color.white;
                knifeHud[5].color = Color.white;
                knifeHud[6].color = Color.white;
                knifeHud[7].color = Color.white;
                knifeHud[8].color = Color.white;
                break;
            case 1:
                knifeHud[0].color = Color.black;
                break;
            case 2:
                knifeHud[1].color = Color.black;
                break;
            case 3:
                knifeHud[2].color = Color.black;
                break;
            case 4:
                knifeHud[3].color = Color.black;
                break;
            case 5:
                knifeHud[4].color = Color.black;
                break;
            case 6:
                knifeHud[5].color = Color.black;
                break;
            case 7:
                knifeHud[6].color = Color.black;
                break;
            case 8:
                knifeHud[7].color = Color.black;
                break;
            case 9:
                knifeHud[8].color = Color.black;
                isGameWon = true;
                currentScore = 0;
                break;
        }
    }

    IEnumerator EnableGameOver()
    {
        yield return new WaitForSeconds(0.4f);
        gameOverBox.SetActive(true);
    }
    
    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(1f);

    }
}
