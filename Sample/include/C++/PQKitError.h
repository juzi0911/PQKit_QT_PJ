#pragma once

// error code
#define _RO_HRESULT_TYPEDEF_(_sc) ((HRESULT)_sc)
// PQKit相关错误
#define RO_PQKIT_E_FIRST	(HRESULT)0x84000E00
#define RO_PQKIT_E_LAST		(HRESULT)0x84000FFF

// PQKit接口的错误返回值
// 传入参数不符合要求
#define PQ_PARAMETER_MISS_MATCH					(_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 1)))
// 传入文件不存在
#define PQ_FILE_NOT_EXIST						(_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 2)))
// 打开文件失败
#define PQ_OPEN_FILE_FAIL						(_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 3)))
// 指定的语言不支持
#define PQ_LANG_NOT_SUPPORT						(_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 4)))
// 传入了空指针
#define PQ_INPUT_POINTER_EMPTY					(_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 5)))
// 权限错误
#define PQ_PERMISSION_ERROR                     (_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 6)))
// 后置异常-轨迹状态异常
#define PQ_PATH_STATE_ERROR                     (_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 7)))
// 后置异常-不支持的指令
#define PQ_INSTRUCTION_UNSUPPORT                (_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 8)))
// 后置异常
#define PQ_POST_ERROR			                (_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 9)))