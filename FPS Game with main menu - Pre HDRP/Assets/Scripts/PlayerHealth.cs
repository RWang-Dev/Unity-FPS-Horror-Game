
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Com.Kawaiisun.SimpleHostile
{
    public class PlayerHealth : MonoBehaviour
    {
        public float Health = 500f;
        public GameObject DeathUI;
        public static bool isGameOver = false;
        public GameObject HealthUI;
        public GameObject StaminaUI;
        public GameObject AmmoUI;
        public GameObject ReloadUI;
        public GameObject CrosshairUI;
        public GameObject HeartImage;
        public GameObject EnergyImage;
        public GameObject CountFragments;
        public GameObject MiniMapCam;
        public GameObject MiniMapImage;
        public Button Restart;

        public void GameOver()
        {
            StopAllCoroutines();
            isGameOver = true;
            DeathUI.SetActive(true);
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


        }
        public void FixedUpdate()
        {
           
        }
        public void restartButton()
        {
            DeathUI.SetActive(false);
            SceneManager.LoadScene("Level 1");
            Time.timeScale = 1;
            
           
        }
        public void setCursor()
        {
            isGameOver = false;
            Debug.Log("Cursor Set");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            Look.cursorLocked = false;
        }

        public void MainMenu()
        {
            
            DeathUI.SetActive(false);
            
            
            SceneManager.LoadScene("Main Menu");
            

        }
    }
}