using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Timer 
{
    static async Task<bool> TimerCounter(float counttime)
    {
        float time = 0f;
        do
        {
            
            time += Time.deltaTime;
        } while (time <= counttime);

        return true;
    }
    
}
