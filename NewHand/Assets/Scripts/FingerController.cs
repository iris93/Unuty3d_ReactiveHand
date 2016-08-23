using UnityEngine;
using System.Collections;

//此脚本需要绑定到palm上
public class FingerController : MonoBehaviour {
	// 两个手指list，使用public格式方便直接在inspector里面直接修改list长度以及进行直接配置
	// 目前配置结果：除大拇指用finger0[4]一个支节表示外，其余食指到小拇指分别用finger0[0~3]表示第一支节，finger1[0~3]表示第二支节
	public Transform[] finger0;          //手指第一支节
	public Transform[] finger1;          //手指第二支节
	private float angle;                //手指打开或合拢的角度，通过这里可以设定运动极限
	public float offset;               //角度步长,后面可以根据需要进行修改

	// Use this for initialization
	void Start () {
		angle = 0;          //默认手指为打开
	
	}
	void OnGUI(){
		// 设定不同按钮改变运动方向
		if(GUILayout.Button("握拳",GUILayout.Height(50)))
		{
			offset = -0.2f;
		}

		if(GUILayout.Button("松拳",GUILayout.Height(50)))
		{
			offset = 0.2f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (angle + offset >= -20 && angle + offset < 2) {//手指运动范围，可以查看效果进行修改
			for (int i = 0; i < 4; i++) {                                   //进行开启或合拢
				finger0[i].Rotate(Vector3.left, offset * 3.5f, Space.Self);  //两级级支节分别进行操作
				finger1[i].Rotate(Vector3.left, offset * 4.2f, Space.Self);
			}
			// 对大拇指需要单独操作，大拇指旋转角度比较复杂，可以通过查看效果进行调节
			finger0[4].Rotate(Vector3.left, offset * -0.5f, Space.Self);
			finger0[4].Rotate(Vector3.up, offset * 3.0f, Space.Self);
			finger0[4].Rotate(Vector3.back, offset * 1.5f, Space.Self);
			angle += offset;
		}
	
	}
}
