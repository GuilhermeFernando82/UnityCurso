using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SkillDown : NetworkBehaviour
{

    public float destroyAfter = 5f;

    public GameObject obj;
    public Rigidbody2D rb;
    public float vel;
    private Vector2 dir;
    // Start is called before the first frame update
    
    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vel = 0.3f;
    }
    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        transform.position += Vector3.down * vel;
    }

}
