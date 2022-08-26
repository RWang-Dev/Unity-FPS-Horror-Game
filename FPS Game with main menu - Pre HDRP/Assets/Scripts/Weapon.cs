using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Com.Kawaiisun.SimpleHostile {
    public class Weapon : MonoBehaviour
    {
        #region Variables
        public Gun[] loadout;
        public Transform weaponParent;
        private GameObject currentWeapon;
        private int currentIndex;
        public ParticleSystem muzzleFlash;
        public ParticleSystem muzzleFlashCrouched;

        public int maxAmmo = 10;
        public int currentAmmo;
        public float reloadTime = 1f;
        public static bool isReloading = false;
        public Text ammoDisplay;

        private float currentCooldown;
        public GameObject bulletholePrefab;
        public GameObject ImpactParticlePrefab;
        public GameObject BloodPrefab;
        public GameObject Hip;
        public GameObject ADS;
        
        public LayerMask canBeShot;
        public LayerMask enemy;
        public AudioSource shootSound;

        public LineRenderer bulletTrail;
        #endregion

        #region Monobehavior Callbacks
        void Start()
        {
            currentAmmo = loadout[currentIndex].maxAmmo;
        }

        void Update()
        {
            if(FindObjectOfType<PlayerHealth>().Health <= 0)
            {
                isReloading = false;
                StopAllCoroutines();
            }
            ammoDisplay.text = currentAmmo.ToString();
            if (isReloading)
            {
                return;
            }
            
                if((Input.GetMouseButtonDown(0)&& currentAmmo == 0) || (Input.GetKeyDown(KeyCode.R) && currentAmmo != 10))
                {
                    Transform anchor = currentWeapon.transform.Find("Anchor");
                    Transform ads = currentWeapon.transform.Find("States/ADS");
                    Transform hip = currentWeapon.transform.Find("States/Hip");
                    reloadBar.instance.Bar();
                    Debug.Log("Reloading");
                    if (Input.GetMouseButton(1))
                    {
                        Debug.Log("aiming");

                        anchor.position = Vector3.MoveTowards(anchor.position, hip.position, 1f);

                    }
                    StartCoroutine(Reload());

                    return;
                }
                
            
           
            
            
            
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                equip(0);
                
            }

            if (currentWeapon != null)
            {
                Aim(Input.GetMouseButton(1));

                if (Input.GetMouseButtonDown(0) && currentCooldown <= 0)
                {
                    
                    shoot();
                    if (Input.GetMouseButton(1))
                    {
                        
                        muzzleFlashCrouched.Play();
                    }
                    else muzzleFlash.Play();
                }
                //weapon position elasticity
                currentWeapon.transform.localPosition = Vector3.Lerp(currentWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 4f);

                if (currentCooldown > 0) currentCooldown -= Time.deltaTime;
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
            
            
            IEnumerator Reload()
            {
                if (FindObjectOfType<PlayerHealth>().Health <= 0) yield break;
                isReloading = true;


                yield return new WaitForSeconds(loadout[currentIndex].reloadSpeed);

                currentAmmo = maxAmmo;
                isReloading = false;
                
                
                
            }

            void Aim(bool p_isAiming)
            {
                
                
                    Transform t_anchor = currentWeapon.transform.Find("Anchor");
                    Transform t_state_ads = currentWeapon.transform.Find("States/ADS");
                    Transform t_state_hip = currentWeapon.transform.Find("States/Hip");

                    Transform t_state_adsc = currentWeapon.transform.Find("States/ADScrouch");
                    
                
            if(Crouch.isCrouching == false)
                {
                    if (p_isAiming)
                    {
                        //aim
                        t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
                    }
                    else
                    {
                        t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
                    }
                } else if(Crouch.isCrouching == true)
                {
                    if (p_isAiming)
                    {
                        //aim
                        t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_adsc.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
                    }
                    else
                    {
                        t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * loadout[currentIndex].aimSpeed);
                    }
                }
                

            }
            void shoot()
            {
               
                if (currentAmmo > 0)
                {
                    currentAmmo--;
                } 
                
                Transform t_spawn = transform.Find("Cameras/Normal Camera");
                shootSound.Play();

                //bloom
                Vector3 t_bloom = t_spawn.position + t_spawn.forward * 1000f;

                if (Input.GetMouseButton(1) == false)
                {
                    if(Input.GetKey(KeyCode.LeftControl) == false)
                    {
                        t_bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.up;
                        t_bloom += Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.right;
                        t_bloom -= t_spawn.position;
                        t_bloom.Normalize();
                    } else if(Input.GetKey(KeyCode.LeftControl) == true)
                    {
                        t_bloom += Random.Range(-loadout[currentIndex].aim_bloom, loadout[currentIndex].aim_bloom) * t_spawn.up;
                        t_bloom += Random.Range(-loadout[currentIndex].aim_bloom, loadout[currentIndex].aim_bloom) * t_spawn.right;
                        t_bloom -= t_spawn.position;
                        t_bloom.Normalize();
                    }
                   
                } else if (Input.GetMouseButton(1) == true) {
                    if (Input.GetKey(KeyCode.LeftControl) == false)
                    {
                        t_bloom += Random.Range(-loadout[currentIndex].aim_bloom, loadout[currentIndex].aim_bloom) * t_spawn.up;
                        t_bloom += Random.Range(-loadout[currentIndex].aim_bloom, loadout[currentIndex].aim_bloom) * t_spawn.right;
                        t_bloom -= t_spawn.position;
                        t_bloom.Normalize();
                    }
                    else if (Input.GetKey(KeyCode.LeftControl) == true)
                    {
                        t_bloom += Random.Range(-loadout[currentIndex].crouch_bloom, loadout[currentIndex].crouch_bloom) * t_spawn.up;
                        t_bloom += Random.Range(-loadout[currentIndex].crouch_bloom, loadout[currentIndex].crouch_bloom) * t_spawn.right;
                        t_bloom -= t_spawn.position;
                        t_bloom.Normalize();
                    }
                }
               

                //Raycast
                RaycastHit t_hit = new RaycastHit();
                
                if (Physics.Raycast(t_spawn.position, t_bloom, out t_hit, 1000f, canBeShot))
                {
                    SpawnBulletTrail(t_hit.point);
                    GameObject t_newHole = Instantiate(bulletholePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity) as GameObject;
                    t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
                    
                    GameObject impactPrefab = Instantiate(ImpactParticlePrefab, t_hit.point + t_hit.normal * 0.0001f, Quaternion.identity);
                    impactPrefab.transform.LookAt(t_hit.point + t_hit.normal);
                    Targets target = t_hit.transform.GetComponent<Targets>();
                    if (target != null)
                    {
                        SpawnBulletTrail(t_hit.point);
                        target.TakeDamage(loadout[currentIndex].damage);
                    }
                    if (t_hit.rigidbody != null && !Physics.Raycast(t_spawn.position, t_bloom, out t_hit, 1000f, enemy))
                    {
                        // t_hit.rigidbody.AddForce(-t_hit.normal * loadout[currentIndex].impactForce);
                        SpawnBulletTrail(t_hit.point);
                    }
                    if (Physics.Raycast(t_spawn.position, t_bloom, out t_hit, 1000f, enemy))
                    {
                        Destroy(t_newHole, 0.1f);
                        Destroy(impactPrefab, 0f);
                        GameObject Blood = Instantiate(BloodPrefab, t_hit.point + t_hit.normal * 0.0001f, Quaternion.identity);
                        BloodPrefab.transform.LookAt(t_hit.point + t_hit.normal);
                        Destroy(Blood, 2f);
                    }
                    else
                    {
                        SpawnBulletTrail(t_hit.point);
                        Destroy(t_newHole, 5f);
                        Destroy(impactPrefab, 5f);
                    }

                    

                   

                }
                

                //gun recoil effect
                if (Input.GetMouseButton(1) == false)
                {
                    if (Input.GetKey(KeyCode.LeftControl) == false)
                    {
                        currentWeapon.transform.Rotate(-loadout[currentIndex].recoil, 0, 0);
                        currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].kickback;
                    }
                    else if (Input.GetKey(KeyCode.LeftControl) == true)
                    {
                        currentWeapon.transform.Rotate(-loadout[currentIndex].aim_recoil, 0, 0);
                        currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].aim_kickback;
                    }

                }
                else if (Input.GetMouseButton(1) == true)
                {
                    if (Input.GetKey(KeyCode.LeftControl) == false)
                    {
                        currentWeapon.transform.Rotate(-loadout[currentIndex].aim_recoil, 0, 0);
                        currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].aim_kickback;
                    }
                    else if (Input.GetKey(KeyCode.LeftControl) == true)
                    {
                        currentWeapon.transform.Rotate(-loadout[currentIndex].crouch_recoil, 0, 0);
                        currentWeapon.transform.position -= currentWeapon.transform.forward * loadout[currentIndex].crouch_kickback;
                    }
                }
                
               

                //cooldown
                currentCooldown = loadout[currentIndex].firerate;
            }
            void SpawnBulletTrail(Vector3 hitPoint)
            {
                Transform firePoint1 = ADS.transform;

                Transform firePoint2 = Hip.transform;


                if (Input.GetKey(KeyCode.Mouse1))
                {
                    GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, firePoint1.position, Quaternion.identity);
                    LineRenderer lineR = bulletTrailEffect.GetComponent<LineRenderer>();
                    lineR.SetPosition(0, firePoint1.position);
                    lineR.SetPosition(1, hitPoint);

                    Destroy(bulletTrailEffect, 1f);
                } else
                {
                    GameObject bulletTrailEffect = Instantiate(bulletTrail.gameObject, firePoint2.position, Quaternion.identity);
                    LineRenderer lineR = bulletTrailEffect.GetComponent<LineRenderer>();
                    lineR.SetPosition(0, firePoint2.position);
                    lineR.SetPosition(1, hitPoint);
                    Destroy(bulletTrailEffect, 1f);
                }
                        
                        
                            
             }
                    
                
            }
            #endregion
        }
    }

