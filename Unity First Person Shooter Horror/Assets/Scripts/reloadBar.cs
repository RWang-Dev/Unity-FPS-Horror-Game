using Com.Kawaiisun.SimpleHostile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reloadBar : MonoBehaviour
{
    public Slider reloading;
    
    
    private int noBullet = 0;
    private int currentA = 0;
    private int maxAmmo = 15;
    private WaitForSeconds regenTick = new WaitForSeconds(0.099f);


    public static reloadBar instance;
    // Start is called before the first frame update
    public GameObject bar;


    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Weapon.isReloading == true)
        {
            Show();
        }
        else Hide();
    }
    void Show()
    {
        
        bar.SetActive(true);
    }
    void Hide()
    {
        bar.SetActive(false);
    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
        currentA = noBullet;
        reloading.maxValue = maxAmmo;
        reloading.value = currentA;
    }
    public void Bar()
    {
        
        currentA = 0;
        reloading.value = currentA;
        StartCoroutine(Reload());
        
    }

    private IEnumerator Reload()
    {
        
        while (currentA <= maxAmmo)
        {
            
            currentA += 1;
            reloading.value = currentA;
            yield return regenTick;
            if(currentA == maxAmmo)
            {
                Hide();
            }
            
        }
    }
   
}
