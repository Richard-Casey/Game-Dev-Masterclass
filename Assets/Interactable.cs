using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class Interactable : MonoBehaviour
{

    [SerializeField] float InteractionDistance = 1f;
    [SerializeField] UnityEvent OnEnterInteractionArea;
    [SerializeField] UnityEvent OnInteraction;
    [SerializeField] UnityEvent OnLeaveInteractionArea;


    void Awake()
    {
        if (!gameObject.TryGetComponent<SphereCollider>(out SphereCollider collider))
        {
            SphereCollider thisCollider = gameObject.AddComponent<SphereCollider>();
            thisCollider.isTrigger = true;
        }
    }

    void OnTriggerEnter()
    {
        OnEnterInteractionArea.Invoke();
        InputManager.Interaction.AddListener(OnInteraction.Invoke);
    }

    void OnTriggerExit()
    {
        InputManager.Interaction.RemoveListener(OnInteraction.Invoke);
        OnLeaveInteractionArea?.Invoke();
    }
}
