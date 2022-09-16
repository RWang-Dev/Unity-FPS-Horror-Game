using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Kawaiisun.SimpleHostile
{
    public class PlayerMovement : MonoBehaviour
    {

        #region Variables
        public CharacterController controller;
        public Rigidbody rb;
        public float speed = 12f;
        public float gravity = -9.81f;
        public Transform groundCheck;
        public float groundDistance = 0.4f;
        public LayerMask groundMask;
        public float jumpHeight = 3f;
        public Camera normCam;
        private float baseFOV;
        private float sprintFOV = 1.5f;
        public Transform weaponParent;
        public Transform flashlightParent;
        bool isGrounded;
        Vector3 velocity;
        private Vector3 weaponParentOrigin;
        private float movementCounter;
        private float idleCounter;
        private Vector3 targetWeaponBobPosition;
        #endregion

        #region Monobehavior Callbacks

        private void Start()
        {
            baseFOV = normCam.fieldOfView;
            weaponParentOrigin = weaponParent.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            bool jump = Input.GetKeyDown(KeyCode.Space);
            bool isJumping = jump;


            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            if (Input.GetKey("left shift"))
            {
                normCam.fieldOfView = Mathf.Lerp(normCam.fieldOfView, baseFOV * sprintFOV, Time.deltaTime * 8f);
                speed = 100f;
            }
            else
            {
                speed = 12f;
                normCam.fieldOfView = Mathf.Lerp(normCam.fieldOfView, baseFOV, Time.deltaTime * 8f);
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                speed = 6f;
                velocity.y = -50f;
            }

            //Headbob

            if (!Input.GetMouseButton(1))
            {
                if (x == 0 && z == 0)
                {
                    HeadBob(idleCounter, 0.025f, 0.025f);
                    idleCounter += Time.deltaTime; //resting headbob
                    weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
                    flashlightParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
                }
                else if (Input.GetKey("left shift"))
                {
                    HeadBob(movementCounter, 0.035f, 0.035f);
                    movementCounter += Time.deltaTime * 7f;
                    weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);
                    flashlightParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);
                }
                else
                {
                    HeadBob(movementCounter, 0.035f, 0.035f);
                    movementCounter += Time.deltaTime * 3.5f;
                    weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6f);
                    flashlightParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 6f);
                }
            }
            else if (Input.GetMouseButton(1))
            {
                if (x == 0 && z == 0)
                {
                    if (Input.GetKey("left ctrl"))
                    {
                        HeadBob(idleCounter, 0.005f, 0.005f);
                        idleCounter += Time.deltaTime * 0.23f; //resting headbob
                        weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 1f);
                        flashlightParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 1f);
                    }
                    else
                    {
                        HeadBob(idleCounter, 0.01f, 0.01f);
                        idleCounter += Time.deltaTime * 0.5f; //resting headbob
                        weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
                        flashlightParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
                    }

                }
                else if (Input.GetKey("left shift"))
                {
                    HeadBob(movementCounter, 0.03f, 0.03f);
                    movementCounter += Time.deltaTime * 7f;
                    weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);
                    flashlightParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);
                }
                else if (x != 0 || z != 0)
                {
                    if (Input.GetKey("left ctrl"))
                    {
                        HeadBob(movementCounter, 0.01f, 0.01f);
                        movementCounter += Time.deltaTime * 1f;
                        weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
                        flashlightParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
                    }
                    else
                    {
                        HeadBob(movementCounter, 0.02f, 0.02f);
                        movementCounter += Time.deltaTime * 2f;
                        weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
                        flashlightParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
                    }

                }
            }
        }


        #endregion

        #region Private Methods
        void HeadBob(float p_z, float p_x_intensity, float p_y_intensity)
        {
            //Headbob
            targetWeaponBobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(p_z) * p_x_intensity, Mathf.Sin(p_z * 2) * p_y_intensity, 0);
        }
        #endregion

    }
}
