这个工程已经是相对于其他例子而言相对比较完整的工程了，主要包含了数据（动态/静态）加载模块及测试，鼠标事件模块及测试，以及手指控制器

LoadData.cs是数据加载包，包含两个类，分别为静态数据加载及动态数据加载

MoveTest.cs为静态本地数据加载测试，txtfolder目录下的angle.txt为包含数据的测试文件

FetchTest为动态COM口数据加载测试，可能会提示没有ports类的情况，只需要将Edit/Project Settings/palyer的Optimization选项中的Api Compatibility Level改为.NET 2.0即可

MouseEvent.cs为鼠标操作包

MouseTest.cs为鼠标操作测试

FingerController.cs为手指动作控制

Materials文件目录下包含了机械手的零件和材质贴图