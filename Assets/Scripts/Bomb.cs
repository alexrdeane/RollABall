using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float explosionRadius = 2f;

    void Start()
    {
        StartCoroutine(Explode());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(.5f);
        Explode(transform.position, explosionRadius);
    }

    void Explode(Vector3 pos, float rad)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {

        }
    }
}
