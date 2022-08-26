using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.Kawaiisun.SimpleHostile {
    public class Motion : MonoBehaviour
    {
        private float idleCounter;
        public Transform weaponParent;
        public Transform flashlightParent;
        private Vector3 targetWeaponBobPosition;
        private float movementCounter;
        private Vector3 weaponParentOrigin;

        public float speed;
        public float sprintModifier;
        public float crouchModifier;
        public float jumpForce;
        public Camera normalCam;
        public Transform groundCheck;
        public LayerMask groundMask;
        public float groundDistance;
        public AudioSource footsteps;
        public bool isGrounded;


        private Rigidbody rig;
        private float baseFOV;

        private float sprintFOVModifier = 1.2f;

        void Start()
        {

            baseFOV = normalCam.fieldOfView;
            rig = GetComponent<Rigidbody>();
            weaponParentOrigin = weaponParent.localPosition;
        }


        void FixedUpdate() // makes computers run at the same speed
        {
            //Axis
            float t_hmove = Input.GetAxisRaw("Horizontal");
            float t_vmove = Input.GetAxisRaw("Vertical");

            //Controls
            bool sprint = Input.GetKey(KeyCode.LeftShift);
            bool jump = Input.GetKey(KeyCode.Space);

            //States
            bool isSprinting = sprint;
            bool isJumping = jump;

            //Ground Check
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            //Jumping
            if (isJumping && isGrounded && staminaBar.currentStamina > 10)
            {
                
                rig.AddForce(Vector3.up * jumpForce);
            }

            //Sprinting
            float t_adjustedSpeed = speed;
            if (isSprinting && staminaBar.currentStamina > 10 && !Input.GetKey(KeyCode.S))
            {
                
                t_adjustedSpeed *= sprintModifier;
            }
            if (Crouch.isCrouching)
            {
                t_adjustedSpeed *= crouchModifier;
            }

            //Movement
            Vector3 t_direction = new Vector3(t_hmove, 0, t_vmove);
            t_direction.Normalize();
            // WASD Movement
            Vector3 t_targetVelocity = transform.TransformDirection(t_direction) * t_adjustedSpeed * Time.deltaTime;
            t_targetVelocity.y = rig.velocity.y;
            rig.velocity = t_targetVelocity;

            //footsteps
            if (isGrounded && t_targetVelocity.magnitude > 1f && footsteps.isPlaying == false && !Crouch.isCrouching && !isSprinting)
            {
                footsteps.volume = Random.Range(0.25f, 0.5f);
                footsteps.pitch = Random.Range(1, 1.2f);
                footsteps.Play();
            } else if (isGrounded && t_targetVelocity.magnitude > 1f && footsteps.isPlaying == false && isSprinting && !Crouch.isCrouching)
            {
                footsteps.volume = Random.Range(0.5f, 0.65f);
                footsteps.pitch = Random.Range(1.6f, 1.8f);
                footsteps.Play();
            } else if (isGrounded && t_targetVelocity.magnitude > 1f && footsteps.isPlaying == false && Crouch.isCrouching)
            {
                footsteps.volume = Random.Range(0.2f, 0.35f);
                footsteps.pitch = Random.Range(0.7f, 0.85f);
                footsteps.Play();
            }

           
            
            

            //field of view
            if (isSprinting && staminaBar.currentStamina > 10 && !Input.GetKey(KeyCode.S))
            {
                normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 6f);
            }
            else
            {
                normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 6f);
            }
            if (!Input.GetMouseButton(1))
            {
                if (t_hmove == 0 && t_vmove == 0)
                {
                    HeadBob(idleCounter, 0.025f, 0.025f);
                    idleCounter += Time.deltaTime; //resting headbob
                    weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
                    flashlightParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 2f);
                }
                else if (Input.GetKey("left shift") && staminaBar.currentStamina > 10)
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
                if (t_hmove == 0 && t_vmove == 0)
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
                else if (Input.GetKey("left shift") && staminaBar.currentStamina > 10)
                {
                    HeadBob(movementCounter, 0.03f, 0.03f);
                    movementCounter += Time.deltaTime * 7f;
                    weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);
                    flashlightParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * 10f);
                }
                else if (t_hmove != 0 || t_vmove != 0)
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
        void HeadBob(float p_z, float p_x_intensity, float p_y_intensity)
        {
            //Headbob
            targetWeaponBobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(p_z) * p_x_intensity, Mathf.Sin(p_z * 2) * p_y_intensity, 0);
        }

    }
}
