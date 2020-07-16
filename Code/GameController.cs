using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using Util;
using System;
using System.Threading;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        public Text WordInd;
        public Text ScoreInd;
        public Text LettersInd;
        public Text Entered;
        public Text dialog1;
        public Text dialog2;
        public Text dialog3;
        public Text dialog4;
        public Text dialog5;
        public Text dialog6;
        public Text dialog7;

        public Image dialog01;
        public Image dialog02;
        
        private VictimController victim;

        public string word;
        public int ok=1;
        public int score;
        public char[] revealed;
        public bool finished;
        public string display;
        public int pun = 0;
        
        public AudioSource correct;
        public AudioSource wrong;
        public AudioSource yay;
        public AudioSource hats;
        
        // Use this for initialization
        void Start()
        {
            victim = GameObject.FindGameObjectWithTag("Player").GetComponent<VictimController>();
            Entered.text ="Tried So Far: ";
            reset();
            dialog01.enabled = true;
            dialog02.enabled = true;
            dialog1.enabled = true;
            dialog2.enabled = true;
            dialog3.enabled = false;
            dialog4.enabled = false;
            dialog5.enabled = false;
            dialog6.enabled = false;
            dialog7.enabled = false;
            dialog1.text = "SAVE ME!";
            dialog2.text = "I WILL SAVE YOU!";
            dialog3.text = "AAAAH!!";
            dialog4.text = "HOLD ON!";
            dialog5.text = "PHEW!!!";
            dialog6.text = "PHEW!!!";
            dialog7.text = "NO!!!!!";
        }

        // Update is called once per frame
        void Update()
        {
            
            if (finished)
            {
                string temp = Input.inputString;
                if (Input.anyKeyDown)
                        next();
                return;
            }
            string str = Input.inputString;
            if (str.Length == 1 && TextUtil.isAlpha(str[0]))
                    Debug.Log("Entered " + str);
            {

                if (!display.Contains(str))
                {
                    display = display + str + "    ";
                    pun = 1;
                }    
                Entered.text = "Tried So Far: " +"\n"+ display;
                if (!check(str.ToUpper()[0])&& pun==1)
                    {
                        victim.punish();
                        wrong.Play();
                        dialog1.enabled = false;
                        dialog2.enabled = false;
                        dialog3.enabled = true;
                        dialog4.enabled = true;                    
                        pun = 0;

                        if (victim.isDead)
                        {
                            WordInd.text = "Answer: " + word;
                            finished = true;
                            ok = 0;
                            dialog1.enabled = false;
                            dialog2.enabled = false;
                            dialog01.enabled = false;
                            dialog3.enabled = false;
                            dialog4.enabled = false;
                            dialog7.enabled = true;
                        }
                    }
              }
        }
        private bool check(char c)
        {
            Debug.Log("HERE");
            bool ret = false;
            int finish = 0;
            int score = 0;
            for (int i = 0; i < revealed.Length; i++)
            {
                if (c == word[i])
                { 
                    ret = true;
                    if (revealed[i] == 0)
                    {
                        correct.Play();
                        revealed[i] = c;
                        score+=2;
                        Debug.Log("Correct");
                    }
                }
                
                if (revealed[i] != 0)
                { finish++; Debug.Log("finish="+finish); }
                
            }
            if(score!=0)
            {
                this.score += score;
                if(finish==revealed.Length)
                {
                    this.finished = true;
                    this.score+=score-2;
                    ok = 0;
                    dialog1.enabled = false;
                    dialog2.enabled = false;
                    dialog3.enabled = false;
                    dialog4.enabled = false;
                    dialog5.enabled = true;
                    dialog6.enabled = true;

                    hats.Pause();
                    yay.Play();
                    StopCoroutine("next");
                    StartCoroutine("Wait"); 
                }
                Debug.Log("score=" + this.score);
                updateWordInd();
                updateScoreInd(); 
            }
            return ret;
        }
        private void updateWordInd()
        {
            string displayed = "";
            for (int i = 0; i < revealed.Length; i++)
            {
                char c = revealed[i];
                if (c == 0)
                {
                    c = '_';
                }
                displayed += "  ";
                displayed += c;
            }
            WordInd.text = "Guess: "+displayed;
            Debug.Log("Display");
        }
        private void updateScoreInd()
        {
            ScoreInd.text = "Score:" + score;
        }
        
        private void setWordInd(string word)
        {
            word = word.ToUpper();
            this.word = word;
            revealed = new char[word.Length];
            LettersInd.text = "Letters:" + word.Length;
            updateWordInd();
        }
        
        public void next()
        {
            if(ok==1)
                setWordInd(Dictionary.instance.next(0));
                //setWordInd("APPLEAPPLEAPPLEAPPLEAPPLE");
        }

        public void trans()
        {
            SceneManager.LoadScene(4);
        }

        IEnumerator Wait()
        {

            yield return new WaitForSecondsRealtime(4);
            trans();
            Debug.Log("Waiting");
        }
        public void reset()
        {
            score = 0;
            finished = false;
            next();
            finished = false;
            updateScoreInd();
            victim.reset();
        }
    }
}
