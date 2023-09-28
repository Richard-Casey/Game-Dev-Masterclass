using System.Collections.Generic;
using UnityEngine;

public class MaterialCheck : MonoBehaviour
{
    #region main

    void Start()
    {
    }

    public LayerMask mask;
    public Camera camera;

    readonly List<MeshRenderer> RenderesActiveThisFrame = new();
    readonly List<MeshRenderer> AllActiveRenderers = new();

    public float OpeningSize = 2f;
    public float MaxSize = 1f;
    float MinSize = 0f;
    float CurrentSize = 0f;



    void FixedUpdate()
    {
        /*Shader.SetGlobalVector("_GlobalPlayerPosition",transform.position + new Vector3(0,0,0));
        Shader.SetGlobalFloat("_Size", Size);
        Shader.SetGlobalFloat("_AngleThreshold", AngleThreshold);*/
        RenderesActiveThisFrame.Clear();

        Shader.SetGlobalFloat("_Size", CurrentSize);

        var Distance = (transform.position - camera.transform.position).magnitude + 1;
        var Direction = (transform.position - camera.transform.position).normalized;
        var ray = new Ray(camera.transform.position, Direction);
        RaycastHit[] Hits = Physics.SphereCastAll(ray.origin, .5f, ray.direction, Distance, mask);
        Debug.DrawRay(ray.origin,ray.direction,Color.red,2f);
        foreach (var data in Hits)
        {
            MeshRenderer renderer;
            Material material;
            if (data.transform.gameObject.TryGetComponent(out renderer))
            {
                if (renderer.material.shader.name == "Shader Graphs/SeeThroughCircle")
                {
                    if (!RenderesActiveThisFrame.Contains(renderer))
                    {
                        RenderesActiveThisFrame.Add(renderer);
                    }

                    if (!AllActiveRenderers.Contains(renderer))
                    {
                        AllActiveRenderers.Add(renderer);
                    }
                }
            }
        }



        if (RenderesActiveThisFrame.Count > 0)
        {
            CurrentSize = Mathf.MoveTowards(CurrentSize, MaxSize, Time.fixedDeltaTime);
        }
        else
        {
            CurrentSize = Mathf.MoveTowards(CurrentSize, MinSize, Time.fixedDeltaTime);
        }


    }

    #endregion
}