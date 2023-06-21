using System.Collections;
using UnityEngine;

public class OperateMachine : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;
    [SerializeField] private float timeFactor = 1f; // Adjust this to control the time it takes for larger fillValues to decrease to 0.47
    [SerializeField] private float delayBetweenUpdates = 2.0f; // Delay after UpdateFillValue is completed

    private float initialAmount;

    public void StartingMachine()
    {
        //Set startingvalue of liquidCoffee to empty before start
        Renderer renderer2 = gameObject2.GetComponent<Renderer>();
        if (renderer2 != null && renderer2.material != null)
        {
            renderer2.material.SetFloat("_FillCoffee", 0.45f);
        }

        StartCoroutine(UpdateFillValue());
    }

    private IEnumerator UpdateFillValue()
    {
        Renderer renderer1 = gameObject1.GetComponent<Renderer>();
        float fillValue = 0f;

        if (renderer1 != null && renderer1.material != null)
        {
            fillValue = renderer1.material.GetFloat("_Fill");
            Debug.Log("Initial Fill Value from gameObject1: " + fillValue);
        }

        // Store the initial value in a range of 0 - 0.06
        initialAmount = Mathf.Clamp(fillValue - 0.47f, 0, 0.06f);
        Debug.Log("Initial Amount: " + initialAmount);

        float elapsedTime = 0f;
        float duration = fillValue * timeFactor; // The duration is proportional to the initial fill value
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newFillValue = Mathf.Lerp(fillValue, 0.47f, elapsedTime / duration);
            if (renderer1 != null && renderer1.material != null)
            {
                renderer1.material.SetFloat("_Fill", newFillValue);
            }
            yield return null;
        }

        // Ensure the final value is precisely 0.47f
        if (renderer1 != null && renderer1.material != null)
        {
            renderer1.material.SetFloat("_Fill", 0.47f);
        }

        yield return new WaitForSeconds(delayBetweenUpdates);

        StartCoroutine(UpdateFillCoffee());
        Debug.Log("Done with first step");
    }

    private IEnumerator UpdateFillCoffee()
    {
        Debug.Log("Starting Coffee");
        Debug.Log("Initial Amount in FillCoffee" + initialAmount);
        Renderer renderer2 = gameObject2.GetComponent<Renderer>();
        float fillCoffeeValue = 0f;

        if (renderer2 != null && renderer2.material != null)
        {
            fillCoffeeValue = renderer2.material.GetFloat("_FillCoffee");
        }

        float targetFillCoffeeValue = Mathf.Clamp(0.45f + initialAmount, 0.45f, 0.51f);
        float elapsedTime = 0f;
        float duration = timeFactor;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newFillCoffeeValue = Mathf.Lerp(fillCoffeeValue, targetFillCoffeeValue, elapsedTime / duration);
            if (renderer2 != null && renderer2.material != null)
            {
                renderer2.material.SetFloat("_FillCoffee", newFillCoffeeValue);
            }
            yield return null;
        }

        // Ensure the final value does not exceed 0.51f
        if (renderer2 != null && renderer2.material != null)
        {
            renderer2.material.SetFloat("_FillCoffee", Mathf.Min(targetFillCoffeeValue, 0.51f));
        }
    }
}