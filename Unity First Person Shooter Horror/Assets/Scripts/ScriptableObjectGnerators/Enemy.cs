using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public new string name;
    public float health;
    public float damage;
    public float range;
    public float sight;
    public float attackSpeed;
    public GameObject prefab;

     
}
