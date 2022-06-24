using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector2 direction;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MapGenerator.instance.CreateMap();
        AppleManager.instance.GrowApple();
        Snake.instance.CreateSnakeParts();

        StartCoroutine(MoveSnake());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) direction = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector2.right;
    }

    private IEnumerator MoveSnake()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            Snake.instance.Move();
        }
    }
}
