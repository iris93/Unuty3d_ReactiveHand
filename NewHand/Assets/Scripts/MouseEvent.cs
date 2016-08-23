using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System;

namespace MouseEvent{
    /*
    主要为了方便鼠标操作函数的调用，使用例子参见MouseTest.cs
    */

public class MyMouse{
	[Flags]
    public enum MouseEventFlag : int   //设置鼠标动作的键值
    {
        Move = 0x0001,               //发生移动
        LeftDown = 0x0002,           //鼠标按下左键
        LeftUp = 0x0004,             //鼠标松开左键
        RightDown = 0x0008,          //鼠标按下右键
        RightUp = 0x0010,            //鼠标松开右键
        MiddleDown = 0x0020,         //鼠标按下中键
        MiddleUp = 0x0040,           //鼠标松开中键
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,              //鼠标轮被移动
        VirtualDesk = 0x4000,        //虚拟桌面
        Absolute = 0x8000
    }
    [DllImport("user32.dll")]
    public static extern int SetCursorPos(int x, int y);//设置鼠标位置
    [DllImport("user32.dll")]
    public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);
    // 鼠标事件，dwFlags即MouseEventFlag中各个鼠标动作键值
    [DllImport("user32.dll")]
    public static extern IntPtr FindWindow(string strClass, string strWindow);
    //该函数获取一个窗口句柄,该窗口雷鸣和窗口名与给定字符串匹配 hwnParent=Null从桌面窗口查找,暂时没有使用
    // 左键按下
    public void DoLeftClickDown()
    {
        mouse_event((int)( MouseEventFlag.LeftDown), 0, 0, 0, new IntPtr(0)); //点击
    }
    // 左键放松
    public void DoLeftClickUp()
    {
        mouse_event((int)( MouseEventFlag.LeftUp), 0, 0, 0, new IntPtr(0)); //点击
    }
    // 点击右键
    public void DoRightClick()
    {
    	mouse_event((int)( MouseEventFlag.RightDown | MouseEventFlag.RightUp), 0, 0, 0, new IntPtr(0));
    }
    // 移动鼠标
    public void MoveMouse(int stepx,int stepy){
    	mouse_event((int)(MouseEventFlag.Move),stepx,stepy,0,IntPtr.Zero);
    }

    // 鼠标移动到指定坐标
    public void SetPosition(int x,int y){
    	SetCursorPos(x,y);
    }
    // 鼠标复位
    public void Reset(){
    	SetCursorPos(0,0);
    }
    //鼠标滚轮滚动，Rotation正号表示向前滚动，负号表示向后滚动
    public void RotateWheel(int Rotation){
    	mouse_event((int)MouseEventFlag.Wheel,0,0,Rotation,IntPtr.Zero);
    }
}
}
