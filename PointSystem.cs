using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSystem 
{
    Text textField;
    float B;
    float points;
    float A;
    float currentDisplayPoints = 0;
    string defaultText;
    float lastX = 0;
    float time;

    public bool done = false;
    public bool numberBase = true;

    public PointSystem (float points, Text text, float denom, string defText)
    {

        this.points = points;
        this.textField = text;
        this.defaultText = defText;

        B = (1f / denom) * Mathf.PI;

        A = DNAMath.calcAmplitude(this.points, B, denom);

      //  Debug.Log("A: " +A);
      //  Debug.Log("B: " + B);

        time = denom;

    }

    public void updateDispPoints (float curTime)
    {
        if (done)
        {

        } else
        {
            if (currentDisplayPoints + DNAMath.sinEq(A, B, 0, 0, curTime) * getDif(curTime) >= points || curTime >= time)
            {
                currentDisplayPoints = points;
                done = true;
            } else
            {
                currentDisplayPoints = currentDisplayPoints + DNAMath.sinEq(A, B, 0, 0, curTime) * getDif(curTime);
            }
           
           
            if (numberBase)
            {
                //Display it onto the text 
                textField.text = defaultText
                    + Mathf.Floor(currentDisplayPoints).ToString();
            } else
            {
                //Convert to time 
                string timeVal = "";

                int hour;
                int min;
                int sec;
                float rem;

                hour = (int)currentDisplayPoints / 3600;
                //- hour * 3600
                rem = currentDisplayPoints % 3600;
                min = (int)rem / 60;
                //- min * 60
                rem = rem % 60;
                sec = (int)rem / 1;
                rem = rem % 1;

                if (hour > 0)
                {
                    //Include hour
                    timeVal = DNAMath.accurateTime(hour) + ":" + DNAMath.accurateTime(min) + ":" + DNAMath.accurateTime(sec) + "." + rem.ToString();

                } else
                {
                    timeVal = DNAMath.accurateTime(min) + ":" + DNAMath.accurateTime(sec) + "." + string.Format("{0:0.00}", rem);
                }

                textField.text = defaultText
                    + timeVal;
            }
            lastX = curTime;
        }
    }

    float getDif (float curTime)
    {
        return curTime - lastX;
    }











}
