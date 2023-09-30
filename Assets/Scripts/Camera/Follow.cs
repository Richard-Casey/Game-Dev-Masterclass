using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Target;

    public Vector3 Offset;

    public float StepedFollowSize = .4f;
    [SerializeField] bool ChangeRotation = false;

    enum FollowType
    {
        FixedFollow,
        DampedFollow,
        StepedFollow

    }


    [SerializeField]FollowType type;
    // Update is called once per frame
    void Update()
    {
        Vector3 Position = Target.position + (Offset);
        if(ChangeRotation)
        {
            transform.LookAt(Target);
        }


        
        switch (type)
        {
            case (FollowType.FixedFollow):
                transform.position = Position;
                break;
        }
    }
}
