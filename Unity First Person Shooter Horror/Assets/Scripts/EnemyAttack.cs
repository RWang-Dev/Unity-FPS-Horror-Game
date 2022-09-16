
using UnityEngine;
using UnityEngine.UI;

namespace Com.Kawaiisun.SimpleHostile
{
    public class EnemyAttack : MonoBehaviour
    {

        public Transform AttackPoint;
        
        public LayerMask PlayerLayer;
        public Enemy enemy;
        bool PlayerCheck;
        private float updateHealth;




        float timeToFire = 0f;
        // Update is called once per frame

        private void Start()
        {
            updateHealth = 500f;
        }
        void Update()
        {
            PlayerCheck = Physics.CheckSphere(AttackPoint.position, enemy.range, PlayerLayer);
            if (PlayerCheck == true && Time.time >= timeToFire)
            {
                Attack();
                timeToFire = Time.time + 1f / enemy.attackSpeed;
            }
        }
        public void Attack()
        {
            updateHealth = FindObjectOfType<PlayerHealth>().Health -= enemy.damage;
            healthBar.currentHealth = updateHealth;
            Debug.Log("Your Health is: " + FindObjectOfType<PlayerHealth>().Health);
            if (FindObjectOfType<PlayerHealth>().Health <= 0)
            {
                FindObjectOfType<PlayerHealth>().GameOver();
            }
        }
    }
}
