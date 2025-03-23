from ftplib import FTP

import UPPython
import os
Engine = UPPython.CRoRobotPost()
lines = Engine.GetFtpInfo()

import json
print(lines)
file = json.loads(lines)

host_address = file["host_address"]
username = file["username"]
password = file["password"]
port = file["port"]
UpLoad = file["UpLoadPath"]
WriteFilePath = file["DownLoadPath"]
FileName = os.path.basename(UpLoad)
WriteFilePath +=FileName
# 创建TF
ftp = FTP()
# 第一个参数可以是ftp服务器的ip或者域名，第二个参数为ftp服务器的连接端口，默认为21
ftp.connect(host_address, port)
# 匿名登录直接使用ftp.login()
ftp.login(username, password)

# 设置缓冲区大小
bufsize = 1024
# 以写的方式打开文件
fp = open(WriteFilePath, 'wb')

# 下载用户输入 文件名 的文件
ftp.retrbinary('RETR ' + UpLoad, fp.write, bufsize)
#    ftp.set_debuglevel(0)
# 关闭ftp链接
fp.close()
file_object2 = open(WriteFilePath, 'r')
lines = file_object2.readlines()
bFlg = False
TcpDat = list()
for line in lines:
    #print(line)
    if line == '工具坐标系\n':
        print("工具坐标系开始")
        bFlg = True
        continue
    if bFlg == True:
        if line == '点动坐标系\n':
            bFlg = False
            continue
        TcpDat.append(line)
strJson ="["
for Tcp in TcpDat:
    if Tcp == "\n":
        continue

    TcpList = Tcp.split(' ')
    a= list(filter(None, TcpList))

    strData="{" +'"name":"'+ a[6].replace('\n','')+'"'+\
            ',"X":' + str(float(a[0]))+\
    ',"Y":' +str(float(a[1]))+\
    ',"Z":' +str(float(a[2]))+ \
            ',"Rx":' + str(float(a[3])) + \
            ',"Ry":' + str(float(a[4])) + \
            ',"Rz":' + str(float(a[5])) +'}';
    strJson += strData +",";
strJson = strJson[0:len(strJson)-1]
strJson += ']'
import UPPython

Engine = UPPython.CRoRobotPost()
Engine.SetRealRobotTcpInfo(strJson)








