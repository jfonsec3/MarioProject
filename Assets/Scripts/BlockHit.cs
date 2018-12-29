using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHit : MonoBehaviour {

    private float time = 10f;
    public float bounce = .5f;
    public float speed = 4f;
    private Vector2 position;
    private SatelliteBehavior sb;
    private GameObject player;
    public bool hit = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sb = player.GetComponent<SatelliteBehavior>();

        position = transform.localPosition;
    }

    void Update()
    {

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
        GameObject coin = new GameObject();
        coin = (GameObject)Instantiate(Resources.Load("G2-coin-pref", typeof(GameObject)));
        coin.transform.SetParent(this.transform.parent);
        coin.transform.localPosition = new Vector2(position.x, position.y + 1);
        this.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("G2-block-hit");
 

        while ((transform.localPosition.y <= position.y + bounce))
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + speed * Time.deltaTime);
            yield return null;
        }

        while (transform.localPosition.y >= position.y)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - speed * Time.deltaTime);
            yield return null;
        }

        if (transform.localPosition.y != position.y)
        {
            transform.localPosition = new Vector2(position.x, position.y);
        }

        coin.SetActive(false);
        sb.IncrementCoins();
    }
}

