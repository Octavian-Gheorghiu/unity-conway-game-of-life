using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_Collide : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<CA_Cube>().SetFutureState(1);
    }

    private void OnTriggerStay(Collider other)
    {
        other.gameObject.GetComponent<CA_Cube>().SetFutureState(1);
    }
}
