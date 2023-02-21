using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Actions action;

    [SerializeField] private List<string> affectableBy;
    [SerializeField] private bool disposable;
    private Color originalColor;
    MeshRenderer meshRenderer;

    private void Start() {
        gameObject.tag = "Interactable";
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Triggered");
        if(affectableBy.Contains(other.gameObject.tag)) {
            meshRenderer.material.color = Color.red;
            ActionsManager.Instance.MakeAction(action, _entered:true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(affectableBy.Contains(other.gameObject.tag)) {

            if(disposable) 
                meshRenderer.material.color = originalColor;
            
            ActionsManager.Instance.MakeAction(action, _entered:false);
        }
    }

}






