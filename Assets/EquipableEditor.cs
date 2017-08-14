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
        armor,
        levelRequired;

    private void OnEnable()
    {
        equipment = serializedObject.FindProperty("equipment");
        aKind = serializedObject.FindProperty("aKind");
        weapon = serializedObject.FindProperty("weapon");
        kind = serializedObject.FindProperty("kind");
        rarity = serializedObject.FindProperty("rarity");
        damage = serializedObject.FindProperty("damage");
        atkSpeed = serializedObject.FindProperty("atkSpeed");
        armor = serializedObject.FindProperty("armor");
        levelRequired = serializedObject.FindProperty("levelRequired");
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

                EditorGUILayout.PropertyField(armor, new GUIContent("armor"));
                EditorGUILayout.PropertyField(levelRequired, new GUIContent("levelRequired"));
                break;

            case Equipable.EquipmentType.Weapon:
                EditorGUILayout.PropertyField(rarity, new GUIContent("rarity"));
                EditorGUILayout.PropertyField(weapon, new GUIContent("weapon"));
                EditorGUILayout.PropertyField(kind, new GUIContent("kind"));

                EditorGUILayout.PropertyField(damage, new GUIContent("damage"));
                EditorGUILayout.PropertyField(atkSpeed, new GUIContent("atkSpeed"));
                EditorGUILayout.PropertyField(levelRequired, new GUIContent("levelRequired"));

                break;
                
        }

        serializedObject.ApplyModifiedProperties();
    }
}
