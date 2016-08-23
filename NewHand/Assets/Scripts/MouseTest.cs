using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using MouseEvent;
using System.Runtime.InteropServices;
public class MouseTest : MonoBehaviour {
	// 对MyMouse类进行实例化
    MyMouse myMouse = new MyMouse();

	void Start () {
		// 鼠标复位
		// myMouse.Reset();
		//鼠标定位
		myMouse.SetPosition(300,320);
		//左击，放开
        myMouse.DoLeftClickDown();
        myMouse.DoLeftClickUp();
        // 滚轮测试,正号表示向前滚动，负号表示向后滚动
        myMouse.RotateWheel(-200);
        //移动鼠标
        myMouse.MoveMouse(100,120);
        //右击
        myMouse.DoRightClick();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
