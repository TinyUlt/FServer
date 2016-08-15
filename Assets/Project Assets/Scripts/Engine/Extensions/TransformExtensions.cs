using UnityEngine;
using System.Collections;
/*
 * 描述：
 *  Transform的一些方法
 * 
 * 目标：
 *  世界坐标重置
 *  相对坐标重置
 * 
 * 
 */
public static class TransformExtensions
{
	public static void Reset(this Transform t)
	{
		t.localPosition = Vector3.zero;
		t.localRotation = Quaternion.identity;
		t.localScale = new Vector3(1, 1, 1);
	}
	
	public static void ResetToParent(this Transform t, GameObject aParent)
	{
		t.parent = aParent.transform;
		t.localPosition = Vector3.zero;
		t.localRotation = Quaternion.identity;
		t.localScale = new Vector3(1, 1, 1);
	}
	
	public static void ResetExcludeScale(this Transform t)
	{
		t.position = Vector3.zero;
		t.localRotation = Quaternion.identity;
	}

}
