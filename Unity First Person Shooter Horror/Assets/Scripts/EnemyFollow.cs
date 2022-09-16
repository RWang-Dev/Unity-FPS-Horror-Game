
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public  NavMeshAgent agent;
    public GameObject Goblin;
    private GameObject player;
    public Enemy enemy;
    public LayerMask whatIsPlayer;
    bool playerInSight;
    public AudioSource zombie;
    bool GotShot;
    public LayerMask whatIsBullet;
    public GameObject EnemyIcon;
    bool shot;
    public GameObject EnemyStats;
    public GameObject LeftEye;
    public GameObject RightEye;


    // Use this for initialization
    void Start()
    {
        EnemyIcon.SetActive(false);
        LeftEye.SetActive(false);
        LeftEye.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerInSight && zombie.isPlaying == false)
        {
            zombie.volume = Random.Range(0.5f, 1f);
            zombie.pitch = Random.Range(0.8f, 1.3f);
            zombie.Play();
        }
        playerInSight = Physics.CheckSphere(transform.position, enemy.sight, whatIsPlayer);
        GotShot = Physics.CheckSphere(transform.position, 2f, whatIsBullet);
        if (GotShot)
        {
            shot = true;
        }
        if (playerInSight || shot)
        {

            Follow();
        }
        
    }
     void Follow()
    {
        EnemyStats.SetActive(true);
        LeftEye.SetActive(true);
        LeftEye.SetActive(true);
        agent.destination = player.gameObject.transform.position + new Vector3(1.25f, 0, 0);
        EnemyIcon.SetActive(true);
        Goblin.transform.position = agent.nextPosition;
    }
}
