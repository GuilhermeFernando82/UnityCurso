using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Mirror;
public class Player : NetworkBehaviour
{
    
    public Rigidbody2D RbPlayer;
   
    Vector2 direcao;
    public GameObject SkillUp;
    public GameObject SkillDown;
    public GameObject SkillLeft;
    public GameObject SkillRight;
    public bool up;
    public bool down;
    public bool left;
    public bool right;
    
    public Image barra;
    public bool soltouSkill;
    [Header("Components")]
    public NavMeshAgent agent;
    public Animator anim;

    [Header("Moviments")]
    public float speed = 5f;

    [Header("Firing")]
    public KeyCode shot = KeyCode.Space;

    public GameObject ParentCamera;
    public Camera camera;
    public GameObject obj;

    [SyncVar]
    public float lifeP;
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
        {
            ParentCamera.gameObject.SetActive(false);
        }
        RbPlayer = GetComponent<Rigidbody2D>();
        lifeP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        
        anim.SetFloat("horizontal", direcao.x);
        anim.SetFloat("vertical", direcao.y);
        anim.SetFloat("velocidade", direcao.sqrMagnitude);
        if(direcao != Vector2.zero)
        {
            anim.SetFloat("horizontalidle", direcao.x);
            anim.SetFloat("verticalidle", direcao.y);
        }
        transform.Translate(direcao * speed * Time.deltaTime);
        input();
        /*if (Input.GetKeyDown(shot) && isClient)
        {
            
            CmdFireClient(up, down, left, right);
            //CmdFire(up, down, left, right);

        }*/
        if (Input.GetKeyDown(shot))
        {
            CmdFire(up, down, left, right);
        }
        DestroyPlayer();
        barra.fillAmount = lifeP / 100;

       
    }
    [Command]
    void DestroyPlayer()
    {
        if (lifeP <= 0)
        {
            //Application.LoadLevel("GameOver");


            NetworkServer.Destroy(gameObject);



        }
    }
 
    [Command]
    void CmdFire(bool up, bool down, bool left, bool right)
    {
        if (up)
        {
            obj = Instantiate(SkillUp, transform.position, transform.rotation);
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            NetworkServer.Spawn(obj);
            RpgFireUp();
            

        }
        if (down)
        {
            obj = Instantiate(SkillDown, transform.position, transform.rotation);
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            NetworkServer.Spawn(obj);
            RpgFireDown();
         

        }
        if (left)
        {
            obj = Instantiate(SkillLeft, transform.position, transform.rotation);
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            NetworkServer.Spawn(obj);
            RpgFireLeft();
           

        }
        if (right)
        {
            obj = Instantiate(SkillRight, transform.position, transform.rotation);
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            NetworkServer.Spawn(obj);
            RpgFireRight();
           

        }
    }
    [ClientRpc]
    void RpgFireUp()
    {
        obj = Instantiate(SkillUp, transform.position, transform.rotation);
        Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //NetworkServer.Spawn(obj);
    }
    [ClientRpc]
    void RpgFireDown()
    {
        obj = Instantiate(SkillDown, transform.position, transform.rotation);
        Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //NetworkServer.Spawn(obj);
    }
    [ClientRpc]
    void RpgFireLeft()
    {
        obj = Instantiate(SkillLeft, transform.position, transform.rotation);
        Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //NetworkServer.Spawn(obj);
    }
    [ClientRpc]
    void RpgFireRight()
    {
        obj = Instantiate(SkillRight, transform.position, transform.rotation);
        Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //NetworkServer.Spawn(obj);
    }
    public void input()
    {
        direcao = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow)){
            direcao += Vector2.up;
            up = true;
            down = false;
            left = false;
            right = false;
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            direcao += Vector2.down;
            up = false;
            down = true;
            left = false;
            right = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            direcao += Vector2.left;
            up = false;
            down = false;
            left = true;
            right = false;
        }
        if (Input.GetKey(KeyCode.RightArrow)){
            direcao += Vector2.right;
            up = false;
            down = false;
            left = false;
            right = true;
        }
    }
    public void FixedUpdate()
    {
        RbPlayer.MovePosition(RbPlayer.position + direcao * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D Collider2D)
    {
        //physics.ignorecollision
        
        if (Collider2D.CompareTag("SkillVilao"))
        {
            lifeP += -30f;
        }
      
        if (Collider2D.CompareTag("SkillPlayer"))
        {

            if (isServer)
            {
                lifeP += -20f;
            }
                
            
                
        
        {
                
        }
            
        }
        
    }
}
