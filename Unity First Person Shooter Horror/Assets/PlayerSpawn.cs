using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform Player;
    public LayerMask spawnOutOf;
    public Transform SpawnParticles;
    public Transform GroundCheck;
    void Start()
    {
        SpawnNow1();
        
        
    }

    Vector3 getRandomPos()
    {
        float x = Random.Range(16, 240);
        float z = Random.Range(14, 240);
        float y = 101;
        Vector3 newPos = new Vector3(x, y, z);
        if (!Physics.CheckSphere(newPos, 1f, spawnOutOf))
        {

            return newPos;

        }
        else
        {

            return Vector3.zero;

        }
    }
    void SpawnNow1()
    {


        int i = 0;
        while (i != 1)
        {
            Vector3 Pos = getRandomPos();
            if (Pos != Vector3.zero)
            {
                Player.transform.position = Pos;
                SpawnParticles.transform.position = GroundCheck.position;
               
                i++;
            }

        }





    }
}
