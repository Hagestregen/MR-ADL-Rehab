using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using UnityEngine;

public class RotateAndEnableParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystemToEnable = null;
    [SerializeField] private float particleDuration = 5f;
    [SerializeField] private float startAmountFilledCoffee = 0f; // Ranging from 0.37 to 6.2
    [SerializeField] private float startAmountFilledWater = 0f; // Ranging from 0.37 to 6.2
    [SerializeField] private float pourRate = 1f; // Units per second
    [SerializeField] private Material materialOne = null; // First material
    [SerializeField] private Material materialTwo = null; // Second material

    private float targetZRotation; // Ranging from 80 to 30 degrees

    private ObjectManipulator objectManipulator;
    private bool isParticleSystemPlaying = false;
    private bool isManipulationInProgress = false;
    private float amountFilled;

     
    //Add method for increasing pourrate

    //Debugmsg
    bool calledDebug = false;

    private void Start()
    {
        if (particleSystemToEnable == null)
        {
            Debug.LogError("Particle System is not assigned in inspector");
        }

        objectManipulator = GetComponent<ObjectManipulator>();
        if (objectManipulator == null)
        {
            Debug.LogError("No ObjectManipulator found on this GameObject");
        }
        else
        {
            objectManipulator.OnManipulationStarted.AddListener((data) => { isManipulationInProgress = true; CheckRotationAndStartParticles(); });
            objectManipulator.OnManipulationEnded.AddListener((data) => { isManipulationInProgress = false; StopParticles(); });
        }
    }

    private void Update()
    {
        // Map amountFilled to targetZRotation
        targetZRotation = Mathf.Lerp(80, 30, (amountFilled - 1) / 9.0f);

        // Decrease amountFilled while particle system is playing
        if (isParticleSystemPlaying)
        {
            amountFilled = Mathf.Max(1f, amountFilled - pourRate * Time.deltaTime);
            Debug.Log("Amount Filled: " + amountFilled);
            IncreaseFill(materialOne, amountFilled);
            DecreaseFill(materialTwo, amountFilled);

        }

        // Check rotation and start particles while manipulation is in progress
        if (isManipulationInProgress)
        {
            CheckRotationAndStartParticles();
        }

        // IncreaseFill(materialOne, amountFilled);
        // DecreaseFill(materialTwo, amountFilled);
    }

    private void CheckRotationAndStartParticles()
    {
        DebugMsg();
        float zAngle = transform.localEulerAngles.z;
        if (zAngle >= targetZRotation)
        {
            if (!isParticleSystemPlaying)
            {
                // Debug.Log("in playing particlesystem");
                particleSystemToEnable.Play();
                isParticleSystemPlaying = true;
                
                StartCoroutine(StopParticlesAfterDelay());
            }
        }
        else
        {
            StopParticles();
        }
    }

    private void StopParticles()
    {
        if (isParticleSystemPlaying)
        {
            Debug.Log("in stopping particlesystem");
            particleSystemToEnable.Stop();
            isParticleSystemPlaying = false;
        }
    }

    private IEnumerator StopParticlesAfterDelay()
    {
        yield return new WaitForSeconds(particleDuration);
        StopParticles();
    }

    void DebugMsg()
    {
        if (!calledDebug)
        {
             Debug.Log("In CheckRotationAndStartParticles");
             calledDebug= true;
        }
    }

    private void IncreaseFill(Material material, float amount)
    {
        // Debug.Log("In IncreaseFill");
        if (material != null)
        {
            float fillValue = material.GetFloat("_Fill");
            float newAmount = amount / 10;
            fillValue = newAmount;
            material.SetFloat("_Fill", fillValue);
            Debug.Log("FillValue: " + fillValue);
        }
    }

    private void DecreaseFill(Material material, float amount)
    {
        // Debug.Log("In DecreaseFill");

        if (material != null)
        {
            float fillValue = material.GetFloat("_Fill");
            float newAmount = amountFilled - amount;

            fillValue = newAmount;
            material.SetFloat("_Fill", fillValue);
        }
    }

    
}
