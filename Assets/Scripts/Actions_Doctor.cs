using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions_Doctor : MonoBehaviour
{
    public Transform Home, Company, Restaurant, Hospital;
    public Manager manager;
    public string State, Health;
    public PathFinder pathfinder;
    bool done;
    string preNow;
    public string Building;
    float timer = 0;
    public int HomeNumber;
    // Start is called before the first frame update
    void Start()
    {
        pathfinder = gameObject.GetComponent<PathFinder>();
        
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        Company = GameObject.Find("Company").GetComponent<Transform>().transform;
        Restaurant = GameObject.Find("Restaurant").GetComponent<Transform>().transform;
        Hospital = GameObject.Find("Hospital").GetComponent<Transform>().transform;
        

        preNow = manager.Now;
    }

    // Update is called once per frame
    void Update()
    {
        Health = gameObject.tag;
        if (manager.Now != preNow)
        {
            done = false;
        }
        preNow = manager.Now;
        switch (State)
        {
            
            case "GoCompany":
                pathfinder.targetPos = Company.position;
                break;
            case "GoHome":
                pathfinder.targetPos = Home.position;
                break;
            case "GoRestaurant":
                pathfinder.targetPos = Restaurant.position;
                break;
            case "GoHospital":
                pathfinder.targetPos = Hospital.position;
                break;
            case "Idle":
                //pathfinder.targetPos = null;
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    switch (Building)
                    {
                        case "Company":
                            Vector3 newPos = new Vector3(Company.position.x + Random.Range(-4f, 4f), 0f, Company.position.z + Random.Range(-4f, 4f));
                            pathfinder.targetPos = newPos;
                            break;
                        case "Restaurant":
                            Vector3 newPos1 = new Vector3(Restaurant.position.x + Random.Range(-4f,4f), 0f, Restaurant.position.z + Random.Range(-4f, 4f));
                            pathfinder.targetPos = newPos1;
                            break;
                        case "Hospital":
                            Vector3 newPos2 = new Vector3(Hospital.position.x + Random.Range(-4f, 4f), 0f, Hospital.position.z + Random.Range(-4f, 4f));
                            pathfinder.targetPos = newPos2;
                            break;
                        case "Home":
                            Vector3 newPos3 = new Vector3(Home.position.x + Random.Range(-1f, 1f), 0f, Home.position.z + Random.Range(-1f, 1f));
                            pathfinder.targetPos = newPos3;
                            break;
                    }
                    timer = 0.5f;
                }

                break;
        }
        if (!manager.quarantine)
        {
            switch (manager.Now)
            {
                case "Morning":
                    if (!done)
                    {
                        State = "GoHospital";
                    }
                    break;
                case "Noon":
                    if (!done)
                    {
                        State = "GoRestaurant";
                    }
                    break;
                case "Afternoon":
                    if (!done)
                    {
                        State = "GoHospital";
                    }
                    break;
                case "Evening":
                    if (!done)
                    {
                        State = "GoHome";
                    }
                    break;

            }
        }
        else
        {
            if ((Health == "Healthy"|| Health == "Infection")&& manager.safe[HomeNumber-1])
            {
                switch (manager.Now)
                {
                    case "Morning":
                        if (!done)
                        {
                            State = "GoHospital";
                        }
                        break;
                    case "Noon":
                        if (!done)
                        {
                            State = "GoRestaurant";
                        }
                        break;
                    case "Afternoon":
                        if (!done)
                        {
                            State = "GoHospital";
                        }
                        break;
                    case "Evening":
                        if (!done)
                        {
                            State = "GoHome";
                        }
                        break;

                }
            }
            else
            {
                if (!done)
                {
                    State = "GoHome";
                }
            }
        }


        //-----------------------Health
        if (Health == "Infection")
        {
            if (gameObject.GetComponent<MeshRenderer>().material.color != Color.yellow)
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                manager.NInfection++;
                //manager.NHealthy--;
            }

        }
        if (Health == "Sick")
        {
            manager.safe[HomeNumber - 1] = false;
            if (gameObject.GetComponent<MeshRenderer>().material.color != Color.red)
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                manager.NSick++;
                
                manager.NInfection--;
            }
        }
        if (Health == "Healthy")
        {
            if (gameObject.GetComponent<MeshRenderer>().material.color != Color.black)
            {
                gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
                manager.NSick--;
                manager.safe[HomeNumber - 1] = true;
                //gameObject.GetComponent<Infection>().enabled = false;
                //manager.NHealthy++;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Company" || other.tag == "Home" || other.tag == "Hospital" || other.tag == "Restaurant")
        {
            done = true;
            State = "Idle";
            Building = other.tag;
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Company" || other.tag == "Home" || other.tag == "Hospital" || other.tag == "Restaurant")
        {
            Building = null;
        }
    }
}