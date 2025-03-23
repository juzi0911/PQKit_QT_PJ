#ifndef MAINWINDOW_H
#define MAINWINDOW_H
#import "RPC.tlb" no_namespace, named_guids, raw_interfaces_only, raw_native_types

#include <QMainWindow>
#include <QCloseEvent>
#include <QMessageBox>

#include "PQKitCallback.h"
#include "..\include\C++\PQKitError.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

	void InitAction();
	void InitMenu();
	void InitToolBar();

	void InitPQKit();
	void ShowPQKitWindown();
	void GetObjIDByName(int i_nType, std::wstring i_wsName, ULONG &o_uID);
	void GetFirstPointOfPath(ULONG i_ulPathID, ULONG& o_ulPointIDe);

private:
    Ui::MainWindow *ui;
	QAction* m_openAction;
	QAction* m_saveasAction;
	QAction* m_compileAction;
	QAction* m_simulateAction;
	QAction* m_insertpathAction;
	QAction* m_closefileAction;
	QAction* m_import3dAction;
	QAction* m_measureAction;
	QAction* m_arcboxAction;
	QAction* m_alignpartAction;
	QAction* m_generatepathAction;
	QAction* m_postAction;
	QAction* m_getpathpoint;

	CComPtr<IPQPlatformComponent> m_ptrKit;
	CPQKitCallback* m_ptrKitCallback;

	QWidget* m_pPQPlatformView;
	QWidget* m_pPQModeTreeView;
	QWidget* m_pPQDebugView;

private:
	void closeEvent(QCloseEvent* event);

protected slots:
	//owner
	void OnOpenRobx();
	void OnSaveAsRobx();
	void OnCompile();
	void OnSimulate();
	void OnInsertPath();
	void CloseFile();
	void Import3DObj();
	void Measure();
	void ArcBox();
	void AlignPart();
	void Generatepath();
	void Post();
	void GetPathPointInfo();

	//PQKit slots
	void OnInitializeResult(long lResult);
	void OnLoginResult(int i_nLoginType);
	void OnElementPickup(unsigned long i_ulObjID, LPWSTR i_lEntityID, int i_nEntityType);
	void OnElementSelection(LPWSTR i_wObjNames, LPWSTR i_wFaceNames, double* i_dPointXYZ, int i_nSize);

	void OnInitializeKitThread();

};
#endif // MAINWINDOW_H
