using UnityEngine;

[System.Serializable]
public class BuffInfo
{
    public BuffData buffData;

    public GameObject creator;

    public GameObject target;

    public float durationTimer;

    public float tickTimer;

    public int currentStack;
}
