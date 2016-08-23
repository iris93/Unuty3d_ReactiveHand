using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System;

namespace LoadData{
	// 包含两个class，分别实现从本地静态文件读取数据和从COM口动态读取数据
	// 使用此包需要进行引用：using LoadData

//静态数据加载，从本地静态文件加载
/**
*class 使用例子
	ArrayList paramList= new ArrayList(); 
	LoadStaticData myLoadStaticData = new LoadStaticData(); //实例化类
	ArrayList info = myLoadStaticData.LoadFile(Application.dataPath,"txtfolder//angle.txt"); //读取文件
	paramList = myLoadStaticData.SplitStaticData(info);  //提取info
	foreach(float[] item in paramList)  //提取参数并赋值
	{
		// pitch:俯仰角,roll:横滚角,yaw:航向角
		pitch = item[0];
		roll = item[1];
		yaw = item[2]; 
	}
*/
public class LoadStaticData{
   /**
   * path：读取文件的路径
   * name：读取文件的名称
   */
	public ArrayList LoadFile(string path,string name)
		{
			//使用流的形式读取
			StreamReader sr =null;
			try{
				sr = File.OpenText(path+"//"+ name);  
			}catch(Exception e)
			{
				//路径与名称未找到文件则直接返回空
				return null;
			}
			string line;
			ArrayList arrlist = new ArrayList();
			while ((line = sr.ReadLine()) != null)
			{
				//一行一行的读取
				//将每一行的内容存入数组链表容器中
				arrlist.Add(line);
			}
			//关闭流
			sr.Close(); 
			//销毁流
			sr.Dispose();
			//将数组链表容器返回
			return arrlist;
		}

		// 对读取到的数据进行分解操作，提取需要的数据，输入参数就是LoadFile函数的结果
	public ArrayList SplitStaticData(ArrayList info)
		{
			ArrayList paramList= new ArrayList();
			foreach(string str in info)
			{
			// str = 'a=num,b=num,c=num'
			// 一级切分，提取a=num,b=num,c=num
			string [] substr = str.Split(new char[] {','});
			// Debug.Log(substr.GetLength(0));
			int dataLength = substr.GetLength(0);
			float [] rotationparam = new float[dataLength];
			int i=0;
			foreach(string subsubstr in substr){
				// 二级切分，提取num
			string [] subparam = subsubstr.Split(new char[]{'='});
			rotationparam[i] = float.Parse(subparam[1]);
			// Debug.Log(rotationparam[i]);
			i++;
			}
			paramList.Add(rotationparam);
			// Debug.Log(paramList[0]);
		}
		return paramList;
	}
	}

//动态数据加载，串口加载
/**
*class 使用例子稍复杂，参考FetchTest.cs
*/
	public class LoadDynamicData{
		//COM口相关设置
		private string portName = "COM7";
		private int baudRate = 9600;
		private Parity parity = Parity.None;
		private int dataBits = 8;
		private StopBits stopBits = StopBits.One;

		private SerialPort sp= null;
		public string ls_Rev2 = "";
		public int iter = 0;
		public float pitch = 0.0f ;
		public float roll = 0.0f;
		public float yaw = 0.0f;

		//此函数的功能为代开指定端口
		public SerialPort OpenPort(string portName,int baudRate,Parity parity,int dataBits,StopBits stopBits)
		{
			sp = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
			sp.ReadTimeout = 6000;
			try{
				sp.Open();
				Debug.Log("Open");
				
			}
			catch(Exception ex){
					Debug.Log("open ex:"+ex.Message);
				}
			return sp;
		}

		//此函数的功能为关闭指定端口
		public void ClosePort(SerialPort sp){
			try{
				sp.Close();
				Debug.Log("Close");
			}
			catch(Exception ex){
				Debug.Log("Close ex:"+ex.Message);
			}
		}
		//这里获取端口所产生的数据
		public IEnumerator DataReceiveFunction(SerialPort sp)
		{
			while(true)
			{
				if(sp != null && sp.IsOpen)
				{   
					try
					{
						// 这里通过读取分号作为读取一条数据的标记。因此要与底层沟通好。
						ls_Rev2 = sp.ReadTo(";");
						Debug.Log("ls_Rev2:"+ls_Rev2);
						iter++;
						// 下面代码块取出读到的参数值
						string[] substr = ls_Rev2.Split(new char[] {','});
						int dataLength = substr.GetLength(0);
						float [] rotationparam = new float[dataLength];
						int i=0;
						foreach(string subsubstr in substr){
							string [] subparam = subsubstr.Split(new char[]{'='});
							rotationparam[i] = float.Parse(subparam[1]);
							// Debug.Log(rotationparam[i]);
							i++;
						}
						// 将提取到的值赋值给相关变量
						Debug.Log(rotationparam[0]+","+rotationparam[1]+","+rotationparam[2]);
						pitch = rotationparam[0];
						roll = rotationparam[1];
						yaw = rotationparam[2];
					}
					catch(Exception ex){
						Debug.Log("receive ex:"+ex.Message);
						
					}
				}
				yield return new WaitForSeconds(Time.deltaTime);
			}
		}
	}


}