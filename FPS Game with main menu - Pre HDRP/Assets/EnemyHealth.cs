using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Com.Kawaiisun.SimpleHostile
{
    public class EnemyHealth : MonoBehaviour
    {
        public Slider health;
        public Enemy enemyHPTrack;

        public static float currentHealth;
        public float trackHealth;
        public Image Fill;

        public float maxHealth;
        public float noHealth = 0;
        public Color maxHealthColor = Color.green;
        public Color noHealthColor = Color.red;

        void Start()
        {
            trackHealth = currentHealth;
            maxHealth = enemyHPTrack.health;
            currentHealth = maxHealth;
            health.maxValue = maxHealth;
            health.value = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            
            trackHealth = currentHealth;

            currentHealth = FindObjectOfType<PlayerHealth>().Health;
            health.value = FindObjectOfType<Targets>().health;
            Fill.color = Color.Lerp(noHealthColor, maxHealthColor, (float)currentHealth / maxHealth);


        }
    }
}
