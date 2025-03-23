 #!/usr/bin/python
# -*- coding: UTF-8 -*-

from ftplib import FTP
import os
import sys
import time
import socket


class MyFTP:


    def __init__(self, host, port):       
        self.host = host
        self.port = port
        self.ftp = FTP()
        self.ftp.encoding = 'gbk'

        self.log_file = open("D:\\log.txt","a")
        self.file_list = []

    def login(self, username, password):     
        try:
            timeout = 60
            socket.setdefaulttimeout(timeout)
            self.ftp.set_pasv(True)
            self.debug_print('Start trying to connect to %s' % self.host)
            self.ftp.connect(self.host, self.port)
            self.debug_print('Successfully connect to %s' % self.host)
            self.debug_print('Start trying to log in to %s' % self.host)
            self.ftp.login(username, password)
            self.debug_print('Successfully log in to %s' % self.host)
            self.debug_print(self.ftp.welcome)
        except Exception as err:
            self.deal_error("FTP connection or login failure, error description as:%s" % err)
            pass

    def is_same_size(self, local_file, remote_file):    
        try:
            remote_file_size = self.ftp.size(remote_file)
        except Exception as err:
            remote_file_size = -1
        try:
            local_file_size = os.path.getsize(local_file)
        except Exception as err:
            local_file_size = -1
        self.debug_print('local_file_size:%d  , remote_file_size:%d' % (local_file_size, remote_file_size))
        if remote_file_size == local_file_size:
            return 1
        else:
            return 0

    def upload_file(self, local_file, remote_file):
        if not os.path.isfile(local_file):
            self.debug_print('%s There is no' % local_file)
            return
        if self.is_same_size(local_file, remote_file):
            self.debug_print('Skip equal files: %s' % local_file)
            return
        direct = os.path.split(remote_file)
        str_Dir = direct[0]
        str_Dir = str_Dir + "/"
        try:
            self.ftp.cwd(str_Dir)
        except Exception as err:
            self.ftp.mkd(str_Dir)
        buf_size = 1024
        file_handler = open(local_file, 'rb')
        self.ftp.storbinary('STOR %s' % remote_file, file_handler, buf_size)
        file_handler.close()
        self.debug_print('UpLoad: %s' % local_file + "Successfully!")

    def upload_file_tree(self, local_path, remote_path):
        if not os.path.isdir(local_path):
            self.debug_print('The local directory %s does not exist' % local_path)
            return
        try:
            self.ftp.cwd(remote_path)
        except Exception as err:
            self.ftp.mkd(remote_path)

        self.ftp.cwd(remote_path)
        self.debug_print('Switch to the remote directory: %s' % self.ftp.pwd())
        local_name_list = os.listdir(local_path)
        for local_name in local_name_list:
            src = os.path.join(local_path, local_name)
            if os.path.isdir(src):
                try:
                    self.ftp.mkd(local_name)
                except Exception as err:
                    self.debug_print("The directory %s already exists. The error is described as %s" % (local_name, err))
                self.debug_print("upload_file_tree()---> Upload directory: %s" % local_name)
                local_Remote = remote_path + "/" +local_name;
                self.upload_file_tree(src, local_Remote)
            else:
                self.debug_print("upload_file_tree()---> Upload a file:%s" % local_name)
                local_Remote = remote_path + "/" + local_name;
                self.upload_file(src, local_Remote)
        self.ftp.cwd("..")


    def close(self):
        self.debug_print("close()---> FTPExit")
        self.ftp.quit()
        self.log_file.close()

    def debug_print(self, s):
        self.write_log(s)

    def deal_error(self, e):
        log_str = 'An error occurred: %s' % e
        self.write_log(log_str)
        sys.exit()

    def write_log(self, log_str):
        time_now = time.localtime()
        date_now = time.strftime('%Y-%m-%d-%H:%M:%S', time_now)
        format_log_str = "%s ---> %s \n " % (date_now, log_str)
        print(format_log_str)
        self.log_file.write(format_log_str)

    def get_file_list(self, line):
        file_arr = self.get_file_name(line)
        if file_arr[1] not in ['.', '..']:
            self.file_list.append(file_arr)

    def get_file_name(self, line):
        pos = line.rfind(':')
        while (line[pos] != ' '):
            pos += 1
        while (line[pos] == ' '):
            pos += 1
        file_arr = [line[0], line[pos:]]
        return file_arr



if __name__ == "__main__":
    #print("11111112222111")
    import UPPython
    Engine = UPPython.CRoRobotPost()
    lines = Engine.GetFtpInfo()
    #fo.write(lines)

    #lines = '{"host_address":"192.168.2.3","username":"","password":"","port":21,"UpLoadPath":"./"}'
    import json
    file = json.loads(lines)
    host_address = file["host_address"]
    username = file["username"]
    password = file["password"]
    port = file["port"]
    UpLoad = file["UpLoadPath"]
    ##  UpLoad =   '/TeachProgram/PQMain'
    my_ftp = MyFTP(host_address, port)
    my_ftp.login(username, password)

    import UPPython
    Engine = UPPython.CRoRobotPost()
    path = Engine.GetSavePath()

    #path = "C:\\Users\\a\\Desktop\\RaMain"
    # path = "C:\\Users\\a\\Desktop\\RaMain"
    #path = 'C:\\Users\\a\\AppData\\Local\\Temp\\RaMain.ls'
    direct = os.path.split(path)
    FileName = direct[1]

    Ftplist = my_ftp.ftp.nlst(UpLoad)
    strRes = False;
    for FtpFileName in Ftplist:
        if FtpFileName == FileName:
            strRes = True;


    if strRes == True:
         Engine.SetReturnStringRes("TRUE")
         #print("1111111")
    else:
         UpLoad = UpLoad + FileName
         my_ftp.upload_file(path,
                                    UpLoad)
    my_ftp.close()
   




     


