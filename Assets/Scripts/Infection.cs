using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour
{
    Manager manager;
    int count=0;
    int infectionDay;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Infection")
        {
            if (count == 0)
            {
                infectionDay = manager.daycount;
                infectionDay += Random.Range(3, 14);
                count = 1;
            }
            if (infectionDay - manager.daycount <= 0)
            {
                gameObject.tag = "Sick";
            }

        }
        if(gameObject.tag == "Sick")
        {
            if (count == 1)
            {
                infectionDay = manager.daycount;
                infectionDay += Random.Range(3, 7);
                count = 2;
            }
            if (infectionDay - manager.daycount <= 0)
            {
                float i = Random.Range(0f, 1f);
                if (i > 0.5f)
                {
                    gameObject.tag = "Healthy";

                    Destroy(gameObject.GetComponent<Infection>());
                }
                else
                {
                    if (gameObject.GetComponent<Actions_Doctor>())
                    {
                        manager.safe[gameObject.GetComponent<Actions_Doctor>().HomeNumber - 1] = true;
                    }
                    if (gameObject.GetComponent<Actions_Student>())
                    {
                        manager.safe[gameObject.GetComponent<Actions_Student>().HomeNumber - 1] = true;
                    }
                    if (gameObject.GetComponent<Actions_Worker>())
                    {
                        manager.safe[gameObject.GetComponent<Actions_Worker>().HomeNumber - 1] = true;
                    }
                    manager.NDeath++;
                    manager.NSick--;
                    gameObject.SetActive(false);
                }
                count = 0;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Infection"|| other.tag == "Sick")
        {
            float i = Random.Range(0f, 1f);
            if (i < manager.infectionChance && gameObject.tag == "Healthy")
            {
                gameObject.tag = "Infection";
            }
        }
    }
}
