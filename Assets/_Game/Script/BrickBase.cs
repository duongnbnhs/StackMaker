using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach(Transform child in transform)
            {
                if (child.name.Equals("dimian"))
                {
                    Debug.Log("Hit at: "+transform.position);
                    child.gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
}
