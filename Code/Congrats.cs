using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Game;
public class Congrats : MonoBehaviour
{
    GameController gameC = new GameController();
    public int ok=0;
    private void Start()
    {
        display();
    }
    public Text Score;
    public string str;
    public void display()
    {
        Score.text = "Score of this Round: " + gameC.score;
    }
    public void PlayGame()
    {
        if (ok == 0)
        {
            SceneManager.LoadScene(5);
            ok = 1;
        }
        else
            SceneManager.LoadScene(2);
    }
    public void ToMain()
    {
        SceneManager.LoadScene(0);
    }
}
