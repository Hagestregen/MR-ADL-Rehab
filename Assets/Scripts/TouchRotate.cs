using System.Collections;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class TouchRotate : MonoBehaviour, IMixedRealityTouchHandler
{
    private Quaternion startRotation;
    private bool isRotated = false;
    public float rotationTime = 1f;

    private void Start()
    {
        // Store the original rotation
        startRotation = transform.rotation;
    }

    public void OnTouchStarted(HandTrackingInputEventData eventData)
    {
        Debug.Log("Touch Started");
        // If the object is not rotated, rotate it 90 degrees around the Y-axis
        if (!isRotated)
        {
            StartCoroutine(SmoothRotate(Quaternion.Euler(0, 90, 0)));
        }
        // If the object is already rotated, rotate it back to its original rotation
        else
        {
            StartCoroutine(SmoothRotate(startRotation));
        }

        // Toggle the isRotated boolean
        isRotated = !isRotated;
    }

    public void OnTouchUpdated(HandTrackingInputEventData eventData) { }

    public void OnTouchCompleted(HandTrackingInputEventData eventData) { }

    private void OnEnable()
    {
        CoreServices.InputSystem?.RegisterHandler<IMixedRealityTouchHandler>(this);
    }

    private void OnDisable()
    {
        CoreServices.InputSystem?.UnregisterHandler<IMixedRealityTouchHandler>(this);
    }

    IEnumerator SmoothRotate(Quaternion targetRotation)
    {
        float elapsedTime = 0;

        Quaternion startingRotation = transform.rotation;

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Lerp(startingRotation, targetRotation, (elapsedTime / rotationTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
