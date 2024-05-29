using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class CreatorOfMedicineChest : MonoBehaviour
{
    [SerializeField] private int _medicineChestCount;
    [SerializeField] private List<MedicineChest> _medicineChests;

    private void Awake()
    {
        if (_medicineChestCount <= _medicineChests.Count)
        {
            while (_medicineChestCount > 0)
            {
                int index = Random.Range(0, _medicineChests.Count);
                _medicineChests[index].gameObject.SetActive(true);
                _medicineChests.RemoveAt(index);
                _medicineChestCount--;
            }
        }
    }
}