﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pets.API.Events
{
    public class EventHandler<T>
    {
        //private List<Action<T>> Actions { get; } = new List<Action<T>>();

        //public void Register(Action<T> callback)
        //{
        //    if (Actions.Exists(a => a.Method == callback.Method))
        //    {
        //        return;
        //    }

        //    Actions.Add(callback);
        //}

        //public void Publish(T args)
        //{
        //    foreach (Action<T> item in Actions)
        //    {
        //        item.Invoke(args);
        //    }
        //}
    }
}
