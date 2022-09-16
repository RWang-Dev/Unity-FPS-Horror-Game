using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineFade : MonoBehaviour
    
{
    public Color color;
    public float speed = 10f;
    LineRenderer LR;
    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //move towards zero
        color.a = Mathf.Lerp(color.a, 0, Time.deltaTime * speed);

        //update color
        //LR.SetColors(color,color);
        LR.startColor = color;
        LR.endColor = color;
    }
}
