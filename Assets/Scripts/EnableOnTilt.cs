using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class EnableOnTilt : MonoBehaviour
{
    public float tiltThreshold = 30.0f; // the tilt threshold in degrees
    public ParticleSystem particlesToEnable; // the particle system to enable when tilted
    public float particleSpeed = 5.0f; // the speed of the particles
    public float particleSpread = 0.1f; // the spread of the particles
    private bool particlesEnabled = false;
    private Quaternion initialRotation; // the initial rotation of the container
    private float lastLoggedTime; // the time when the last log occurred


    private void Start()
    {
        // store the initial rotation of the container
        initialRotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);
    }

    private void Update()
    {
        // calculate the current z-axis tilt angle relative to the initial angle
        float zAngle = transform.rotation.eulerAngles.z;
        float tiltAngle = Mathf.Abs(zAngle - initialRotation.eulerAngles.z);
        Debug.Log("tiltangle: " + tiltAngle);

        if (Time.time >= lastLoggedTime + 1.0f)
        {
            // log the initial rotation and tilt angle values to the console
            Debug.Log("initialRotation: " + initialRotation);
            Debug.Log("tiltAngle: " + tiltAngle);

            // update the last logged time to the current time
            lastLoggedTime = Time.time;
        }

        // adjust tilt angle to be within 0 to 360 range
        if (tiltAngle > 180f)
        {
            tiltAngle = 360f - tiltAngle;
        }

        // check if the tilt angle is greater than the threshold and the particles are not already enabled
        if (tiltAngle >= tiltThreshold && !particlesEnabled)
        {
            // enable the particle system and set particle velocity
            particlesToEnable.Play();
            particlesEnabled = true;
            Debug.Log("Particles Enabled");
        }
        // check if the tilt angle is less than the threshold and the particles are enabled
        else if (tiltAngle < tiltThreshold && particlesEnabled)
        {
            // disable the particle system
            particlesToEnable.Stop();
            particlesEnabled = false;
        }
    }
}

