using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public float health = 125;
    public GameObject frankhp;

    public GameObject frank;
    public GameObject stone;

    // Start is called before the first frame update
    void Start()
    {
        stone.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        frankhp.GetComponent<Text>().text = health.ToString();

        if (health < 0)
        {
            stone.SetActive(true);
            frank.SetActive(false);
        }
        else
        {
            health -= Time.deltaTime;
        }
    }
}
