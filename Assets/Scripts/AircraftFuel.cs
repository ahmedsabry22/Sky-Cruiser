using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AircraftFuel : MonoBehaviour
{
    public float fuelPercent;
    public Slider fuelSlider;

    private void Awake()
    {
        fuelPercent = Random.Range(90, 100);
        fuelSlider = GameObject.Find("Fuel Slider").GetComponent<Slider>();
    }

    private void Update()
    {
        fuelPercent -= 2f * Time.deltaTime;
        fuelPercent = Mathf.Clamp(fuelPercent, 0, 100);

        fuelSlider.value = fuelPercent;

        if (fuelPercent <= 1)
            DamageEngine();
    }

    private void DamageEngine()
    {
        GetComponent<DamagedEngine>().damageTorque = 100;
        GetComponent<DamagedEngine>().PlayParticles();
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}