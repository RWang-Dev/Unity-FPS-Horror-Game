using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public GameObject[] enemiesToSpawn;
    public LayerMask spawnOutOf;
    public LayerMask spawnAway;
    public int count = 0;
    void Start()
    {
        InvokeRepeating("SpawnNow", 5, 1);
    }

    Vector3 getRandomPos()
    {
        float x = Random.Range(16, 240);
        float z = Random.Range(14, 240);
        float y = 101;
        Vector3 newPos = new Vector3(x, y, z);
        if (!Physics.CheckSphere(newPos, 1f, spawnOutOf) && !Physics.CheckSphere(newPos, 15f, spawnAway))
        {
            
            return newPos;

        }
        else
        {
            
            return Vector3.zero;

        }
    }

    void SpawnNow()
    {
        int i = 0;
        count += 5;
        
        while (i < 5)
        {
            Vector3 Pos = getRandomPos();
            if(Pos != Vector3.zero)
            {
                Instantiate(enemiesToSpawn[0], Pos, Quaternion.identity);
                i++;
            }
            
        }
                

    }
    private void FixedUpdate()
    {
        if(count == 30)
        {
            CancelInvoke();
        }
    }


}

