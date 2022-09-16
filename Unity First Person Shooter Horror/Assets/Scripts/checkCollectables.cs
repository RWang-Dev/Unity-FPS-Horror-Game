using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Com.Kawaiisun.SimpleHostile
{
    public class checkCollectables : MonoBehaviour
    {
        public LayerMask Player;
        public GameObject Items;
        public AudioSource Collecting;
        public bool canCollect;
        public static float numberCollected;

        private void Start()
        {
            numberCollected = 0;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            canCollect = Physics.CheckSphere(transform.position, 0.5f, Player);
            if (canCollect)
            {
                if (FindObjectOfType<PlayerHealth>().Health <= FindObjectOfType<healthBar>().maxHealth - 100)
                {
                    FindObjectOfType<PlayerHealth>().Health += 100;
                }
                else FindObjectOfType<PlayerHealth>().Health += (FindObjectOfType<healthBar>().maxHealth - FindObjectOfType<PlayerHealth>().Health);

                FindObjectOfType<countCollectables>().Collecting.Play();
                numberCollected++;
                Destroy(Items);
            }
            if (numberCollected == 10)
            {
                FindObjectOfType<countCollectables>().WonGame();
            }
        }
    }
}
