using System.Collections;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private float targetZRotation = 90f;
    [SerializeField] private ParticleSystem particleSystemToEnable = null;

    private bool rotationCompleted = false;
    
    // Start is called before the first frame update
    private void Start()
    {
        if (particleSystemToEnable == null)
        {
            Debug.LogError("Particle System is not assigned in inspector");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!rotationCompleted)
        {
            RotateObject();
        }
    }

    private void RotateObject()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetZRotation);
        if (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            // Rotate towards target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Complete the rotation, enable the particle system and set the rotation to be completed
            transform.rotation = targetRotation;
            particleSystemToEnable.gameObject.SetActive(true);
            particleSystemToEnable.Play();
            rotationCompleted = true;
        }
    }
}
