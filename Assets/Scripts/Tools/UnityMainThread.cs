using System;
using System.Collections.Generic;
using UnityEngine;

public class UnityMainThread : MonoBehaviour
{
    static Queue<Action> jobs = new Queue<Action>();

    void Update()
    {
        while (jobs.Count > 0)
            jobs.Dequeue().Invoke();
    }

    public static void AddJob(Action newJob)
    {
        jobs.Enqueue(newJob);
    }
}