using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float runSpeed;
    public float walkSpeed;

    public float moveSpeed;

    public float jumpSpeed;

    public Rigidbody rb;
    public Animator anim;

    public bool facingRight = true;

    public GameObject bullet;

    public int hits = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetFloat("walk")>0.01)
        {
            moveSpeed = walkSpeed;
        }
        else
        {
            moveSpeed = runSpeed;
        }
        if (Input.GetAxis("Jump")> .1 && rb.velocity.y <= .000001f)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            anim.Play("Rise");
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject tmp = Instantiate(bullet);
            tmp.transform.position = bullet.transform.position;
            tmp.transform.rotation = bullet.transform.rotation;
            tmp.transform.localScale = bullet.transform.localScale;
            tmp.SetActive(true);
        }

        if (hits <= 0)
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            Invoke("ReloadLevel", 3f);
        }
    }

    private void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        anim.SetFloat("jump", rb.velocity.y);
        rb.velocity = new Vector3(move * moveSpeed, rb.velocity.y, 0);

        if (move < 0 && facingRight)
        {
            facingRight = false;
            flip();
        }
        if (move > 0 && !facingRight)
        {
            facingRight = true;
            flip();
        }

        anim.SetFloat("walk", Input.GetAxis("walk"));
    }

    public void flip()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * -1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            hits--;
        }
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
