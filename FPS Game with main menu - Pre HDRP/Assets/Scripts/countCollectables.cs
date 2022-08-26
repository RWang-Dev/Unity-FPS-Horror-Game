using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace Com.Kawaiisun.SimpleHostile
{
    public class countCollectables : MonoBehaviour
    {
        public Text fragmentCount;
        public float count;
        public GameObject WinningUI;
        public GameObject HealthUI;
        public GameObject StaminaUI;
        public GameObject AmmoUI;
        public GameObject ReloadUI;
        public GameObject CrosshairUI;
        public GameObject HeartImage;
        public GameObject EnergyImage;
        public GameObject CountFragments;
        public GameObject MiniMapCam;
        public AudioSource Collecting;
        public GameObject MiniMapImage;
        public Button Restart;
        public static bool isWon = false;
        void Start()
        {
            count = 0;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
            fragmentCount.text = count.ToString();
            count = checkCollectables.numberCollected;
            
            

            
            
        }
        public void restartButton()
        {
           
            SceneManager.LoadScene("Level 1");
            Time.timeScale = 1;



        }
        public void setCursor()
        {
            isWon = false;
            Debug.Log("Cursor Set");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Look.cursorLocked = false;
        }
        public void WonGame()
        {
            
            WinningUI.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
            HealthUI.SetActive(false);
            StaminaUI.SetActive(false);
            AmmoUI.SetActive(false);
            ReloadUI.SetActive(false);
            CrosshairUI.SetActive(false);
            HeartImage.SetActive(false);
            EnergyImage.SetActive(false);
            CountFragments.SetActive(false);
            MiniMapCam.SetActive(false);
            MiniMapImage.SetActive(false);
            isWon = true;


        }
        public void MainMenu()
        {
            WinningUI.SetActive(false);
            SceneManager.LoadScene("Main Menu");
        }

    }
}
