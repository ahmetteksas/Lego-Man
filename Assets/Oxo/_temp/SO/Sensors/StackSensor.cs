using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackSensor : MonoBehaviour
{
    [SerializeField]
    HealthSensorSettings settings;

    public GameObjectCollection relatedList;
    public FloatReference currentCount;

    float tempCount;

    private int currentGear;

    private void Awake()
    {
        if (settings == null)
            Debug.LogError("You need a settings for use this.");

        //Initialize
        //stackCollectable.Value = settings.startCount;
    }

    public void OnStackChange()
    {
        currentGear = SelectGear();

        if (tempCount - currentCount.Value > 0)
            OnStackDecrease();
        else
            OnStackIncrease();

        tempCount = currentCount.Value;

        StageChange();
    }

    void OnStackDecrease()
    {
        ReArrangeFollowers();
    }

    void OnStackIncrease()
    {

    }

    void StageChange()
    {
        return;
        settings.PlayerStageChange?.Raise(settings.stageList[currentGear]);
    }

    private int SelectGear()
    {
        int _currentGear = 0;

        foreach (int gear in settings.gearValueList)
            if (gear < relatedList.Count)
                _currentGear = settings.gearValueList.IndexOf(gear) + 1;

        return _currentGear;
    }

    public void ReArrangeFollowers()
    {
        //relatedList = relatedList.OrderByDescending(x => x.transform.position.z).ToList();
        //foreach (var stack in relatedList)
        //{
        //    if (stack == relatedList.FirstOrDefault())
        //    {
        //        stack.GetComponent<CanFollow>().SetTarget(transform);
        //    }
        //    else
        //    {
        //        stack.GetComponent<CanFollow>().SetTarget(relatedList[relatedList.IndexOf(stack) - 1].transform);
        //    }
        //}
    }
}
