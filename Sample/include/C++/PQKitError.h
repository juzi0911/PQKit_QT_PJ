#pragma once

// error code
#define _RO_HRESULT_TYPEDEF_(_sc) ((HRESULT)_sc)
// PQKit��ش���
#define RO_PQKIT_E_FIRST	(HRESULT)0x84000E00
#define RO_PQKIT_E_LAST		(HRESULT)0x84000FFF

// PQKit�ӿڵĴ��󷵻�ֵ
// �������������Ҫ��
#define PQ_PARAMETER_MISS_MATCH					(_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 1)))
// �����ļ�������
#define PQ_FILE_NOT_EXIST						(_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 2)))
// ���ļ�ʧ��
#define PQ_OPEN_FILE_FAIL						(_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 3)))
// ָ�������Բ�֧��
#define PQ_LANG_NOT_SUPPORT						(_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 4)))
// �����˿�ָ��
#define PQ_INPUT_POINTER_EMPTY					(_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 5)))
// Ȩ�޴���
#define PQ_PERMISSION_ERROR                     (_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 6)))
// �����쳣-�켣״̬�쳣
#define PQ_PATH_STATE_ERROR                     (_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 7)))
// �����쳣-��֧�ֵ�ָ��
#define PQ_INSTRUCTION_UNSUPPORT                (_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 8)))
// �����쳣
#define PQ_POST_ERROR			                (_RO_HRESULT_TYPEDEF_(((RO_PQKIT_E_FIRST) + 9)))