using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utility.Editor {
[CustomPropertyDrawer(typeof(UniqueIdAttribute))]
public class UniqueIdDrawer : PropertyDrawer {
	const float SPACING = 2f;
	const double CACHE_SECONDS = 1.0;

	// searching every asset on each Inspector repaint is slow, so the warning is cached per asset for a moment
	static readonly Dictionary<Object, (string id, string message, double time)> warningCache = new();

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		float height = EditorGUIUtility.singleLineHeight;
		string warning = GetWarning(property);
		if (warning != null) height += SPACING + HelpBoxHeight(warning);
		return height;
	}

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		Rect fieldRect = new(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

		if (property.propertyType != SerializedPropertyType.String) {
			EditorGUI.LabelField(fieldRect, label.text, "[UniqueId] only works on string fields");
			return;
		}

		EditorGUI.BeginProperty(fieldRect, label, property);
		EditorGUI.BeginChangeCheck();
		// delayed intentionally: only checks once you press Enter or click away, not on every key
		string newId = EditorGUI.DelayedTextField(fieldRect, label, property.stringValue).Trim();
		if (EditorGUI.EndChangeCheck()) {
			TrySetId(property, newId);
		}
		EditorGUI.EndProperty();

		string warning = GetWarning(property);
		if (warning != null) {
			Rect boxRect = new(position.x, fieldRect.yMax + SPACING, position.width, HelpBoxHeight(warning));
			EditorGUI.HelpBox(boxRect, warning, MessageType.Error);
		}
	}

	void TrySetId(SerializedProperty property, string newId) {
		Object target = property.serializedObject.targetObject;

		if (newId.Length > 0 && property.serializedObject.isEditingMultipleObjects) {
			Debug.LogError($"Can't give {property.serializedObject.targetObjects.Length} assets the same id \"{newId}\". Ids must be unique.", target);
			return;
		}

		Object owner = FindOtherAssetWithId(property, newId);
		if (owner) {
			Debug.LogError($"{target.name}: id \"{newId}\" is already used by {owner.name}. The id was not changed.", owner);
			return;
		}

		property.stringValue = newId;
		warningCache.Remove(target);
	}

	string GetWarning(SerializedProperty property) {
		if (property.propertyType != SerializedPropertyType.String) return null;
		if (property.hasMultipleDifferentValues) return null;

		Object target = property.serializedObject.targetObject;
		Object key = target;
		string id = property.stringValue;
		double now = EditorApplication.timeSinceStartup;

		if (warningCache.TryGetValue(key, out var cached) && cached.id == id && now - cached.time < CACHE_SECONDS) {
			return cached.message;
		}

		string message = null;
		if (string.IsNullOrWhiteSpace(id)) {
			message = "Id is empty. Give this asset a unique id.";
		}
		else {
			Object owner = FindOtherAssetWithId(property, id);
			if (owner) message = $"Id \"{id}\" is also used by {owner.name}. Ids must be unique.";
		}

		warningCache[key] = (id, message, now);
		return message;
	}

	/// <summary>
	/// Returns another asset of the same class whose field (same path) has this id, or null if none
	/// </summary>
	Object FindOtherAssetWithId(SerializedProperty property, string id) {
		if (string.IsNullOrEmpty(id)) return null;

		Object self = property.serializedObject.targetObject;
		Type type = GetIdOwnerType(self);

		foreach (string guid in AssetDatabase.FindAssets($"t:{type.Name}")) {
			string path = AssetDatabase.GUIDToAssetPath(guid);
			foreach (Object asset in AssetDatabase.LoadAllAssetsAtPath(path)) {
				if (!asset || asset == self || !type.IsInstanceOfType(asset)) continue;

				using SerializedObject other = new(asset);
				SerializedProperty otherId = other.FindProperty(property.propertyPath);
				if (otherId != null && otherId.propertyType == SerializedPropertyType.String && otherId.stringValue.Trim() == id) {
					return asset;
				}
			}
		}
		return null;
	}

	/// <summary>
	/// Ids are unique per class that declares the field, so subclasses share one set of ids
	/// </summary>
	Type GetIdOwnerType(Object target) {
		Type declaring = fieldInfo.DeclaringType;
		return declaring != null && typeof(Object).IsAssignableFrom(declaring) ? declaring : target.GetType();
	}

	static float HelpBoxHeight(string message) {
		float width = EditorGUIUtility.currentViewWidth - 40f;
		return Mathf.Max(EditorGUIUtility.singleLineHeight * 2f, EditorStyles.helpBox.CalcHeight(new GUIContent(message), width));
	}
}
}
