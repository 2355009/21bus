using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

public class viewTime : MonoBehaviour
{
    List<TimeSpan> times = new List<TimeSpan>();
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/Script/Data/sorttime.csv");
        text = GetComponent<Text>();
        bool notEmpty = true;
        while (notEmpty)
        {
            string data = sr.ReadLine();
            if (data == null)
            {
                notEmpty = !notEmpty;
                break;
            }
            var nowdata = data.Split(":");
            times.Add(new TimeSpan(int.Parse(nowdata[0]), int.Parse(nowdata[1]), 0));
        }
        print(times[times.Count - 1]);
    }

    // Update is called once per frame
    void Update()
    {
        DateTime nowtime = DateTime.Now;
        TimeSpan now = new TimeSpan(nowtime.Hour, nowtime.Minute, nowtime.Second);
        TimeSpan nexttime;
        bool over = now > times[times.Count - 1];
        if (over)
            nexttime = times[0];
        else
            nexttime = times.Find(time => time > now);
        string result = $"버스가 다음에 올 시간 : {printTime(nexttime)}\n";
        if (over)
            nexttime += new TimeSpan(24, 00, 00) - now;
        result += $"남은 시간 : {printTime(nexttime-now)} {((nexttime-now).Seconds)}초";
        text.text=result;

    }
    String printTime(TimeSpan time)
    {
        return string.Format("{0:D2}", time.Hours) + "시 " + string.Format("{0:D2}", time.Minutes) + "분";
    }
}
