using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectableSpawn : MonoBehaviour
{
    public GameObject[] collectablesToSpawn;
    public Levels[] level;
    
    public LayerMask spawnOutOf;
    public LayerMask spawnAway;
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
        if (!Physics.CheckSphere(newPos, 1f, spawnOutOf) && !Physics.CheckSphere(newPos, 15f, spawnAway))
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
        while (i < level[0].numberOfFragments)
        {
            Vector3 Pos = getRandomPos();
            if (Pos != Vector3.zero)
            {
                Instantiate(collectablesToSpawn[0], Pos, Quaternion.identity);

                i++;
            }
            
        }
           
            
            
           

        }
    }

   

