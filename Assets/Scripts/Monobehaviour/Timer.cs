using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLimit;

    private float myTime;

    public float MyTime
    {
        get => myTime;
        set
        {
            myTime = value;
            OnTimeChange?.Invoke(myTime);
        }
    }
    
    public event Action<float>  OnTimeChange;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MyTime = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        MyTime -= Time.deltaTime;
    }
}
