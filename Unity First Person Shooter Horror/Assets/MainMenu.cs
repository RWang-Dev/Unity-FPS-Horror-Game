using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Com.Kawaiisun.SimpleHostile
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject settingsMenu;
        public GameObject levelMenu;
        public GameObject creditsMenu;



        public void Settings()
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
        public void Levels()
        {
            mainMenu.SetActive(false);
            levelMenu.SetActive(true);
        }
        public void Credits()
        {
            mainMenu.SetActive(false);
            creditsMenu.SetActive(true);
        }
        public void returnFromSettings()
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        public void returnFromLevels()
        {
            levelMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        public void returnFromCredits()
        {
            creditsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        public void PlayGame()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Look.cursorLocked = false;
            PlayerHealth.isGameOver = false;
            countCollectables.isWon = false;
            SceneManager.LoadScene("Level 1");
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            
        }

    }
}