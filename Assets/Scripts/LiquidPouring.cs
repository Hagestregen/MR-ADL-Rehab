using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPouring : MonoBehaviour
{
    ParticleSystem myParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Angle(Vector3.down, transform.forward) >= 45f)
        {
            myParticleSystem.Play();
        }
        else
        {
            myParticleSystem.Stop();
        }
    }
}
