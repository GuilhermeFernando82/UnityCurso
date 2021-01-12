using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEnemy : MonoBehaviour
{
    public Rigidbody2D rbSkill;
    private Vector2 dir;
    public GameObject[] alvo;
    public float vel;

    // Start is called before the first frame update
    void Start()
    {
        rbSkill = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        alvo = GameObject.FindGameObjectsWithTag("Player");
        vel = 5f;
        foreach(GameObject play in alvo)
        {
            Vector3 alvoP = Enemy.instance.alvo;
            dir = alvoP - transform.position;
            rbSkill.velocity = dir.normalized * vel;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
