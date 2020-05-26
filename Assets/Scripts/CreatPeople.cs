using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatPeople : MonoBehaviour
{
    public GameObject student, worker, doctor;
    public int Nstudent, Nworker, Ndoctor;
    public Manager manager;
    public int MaxS, MaxW, MaxD;
    public int HomeNumber;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        Nstudent = Random.Range(1, MaxS);
        Nworker = Random.Range(2, MaxW);
        Ndoctor = Random.Range(0, MaxD);
        manager.NStudent += Nstudent;
        manager.NWorker += Nworker;
        manager.NDoctor += Ndoctor;
        for (int i = Nstudent; i > 0; i--)
        {
            GameObject prefab;
            prefab = Instantiate(student, transform.position,transform.rotation);
            prefab.GetComponent<Actions_Student>().Home = transform;
            prefab.GetComponent<Actions_Student>().HomeNumber = HomeNumber;
        }
        for (int i = Nworker; i > 0; i--)
        {
            GameObject prefab;
            prefab = Instantiate(worker, transform.position, transform.rotation);
            prefab.GetComponent<Actions_Worker>().Home = transform;
            prefab.GetComponent<Actions_Worker>().HomeNumber = HomeNumber;
        }
        for (int i = Ndoctor; i > 0; i--)
        {
            GameObject prefab;
            prefab = Instantiate(doctor, transform.position, transform.rotation);
            prefab.GetComponent<Actions_Doctor>().Home = transform;
            prefab.GetComponent<Actions_Doctor>().HomeNumber = HomeNumber;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
