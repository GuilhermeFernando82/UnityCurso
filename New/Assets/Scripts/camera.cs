using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private Vector2 velocity;

    public float smoothTimeY;
    public float smoothTimeX;
    public float deltaY;

    public GameObject player;

    void Start()
    {
        smoothTimeY = 0.5f;
        smoothTimeX = 0.5f;
        deltaY = 0.5f;
    }

    void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + deltaY, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}