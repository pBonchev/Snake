using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public static Snake instance;

    [SerializeField]
    private GameObject body;

    [SerializeField]
    private Sprite headSprite;

    [SerializeField]
    private Sprite bodySprite1;

    [SerializeField]
    private Sprite bodySprite2;

    [SerializeField]
    private Sprite tailSprite1;

    [SerializeField]
    private Sprite tailSprite2;

    private List<BodyPart> parts = new List<BodyPart>();
    private int head;

    private void Awake()
    {
        instance = this;
    }

    public void CreateSnakeParts()
    {
        transform.position = new Vector3(1f, 1f, 0f);

        parts.Add(Instantiate(body, new Vector3(1f, 2f, 0f), Quaternion.identity, this.transform).GetComponent<BodyPart>());
        parts.Add(Instantiate(body, new Vector3(1f, 1f, 0f), Quaternion.identity, this.transform).GetComponent<BodyPart>());
        parts.Add(Instantiate(body, new Vector3(1f, 0f, 0f), Quaternion.identity, this.transform).GetComponent<BodyPart>());

        head = 0;
        
        Visualize();
    }

    public void AddBody()
    {
        BodyPart newBodyPart = (Instantiate(body, parts[Tail()].transform.position, Quaternion.identity, this.transform).GetComponent<BodyPart>());
        newBodyPart.ChangeSprite(null);
        
        parts.Insert(head, newBodyPart);

        head++;
    }

    public void Move()
    {
        if (GrassContainsSnake(new Vector3(parts[head].transform.position.x + GameManager.instance.direction.x,
            parts[head].transform.position.y + GameManager.instance.direction.y, 0f)) ||
            parts[head].transform.position.x + GameManager.instance.direction.x >= MapGenerator.instance.widgth ||
            parts[head].transform.position.x + GameManager.instance.direction.x < 0 ||
            parts[head].transform.position.y + GameManager.instance.direction.y >= MapGenerator.instance.heigth ||
            parts[head].transform.position.y + GameManager.instance.direction.y < 0) 
        {
            Die();
            Restart();
            return;
        }

        parts[Tail()].transform.position = new Vector3(parts[head].transform.position.x + GameManager.instance.direction.x,
            parts[head].transform.position.y + GameManager.instance.direction.y, 0f);

        head = Tail();

        Visualize();

        AppleManager.instance.CheckCollision(parts[head].transform.position);
    }

    public bool GrassContainsSnake(Vector3 pos)
    {
        for (int i = 0; i < parts.Count; i++)
            if (parts[i].transform.position == pos)
                return true;

        return false;
    }

    private void Die()
    {
        for (int i = 0; i < parts.Count; i++)
            Destroy(parts[i].gameObject);

        parts.Clear();
    }

    public void Restart()
    {
        GameManager.instance.direction = Vector2.zero;

        CreateSnakeParts();
    }

    private void Visualize()
    {
        parts[head].ChangeSprite(headSprite);
        if (GameManager.instance.direction == Vector2.up) parts[head].transform.eulerAngles = new Vector3(0, 0, 0);
        if (GameManager.instance.direction == Vector2.down) parts[head].transform.eulerAngles = new Vector3(0, 0, 180);
        if (GameManager.instance.direction == Vector2.left) parts[head].transform.eulerAngles = new Vector3(0, 0, 90);
        if (GameManager.instance.direction == Vector2.right) parts[head].transform.eulerAngles = new Vector3(0, 0, 270);

        int i = head;
        int j = head + 2;
        if (j >= parts.Count) j -= parts.Count;

        while (j != head)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) break;

            int k = j - 1;
            if (k < 0) k = parts.Count - 1;

            if (k != head)
            {
                if (parts[i].transform.position.x != parts[j].transform.position.x && parts[i].transform.position.y != parts[j].transform.position.y)
                {
                    parts[k].ChangeSprite(bodySprite2);

                    if ((parts[k].transform.position.x < parts[i].transform.position.x && parts[k].transform.position.y < parts[j].transform.position.y) ||
                        (parts[k].transform.position.x < parts[j].transform.position.x && parts[k].transform.position.y < parts[i].transform.position.y))
                        parts[k].transform.eulerAngles = new Vector3(0, 0, 180);
                    if ((parts[k].transform.position.x > parts[i].transform.position.x && parts[k].transform.position.y < parts[j].transform.position.y) ||
                        (parts[k].transform.position.x > parts[j].transform.position.x && parts[k].transform.position.y < parts[i].transform.position.y))
                        parts[k].transform.eulerAngles = new Vector3(0, 0, 270);
                    if ((parts[k].transform.position.x < parts[i].transform.position.x && parts[k].transform.position.y > parts[j].transform.position.y) ||
                        (parts[k].transform.position.x < parts[j].transform.position.x && parts[k].transform.position.y > parts[i].transform.position.y))
                        parts[k].transform.eulerAngles = new Vector3(0, 0, 90);
                    if ((parts[k].transform.position.x > parts[i].transform.position.x && parts[k].transform.position.y > parts[j].transform.position.y) ||
                        (parts[k].transform.position.x > parts[j].transform.position.x && parts[k].transform.position.y > parts[i].transform.position.y))
                        parts[k].transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    parts[k].ChangeSprite(bodySprite1);
                    if (parts[i].transform.position.x == parts[j].transform.position.x)
                        parts[k].transform.eulerAngles = new Vector3(0, 0, 0);
                    else parts[k].transform.eulerAngles = new Vector3(0, 0, 90);
                }
            }

            j++;
            if (j == parts.Count) j = 0;
            i++;
            if (i == parts.Count) i = 0;
        }

        j = Tail() - 1;
        if (j < 0) j = parts.Count - 1;

        parts[Tail()].ChangeSprite(tailSprite1);

        if (parts[j].transform.position.x < parts[Tail()].transform.position.x) parts[Tail()].transform.eulerAngles = new Vector3(0, 0, 90);
        if (parts[j].transform.position.x > parts[Tail()].transform.position.x) parts[Tail()].transform.eulerAngles = new Vector3(0, 0, 270);
        if (parts[j].transform.position.y < parts[Tail()].transform.position.y) parts[Tail()].transform.eulerAngles = new Vector3(0, 0, 180);
        if (parts[j].transform.position.y > parts[Tail()].transform.position.y) parts[Tail()].transform.eulerAngles = new Vector3(0, 0, 0);
    }

    private int Tail()
    {
        if (head == 0) return parts.Count - 1;
        return head - 1;
    }
}
