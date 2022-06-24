using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public static Snake instance;

    [SerializeField]
    private GameObject body;

    private List<BodyPart> parts = new List<BodyPart>();
    private int head;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }

    public void CreateSnakeParts()
    {
        transform.position = new Vector3(1f, 1f, 0f);

        parts.Add(Instantiate(body, transform.position, Quaternion.identity, this.transform).GetComponent<BodyPart>());
        head = 0;

        AddBody();
        AddBody();
    }

    public void AddBody()
    {
        parts.Add(Instantiate(body, parts[Tail()].transform.position, Quaternion.identity, this.transform).GetComponent<BodyPart>());
    }

    public void Move()
    {
        parts[Tail()].transform.position = new Vector3(parts[head].transform.position.x + GameManager.instance.direction.x,
            parts[head].transform.position.y + GameManager.instance.direction.y, 0f);

        head = Tail();

        AppleManager.instance.CheckCollision(parts[head].transform.position);
    }

    private int Tail()
    {
        if (head == 0) return parts.Count - 1;
        return head - 1;
    }
}
