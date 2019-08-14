﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;
public class Player : NetworkBehaviour
{
    public GameObject bombPrefab;

    public Camera attachedCamera;
    public Transform attachedVirtualCamera;
    public SkinnedMeshRenderer rend;
    public float speed = 10f, jump = 10f;
    public LayerMask ignoreLayers;
    public float rayDistance = 10f;
    public bool isGrounded = false;
    private Rigidbody rigid;
    #region Unity Events
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayDistance);
    }
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        attachedCamera.transform.SetParent(null);
        attachedVirtualCamera.SetParent(null);
        if (isLocalPlayer)
        {
            attachedCamera.enabled = true;
        }
        else
        {
            attachedCamera.enabled = false;
        }
    }
    private void FixedUpdate()
    {
        Ray groundRay = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(groundRay, rayDistance, ~ignoreLayers);
    }
    private void OnTriggerEnter(Collider col)
    {
        Item item = col.GetComponent<Item>();
        if (item)
        {
            item.Collect();
        }
    }
    private void Update()
    {
        if (isLocalPlayer)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                CmdSpawnBomb(transform.position);
            }
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            Move(inputH, inputV);
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    [Command]
    public void CmdSpawnBomb(Vector3 position)
    {
        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        NetworkServer.Spawn(bomb);

    }
    public void OnDestroy()
    {
        Destroy(attachedCamera.gameObject);
        Destroy(attachedVirtualCamera.gameObject);
    }
    #endregion
    #region Custom
    private void Jump()
    {
        if (isGrounded)
        {
            rigid.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }
    private void Move(float inputH, float inputV)
    {
        Vector3 direction = new Vector3(inputH, 0, inputV);

        // [Optional] Move with camera
        Vector3 euler = Camera.main.transform.eulerAngles;
        direction = Quaternion.Euler(0, euler.y, 0) * direction; // Convert direction to relative direction to camera only on Y

        rigid.AddForce(direction * speed);
    }
    #endregion
}
