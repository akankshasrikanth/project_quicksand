using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using System.Threading;
namespace Game
{
    public class VictimController : MonoBehaviour
    {
        public GameObject Head;
        public GameObject Torso;
        public GameObject ARight;
        public GameObject ALeft;
        public GameObject LRight;
        public GameObject LLeft;
        public int ok = 0;
        
        public AudioSource scream;
        public AudioSource hats;
        //string[] has = new string[100];
        private int lives;
        private GameObject[] parts;
        GameController gameC = new GameController();
        

        public bool isDead
        {
            get { return lives < 0; }
        }
        // Use this for initialization
        void Start()
        {
            //StartCoroutine(Example());
            parts = new GameObject[] {Head,Torso,ARight,ALeft,LRight,LLeft};
            reset();
            Debug.Log("HLives=" + lives);
            
            
        }
        
        public void punish()
        {
            if (lives >-2)
            {
                parts[lives--].SetActive(false);
                Debug.Log("Lives=" + lives);
               if (lives == -1)
                { lives--; Debug.Log("Lives=" + lives); }
             }

            if (lives == -2)
            {
                StopCoroutine("punish");
                StartCoroutine("Wait");
                hats.Pause();
                scream.Play();
            }
        }
        public void halt()
        {
            SceneManager.LoadScene(3);
        }
        IEnumerator Wait()
        {
            
            yield return new WaitForSecondsRealtime(3);
            halt();
            Debug.Log("Waiting");
        }
        public void reset()
        {
            if (parts == null)
                return;
            lives = parts.Length-1;
            foreach (GameObject g in parts)
            {
                g.SetActive(true);
            }
            
        }
    }
}

