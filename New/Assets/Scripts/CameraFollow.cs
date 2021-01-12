using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 velocity;

    public float X;
    public float Y;
    public float DeltaY;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        X = 0.5f;
        Y = 0.5F;
        DeltaY = 0.5f;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float PosX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref velocity.x, X);
        float PosY = Mathf.SmoothDamp(transform.position.y, target.transform.position.y + DeltaY, ref velocity.y, Y);
        transform.position = new Vector3(PosX, PosY, transform.position.z);
    }
}
