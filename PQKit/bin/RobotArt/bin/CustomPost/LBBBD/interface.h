#ifndef BCOFFLINE_GLOBAL_H
#define BCOFFLINE_GLOBAL_H


#define MAX_TASK_NAME_LEN	20
#define MAX_TASKPARAS_NUM	15
#define MAX_TASKPARAS_LEN	20

typedef struct _TaskRecord
{
    char TaskName[MAX_TASK_NAME_LEN];
    char ParaNum;
    char ParaType[MAX_TASKPARAS_NUM];
    char TaskParas[MAX_TASKPARAS_NUM][MAX_TASKPARAS_LEN];
}TASK_REC;

/*
 * 功能:查看sdk版本号
 * 返回:代表版本信息的字符串
 */
extern "C" BCOFFLINE_EXPORT char* _CM_Get_Dll_Version();

/*
 * 功能:校验任务参数合法性
 * 输入:
 * 任务结构体
 * 返回:
 * 1、校验通过则返回任务详情，校验失败则返回相关错误提示
 * 2、校验通过cResult值为0，校验失败则cResult值不为0
 */
extern "C" BCOFFLINE_EXPORT char* _CM_GetTaskInfo(TASK_REC sTaskRec,char& cResult);

/*
 * 功能:程序指令集结构校验
 * 输入:
 * .off文件路径
 * 返回:
 * 1、校验通过则返回成功提示，校验失败则返回相关错误提示
 * 2、校验通过cResult值为0，校验失败则cResult值不为0
 */
extern "C" BCOFFLINE_EXPORT char* _CM_Program_Check(char* pFilePath,char& cResult);

#endif // BCOFFLINE_GLOBAL_H
