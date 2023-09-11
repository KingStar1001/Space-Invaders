using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders
{
    public class Utils
    {
        public static void SendMessage(string name, string method, object param)
        {
            GameObject obj = GameObject.Find(name);
            if (obj != null)
            {
                obj.SendMessage(method, param);
            }
        }
    }

}