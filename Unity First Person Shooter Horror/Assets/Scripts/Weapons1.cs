using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.Kawaiisun.SimpleHostile
{


    public class Weapons : MonoBehaviour
    {
        #region Variables
        public Gun[] loadout;
        public Transform weaponParent;
        private GameObject currentWeapon;
        private int currentIndex;
        private bool gunEnabled = false;
        public GameObject bulletholePrefab;
        public LayerMask canBeShot;
        #endregion

        #region Monobehavior Callbacks
        void Start()
        {

        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                equip(0);
                gunEnabled = true;
            }
            if (gunEnabled)
            {
                Aim(Input.GetMouseButton(1));
                if (Input.GetMouseButtonDown(0))
                {
                    shoot();
                }
            }
            #endregion

            #region Private Methods

            void equip(int p_ind)
            {
                if (currentWeapon != null) Destroy(currentWeapon);

                currentIndex = p_ind;
                GameObject t_newWeapon = Instantiate(loadout[p_ind].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
                t_newWeapon.transform.localPosition = Vector3.zero;
                t_newWeapon.transform.localEulerAngles = Vector3.zero;

                currentWeapon = t_newWeapon;

            }

            void Aim(bool p_isAiming)
            {
                Transform t_anchor = currentWeapon.transform.Find("Anchor");
                Transform t_state_ads = currentWeapon.transform.Find("States/ADS");
                Transform t_state_hip = currentWeapon.transform.Find("States/Hip");

                if (p_isAiming)
                {
                    //aim
                    t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
                }
                else
                {
                    t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
                }

            }
            void shoot()
            {
                Transform t_spawn = transform.Find("Cameras/Normal Camera");

                RaycastHit t_hit = new RaycastHit();
                if (Physics.Raycast(t_spawn.position, t_spawn.forward, out t_hit, 1000f, canBeShot))
                {
                    GameObject t_newHole = Instantiate(bulletholePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
                    t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
                    Destroy(t_newHole, 5f);
                }

            }
            #endregion
        }
    }
}