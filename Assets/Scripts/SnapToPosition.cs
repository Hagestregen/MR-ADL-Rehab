using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class SnapToPosition : MonoBehaviour
{
    [SerializeField] private float snapDistance = 0.075f; // Distance within which the object will snap to the target

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private ObjectManipulator objectManipulator;
    private bool originalPositionSet = false;

    private void Start()
    {
        objectManipulator = GetComponent<ObjectManipulator>();
        if (objectManipulator == null)
        {
            Debug.LogError("ObjectManipulator component is missing. Please add an ObjectManipulator component to the GameObject.");
            return;
        }

        objectManipulator.OnManipulationStarted.AddListener((eventData) => OnObjectPicked());
        objectManipulator.OnManipulationEnded.AddListener((eventData) => OnObjectReleased());
    }

    private void OnObjectPicked()
    {
        if (!originalPositionSet)
        {
            originalPosition = transform.position;
            originalRotation = transform.rotation;
            originalPositionSet = true;
        }
    }

    private void OnObjectReleased()
    {
        float distanceToOriginalPosition = Vector3.Distance(transform.position, originalPosition);

        if (distanceToOriginalPosition <= snapDistance)
        {
            SnapToObject();
        }
    }

    private void SnapToObject()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
