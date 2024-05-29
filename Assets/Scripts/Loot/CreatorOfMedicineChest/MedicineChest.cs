using System;
using UnityEngine;

public class MedicineChest : Loot
{
    [SerializeField] public int HealingPoints { get; } = 25;
}