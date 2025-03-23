#include "MainWindow.h"
#include "ui_MainWindow.h"
#include <QFileDialog>
#include <QSplitter>
#include <QWindow>
#include <QThread>
#include "InsertPathDlg.h"
#include <QDebug>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

	//init menu/toolbar
	InitAction();
	InitMenu();
	InitToolBar();

	//load pqkit
	InitPQKit();
}

MainWindow::~MainWindow()
{
	CoUninitialize();
    delete ui;
}

void MainWindow::InitAction()
{
	m_openAction = new QAction(QIcon(":/Images/open.png"), QString::fromLocal8Bit("打开"), this);
	m_saveasAction = new QAction(QIcon(":/Images/save.png"), QString::fromLocal8Bit("另存"), this);
	m_compileAction = new QAction(QIcon(":/Images/compile.png"), QString::fromLocal8Bit("编译"), this);
	m_simulateAction = new QAction(QIcon(":/Images/simulate.png"), QString::fromLocal8Bit("仿真"), this);
	m_insertpathAction = new QAction(QIcon(":/Images/insertpath.png"), QString::fromLocal8Bit("导入轨迹点"), this);
	m_closefileAction = new QAction(QIcon(":/Images/closefile.png"), QString::fromLocal8Bit("关闭文档"), this);
	m_import3dAction = new QAction(QIcon(":/Images/import3d.png"), QString::fromLocal8Bit("导入3d文件"), this);
	m_measureAction= new QAction(QIcon(":/Images/measure.png"), QString::fromLocal8Bit("测量"), this);
	m_arcboxAction= new QAction(QIcon(":/Images/arcbox.png"), QString::fromLocal8Bit("三维球"), this);
	m_alignpartAction = new QAction(QIcon(":/Images/alignpart.png"), QString::fromLocal8Bit("三点校准"), this);
	m_generatepathAction= new QAction(QIcon(":/Images/generatepath.png"), QString::fromLocal8Bit("生成轨迹"), this);
	m_postAction= new QAction(QIcon(":/Images/post.png"), QString::fromLocal8Bit("后置"), this);
	m_getpathpoint= new QAction(QIcon(":/Images/getpathpoint.png"), QString::fromLocal8Bit("轨迹点信息"), this);

	connect(m_openAction, SIGNAL(triggered()), this, SLOT(OnOpenRobx()));
	connect(m_saveasAction, SIGNAL(triggered()), this, SLOT(OnSaveAsRobx()));
	connect(m_compileAction, SIGNAL(triggered()), this, SLOT(OnCompile()));
	connect(m_simulateAction, SIGNAL(triggered()), this, SLOT(OnSimulate()));
	connect(m_insertpathAction, SIGNAL(triggered()), this, SLOT(OnInsertPath()));
	connect(m_closefileAction, SIGNAL(triggered()), this, SLOT(CloseFile()));
	connect(m_import3dAction, SIGNAL(triggered()), this, SLOT(Import3DObj()));
	connect(m_measureAction, SIGNAL(triggered()), this, SLOT(Measure()));
	connect(m_arcboxAction, SIGNAL(triggered()), this, SLOT(ArcBox()));
	connect(m_alignpartAction, SIGNAL(triggered()), this, SLOT(AlignPart()));
	connect(m_generatepathAction, SIGNAL(triggered()), this, SLOT(Generatepath()));
	connect(m_postAction, SIGNAL(triggered()), this, SLOT(Post()));
	connect(m_getpathpoint, SIGNAL(triggered()), this, SLOT(GetPathPointInfo()));

}

void MainWindow::InitMenu()
{
	QMenuBar *mBar = menuBar();
	QMenu *pFile = mBar->addMenu("PQKit");

	//
	pFile->addAction(m_openAction);
	pFile->addAction(m_saveasAction);
	pFile->addAction(m_closefileAction);
	pFile->addSeparator();
	pFile->addAction(m_compileAction);
	pFile->addAction(m_simulateAction);
	pFile->addSeparator();
	pFile->addAction(m_insertpathAction);
	pFile->addAction(m_generatepathAction);
	pFile->addAction(m_getpathpoint);
	pFile->addAction(m_import3dAction);
	pFile->addSeparator();
	pFile->addAction(m_measureAction);
	pFile->addAction(m_arcboxAction);
	pFile->addAction(m_alignpartAction);
	pFile->addAction(m_postAction);
	pFile->addSeparator();

}

void MainWindow::InitToolBar()
{
	ui->toolBar->setToolButtonStyle(Qt::ToolButtonTextUnderIcon);
	ui->toolBar->addAction(m_openAction);
	ui->toolBar->addAction(m_saveasAction);
	ui->toolBar->addAction(m_closefileAction);
	ui->toolBar->addSeparator();
	ui->toolBar->addAction(m_compileAction);
	ui->toolBar->addAction(m_simulateAction);
	ui->toolBar->addSeparator();
	ui->toolBar->addAction(m_insertpathAction);
	ui->toolBar->addAction(m_getpathpoint);
	ui->toolBar->addAction(m_generatepathAction);
	ui->toolBar->addAction(m_import3dAction);
	ui->toolBar->addSeparator();
	ui->toolBar->addAction(m_measureAction);
	ui->toolBar->addAction(m_arcboxAction);
	ui->toolBar->addAction(m_alignpartAction);
	ui->toolBar->addAction(m_postAction);
	ui->toolBar->addSeparator();
}

void MainWindow::InitPQKit()
{
	//
	CoInitializeEx(0, COINIT_APARTMENTTHREADED);
	HRESULT hr = m_ptrKit.CoCreateInstance(__uuidof(PQPlatformComponent));
	if (S_OK != hr)
	{
		QString strError = QString::fromLocal8Bit("PQKit创建失败！\n请排查当前启动或调试exe同目录是否有PQKit环境。\nPQkit环境包含PQkit文件夹、PQKit.manifest文件、启动或调试exe的manifest文件。");
		QMessageBox::information(NULL, "PQKit", strError, QMessageBox::Yes | QMessageBox::No, QMessageBox::Yes);
	}

	//
	m_ptrKitCallback = new CPQKitCallback(this);
	connect(m_ptrKitCallback, &CPQKitCallback::signalInitializeResult, this, &MainWindow::OnInitializeResult);
	connect(m_ptrKitCallback, &CPQKitCallback::signalLoginResult, this, &MainWindow::OnLoginResult);
	connect(m_ptrKitCallback, &CPQKitCallback::signalElementPickup, this, &MainWindow::OnElementPickup);
	connect(m_ptrKitCallback, &CPQKitCallback::signalElementSelection, this, &MainWindow::OnElementSelection);

	//
	QThread *ptrThread = new QThread;
	connect(ptrThread, &QThread::started, this, &MainWindow::OnInitializeKitThread);
	ptrThread->start();
}

void MainWindow::OnInitializeKitThread()
{
	//initialize pqkit
	CComBSTR bsName = L"";
	CComBSTR bsPWD = L"";
	HRESULT hr = m_ptrKit->pq_InitPlatformComponent(m_ptrKitCallback, (int)(this->winId()), bsName, bsPWD);
	if (S_OK != hr)
	{
		QString strError = QString::fromLocal8Bit("PQKit初始化失败！\n请参考Fire_Initialize_Result或Fire_Login_Result函数中的具体lResult值。");
		QMessageBox::information(NULL, "PQKit", strError, QMessageBox::Yes | QMessageBox::No, QMessageBox::Yes);
	}
}



void MainWindow::ShowPQKitWindown()
{
	int nHWND = 0;
	m_ptrKit->pq_GetPlatformView(&nHWND);
	HWND hWnd = (HWND)(UINT_PTR)nHWND;
	if (nullptr == hWnd)
	{
		return;
	}
	m_pPQPlatformView = QWidget::createWindowContainer(QWindow::fromWinId((WId)hWnd), this);

	ULONG_PTR nModelDoc = NULL;
	m_ptrKit->pq_GetModelTreeView(&nModelDoc);
	HWND hModelWnd = (HWND)nModelDoc;
	if (nullptr != hModelWnd)
	{
		m_pPQModeTreeView = QWidget::createWindowContainer(QWindow::fromWinId((WId)hModelWnd), this);
	}

	ULONG_PTR nDebugDoc = NULL;
	m_ptrKit->pq_GetDebugTreeView(&nDebugDoc);
	HWND hDebugWnd = (HWND)nDebugDoc;
	if (nullptr != hDebugWnd)
	{
		m_pPQDebugView = QWidget::createWindowContainer(QWindow::fromWinId((WId)hDebugWnd), this);
	}

	auto splitter = new QSplitter;
	splitter->addWidget(m_pPQModeTreeView);
	splitter->addWidget(m_pPQPlatformView);
	splitter->addWidget(m_pPQDebugView);
	splitter->setStretchFactor(0, 1);
	splitter->setStretchFactor(1, 5);
	splitter->setStretchFactor(2, 1);
	setCentralWidget(splitter);
}

void MainWindow::closeEvent(QCloseEvent* event)
{
	if (m_ptrKit)
	{
		m_ptrKit->pq_CloseComponent();
	}

	event->accept();
}

void MainWindow::OnOpenRobx()
{
	QString fileName = QFileDialog::getOpenFileName(this, tr("open robx file"), "",
		tr("Robx files(*.robx)"));
	if (fileName.isEmpty())
	{
		return;
	}
	//
	std::wstring wsFilePath = fileName.toStdWString();
	m_ptrKit->Doc_open_robx((LPWSTR)(wsFilePath.c_str()));
	
}

void MainWindow::OnSaveAsRobx()
{
	QString fileName = QFileDialog::getSaveFileName(this, tr("open robx file"), "",
		tr("Robx files(*.robx)"));
	if (fileName.isEmpty())
	{
		return;
	}
	//
	std::wstring wsFilePath = fileName.toStdWString();
	m_ptrKit->Doc_saveas_robx((LPWSTR)(wsFilePath.c_str()));
	
}

void MainWindow::OnCompile()
{
	wchar_t whMoudle[256] = L"RO_CMD_COMPILE";
	m_ptrKit->Doc_start_module((LPWSTR)whMoudle);
	
}

void MainWindow::OnSimulate()
{
	wchar_t whMoudle[256] = L"RO_CMD_SIMULATE";
	m_ptrKit->Doc_start_module((LPWSTR)whMoudle);

}

void MainWindow::OnInsertPath()
{
	InsertPathDlg dlg;
	if (QDialog::Rejected == dlg.exec())
	{
		return;
	}
		
	//默认取第一个机器人
	ULONG uRobotID = 0;
	GetObjIDByName(32, _T(""), uRobotID);

	//
	int nType = 1;
	double dPosition[6] = { 0.0 };
	dlg.GetPosture(dPosition[0], dPosition[1], dPosition[2], dPosition[3], dPosition[4], dPosition[5], nType);

	int nInstruction[1] = { 1 };
	double dVel[1] = { 200.0 };
	double dSpeedP[1] = { 100.0 };
	int nApproach[1] = { -1 };
	CComBSTR bsPathName = "Test_Path";
	CComBSTR bsPathGroupName = "Test_Group";

	ULONG uPathID = 0;
	m_ptrKit->Path_insert_from_point(uRobotID, 1, dPosition, nType, nInstruction, dVel, dSpeedP, nApproach, bsPathName, bsPathGroupName, 0, FALSE, &uPathID, TRUE);
}

void MainWindow::GetObjIDByName(int i_nType, std::wstring i_wsName, ULONG &o_uID)
{
	VARIANT vNamePara;
	vNamePara.parray = NULL;
	VARIANT vIDPara;
	vIDPara.parray = NULL;
	m_ptrKit->Doc_get_obj_bytype(i_nType, &vNamePara, &vIDPara);
	if (NULL == vNamePara.parray || NULL == vIDPara.parray)
	{
		return;
	}
	//缓存指定对象名称
	BSTR* bufName;
	long lenName = vNamePara.parray->rgsabound[0].cElements;
	SafeArrayAccessData(vNamePara.parray, (void**)&bufName);
	int nTarIndex = 0;
	if (!i_wsName.empty())
	{
		for (int i = 0; i < lenName; i++)
		{
			if (0 == i_wsName.compare(bufName[i]))
			{
				nTarIndex = i;
			}
		}
	}
	SafeArrayUnaccessData(vNamePara.parray);


	//缓存指定对象ID
	ULONG* bufID;
	long lenID = vIDPara.parray->rgsabound[0].cElements;
	SafeArrayAccessData(vIDPara.parray, (void**)&bufID);
	o_uID = bufID[nTarIndex];
	SafeArrayUnaccessData(vIDPara.parray);
}

void MainWindow:: GetFirstPointOfPath(ULONG i_ulPathID, ULONG& o_ulPointID)
{
	VARIANT vPointPara;
	vPointPara.parray = NULL;
	m_ptrKit->PQAPIGetPointsID(i_ulPathID, &vPointPara);
	long lenPointCount = vPointPara.parray->rgsabound[0].cElements;
	ULONG* t_bufPoint = NULL;
	SafeArrayAccessData(vPointPara.parray, (void**)&t_bufPoint);
	if (1 <= lenPointCount)
	{
		o_ulPointID = t_bufPoint[0];
	}
	SafeArrayUnaccessData(vPointPara.parray);
}

void MainWindow::GetPathPointInfo()
{
	//以取第一条轨迹的第一个点为例
	ULONG uPathID=0;
	GetObjIDByName(80, _T(""), uPathID);

	ULONG uPointID = 0;
	GetFirstPointOfPath(uPathID, uPointID);

	int nType = 1;
	int nPostureCount=0;
	double* dPointPosture = nullptr;
	double dVelocity = 0.0;
	double dSpeedPercent = 0.0;
	int iInstruct = 0;
	int iApproach = 0;
	m_ptrKit->PQAPIGetPointInfo(uPointID, nType, &nPostureCount, &dPointPosture, &dVelocity, &dSpeedPercent, &iInstruct, &iApproach);
	QString result = "PostureCount :";
	result += QString::number(nPostureCount);
	QString Linebreaks = "\n";
	result += Linebreaks;
	result += "Posture :";
	for (int i=0;i<nPostureCount;i++)
	{
		result += QString::number(dPointPosture[i]);
		result += ", ";
	}
	result += Linebreaks;
	result += "Velocity :";
	result += QString::number(dVelocity);
	result += Linebreaks;
	result += "Speedpercent :";
	result += QString::number(dSpeedPercent);
	result += Linebreaks;
	result += "Instruct :";
	switch (iInstruct)
	{
	case(0):
		result += "Line";
		result += Linebreaks;
		break;
	case(1):
		result += "Circle";
		result += Linebreaks;
		break;
	case(2):
		result += "Spline";
		result += Linebreaks;
		break;
	case(3):
		result += "Joint";
		result += Linebreaks;
		break;
	case(4):
		result += "AbsJoint";
		result += Linebreaks;
		break;
	default:
		break;
	}
	result += "Approach :";
	result += QString::number(iApproach);
	result += Linebreaks;
	QMessageBox::information(this,"PointInfo",result);
	m_ptrKit->PQAPIFreeArray((LONG_PTR*)dPointPosture);
}




void MainWindow::CloseFile()
{
	CComBSTR strFilePath = "";
	m_ptrKit->Doc_get_name(&strFilePath);
	m_ptrKit->pq_CloseDocument(strFilePath);
}

void MainWindow::Import3DObj()
{
	CComBSTR strCMD = "RO_CMD_IMPORT_ACCESSORY_PART";
	m_ptrKit->Doc_start_module(strCMD);
}

void MainWindow::Measure()
{
	CComBSTR strCMD = "RO_CMD_MEASUREMENT";
	m_ptrKit->Doc_start_module(strCMD);
}

void MainWindow::ArcBox()
{
	CComBSTR strCMD = "RO_CMD_ARCBALL_PROXY";
	m_ptrKit->Doc_start_module(strCMD);
}

void MainWindow::AlignPart()
{
	CComBSTR strCMD = "RO_CMD_AlignPart3Point";
	m_ptrKit->Doc_start_module(strCMD);
}

void MainWindow::Generatepath()
{
	CComBSTR strCMD = "RO_CMD_EXTERNAL_PATH_GENERATE";
	m_ptrKit->Doc_start_module(strCMD);
}

void MainWindow::Post()
{
	CComBSTR strCMD = "RO_CMD_POST";
	m_ptrKit->Doc_start_module(strCMD);
}

void MainWindow::OnInitializeResult(long lResult)
{
	if (lResult > 0)
	{
		//show kit
		ShowPQKitWindown();
	}
	else
	{
		QString strError = QString::fromLocal8Bit("组件初始化失败:\n");
		switch (lResult)
		{
		case -1:
			strError.append(QString::fromLocal8Bit("未知错误"));
			break;
		case -2:
			strError.append(QString::fromLocal8Bit("初始化超时"));
			break;
		case -3:
		case -4:
		case -6:
			strError.append(QString::fromLocal8Bit("加载组件动态库错误"));
			break;
		case -7:
			strError.append(QString::fromLocal8Bit("组件重复初始化"));
			break;
		case -9:
		case -10:
		case -11:
		case -12:
			strError.append(QString::fromLocal8Bit("内部数据错误"));
			break;
		case -14:
			strError.append(QString::fromLocal8Bit("非开发版账号不能登录开发版"));
			break;
		default:
			strError.append(QString::fromLocal8Bit("错误码: "));
			strError.append(QString::number(lResult, 10));
			break;
		}

		QMessageBox::information(NULL, "PQKit Info", strError, QMessageBox::Yes | QMessageBox::No, QMessageBox::Yes);
	}
}

void MainWindow::OnLoginResult(int i_nLoginType)
{
	if (i_nLoginType < 0)
	{
		QString strError = QString::fromLocal8Bit("用户校验失败:\n");
		switch (i_nLoginType)
		{
		case -1:
			strError.append(QString::fromLocal8Bit("密码错误"));
			break;
		case -2:
			strError.append(QString::fromLocal8Bit("用户不存在"));
			break;
		case -4:
			strError.append(QString::fromLocal8Bit("用户已过期"));
			break;
		case -6:
			strError.append(QString::fromLocal8Bit("网络连接失败"));
			break;
		case -7:
			strError.append(QString::fromLocal8Bit("用户未激活"));
			break;
		case -14:
			strError.append(QString::fromLocal8Bit("账号与产品不匹配"));
			break;
		default:
			strError.append(QString::fromLocal8Bit("错误码: "));
			strError.append(QString::number(i_nLoginType));
			break;
		}

		QMessageBox::information(NULL, "PQKit Info", strError, QMessageBox::Yes | QMessageBox::No, QMessageBox::Yes);

	}
}

void MainWindow::OnElementPickup(unsigned long i_ulObjID, LPWSTR i_lEntityID, int i_nEntityType)
{
	qDebug() << QString::fromLocal8Bit("当前拾取到的元素是： ") << QString::fromWCharArray(i_lEntityID);
}

void MainWindow::OnElementSelection(LPWSTR i_wObjNames, LPWSTR i_wFaceNames, double* i_dPointXYZ, int i_nSize)
{
	qDebug() << QString::fromLocal8Bit("当前拾取到的元素是： ") << QString::fromWCharArray(i_wObjNames);
}

