using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class XLocalize : MonoBehaviour {
	
	public string Key;
	[HideInInspector]
	public string filePath;

	/// <summary>
	/// Calls the LoadKey on Awake
	/// </summary>
	void Awake()
	{
		LoadKey();
	}

	/// <summary>
	/// Enable or disable the component in here.
	/// </summary>
	void Start(){}

	/// <summary>
	/// Loads the text from the localization file and images from resouces folder
	/// </summary>
	public void LoadKey()
	{
		if (this.GetComponent<Text>() != null) 
		{
			this.GetComponent<Text>().text = XLocalization.Get(this.gameObject, Key);
		}
		else if (this.GetComponent<Image>() != null)
		{
			filePath = XLocalization.Get (this.gameObject, "FilePath");
			Sprite tempSprite = Resources.Load<Sprite>(filePath+XLocalization.Get(this.gameObject, Key));
			this.GetComponent<Image>().sprite = tempSprite;
		}
		else if (this.GetComponent<RawImage>() != null) 
		{
			filePath = XLocalization.Get (this.gameObject, "FilePath");
			Texture tempTexture = Resources.Load<Texture>(filePath+XLocalization.Get(this.gameObject, Key));
			this.GetComponent<RawImage>().texture = tempTexture;
		}
	}
}
