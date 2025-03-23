from ftplib import FTP
import os
import UPPython
import json
'''
import UPPython

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
'''
def GetFileList():

    lines = Engine.GetFtpInfo()
    # lines='{"host_address":"192.168.2.3","username":"","password":"","port":21,"UpLoadPath":"./","DownLoadPath":"C:/avalib/"}'
    file = json.loads(lines)
    host_address = file["host_address"]
    username = file["username"]
    password = file["password"]
    port = file["port"]
    UpLoad = file["UpLoadPath"]

    # 创建TF
    ftp = FTP()
    # 第一个参数可以是ftp服务器的ip或者域名，第二个参数为ftp服务器的连接端口，默认为21
    ftp.connect(host_address, port)
    # 匿名登录直接使用ftp.login()
    ftp.login(username, password)
    Ftplist = ftp.nlst(UpLoad)
    strJson = "["
    for FileName in Ftplist:
        FileExt = os.path.splitext(FileName)[-1]
        strJson += '{"FileName":' + '"' + FileName + '"' + ',"FileExt":' + '"' + FileExt + '"''}'
        strJson += ','

    strJson = strJson[0:len(strJson) - 1]
    strJson += ']'
    Engine.SetFileList(strJson)
def DownLoadFile():
    #+		strFileList	"{\"Data\":[{\"FileName\":\"./dadasda.txt\"},{\"FileName\":\"./FRAME.DG\"}]}"
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
    # 创建TF
    ftp = FTP()
    # 第一个参数可以是ftp服务器的ip或者域名，第二个参数为ftp服务器的连接端口，默认为21
    ftp.connect(host_address, port)
    # 匿名登录直接使用ftp.login()
    ftp.login(username, password)
    strFileList = Engine.GetFileList()

    import json
    file = json.loads(strFileList)
    for dat in file["Data"]:
        UpLoad = dat["FileName"]
        filePath = WriteFilePath
        fileName = os.path.basename(dat["FileName"])
        filePath += fileName
        bufsize = 1024
        fp = open(filePath, 'wb')
        ftp.retrbinary('RETR ' + UpLoad, fp.write, bufsize)
        fp.close()

if __name__ == '__main__':
    Engine = UPPython.CRoRobotPost()
    flg = Engine.GetFileListFlg()
    if 1== flg:
        GetFileList()
    else:
        DownLoadFile()

'''
# 设置缓冲区大小
bufsize = 1024
# 以写的方式打开文件
fp = open(WriteFilePath, 'wb')

# 下载用户输入 文件名 的文件
ftp.retrbinary('RETR ' + UpLoad, fp.write, bufsize)
#    ftp.set_debuglevel(0)
# 关闭ftp链接
fp.close()
'''









