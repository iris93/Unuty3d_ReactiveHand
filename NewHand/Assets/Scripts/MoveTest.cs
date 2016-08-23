using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using LoadData;

using System.Runtime.InteropServices;

public class MoveTest : MonoBehaviour 
{

	//是否开始插值旋转
	bool isRotation = false;
	public int iter0 = 0;
	public int iter1 = 0;
	// public float timeParameter;
	//pitch:俯仰角,roll:横滚角,yaw:航向角
	float pitch = 0.0f ;
	float roll = 0.0f;
	float yaw = 0.0f;
	// 储存从文件读取的角度参数
	ArrayList paramList= new ArrayList();
	LoadStaticData myLoadStaticData = new LoadStaticData();
	void Start(){
		ArrayList info = myLoadStaticData.LoadFile(Application.dataPath,"txtfolder//angle.txt");
		paramList = myLoadStaticData.SplitStaticData(info);
	}
	void OnGUI()
	{
		GUI.backgroundColor = Color.red;
		if(GUI.Button(new Rect(100,10,90,30),"旋转固定角度"))
		{
			gameObject.transform.rotation = Quaternion.Euler(0.0f,-30.0f,0.0f);
		}

		if(GUI.Button(new Rect(200,10,90,30),"插值旋转"))
		{
			isRotation = true;
		}

	}
	

	void Update()
	{
		//开始差值旋转
		if(isRotation)
		{
			foreach(float[] item in paramList)
			{
				// Debug.Log(item[0]);
				// pitch:俯仰角,roll:横滚角,yaw:航向角
				pitch = item[0]*10;
				roll = item[1];
				yaw = item[2]; 
				//gameObject.transform.rotation = Quaternion.Euler(pitch,roll,yaw);
				gameObject.transform.rotation = Quaternion.Slerp (gameObject.transform.rotation, Quaternion.Euler(pitch,roll,yaw), Time.time*0.01f);
				// gameObject.transform.rotation = Quaternion.Euler(pitch,roll,yaw);
				// Debug.Log(pitch);  
				// iter0++;
			}
		}
		iter1++;
	}

}