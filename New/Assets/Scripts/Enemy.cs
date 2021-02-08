using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class Enemy : NetworkBehaviour
{
    public Rigidbody2D RbEnemy;
    public float RaiodeVisao, RaiodeAtaque, vel;
    public LayerMask OqePlayer;

    public GameObject Player;
    public List<GameObject> PLAYER = new List<GameObject>();
    public LayerMask Raycast;

    [SerializeField]
    private Vector3 posicaoInicial;
    public Vector3 alvo;
    [SerializeField]
    private float fov;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private Vector3 dir;
    public Rigidbody2D projectil;
    public static Enemy instance;
    public bool atk;
    public float life = 100;
    public WaitForSeconds tempo = new WaitForSeconds(1.5f);
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Raycast = 1 << LayerMask.NameToLayer("Player");
        RbEnemy = GetComponent<Rigidbody2D>();
        posicaoInicial = transform.position;
        alvo = posicaoInicial;
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0)
        {
           NetworkServer.Destroy(gameObject);
        }
        if(dir.x != 0 || dir.y != 0)
        {
            anim.SetFloat("x", dir.x);
            anim.SetFloat("y", dir.y);
        }
        RaioJogador();
    }
    IEnumerator Tiro()
    {
        atk = true;
        Instantiate(projectil, transform.position, Quaternion.identity);
        yield return tempo;
        atk = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(posicaoInicial, RaiodeVisao);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posicaoInicial, RaiodeAtaque);

    }
    public void RaioJogador()
    {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Player.transform.position - transform.position, RaiodeVisao, OqePlayer);
            Vector3 temp = transform.TransformDirection(Player.transform.position - transform.position);
            Debug.DrawRay(transform.position, temp, Color.cyan);

            if (hit.collider != null)
            {
                if (hit.collider.tag == "Player")
                {
                    alvo = Player.transform.position;
                    print("Colidiu");
                    if (!atk)
                    {
                        StartCoroutine("Tiro");
                    }
                }
            }
            else
            {
                alvo = posicaoInicial;
            }
            float distTemp = Vector3.Distance(alvo, transform.position);
            dir = (alvo - transform.position).normalized;

            if (alvo != posicaoInicial && distTemp < RaiodeAtaque)
            {

            }
            else
            {
                RbEnemy.MovePosition(transform.position + dir * vel * Time.deltaTime);
            }
            if (alvo == posicaoInicial && distTemp <= 0.02f)
            {
                transform.position = posicaoInicial;
            }

        
    }

      void OnTriggerEnter2D(Collider2D collision)
        {
        if (collision.CompareTag("Player"))
        {
            Player = collision.gameObject;
        }
            if (collision.CompareTag("SkillPlayer"))
            {
                life += -20f;
            }
        }
    }
