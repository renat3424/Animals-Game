using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stages : MonoBehaviour
{
    // Start is called before the first frame update
    List<WaveSettings> stages;
    int currentStage;
    int stagesCount;
    public Stages()
    {
        currentStage = -1;
        stages = new List<WaveSettings>() { 
            //type, count, hp, speed
            new WaveSettings(1, 15), 
            new WaveSettings(1, 20, 750), 
            new WaveSettings(1, 25, 750, 1.4f), 
            new WaveSettings(2, 15), 
            new WaveSettings(2, 20, 1400), 
            new WaveSettings(2, 25, 1400, 1.6f), 
            new WaveSettings(3, 15), 
            new WaveSettings(3, 20, 2050),
            new WaveSettings(3, 25, 2050, 2.0f) };
        stagesCount = stages.Count;
    }
    public WaveSettings NextStage()
    {
        if (currentStage != stagesCount - 1)
        {
            currentStage++;
            return stages[currentStage];           
        }
        else
        {
            return new WaveSettings(1, 0);
        }
    }
}
