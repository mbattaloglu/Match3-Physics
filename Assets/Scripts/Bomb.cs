using UnityEngine;

public class Bomb : MonoBehaviour
{
    public void Explode()
    {
        RaycastHit2D[] objects = Physics2D.BoxCastAll(transform.position, new Vector2(10, 0.1f), 0, Vector2.zero);
        foreach (RaycastHit2D obj in objects)
        {
            if (obj.collider.CompareTag("Ball") || obj.collider.CompareTag("Bomb"))
            {
                switch (obj.collider.gameObject.tag)
                {
                    case "Ball":
                        StartCoroutine(obj.collider.gameObject.GetComponent<Ball>().Pop());
                        break;
                    case "Bomb":
                        obj.collider.gameObject.GetComponent<Bomb>().Pop();
                        break;
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(10, 0.1f, 0));
    }

    public void Pop()
    {
        Destroy(gameObject);
    }
}
