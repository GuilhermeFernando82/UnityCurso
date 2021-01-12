using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRight : MonoBehaviour
{
    public GameObject obj;
    public Rigidbody2D rb;
    public float vel;
    private Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vel = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        transform.position += Vector3.right * vel;
    }
 
}
