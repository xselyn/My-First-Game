using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float viewDist;
    public float moveSpeed;

    private Animator anim;
    private GameObject player;
    private Rigidbody rb;

    public int hits = 5;
    public GameObject hitBox;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 starPos = new Vector3(transform.position.x, GetComponent<BoxCollider>().bounds.max.y, transform.position.z);

        int layerMask = 1 << 7;

        RaycastHit hit;

        if (Physics.Raycast(starPos, transform.TransformDirection(Vector3.forward), out hit, viewDist, layerMask))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                anim.Play("Run");
                player = GameObject.Find("soldier");
            }
        }
        else
        {

        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            ChasePlayer();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            hitBox.SetActive(true);
        }
        else
        {
            hitBox.SetActive(false);
        }

        if (hits <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void ChasePlayer()
    {
        if (player.transform.position.x > this.transform.position.x)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -1);
        }

        rb.velocity = new Vector3(moveSpeed*transform.localScale.z, rb.velocity.y, 0);

        if (Vector2.Distance(transform.position, player.transform.position) < 1)
        {
            anim.Play("Attack");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            hits--;
            Destroy(other.gameObject);
        }
    }
}
