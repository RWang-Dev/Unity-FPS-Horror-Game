using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.Kawaiisun.SimpleHostile {
    public class Look : MonoBehaviour
    {
        public static bool cursorLocked = true;
        public Transform player;
        public Transform cams;
        public Transform weapon;
        public Transform FlashLight;
        public float xRotation = 0f;
        public float xSens;
        public float ySens;
        public float maxAngle;
        public GameObject DeathUI;

        private Quaternion camCenter;
        // Start is called before the first frame update
        void Start()
        {
            camCenter = cams.localRotation; //set rotation origin for camera to camCenter
        }

        // Update is called once per frame
        void Update()
        {
            
            SetY();
            SetX();
            UpdateCursorLock();
            
        }

        void SetY()
        {
            float t_input = Input.GetAxis("Mouse Y") * ySens * Time.deltaTime;
            Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);
            Quaternion t_delta = cams.localRotation * t_adj;
            xRotation -= t_input;
            
            if (Quaternion.Angle(camCenter, t_delta) < maxAngle)
            {
                cams.localRotation = t_delta;
                weapon.localRotation = cams.localRotation;
                FlashLight.localRotation = cams.localRotation;
            }
            


        }
        void SetX()
        {
            float t_input = Input.GetAxis("Mouse X") * xSens * Time.deltaTime;
            Quaternion t_adj = Quaternion.AngleAxis(t_input, Vector3.up);
            Quaternion t_delta = player.localRotation * t_adj;
            player.localRotation = t_delta;
            

        }
        void UpdateCursorLock()
        {
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if(Input.GetKey(KeyCode.Escape) || PlayerHealth.isGameOver || countCollectables.isWon)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            
        }
    }
}
