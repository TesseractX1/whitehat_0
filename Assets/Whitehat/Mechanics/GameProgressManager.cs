namespace Whitehat.Mechanics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class GameProgressManager : MonoBehaviour
    {
        [SerializeField] private AttackWaveManager attackWaveManager;

        public GameObject core;

        public GameObject gameOver;
        public GameObject pause;

        [SerializeField] private bool paused;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused=!paused;
            }

            if (paused)
            {
                pause.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pause.SetActive(false);
                Time.timeScale = 1;
            }

            if (!core)
            {
                Time.timeScale = 0;
                gameOver.SetActive(true);
                gameOver.GetComponent<Text>().text = "Game Over. You survived " + attackWaveManager.wave + " waves of attack.";
            }
        }

        public void ResumePause()
        {
            paused = false;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}