using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    #region Variables
    public Transform height;
    public static bool isCrouching = false;
    #endregion

    #region Monobehavior Callbacks
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            height.transform.localScale = new Vector3(0.85f, 0.75f, 0.85f);


        }
        else
        {
            isCrouching = false;
            height.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    #endregion
}
