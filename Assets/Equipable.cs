using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipable : MonoBehaviour
{
    public WeaponType weapon;
    public WeaponKind kind;
    public Rarity rarity;

    public float damage = 20;
    [Range(0, 2)]
    public float atkSpeed = 1;

    public int levelRequired = 1;



    void Start()
    {

    }

    public void OnCreation(WeaponType type)
    {
        damage = Random.Range(1 + Player.instance.Level, 5 + Player.instance.Level);
        atkSpeed = Random.Range(0, 2);

        weapon = type;

        if (type == WeaponType.Ranged)
        {
            kind = WeaponKind.Bow;
        }

        if (type == WeaponType.Melee)
        {
            kind = WeaponKind.Sword;
        }

        rarity = GetRandomEnum<Rarity>();

        levelRequired = Player.instance.Level;
    }

    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    public enum WeaponType
    {
        Melee,
        Ranged
    }

    public enum WeaponKind
    {
        Sword,
        Bow
    }

    public enum Rarity
    {
        Common,
        UnCommon,
        Rare,
        UberRare
    }
}
