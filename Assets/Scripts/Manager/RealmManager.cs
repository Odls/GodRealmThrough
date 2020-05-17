using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RealmManager{
	static Dictionary<int, Realm> realmList = new Dictionary<int, Realm>();

	/// <summary> 加入神界物件 </summary>
	internal static void AddRealm(Realm p_realm) {
		realmList.Add(p_realm.GetInstanceID(), p_realm);
	}
	/// <summary> 移除神界物件 </summary>
	internal static void RemoveRealm(Realm p_realm) {
		realmList.Remove(p_realm.GetInstanceID());
	}
	/// <summary> 指定座標是否在任一神界之內 </summary>
	/// <param name="p_pos">座標</param>
	public static bool IsInRealm(Vector2 p_pos) {
		foreach(Realm _realm in realmList.Values) {
			if (_realm.IsInRealm(p_pos)) {
				return true;
			}
		}
		return false;
	}
}
