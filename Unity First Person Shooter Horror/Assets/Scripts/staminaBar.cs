using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class staminaBar : MonoBehaviour
{
    public Slider stamina;

    public float maxStamina = 1000f;
    public static float currentStamina;
    public float tiredModifier = 0.5f;
    public static staminaBar running;
    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance;
    bool isGrounded;
    private WaitForSeconds regenTick = new WaitForSeconds(0.2f);
    private WaitForSecondsRealtime waitStamina = new WaitForSecondsRealtime(4f);
    private Coroutine regen;
    public static staminaBar instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        currentStamina = maxStamina;
        stamina.maxValue = maxStamina;
        stamina.value = maxStamina;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool sprint = Input.GetKey(KeyCode.LeftShift);
        bool jump = Input.GetKey(KeyCode.Space);
        bool isSprinting = sprint;
        bool isJumping = jump;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        
            if (((Input.GetKey(KeyCode.Space) && isGrounded) || Input.GetKey(KeyCode.LeftShift)) && !Input.GetKey(KeyCode.S))
            {
                if ((Input.GetKey(KeyCode.Space) && isGrounded) && Input.GetKey(KeyCode.LeftShift))
                {
                    UseStamina(5);
                }
                if ((Input.GetKey(KeyCode.Space) && isGrounded) && !Input.GetKey(KeyCode.LeftShift))
                {
                    UseStamina(2);
                }
                if (!(Input.GetKey(KeyCode.Space) && isGrounded) && Input.GetKey(KeyCode.LeftShift))
                {
                    UseStamina(2);
                }

            }
        




    }
    public void Bar()
    {



    }
    public void UseStamina(float amount)
    {
        if (currentStamina - amount >= 5)
        {
            currentStamina -= amount;
            stamina.value = currentStamina;
            if(regen != null)
            {
                StopCoroutine(regen);
            }
            regen = StartCoroutine(regenStamina());

        }
        else
        {
            Debug.Log("not enough Stamina");
        }
    }
    private IEnumerator regenStamina()
    {
        yield return new WaitForSeconds(4);
        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            stamina.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }
}

   
        
    

