using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private Spawner spawner;

    public Game game;
    public GameObject ship;

    public bool isToggled = false;
    public bool isDestroyed = false;

    void Start()
    {
    }

    void Update()
    {
		if (isToggled)
		{
            transform.position = ship.transform.position;
        }
    }

    void LateUpdate()
	{
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleShield();
		}
	}

    public void ToggleShield()
    {
        if (!game.isGameOver && !isDestroyed)
        {
            if (isToggled)
            {
                isToggled = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                isToggled = true;
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Asteroid")
		{
            if (collision.gameObject.GetComponent<Asteroid>() != null)
            {
                if (!isDestroyed && isToggled)
                {
                    spawner.asteroids.Remove(collision.gameObject);
                    Destroy(collision.gameObject);

                    isToggled = false;
                    isDestroyed = true;
                    GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }
}
