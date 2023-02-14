using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region Fields
    // timer duration 
    float totalSeconds = 0;

    // timer execution
    float elapedSeconds = 0;
    bool running = false;

    // support for Finished property
    bool started = false;
    #endregion

    #region Properties
    /// <summary>
    /// Sets the duration of the timer
    /// The duration can only be set if the timer isn't currently running
    /// </summary>
    /// <value>duration</value>
    public float Duration
    {
        set
        {
            if (!running)
            {
                totalSeconds = value;
            }
        }
    }

    /// <summary>
    /// Gets whether or not the timer has finished running
    /// This property returns false if the timer has never been started
    /// </summary>
    /// <value>true if finished; otherwise, false.</value>
    public bool Finished
    {
        get { return started && !running; }
    }

    /// <summary>
    /// Gets whether or not the timer is currently running
    /// </summary>
    /// <value>true if running; otherwise, false.</value>
    public bool Running
    {
        get { return running; }
    }
    #endregion

    #region Methods

    // Update is called once per frame
    void Update()
    {
        // update timer and check for finished
        if (running)
        {
            elapedSeconds += Time.deltaTime;
            if (elapedSeconds >= totalSeconds)
            {
                running = false;
            }
        }
    }
    /// <summary>
    /// Runs the timer
    /// Because a timer of 0 duration doesn't really make sense,
    /// the timer only runs if the total seconds is large than 0
    /// This is also makes sure the consumer of the class has actully 
    /// set the duration to something higher than 0
    /// </summary>
    public void Run()
    {
        if (totalSeconds > 0)
        {
            started = true;
            running = true;
            elapedSeconds = 0;
        }
    }

    #endregion
}
