using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedEngine : MonoBehaviour
{
    public float waitTime = 10;
    public float damageTorque = 20;
    public GameObject explosionParticle;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        explosionParticle = transform.Find("EngineDamagedParticle").gameObject;
    }

    private void Start()
    {
        StartCoroutine(AddTorque());
    }

    private IEnumerator AddTorque()
    {
        yield return (new WaitForSeconds(waitTime));
        rigidbody.AddTorque(new Vector3(0, 0, -damageTorque), ForceMode.Acceleration);
    }

    public void PlayParticles()
    {
        ParticleSystem[] explosions = explosionParticle.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in explosions)
        {
            p.Play();
        }
    }
}