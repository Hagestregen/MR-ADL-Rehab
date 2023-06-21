using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceEnvironment : MonoBehaviour
{
    public GameObject objectToPlace; // The GameObject you want to place
    public Transform targetTransform; // The transform of the GameObject where you want to place the other GameObject
    public Vector3 offset; // The offset vector in x, y, and z dimensions

    // Public method to place object
    public void Place()
    {
        if (objectToPlace != null && targetTransform != null)
        {
            objectToPlace.transform.position = targetTransform.position + offset;
            //objectToPlace.transform.rotation = targetTransform.rotation;

            // If you want the objectToPlace to become a child of the target object, uncomment the following line
            //objectToPlace.transform.parent = targetTransform;
        }
        else
        {
            Debug.LogError("Both 'objectToPlace' and 'targetTransform' need to be set in the inspector!");
        }
    }
}
