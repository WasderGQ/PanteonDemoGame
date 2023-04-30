using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Timer 
{
    public static async Task<bool> TimerCounter(float counttime)
    {
        float time = 0f;
        do
        {
            
            time += Time.deltaTime;
        } while (time <= counttime);

        return true;
    }
    public static async Task<int> TimerCounterToInt(float counttime,int value)
    {
        float time = 0f;
        do
        {
            
            time += Time.deltaTime;
        } while (time <= counttime);

        value = 0;
        return value;
    }
    public static async Task<int> AsyncTimeSetIntToZero(int counttime, int value)
    {

        value = TimerCounterToInt(counttime, value).Result;
        Debug.Log("Timers Up");
        return value;
       


    }

    public static async Task<bool> SetAsyncTimerCounterBoolToDefault()
    {
        return false;
    }
}
