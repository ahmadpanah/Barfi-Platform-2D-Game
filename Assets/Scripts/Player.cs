using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5;
    private Rigidbody2D rb;

    public float jump = 5;

    private bool isgrounded = false;

    private Animator anim;
    private Vector3 rotation;

    private CoinManagment m;

    public GameObject panel;
    public GameObject fire;

    public GameObject camera;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotation = transform.eulerAngles;
        m = GameObject.FindGameObjectWithTag("Text").GetComponent<CoinManagment>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        if (direction != 0) {
            anim.SetBool("IsRunning" , true);
        } else {
            anim.SetBool("IsRunning" , false);
        }

        if (direction < 0) {
            transform.eulerAngles = rotation - new Vector3(0,180,0);
            transform.Translate(Vector2.right * speed * -direction * Time.deltaTime);
        }
        if (direction > 0) {
            transform.eulerAngles = rotation;
            transform.Translate(Vector2.right * speed * direction * Time.deltaTime);
        }

        if (isgrounded == false) {
            anim.SetBool("IsJumping", true);
        } else {
            anim.SetBool("IsJumping", false);
        }

        

        if (Input.GetKeyDown(KeyCode.Space) && isgrounded) {
            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
            isgrounded = false;
        }

        camera.transform.position = new Vector3(transform.position.x , 0 , -10);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "ground") {
            isgrounded = true;
        }
         if (collision.gameObject.tag == "Enemy") {
            Instantiate(fire, transform.position , Quaternion.identity);
            panel.SetActive(true);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Die");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "coin") {
            m.Addmoney();
            Destroy(other.gameObject);
            FindObjectOfType<AudioManager>().Play("Coin");
        }
        if (other.gameObject.tag == "Spike") {

            Instantiate(fire, transform.position , Quaternion.identity);
            panel.SetActive(true);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Die");
        }

          if (other.gameObject.tag == "Finish") {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
