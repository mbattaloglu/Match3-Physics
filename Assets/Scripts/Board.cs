using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject bombPrefab;
    private Vector2 mousePos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                GameObject clickedObject = hit.collider.gameObject;

                switch (clickedObject.tag)
                {
                    case "Ball":
                        Pop(clickedObject.GetComponent<Ball>());
                        break;
                    case "Bomb":
                        clickedObject.GetComponent<Bomb>().Explode();
                        break;
                }


            }
        }
    }

    private void Pop(Ball ball)
    {
        List<Ball> connectedBalls = ball.GetConnectedBalls();
        connectedBalls.Add(ball);
        if (connectedBalls.Count >= 2)
        {
            foreach (Ball connectedBall in connectedBalls)
            {
                StartCoroutine(connectedBall.Pop());
            }
        }
        if (connectedBalls.Count >= 3)
        {
            Instantiate(bombPrefab, ball.transform.position, Quaternion.identity);
        }
    }
}
