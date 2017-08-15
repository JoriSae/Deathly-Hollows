using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : MonoBehaviour {

    public EquipmentType equipment;
    public ArmorKind aKind;
    public WeaponType weapon;
    public WeaponKind kind;
    public Rarity rarity;

    public float damage = 20;
    public float atkSpeed = 1;
    public float armor = 1;

    public int levelRequired = 10;



    void Start()
    {
       
    }

    public void OnCreation()
    {
        damage = Random.Range(10, 50);
        atkSpeed = Random.Range(0, 2);

        equipment = GetRandomEnum<EquipmentType>();
        if (equipment == EquipmentType.Weapon)
        {
            weapon = GetRandomEnum<WeaponType>();
            kind = GetRandomEnum<WeaponKind>();
        }

        if (equipment == EquipmentType.Armor)
        {
            weapon = GetRandomEnum<WeaponType>();
            aKind = GetRandomEnum<ArmorKind>();
        }
        rarity = GetRandomEnum<Rarity>();
    }

    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    public enum EquipmentType
    {
        Weapon,
        Armor
    }

    public enum WeaponType
    {
        Melee,
        Ranged,
        Magic
    }

    public enum WeaponKind
    {
        Sword,
        Axe,
        Bow,
        Wand,
        Staff
    }

    public enum Rarity
    {
        Common,
        UnCommon,
        Rare,
        UberRare
    }

    public enum ArmorKind
    {
        Chest,
        Legs,
        Head,
        Arms,
        Boots
    }

}
