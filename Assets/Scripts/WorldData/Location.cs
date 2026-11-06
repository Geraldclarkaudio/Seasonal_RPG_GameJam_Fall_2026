using UnityEngine;
using Utility;

namespace WorldData {
[CreateAssetMenu(fileName = "Location", menuName = "World/Location")]
public class Location : ScriptableObject {
	[UniqueId] [SerializeField] string id;
	[Tooltip("Name shown to the player. Falls back to the asset name when empty")]
	[SerializeField] string displayName;
	[SerializeField] [TextArea] string description;

	public string Id => id;
	public string DisplayName => string.IsNullOrWhiteSpace(displayName) ? name : displayName;
	public string Description => description;
}
}
