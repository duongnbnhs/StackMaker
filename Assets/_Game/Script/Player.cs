using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
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
    public static Player Instance;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject brick;
    [SerializeField] Transform character;
    [SerializeField] Animator anim;
    Vector2 startPoint;
    Vector2 endPoint;
    Direct direct;
    bool isMoving;
    public List<GameObject> gameObjects;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        OnInit();
    }
    private void Update()
    {
        Swipe();
        if (!isMoving) ChangeAnim("stay");
    }
    void OnInit()
    {
        isMoving = false;
        gameObjects = new List<GameObject>();
    }
    Vector3 GetLastPoint(Vector3 direction)
    {
        int i = 1;
        Vector3 result;
        while (true)
        {
            if (Physics.Raycast(transform.position + direction * i, Vector3.down))
            {
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
        transform.DOMove(pos, 0.75f).OnComplete(() => isMoving = false) ;

        ChangeAnim("jump");
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
    public void AddBrickStack()
    {
        GameObject go = Instantiate(brick);
        go.transform.parent = transform;
        if (gameObjects.Count == 0 || gameObjects == null)
        {
            Vector3 firstPos = transform.position;
            firstPos.y = -0.18f;
            go.transform.position = firstPos;
            gameObjects.Add(go);
        }
        else
        {
            Vector3 newBrickPos = gameObjects.Last().gameObject.transform.position;
            newBrickPos.y += 0.25f;
            go.transform.position = newBrickPos;
            gameObjects.Add(go);
            Vector3 newCharacterPos = character.position;
            newCharacterPos.y += 0.25f;
            character.position = newCharacterPos;
        }
    }
    public void RemoveBrickStack()
    {
        GameObject go = gameObjects.Last();
        gameObjects.Remove(go);
        Destroy(go);
        Vector3 newCharacterPos = character.position;
        newCharacterPos.y -= 0.25f;
        character.position = newCharacterPos;
    }
    protected void ChangeAnim(string animName)
    {
        /*if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }*/
        anim.ResetTrigger(animName);
        anim.SetTrigger(animName);
    }
}

