using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyBrickBase : MonoBehaviour
{
    [SerializeField] GameObject Brick;
    bool isTakingBrick;
    private void Start()
    {
        OnInit();
    }
    void OnInit()
    {
        isTakingBrick = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTakingBrick)
        {
            isTakingBrick = true;
            Player.Instance.RemoveBrickStack();
            GameObject go = Instantiate(Brick);
            Vector3 brickPos = transform.position;
            brickPos.y = -0.18f;
            go.transform.position = brickPos;
        }
    }

}
