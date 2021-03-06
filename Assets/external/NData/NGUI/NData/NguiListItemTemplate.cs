using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[AddComponentMenu("NGUI/NData/List Item Template")]
public class NguiListItemTemplate : MonoBehaviour
{
	public GameObject Template;
	public UIDraggableCamera DraggableCamera;
	
	public virtual GameObject Instantiate(EZData.Context itemContext, int index)
	{
		if (Template == null)
		{
			return null;
		}
		
		GameObject instance = (GameObject)Instantiate(Template);
		
		var subTemplates = instance.GetComponentsInChildren<NguiListItemTemplate>();
		foreach (var st in subTemplates)
		{
			if (st.Template == instance)
				st.Template = Template;
		}
		
		foreach(UIDragCamera cd in instance.GetComponentsInChildren<UIDragCamera>())
		{
			if (cd.draggableCamera == null && DraggableCamera != null)
				cd.draggableCamera = DraggableCamera;
		}
		var itemData = instance.AddComponent<NguiItemDataContext>();
		
		instance.transform.parent = transform;
        //Debug.LogError("SetContext " + itemContext.GetType().Name);
		itemData.SetContext(itemContext);
		instance.transform.parent = null;
		
		itemData.SetIndex(index);
			
		return instance;
	}
}
