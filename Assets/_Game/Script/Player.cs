using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public enum Direct
{
    Forward,
    Backward,
    Left,
    Right,
    None
}
public class Player : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    //[SerializeField] private LayerMask brickLayer;
    Vector2 startPoint;
    Vector2 endPoint;
    Direct direct;
    bool isMoving;
    List<Vector3> wayPoint;
    private void Start()
    {
        OnInit();
    }
    private void Update()
    {
        Swipe();
    }
    void OnInit()
    {
        isMoving = false;
        wayPoint = new List<Vector3>();
    }
    Vector3 GetLastPoint(Vector3 direction)
    {
        int i = 1;
        Vector3 result;
        wayPoint.Clear();
        while (true)
        {
            if (Physics.Raycast(transform.position + direction * i, Vector3.down))
            {
                wayPoint.Add(transform.position + direction * i);
                i++;
            }
            else
            {
                result = transform.position + direction * (i - 1);
                break;
            }
        }
        Debug.Log("b" + result);
        return result;
    }
    private void Move(Vector3 pos)
    {
        Debug.Log("Move");
        isMoving = true;
        transform.DOMove(pos,0.75f).OnComplete(() => isMoving = false);
    }
    void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPoint = Input.mousePosition;
            float offsetX = endPoint.x - startPoint.x;
            float offsetY = endPoint.y - startPoint.y;
            if (Mathf.Abs(offsetY) > Mathf.Abs(offsetX))
            {
                if (offsetY > 0)
                {
                    //tang gia tri cua z
                    direct = Direct.Forward;
                    if (!isMoving)
                    {
                        Move(GetLastPoint(Vector3.forward));
                    }
                }
                else
                {
                    //giam gia tri cua z
                    direct = Direct.Backward;
                    if (!isMoving)
                    {
                        Move(GetLastPoint(Vector3.back));
                    }
                }
            }
            else
            {
                if (offsetX > 0)
                {
                    //tang gia tri cua x
                    direct = Direct.Right;

                    if (!isMoving)
                    {
                        Move(GetLastPoint(Vector3.right));
                    }
                }
                else
                {
                    //giam gia tri cua x
                    direct = Direct.Left;

                    if (!isMoving)
                    {
                        Move(GetLastPoint(Vector3.left));
                    }
                }
            }
            Debug.Log(direct);
        }
    }
}

