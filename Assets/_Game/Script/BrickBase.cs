using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Transform child in transform)
            {
                if (child.name.Equals("dimian") && child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(false);
                    Player.Instance.AddBrickStack();
                    break;
                }
            }
        }
    }
}
