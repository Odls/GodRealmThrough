using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour{
    void Awake(){
        transform.localRotation = Quaternion.Euler(-90, 0, 0);
    }
}
