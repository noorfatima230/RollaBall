using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    GameObject player;
    private Rigidbody rb;
    public float speed;

    private int count;
    public Text countText;
    public Text GameCompleted;
    
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        rb = player.GetComponent<Rigidbody>();

        GameCompleted.gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start() {
        count = 0;
        CountText();
    }

    void Update() {
        if (count > 90)
        {
            rb.isKinematic = true;
            GameCompleted.gameObject.SetActive(true);
            GameCompleted.text = "You Win!";
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
	    {
            other.gameObject.SetActive(false);
            count = count + 10;
            CountText();
        }
        if (other.gameObject.tag == "hazard")
        {
            rb.isKinematic = true;
            GameCompleted.gameObject.SetActive(true);
            GameCompleted.text = "Game Over";
            other.gameObject.SetActive(false);
            Vector3 jump = new Vector3(0.0f, 30, 0.0f);
            GetComponent<Rigidbody>().AddForce(jump * speed * Time.deltaTime);
        }
        if (other.gameObject.tag == "Walls")
        {
            rb.isKinematic = true;
            GameCompleted.gameObject.SetActive(true);
            GameCompleted.text = "Game Over";
        }
    }

   

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed * Time.deltaTime);

    }

    
    void CountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
