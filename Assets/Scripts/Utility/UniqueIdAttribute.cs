using UnityEngine;

namespace Utility {
/// <summary>
/// Marks a string field on an asset as an id that must be unique among all assets of the
/// class that declares the field. The Inspector refuses an id another asset already uses, and warns when
/// the current id is empty or duplicated 
/// </summary>
public class UniqueIdAttribute : PropertyAttribute { }
}
