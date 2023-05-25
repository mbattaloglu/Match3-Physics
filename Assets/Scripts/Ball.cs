using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallType ballType;

    public IEnumerator Pop()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        Destroy(GetComponent<Collider2D>());
        Destroy(GetComponentInChildren<SpriteRenderer>());
        Destroy(GetComponent<Rigidbody2D>());
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    public List<Ball> GetAdjacentBalls()
    {
        List<Ball> adjacentBalls = new List<Ball>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.4f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Ball") && collider.gameObject.GetComponent<Ball>().ballType == ballType)
            {
                adjacentBalls.Add(collider.gameObject.GetComponent<Ball>());
            }
        }
        return adjacentBalls;
    }

    public List<Ball> GetConnectedBalls(List<Ball> exclude = null)
    {
        List<Ball> connectedBalls = new List<Ball>();

        if (exclude == null)
        {
            exclude = new List<Ball> { this };
        }
        else
        {
            exclude.Add(this);
        }

        List<Ball> adjacentBalls = GetAdjacentBalls();

        foreach (Ball ball in adjacentBalls)
        {
            if (!exclude.Contains(ball))
            {
                connectedBalls.Add(ball);
                connectedBalls.AddRange(ball.GetConnectedBalls(exclude));
            }
        }

        return connectedBalls;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.4f);
    }
}
