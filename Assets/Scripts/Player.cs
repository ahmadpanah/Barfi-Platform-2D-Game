using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;

    public float jump = 5;

    private bool isgrounded = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isgrounded) {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isgrounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "ground") {
            isgrounded = true;
        }
    }
}
