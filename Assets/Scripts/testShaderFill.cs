using System;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;



public class testShaderFill : MonoBehaviour
{
    private ObjectManipulator objectManipulator;
    public Material myMaterial; // Assign the material with your shader in the Inspector.

    private void Start()
    {
        
        if(myMaterial.HasProperty("_Fill")) // The property name is prefixed with an underscore in the shader.

        {
            Debug.Log("Fill found");
            float fillValue = myMaterial.GetFloat("_Fill"); // Get the current value.
            Debug.Log("Current fill value: " + fillValue);

            myMaterial.SetFloat("_Fill", 0.5f); // Set a new value.
            Debug.Log("New Fill Value: " + myMaterial.GetFloat("_Fill"));
        }
        else
        {
            Debug.LogError("This material does not have a '_Fill' property.");
        }
    }

    private void Update()
    {
        
    }

    private void IncreaseFill()
    {
        
    }
}
