using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class PowerBlock : MonoBehaviour {

    public float bounce = .5f;
    public float speed = 4f;
    private Vector2 position;

    private bool powerUpExists  = false;

    private SatelliteBehavior sb;
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private GameObject p_up;
    private GameObject player;

    private bool hit = false;
    private bool triggered = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sb = player.GetComponent<SatelliteBehavior>();

        position = transform.localPosition;
    }

    void Update()
    {
        if (powerUpExists)
        {
            if (p_up.transform.localPosition.x > position.x && p_up.transform.localPosition.y < position.y && !triggered)
            {
                trigger();
            }
            rb.velocity = new Vector2(7.5f, -5f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.y - collision.collider.bounds.max.y > 0 && collision.collider.tag == "Player")
            BlockBounce();

    }

    public void BlockBounce()
    {
        if (!hit)
        {
            hit = true;
            StartCoroutine(boxHit());
        }
    }

    IEnumerator boxHit()
    {
        while ((transform.localPosition.y < position.y + bounce))
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed * Time.deltaTime);
            yield return null;
        }

        while (transform.localPosition.y > position.y)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * Time.deltaTime);
            yield return null;
        }

        if (transform.localPosition.y != position.y)
        {
            transform.localPosition = new Vector2(position.x, position.y);
        }

        PowerUp();
    }

    void PowerUp()
    {
        p_up = new GameObject();

        if (sb.getMode() == 0)
        {
            p_up = (GameObject)Instantiate(Resources.Load("Mushroom", typeof(GameObject)));
        }
        else if (sb.getMode() == 1)
        {
            System.Random rand = new System.Random();
            int powerUp = rand.Next(2, 5);

            if (powerUp == 2)
                p_up = (GameObject)Instantiate(Resources.Load("Mace", typeof(GameObject)));
            else if (powerUp == 3)
                p_up = (GameObject)Instantiate(Resources.Load("Mace", typeof(GameObject)));
            else if (powerUp == 4)
                p_up = (GameObject)Instantiate(Resources.Load("Mace", typeof(GameObject)));
            else if (powerUp == 5)
                p_up = (GameObject)Instantiate(Resources.Load("Mace", typeof(GameObject)));
        }
        

        p_up.transform.SetParent(this.transform.parent);
        p_up.transform.localPosition = new Vector2(position.x, position.y + 1);

        rb = p_up.GetComponent<Rigidbody2D>();
        cc = p_up.GetComponent<CircleCollider2D>();

        powerUpExists = true;

        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("G2-block-hit");

    }

    void trigger()
    {
        cc.isTrigger = true;
        triggered = true;
    }
}
