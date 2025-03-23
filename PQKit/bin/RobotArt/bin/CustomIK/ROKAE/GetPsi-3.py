import UPPython
Engine = UPPython.CRoRobotPost()
allCount = Engine.GetCalPostItenValueParams()
import json
from ctypes import *
from math import *
'''
strjson = {"iArmCount":7,
"vec_DH":[0.0,404.0,90.0,0.0,0.0,0.0,-90.0,0.0,0.0,437.5,90.0,0.0,0.0,0.0,-90.0,0.0,0.0,412.5,90.0,0.0,0.0,0.0,-90.0,0.0,0.0,275.5,0.0,0.0],
"vec_EndPosture":[ 0,0,-0.99999999999513745, 0.000
,0,0.99999999999514089, 0 ,0,
0.99999999999995892 ,0 ,0 ,0,
570.76689443992052 ,-33.407853676665340,837.55154995767953 ,1],
"vec_JointMaxMin":[-170.0,170.0,-120.0,120.0,-170.0,170.0,-120.0,120.0,-170.0,170.0,-120.0,120.0,-355.0,355.0],
"vec_CurJoint":[-0.11266100321623397,-0.26642451031693443,0,1.8093653822500015,1.3294521978291207,0.11604694196510297,-1.3278814015023257]};
'''
strjson =  json.loads(allCount)
json_str = json.dumps(strjson)

data2 = json.loads(json_str)
iArmCount =data2['iArmCount']  ##关节轴数
tmp_DH = data2['vec_DH']      ##DH参数
tmp_endPosture = data2['vec_EndPosture']   ##机器人法兰末端数据
JointMinMax = data2['vec_JointMaxMin']  ##机器人关节最大最小值
CurJoint = data2['vec_CurJoint']     ##当前机器人关节
so = cdll.LoadLibrary
dllPath = Engine.GetDllPath()
path = dllPath +"\\CustomIK\\ROKAE\\Cal_IK3.dll"
lib = so(path)
getPsi = lib.Get_Psi

getPsi.argtypes = [c_double * 7, c_double * 14]  ##定义求臂角的函数 参数
getPsi.restype = c_double  ##函数返回值
DEG2RAD=0.01745329251994329576923690768489
Joints = (c_double*len(CurJoint))(*CurJoint)
JointMinMax = (c_double*len(JointMinMax))(*JointMinMax)
dres = lib.Get_Psi(Joints,JointMinMax)
strValue = str(dres)
Engine.SetReturnStringRes(strValue)


