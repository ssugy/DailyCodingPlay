using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
//using static UnityEditor.PlayerSettings;
//using UnityEngine.WSA;

public class SampleUniTask : MonoBehaviour
{
    #region �⺻ Ŭ���� ����
    internal class Bacon { }
    internal class Coffee { }
    internal class Egg { }
    internal class Juice { }
    internal class Toast { }

    private static Juice PourOJ()
    {
        Debug.Log("Pouring orange juice");
        return new Juice();
    }

    private static void ApplyJam(Toast toast) =>
        Debug.Log("Putting jam on the toast");

    private static void ApplyButter(Toast toast) =>
        Debug.Log("Putting butter on the toast");

    private static Toast ToastBread(int slices)
    {
        for (int slice = 0; slice < slices; slice++)
        {
            Debug.Log("Putting a slice of bread in the toaster");
        }
        Debug.Log("Start toasting...");
        Task.Delay(3000).Wait();
        Debug.Log("Remove toast from toaster");

        return new Toast();
    }

    private static Bacon FryBacon(int slices)
    {
        Debug.Log($"putting {slices} slices of bacon in the pan");
        Debug.Log("cooking first side of bacon...");
        Task.Delay(3000).Wait();
        for (int slice = 0; slice < slices; slice++)
        {
            Debug.Log("flipping a slice of bacon");
        }
        Debug.Log("cooking the second side of bacon...");
        Task.Delay(3000).Wait();
        Debug.Log("Put bacon on plate");

        return new Bacon();
    }

    private static Egg FryEggs(int howMany)
    {
        Debug.Log("Warming the egg pan...");
        Task.Delay(3000).Wait();
        Debug.Log($"cracking {howMany} eggs");
        Debug.Log("cooking the eggs ...");
        Task.Delay(3000).Wait();
        Debug.Log("Put eggs on plate");

        return new Egg();
    }

    private static Coffee PourCoffee()
    {
        Debug.Log("Pouring coffee");
        return new Coffee();
    }
#endregion

    // �Ϲ� Task�� ���ٴ� ���� voidó�� ���ϰ��� ���ٴ� �ǹ��̴�.
    // ���ϰ��� �����Ѵ� ��� Task<����Ÿ��> �̷������� ����Ѵ�.
    async Task<int> Sample()
    {
        await Task.Run(() => { });
        return 0;
    }
}
