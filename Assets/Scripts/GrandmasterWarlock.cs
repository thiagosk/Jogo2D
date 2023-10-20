using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrandmasterWarlock : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject fire;
    public Transform firePos;
    public bool facingRight = false;
    public float moveSpeed = 3f;
    private Vector2 moveDirection;
    private GameObject player;
    private float timerShoot3Times;
    private float[] shootAngles = { 0,30f, -30f };
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player){
            if(Vector2.Distance(transform.position,player.transform.position) > 0.1f){
                timerShoot3Times+= Time.deltaTime;

                if(timerShoot3Times>1.5f)
                {
                    timerShoot3Times = 0;
                    shoot();
                }
            }

            Vector3 direction = (player.transform.position - transform.position).normalized;
            moveDirection = direction;
            print(moveDirection);
        }

        if (player.transform.position.x < transform.position.x && !facingRight){
            Flip ();
        }
        if (player.transform.position.x > transform.position.x && facingRight){
            Flip ();
        }
    }

    private void Flip(){
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    void FixedUpdate(){
        if(player){
            rb.velocity = new Vector2(moveDirection.x,moveDirection.y)*moveSpeed;
        }
    }

    void shoot()
    {
        for(int i=0;i<3;i++){
            GameObject newFire = Instantiate(fire, firePos.position, Quaternion.identity);
            float shootAngle = shootAngles[i];
            Vector3 direction = player.transform.position - firePos.position;
            Vector3 combined = Quaternion.Euler(0,0,shootAngle)*direction.normalized;
            newFire.GetComponent<Rigidbody2D>().velocity = combined * 5f;
        }
    }

    void directShoot(){

    }
}
