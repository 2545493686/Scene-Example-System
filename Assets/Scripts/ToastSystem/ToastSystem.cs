using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastSystem : MonoBehaviour
{
    public Toast toast;
    static ToastSystem intance;

    private void Awake()
    {
        intance = this;
    }

    public static void Show(string text)
    {
        intance.toast.Show(text);
    }
}
