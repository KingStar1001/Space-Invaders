using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

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

        public static string FormatNumber(long number)
        {
            return number.ToString("N0");
        }

        public static void DestroyChildren(Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.GameObject.Destroy(transform.GetChild(i).gameObject);
            }
            transform.DetachChildren();
        }

        public static string TranslationString(string table, string key)
        {
            var localizedString = new LocalizedString();
            localizedString.TableReference = table;
            localizedString.TableEntryReference = key;
            return localizedString.GetLocalizedString();
        }
    }

}