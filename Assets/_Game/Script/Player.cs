using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private float speed = 250;
    Vector2 startPoint;
    Vector2 endPoint;
    bool isVerticalSwipe;
    bool isUpValue;
    public Direct direct;
    bool isMoving;
    void OnInit()
    {

    }
    private void Update()
    {
        Swipe();
    }
    Vector3 LastRightPoint()
    {
        int i = 1;
        RaycastHit hit;
        Vector3 result;
        while (true)
        {
            if (Physics.Raycast(transform.position + Vector3.right * i, Vector3.down, out hit, 10f))
            {
                i++;
                print("hit");
            }
            else
            {
                result = transform.position + Vector3.right * (i - 1);
                break;
            }
        }
        Debug.Log("r"+result);
        return result;
    }
    Vector3 LastLeftPoint()
    {
        int i = 1;
        RaycastHit hit;
        Vector3 result;
        while (true)
        {
            if (Physics.Raycast(transform.position + Vector3.left * i, Vector3.down, out hit))
            {
                i++;
            }
            else
            {
                result = transform.position + Vector3.left * (i - 1);
                break;
            }
        }
        Debug.Log("l"+result);
        return result;
    }
    public Vector3 LastForwardPoint()
    {
        int i = 1;
        RaycastHit hit;
        Vector3 result;
        while (true)
        {
            if (Physics.Raycast(transform.position + Vector3.forward * i, Vector3.down, out hit))
            {
                i++;
            }
            else
            {
                result = transform.position + Vector3.forward * (i - 1);
                break;
            }
        }
        Debug.Log("f"+result);
        return result;
    }
    Vector3 LastBackwardPoint()
    {
        int i = 1;
        RaycastHit hit;
        Vector3 result;
        while (true)
        {
            if (Physics.Raycast(transform.position + Vector3.back * i, Vector3.down, out hit))
            {
                i++;
            }
            else
            {
                result = transform.position + Vector3.back * (i - 1);
                break;
            }
        }
        Debug.Log("b"+result);
        return result;
    }
    bool CheckDirectionSwipe(float offsetX, float offsetY)
    {
        return Mathf.Abs(offsetY) > Mathf.Abs(offsetX);
    }
    private void Move(Vector3 pos)
    {

        if (!isMoving)
        {
            isMoving = true;
            transform.DOMove(pos, 0.7f).Complete(isMoving = false);
        }
            
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
            isVerticalSwipe = CheckDirectionSwipe(offsetX, offsetY);
            if (isVerticalSwipe)
            {
                if (offsetY > 0)
                {
                    //tang gia tri cua z
                    direct = Direct.Forward;
                    Move(LastForwardPoint());
                }
                else
                {
                    //giam gia tri cua z
                    direct = Direct.Backward;
                    Move(LastBackwardPoint());
                }
            }
            else
            {
                if (offsetX > 0)
                {
                    //tang gia tri cua x
                    direct = Direct.Right;
                    Move(LastRightPoint());
                }
                else
                {
                    //giam gia tri cua x
                    direct = Direct.Left;
                    Move(LastLeftPoint());
                }
            }
            Debug.Log(direct);
        }
    }
}

