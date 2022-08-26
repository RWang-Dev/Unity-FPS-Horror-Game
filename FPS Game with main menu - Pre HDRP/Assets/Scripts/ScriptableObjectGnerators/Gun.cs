using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Kawaiisun.SimpleHostile;

namespace Com.Kawaiisun.SimpleHostile
{
    [CreateAssetMenu(fileName = "Gun", menuName = "Gun")]
    public class Gun : ScriptableObject
    {
        public new string name;
        public float impactForce;
        public float damage;
        public float aimSpeed;
        public GameObject prefab;
        public int maxAmmo;
        public float reloadSpeed;
        public float bloom;
        public float aim_bloom;
        public float crouch_bloom;

        public float recoil;
        public float aim_recoil;
        public float crouch_recoil;

        public float kickback;
        public float aim_kickback;
        public float crouch_kickback;

        public float firerate;
        

    }
}
