using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public int amount;
    public float timer;

    public Transform board;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(prefabs[Random.Range(0, prefabs.Length)], transform.position, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-180, 180), Random.Range(-180, 180)).normalized);
            temp.transform.parent = board;
            yield return new WaitForSeconds(timer);
        }
    }
}
