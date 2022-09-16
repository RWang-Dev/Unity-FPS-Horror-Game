using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ElectricityBar : MonoBehaviour
{
    public Slider stamina;

    public float maxStamina = 1000f;
    public static float currentStamina;
    public float tiredModifier = 0.5f;
    public static staminaBar running;
    public GameObject Light;
    public GameObject Light2;
    
  
    private WaitForSeconds regenTick = new WaitForSeconds(0.2f);
    private WaitForSecondsRealtime waitStamina = new WaitForSecondsRealtime(4f);
    private Coroutine regen;
    public static ElectricityBar instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Light.SetActive(false);
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
        


        if (Input.GetKey(KeyCode.Mouse2))
        {
           
                UseStamina(5);
            



        }
        if (Input.GetKey(KeyCode.Mouse2) && currentStamina > 5 && !Input.GetKey(KeyCode.Mouse1))
        {
            Light.SetActive(true);
            Light.SetActive(true);
        }
        else
        {
            Light.SetActive(false);
            Light.SetActive(false);
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
            if (regen != null)
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
        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            stamina.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }
}
