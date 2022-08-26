using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShow : MonoBehaviour
{
    public GameObject bar;
  

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Show();
        }
    }
    void Show()
    {
        Debug.Log("showing");
        bar.SetActive(true);
    }
    public static void Hide()
    {
        
    }
}
