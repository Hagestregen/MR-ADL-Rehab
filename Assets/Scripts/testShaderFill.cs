using UnityEngine;

public class testShaderFill : MonoBehaviour
{
    public Material myMaterial; // Assign the material with your shader in the Inspector.

    private void Start()
    {
        if(myMaterial.HasProperty("_Fill")) // The property name is prefixed with an underscore in the shader.
        {
            float fillValue = myMaterial.GetFloat("_Fill"); // Get the current value.
            Debug.Log("Current fill value: " + fillValue);

            myMaterial.SetFloat("_Fill", 0.5f); // Set a new value.
        }
        else
        {
            Debug.LogError("This material does not have a '_Fill' property.");
        }
    }
}
