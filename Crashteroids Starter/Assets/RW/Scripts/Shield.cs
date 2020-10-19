using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Game game;
    public GameObject ship;

    private bool isToggled = false;

    private GameObject shield;

    void Start()
    {
        shield = transform.GetChild(0).gameObject;
    }

    void Update()
    {
		if (isToggled)
		{
            shield.transform.position = ship.transform.position;
        }
    }

    void LateUpdate()
	{
		if (!game.isGameOver)
		{
            ToggleShield();
		}
	}

    void ToggleShield()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			if (isToggled)
			{
                isToggled = false;
                shield.GetComponent<SpriteRenderer>().enabled = false;
            }
			else
			{
                isToggled = true;
                shield.GetComponent<SpriteRenderer>().enabled = true;
            }
		}
	}
}
