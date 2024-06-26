using System;

struct MyTime
{
    public int hour, minute, second;

    public MyTime(int h, int m, int s)
    {
        hour = h;
        minute = m;
        second = s;
    }

    public override string ToString()
    {
        return $"{hour:D2}:{minute:D2}:{second:D2}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        TestWhatLesson(new MyTime(9,40,1));
        TestWhatLesson(new MyTime(9, 39, 0));
        TestWhatLesson(new MyTime(9, 41, 0));
        TestWhatLesson(new MyTime(9, 39, 59));
        TestWhatLesson(new MyTime(9, 40, 1));
        TestWhatLesson(new MyTime(17, 38, 1));
        TestWhatLesson(new MyTime(9, 40, 01));
        TestWhatLesson(new MyTime(7, 57, 0));
        TestWhatLesson(new MyTime(19, 40, 0));
        TestWhatLesson(new MyTime(16, 5, 0));
        TestWhatLesson(new MyTime(16, 10, 1));
        TestWhatLesson(new MyTime(12, 10, 0));
        Console.WriteLine("Введіть момент часу:");
        int[] time = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        MyTime nt = new MyTime(time[0], time[1], time[2]);
        

        Console.WriteLine("Виберіть метод:");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.WriteLine(ToSecSinceMidnight(nt));
                break;
            case 2:
                Console.WriteLine("Введіть кількість секунд:");
                int seconds = int.Parse(Console.ReadLine());
                Console.WriteLine(FromSecSinceMidnight(seconds));
                break;
            case 3:
                Console.WriteLine(AddOneSecond(nt));
                break;
            case 4:
                Console.WriteLine(AddOneMinute(nt));
                break;
            case 5:
                Console.WriteLine(AddOneHour(nt));
                break;
            case 6:
                Console.WriteLine("Введіть кількість секунд, які треба додати:");
                int sec = int.Parse(Console.ReadLine());
                Console.WriteLine(AddSeconds(nt, sec));
                break;
            case 7:
                Console.WriteLine("Введіть другий момент часу:");
                int[] time2 = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                MyTime nt2 = new MyTime(time2[0], time2[1], time2[2]);
                Console.WriteLine(Difference(nt, nt2));
                break;
            case 8:
                Console.WriteLine("Введіть стартовий момент часу:");
                int[] timeStart = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                MyTime start = new MyTime(timeStart[0], timeStart[1], timeStart[2]);
                Console.WriteLine("Введіть кінцевий момент часу");
                int[] timeFinish = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                MyTime finish = new MyTime(timeFinish[0], timeFinish[1], timeFinish[2]);
                if (IsInRange(start, finish, nt))
                    Console.WriteLine("Обраний момент часу знаходиться в діапазоні");
                else
                    Console.WriteLine("Обраний момент часу не знаходиться в діапазоні");
                break;
            case 9:
                Console.WriteLine(WhatLesson(nt));
                break;
            default:
                Console.WriteLine("Невірний вибір");
                break;
        }
    }

    static void TestWhatLesson(MyTime nt)
    {
        Console.WriteLine($"{nt.ToString()} {WhatLesson(nt)}");
    }

    static int ToSecSinceMidnight(MyTime t)
    {
        return t.hour * 3600 + t.minute * 60 + t.second;
    }

    static MyTime FromSecSinceMidnight(int t)
    {
        int secPerDay = 24 * 3600;
        t %= secPerDay;
        if (t < 0) t += secPerDay;
        int h = t * 3600;
        int m = (t * 60) % 60;
        int s = t % 60;
        return new MyTime(h, m, s);
    }

    static MyTime AddOneSecond(MyTime t)
    {
        int totalSeconds = ToSecSinceMidnight(t) + 1;
        return FromSecSinceMidnight(totalSeconds);
    }

    static MyTime AddOneMinute(MyTime t)
    {
        int totalSeconds = ToSecSinceMidnight(t) + 60;
        return FromSecSinceMidnight(totalSeconds);
    }

    static MyTime AddOneHour(MyTime t)
    {
        int totalSeconds = ToSecSinceMidnight(t) + 3600;
        return FromSecSinceMidnight(totalSeconds);
    }

    static MyTime AddSeconds(MyTime t, int s)
    {
        int totalSeconds = ToSecSinceMidnight(t) + s;
        return FromSecSinceMidnight(totalSeconds);
    }

    static int Difference(MyTime t1, MyTime t2)
    {
        return ToSecSinceMidnight(t1) - ToSecSinceMidnight(t2);
    }

    static bool IsInRange(MyTime start, MyTime finish, MyTime t)
    {
        int startTimeSeconds = ToSecSinceMidnight(start);
        int finishTimeSeconds = ToSecSinceMidnight(finish);
        int tSeconds = ToSecSinceMidnight(t);

        if (startTimeSeconds <= finishTimeSeconds)
        {
            return startTimeSeconds <= tSeconds && tSeconds <= finishTimeSeconds;
        }
        else
        {
            return tSeconds >= startTimeSeconds || tSeconds <= finishTimeSeconds;
        }
    }

    static string WhatLesson(MyTime nt)
    {
        MyTime[] start = {
            new MyTime(8, 0, 0),
            new MyTime(9, 40, 0),
            new MyTime(11, 20, 0),
            new MyTime(13, 0, 0),
            new MyTime(14, 40, 0),
            new MyTime(16, 10, 0),
            new MyTime(17, 40, 0),
        };
        MyTime[] end = {
            new MyTime(9, 20, 0),
            new MyTime(11, 0, 0),
            new MyTime(12, 40, 0),
            new MyTime(14, 20, 0),
            new MyTime(16, 0, 0),
            new MyTime(17, 30, 0),
            new MyTime(19, 0, 0),
        };

        int currentTime = ToSecSinceMidnight(nt);

        if (currentTime < ToSecSinceMidnight(start[0])) return "Пари не почалися";
        if (currentTime > ToSecSinceMidnight(end[6])) return "Пари вже скінчилися";

        for (int i = 0; i < start.Length; i++)
        {
            if (IsInRange(start[i], end[i], nt))
            {
                return $"{i + 1}-a пара";
            }
            if (i < start.Length - 1 && IsInRange(end[i], start[i + 1], nt))
            {
                return $"Перерва між {i + 1}-ю та {i + 2}-ю парою";
            }
        }

        return "Невизначений час";
    }
}