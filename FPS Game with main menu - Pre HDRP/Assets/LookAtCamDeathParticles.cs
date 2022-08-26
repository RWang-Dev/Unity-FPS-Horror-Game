using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamDeathParticles : MonoBehaviour
{
    
   

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y + 180, rot.z);
        transform.rotation = Quaternion.Euler(rot);
    }
}
