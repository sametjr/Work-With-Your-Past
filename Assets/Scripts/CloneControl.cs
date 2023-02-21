using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneControl : MonoBehaviour
{
    private void Start() {
        gameObject.tag = "Clone";
        gameObject.layer = 3; // Clone layer
    }
    
}
