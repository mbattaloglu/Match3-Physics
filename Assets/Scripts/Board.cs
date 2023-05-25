using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Vector2 mousePos;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if(hit.collider != null)
            {
                List<Ball> cb = hit.collider.gameObject.GetComponent<Ball>().GetConnectedBalls();
                cb.Add(hit.collider.gameObject.GetComponent<Ball>());
                if(cb.Count >= 2)
                {
                    foreach(Ball ball in cb)
                    {
                        StartCoroutine(ball.Pop());
                    }
                }
            }
        }
    }
}
