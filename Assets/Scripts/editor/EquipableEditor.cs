using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Equipable)), CanEditMultipleObjects]
public class EquipableEditor : Editor
{

    public SerializedProperty
        weapon,
        kind,
        rarity,
        damage,
        atkSpeed,
        levelRequired;

    private void OnEnable()
    {
        weapon = serializedObject.FindProperty("weapon");
        kind = serializedObject.FindProperty("kind");
        rarity = serializedObject.FindProperty("rarity");
        damage = serializedObject.FindProperty("damage");
        atkSpeed = serializedObject.FindProperty("atkSpeed");
        levelRequired = serializedObject.FindProperty("levelRequired");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(weapon);

        Equipable.WeaponType et = (Equipable.WeaponType)weapon.enumValueIndex;

        switch (et)
        {
            case Equipable.WeaponType.Melee:
                EditorGUILayout.PropertyField(rarity, new GUIContent("rarity"));
                EditorGUILayout.PropertyField(kind, new GUIContent("kind"));

                EditorGUILayout.PropertyField(damage, new GUIContent("damage"));
                EditorGUILayout.PropertyField(atkSpeed, new GUIContent("atkSpeed"));
                EditorGUILayout.PropertyField(levelRequired, new GUIContent("levelRequired"));

                break;

            case Equipable.WeaponType.Ranged:
                EditorGUILayout.PropertyField(rarity, new GUIContent("rarity"));
                EditorGUILayout.PropertyField(kind, new GUIContent("kind"));

                EditorGUILayout.PropertyField(damage, new GUIContent("damage"));
                EditorGUILayout.PropertyField(atkSpeed, new GUIContent("atkSpeed"));
                EditorGUILayout.PropertyField(levelRequired, new GUIContent("levelRequired"));

                break;
                
        }

        serializedObject.ApplyModifiedProperties();
    }
}
