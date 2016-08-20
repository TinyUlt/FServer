using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ResourceManager {

	private Dictionary<string,Sprite> m_HeadSprites;
	private static ResourceManager m_Instance;
	public static ResourceManager Instance{
		get{ 
			if (m_Instance == null)
				m_Instance = new ResourceManager ();
			return m_Instance;
		}
	}
	private ResourceManager(){}
	public Sprite GetHeadSpriteByName(string name){
		if(m_HeadSprites == null || m_HeadSprites.Count == 0){
//			m_HeadSprites = Resources.LoadAll<Sprite> (DataRule.UI + "ChangePhoto").ToDictionary (c=>c.name);
		}
		return m_HeadSprites [name];
	}
}
