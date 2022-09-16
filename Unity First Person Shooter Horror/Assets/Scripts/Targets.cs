using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





namespace Com.Kawaiisun.SimpleHostile
{


    public class Targets : MonoBehaviour
    {
        public Enemy enemy;
        public float health;
        public Slider healthBar;
        public GameObject EnemyDeath1;
        public GameObject EnemyDeath2;
        public GameObject EnemyDeath3;
        public GameObject EnemyDeath4;
        public GameObject EnemyDeath5;
        public AudioSource BodyExplosion;




        public static float currentHealth;
        public float trackHealth;
        public Image Fill;

        public float maxHealth;
        public float noHealth = 0;
        public Color maxHealthColor = Color.green;
        public Color noHealthColor = Color.red;

       

        // Update is called once per frame
        void Update()
        {

            

        }
        public void Start()
        {
            health = enemy.health;
            trackHealth = currentHealth;
            maxHealth = enemy.health;
            currentHealth = maxHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
            Fill.color = Color.Lerp(noHealthColor, maxHealthColor, (float)health / maxHealth);
        }
        public void TakeDamage(float amount)
        {

            health -= amount;
            healthBar.value = health;
            Fill.color = Color.Lerp(noHealthColor, maxHealthColor, (float)health / maxHealth);
            Debug.Log(health);
            if (health <= 0f)
            {
                
                Die();
            }
        }
        void Die()
        {
            
            BodyExplosion.pitch = Random.Range(0.8f, 1.2f);
            BodyExplosion.Play();
            Destroy(gameObject);
            
            EnemyDeath1.SetActive(true);
            EnemyDeath2.SetActive(true);
            EnemyDeath3.SetActive(true);
            EnemyDeath4.SetActive(true);
            EnemyDeath5.SetActive(true);
            Debug.Log("destroyed");
        }
    }
}
