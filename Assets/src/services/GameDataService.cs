using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDataService : IGameDataService {

	private Dictionary<string, float> commonData = new Dictionary<string, float>() {
		{"CellCountWidth",10},
		{"CellCountHeight",10},
		{"CellSize",1}

	};

	public float GetCommonData(string key) {
		return commonData[key];
	}

}
