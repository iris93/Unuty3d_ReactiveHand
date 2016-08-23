using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO.Ports;
using System.IO;
using System;

//调用获取数据的包
using LoadData;

public class FetchTest : MonoBehaviour 
{
	//是否开始插值旋转
	bool isRotation = false;

	//pitch:俯仰角,roll:横滚角,yaw:航向角
	public float pitch = 0.0f ;
	public float roll = 0.0f;
	public float yaw = 0.0f;

	// 与从串口获取数据相关的设置
	public string portName = "COM7";
	public int baudRate = 9600;
	public Parity parity = Parity.None;
	public int dataBits = 8;
	public StopBits stopBits = StopBits.One;

	//public int iter = 0;  //此参数是为了查看接收数据的次数。
	SerialPort sp = null;

	//对LoadData中LoadDynamicData类进行实例化
	LoadDynamicData myLoadDynamicData = new LoadDynamicData();
	void Start(){

		//调用类中打开端口函数，使用协程打开指定端口
		sp = myLoadDynamicData.OpenPort(portName,baudRate,parity,dataBits,stopBits);
		StartCoroutine(myLoadDynamicData.DataReceiveFunction(sp));

	}

	void OnGUI()
	{
		
		if(GUILayout.Button("旋转固定角度",GUILayout.Height(50)))
		{
			gameObject.transform.rotation = Quaternion.Euler(0.0f,-30.0f,0.0f);
		
		}
		if(GUILayout.Button("插值旋转固定角度",GUILayout.Height(50)))
		{
			isRotation = true;
		}

	}
	

	void Update()
	{
		// 串口实例将获取数据赋值给旋转参数
		// pitch:俯仰角,roll:横滚角,yaw:航向角
		pitch = myLoadDynamicData.pitch;
		roll = myLoadDynamicData.roll;
		yaw = myLoadDynamicData.yaw;
		//开始差值旋转
		if(!isRotation)
		{
			// gameObject.transform.rotation = Quaternion.Slerp (gameObject.transform.rotation, Quaternion.Euler(pitch,roll,yaw), Time.time);
		gameObject.transform.rotation = Quaternion.Euler(pitch,roll,yaw);
		}
	}
	
	//应用退出时关闭端口
	void OnApplicationQuit(){
		myLoadDynamicData.ClosePort(sp);
	}

}