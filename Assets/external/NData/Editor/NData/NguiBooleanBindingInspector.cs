using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NguiBooleanBinding), true)]
[CanEditMultipleObjects]
public class NguiBooleanBindingInspector : Editor {
    private Type type;
    private String typeName;

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if (targets.Length > 1) return;
        var binding = (NguiBooleanBinding)target;
        if (binding.CheckType == NguiBooleanBinding.CHECK_TYPE.ENUM) {
            EditorGUILayout.Space();

            DrawEnumBindingFields(binding);
        } else {
            binding.EnumType = "";
            if (binding.EnumValues != null) {
                binding.EnumValues.Clear();
            }
        }
    }

    private void DrawEnumBindingFields(NguiBooleanBinding binding) {
        if (String.IsNullOrEmpty(typeName)) {
            type = GetType(binding.EnumType);
            if (type != null) {
                if (GetTypeByName(type.Name).Length > 1) {
                    typeName = type.FullName;
                } else {
                    typeName = type.Name;
                }
            } 
        }

        typeName = EditorGUILayout.TextField("Type", typeName);

        if (type == null || (type != null && type.Name != typeName && type.FullName != typeName)) {
            if (!String.IsNullOrEmpty(typeName)) {
                if (typeName.Contains(".")) {
                    type = GetType(typeName);
                    if (type == null) {
                        ShowWarning("Type not found");
                    } else {
                        binding.EnumType = type.FullName;
                    }
                } else {
                    Type[] types = GetTypeByName(typeName);
                    if (types.Length == 0) {
                        ShowWarning("Type not found");
                        type = null;
                    } else if (types.Length > 1) {
                        ShowWarning("There are more than 1 type");
                        type = null;
                    } else {
                        type = types[0];
                        binding.EnumType = type.FullName;
                    }
                }
            }
        }

        if (type != null) {
            EditorGUILayout.BeginVertical("Box");
            var enumValues = Enum.GetValues(type);
            foreach (var enumValue in enumValues) {
                String valueName = enumValue.ToString();
                bool oldValue = binding.EnumValues.Contains((int) enumValue);
                bool newValue = EditorGUILayout.Toggle(valueName, oldValue);

                if (newValue != oldValue) {
                    if (newValue) {
                        binding.EnumValues.Add((int) enumValue);
                    } else {
                        binding.EnumValues.Remove((int) enumValue);
                    }
                }
            }
            EditorGUILayout.EndVertical();
        } else {
            binding.EnumValues.Clear();
        }
    }

    private void ShowWarning(String message) {
        GUI.color = Color.yellow;
        EditorGUILayout.HelpBox(message, MessageType.Warning, true);
        GUI.color = Color.white;
    }

    private static Type[] GetTypeByName(String name) {
        List<Type> returnVal = new List<Type>();

        foreach (Assembly a in AppDomain.CurrentDomain.GetAssemblies()) {
            Type[] assemblyTypes = a.GetTypes();
            for (int j = 0; j < assemblyTypes.Length; j++) {
                if (assemblyTypes[j].Name == name && assemblyTypes[j].IsEnum) {
                    returnVal.Add(assemblyTypes[j]);
                }
            }
        }

        return returnVal.ToArray();
    }

    private static Type GetType(String name) {
        if (String.IsNullOrEmpty(name)) return null;

        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assembly in assemblies) {
            var type = assembly.GetType(name);
            if (type != null && type.IsEnum) return type;
        }
        return null;
    }
}
