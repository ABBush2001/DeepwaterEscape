using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Unknown script function

public class TriggerZone : MonoBehaviour
{
    [SerializeField] string tagFilter;
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;

    void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) {
            return;
        }

        onTriggerEnter.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.gameObject.CompareTag(tagFilter)) {
            return;
        }

        onTriggerEnter.Invoke();
    }
}
