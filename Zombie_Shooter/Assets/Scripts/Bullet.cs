using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1;
    public int facing = 1;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = null;
        StartCoroutine (yieldDestroy(0.75f));
        if (GameObject.Find("soldier").GetComponent<PlayerController>().facingRight)
        {
            facing = 1;
        }
        else
        {
            facing = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * facing, 0, 0),Space.World);
    }
    public IEnumerator yieldDestroy (float timestop)
    {
        yield return new WaitForSeconds(timestop);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            Destroy(this.gameObject);
        }
    }
}
