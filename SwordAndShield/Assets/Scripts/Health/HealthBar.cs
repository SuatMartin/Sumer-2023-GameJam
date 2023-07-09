using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;
    [SerializeField] private float maxHealth;
    // Start is called before the first frame update
    void Start(){
        totalhealthBar.fillAmount = maxHealth / 10;
    }

    // Update is called once per frame
    void Update(){
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10;
    }
}
