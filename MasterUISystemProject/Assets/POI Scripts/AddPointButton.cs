﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

//this is attached to the Add bookmark button in POIEdit Window
public class AddPointButton : MonoBehaviour {

	public void onClicked(){
		//create new Point object
		string sFlag = Application.loadedLevelName; //!!!! need to be implemented later when sceneflag is set
		//validate input
		//validate button name
		if(checkButtonNameExist()){
			POI_ReferenceHub.Instance.NameExistedWarning.gameObject.SetActive(true);
		}
		
		if(!validateInput()){
			POI_ReferenceHub.Instance.InvalidInputWarning.gameObject.SetActive(true);
		}


		Vector3 pos = new Vector3 (float.Parse(POI_ReferenceHub.Instance.poiInfoFields [0].text), float.Parse(POI_ReferenceHub.Instance.poiInfoFields [1].text), float.Parse(POI_ReferenceHub.Instance.poiInfoFields [2].text));
		Vector3 rot = new Vector3 (0, float.Parse(POI_ReferenceHub.Instance.poiInfoFields [3].text), 0);
		POI point = new POI (sFlag, POI_ReferenceHub.Instance.poiInfoFields [4].text, pos, rot, POI_GlobalVariables.defaultMarker);
		//generate button and marker pair
		POIButtonManager.instance.GenerateButMarkerPair (point);
		//add the point into the orginalHandler
		POIButtonManager.originalHandler.AddPoint(point);



	}

	bool checkButtonNameExist(){
		string butName = POI_ReferenceHub.Instance.poiInfoFields [4].text;
		foreach (POI point in POIButtonManager.originalHandler.projectPOIs){
			if(point.buttonName == butName){
				return true; //button name existed
			}
		}
		return false; //button name does not exist
	}

	//return false when input not valid
	bool validateInput(){
		for(int i =0; i < 4; i++){ //traverse the poi input fields, 0-4 are pos and rot input fields
			 InputField field = POI_ReferenceHub.Instance.poiInfoFields[i];
			string value = field.text;
			float result; //dummy var for filling in tryparse below
			if(!float.TryParse(value, out result)){
				return false;
			}
		}

		return true;
	}
}
