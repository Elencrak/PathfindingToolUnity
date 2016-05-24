using UnityEngine;
using System.Collections;

public class Task :NodeTree {
    
    public delegate bool MyDelegate();
    MyDelegate myDelegate;

    public Task(MyDelegate del)
    {
        myDelegate += del;
    }

    public override bool execute()
    {
        return myDelegate();
    }
}
