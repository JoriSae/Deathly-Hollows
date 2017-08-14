using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Equipable)), CanEditMultipleObjects]
public class EquipableEditor : Editor
{

    public SerializedProperty
        equipment,
        aKind,
        weapon,
        kind,
        rarity,
        damage,
        atkSpeed,
        block,
        blockChance;

    private void OnEnable()
    {
        equipment = serializedObject.FindProperty("equipment");
        aKind = serializedObject.FindProperty("aKind");
        weapon = serializedObject.FindProperty("weapon");
        kind = serializedObject.FindProperty("kind");
        rarity = serializedObject.FindProperty("rarity");
        damage = serializedObject.FindProperty("damage");
        atkSpeed = serializedObject.FindProperty("atkSpeed");
        block = serializedObject.FindProperty("block");
        blockChance = serializedObject.FindProperty("blockChance");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(equipment);

        Equipable.EquipmentType et = (Equipable.EquipmentType)equipment.enumValueIndex;

        switch (et)
        {
            case Equipable.EquipmentType.Armor:
                EditorGUILayout.PropertyField(rarity, new GUIContent("rarity"));
                EditorGUILayout.PropertyField(aKind, new GUIContent("aKind"));

                EditorGUILayout.PropertyField(block, new GUIContent("block"));
                EditorGUILayout.PropertyField(blockChance, new GUIContent("blockChance"));
                break;

            case Equipable.EquipmentType.Weapon:
                EditorGUILayout.PropertyField(rarity, new GUIContent("rarity"));
                EditorGUILayout.PropertyField(weapon, new GUIContent("weapon"));
                EditorGUILayout.PropertyField(kind, new GUIContent("kind"));

                EditorGUILayout.PropertyField(damage, new GUIContent("damage"));
                EditorGUILayout.PropertyField(atkSpeed, new GUIContent("atkSpeed"));

                break;
                
        }

        serializedObject.ApplyModifiedProperties();
    }
}
