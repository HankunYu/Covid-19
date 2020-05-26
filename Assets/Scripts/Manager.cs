using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    public GameObject PStudent, PWorker, PDoctor;
    public Transform Hospital, Company, School;
    public float Duration = 20;
    public bool run;
    public Text NowText, DayCount;
    public int daycount = 0;
    [Range(1f, 4f)]
    public float TimeSpeed = 1;
    float timer;
    public string Now = "Morning";
    public Slider slider;
    public int NStudent, NWorker, NDoctor, NHealthy, NInfection, NSick, NDeath;
    public Text NumberS, NumberW, NumberD, Total, Healthy, Infection, Sick, ChanceOfInfection, Death;
    public float infectionChance;
    public GameObject Report;
    public Text UI1, UI2, UI3;
    bool mask;
    public bool quarantine;
    public bool[] safe ; 

    // Start is called before the first frame update
    void Start()
    {
        timer = Duration;
        GameObject.Find("Student(Clone)").tag = "Infection";
     
    }

    // Update is called once per frame
    void Update()
    {
        if(NSick ==0&& NInfection == 0 && daycount >1)
        {
            run = false;
            Report.SetActive(true);
            UI1.text = "Duration: " + daycount.ToString() + "Days";
            UI2.text = "Death: " + NDeath;
            if (NHealthy > 0)
            {
                UI3.text = "the Virus was eliminated";
            }
            else
            {
                UI3.text = "Virus Kill All People.";
            }
        }
        //time run
        NowText.text = Now;
        DayCount.text = "Day: " + daycount.ToString()  ;
        NumberS.text = "Student: " + NStudent;
        NumberW.text = "Worker: " + NWorker;
        NumberD.text = "Doctor: " + NDoctor;
        Total.text = "Total: " + (NStudent + NWorker + NDoctor).ToString();

        NHealthy = NStudent + NWorker + NDoctor - NSick - NInfection - NDeath;
        Healthy.text = "Healthy: " + NHealthy;
        Infection.text = "Infection: " + NInfection;
        Sick.text = "Sick: " + NSick;
        Death.text = "Death: " + NDeath;
        ChanceOfInfection.text = "Chance of Infection: "+ infectionChance.ToString();
        if (run)
        {
            Time.timeScale = TimeSpeed;
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                switch (Now)
                {
                    case "Morning":
                        Now = "Noon";
                        break;
                    case "Noon":
                        Now = "Afternoon";
                        break;
                    case "Afternoon":
                        Now = "Evening";
                        break;
                    case "Evening":
                        Now = "Morning";
                        daycount++;
                        break;
                }
                timer = Duration;
            }
        }
        else
        {
            Time.timeScale = 0;
        }
        if (mask)
        {
            infectionChance = 0.0001f;
        }
        else
        {
            infectionChance = 0.001f;
        }
    }

    public void Run()
    {
        run = true;
    }
    public void Mask()
    {
        mask = true; 
    }
    public void Maskoff()
    {
        mask = false;
    }
    public void Pause()
    {
        run = false;
    }
    public void ChangeTimeScale()
    {
        TimeSpeed = slider.value;
    }
    public void restart()
    {
        SceneManager.LoadScene(0);
    }
    public void StartQ()
    {
        quarantine = true;
    }
    public void StopQ()
    {
        quarantine = true;
    }
}
