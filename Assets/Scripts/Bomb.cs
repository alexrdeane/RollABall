using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;
public class Bomb : NetworkBehaviour
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
        CmdExplode(transform.position, explosionRadius);

    }

    void CmdExplode(Vector3 pos, float rad)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.GetComponent<Enemy>())
            {
            NetworkServer.Destroy(hit.gameObject);

            }
        }
    }
    void Update()
    {
        
    }
}
