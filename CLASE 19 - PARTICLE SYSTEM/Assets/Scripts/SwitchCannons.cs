using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchCannons : MonoBehaviour
{
    [SerializeField] private UnityEvent OnSwitchCannons;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("DESACTIVAR ARMAS");
            OnSwitchCannons?.Invoke();
        }
    }
}
