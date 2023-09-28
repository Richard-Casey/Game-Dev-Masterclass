using System.Collections.Generic;
using UnityEngine;

public class MaterialCheck : MonoBehaviour
{
    #region main

    public float Size = 1f;
    public float AngleThreshold = 1f;
    public float Opacity = 1f;
    void FixedUpdate()
    {
        Shader.SetGlobalVector("_GlobalPlayerPosition",transform.position + new Vector3(0,0,0));
        Shader.SetGlobalFloat("_Size", Size);
        Shader.SetGlobalFloat("_AngleThreshold", AngleThreshold);
        Shader.SetGlobalFloat("_Opacity", Opacity);
    }

    #endregion
}