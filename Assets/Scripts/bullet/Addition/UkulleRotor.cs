using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UkulleRotor : MonoBehaviour {

    public int rotSpeed;
    public Transform rotor;
    void OnDisable()
    {
        rotor.transform.rotation = Quaternion.identity;
    }
	void Update () 
    {
        rotor.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
	}
}
