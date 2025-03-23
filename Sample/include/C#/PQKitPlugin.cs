using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


namespace PQKitPlugin
{
    /// <summary>
    /// PQKit plugin class
    /// </summary>
    /// 
    public class CPQKitPlugin
    {
        public IPQPlatformComponent LoadPQKit()
        {
            Guid g = new Guid("32087F6B-AABF-420F-AFEB-5B1EAF4D88F2");
            Type t = Type.GetTypeFromCLSID(g);
            if (t == null)
            {
                return null;
            }
            object resObj = null;
            try
            {
                resObj = Activator.CreateInstance(t);
            }
            catch (Exception ex)
            {
                return null;
            }
            return (IPQPlatformComponent)resObj;
        }

        public IPQComponentUtility LoadPQKitUtility()
        {
            Guid gUtility = new Guid("E9B90F9C-9D0E-4A83-B73B-D48819F10A2D");
            Type tUtility = Type.GetTypeFromCLSID(gUtility);
            if (tUtility == null)
            {
                return null;
            }
            object resUtility = null;
            try
            {
                resUtility = Activator.CreateInstance(tUtility);
            }
            catch (Exception ex)
            {
                return null;
            }
            return (IPQComponentUtility)resUtility;
        }
    }

    //struct
    public struct struct_PQKitOption
    {
        public int nEmbedded;
    }

    public struct struct_PQPostOption
    {
        public int nFirstPtIndex;
        public int nOffsetDir;
        public int nOffsetData;
        public int nPtDataType;
        public int nPtCoor;
        public int nLineIndex;
        public int nOutputIO;
        public int nUseComment;
        public int nIgnorePathState;
    }

    public struct struct_GP3ReconParm
    {
        public uint m_uKSearch;
        public double m_dKRadius;
        public double m_dMu;
        public uint m_iMaxNearestNeighborsNum;
    }


    //error code
    public enum PQErrorCode : uint
    {
        PQ_PARAMETER_MISS_MATCH = 0x84000E00 + 1,
        PQ_FILE_NOT_EXIST = 0x84000E00 + 2,
        PQ_OPEN_FILE_FAIL = 0x84000E00 + 3,
        PQ_LANG_NOT_SUPPORT = 0x84000E00 + 4,
        PQ_INPUT_POINTER_EMPTY = 0x84000E00 + 5,
        PQ_PERMISSION_ERROR = 0x84000E00+ 6,
        PQ_PATH_STATE_ERROR = 0x84000E00 + 7,
        PQ_INSTRUCTION_UNSUPPORT = 0x84000E00 + 8,
        PQ_POST_ERROR = 0x84000E00 + 9
    }

    //enum
    public enum eLoopType
    {
        E_OUTLOOP = 0,
        E_INNERLOOP = 1
    }

    //
    public enum PQPostureType
    {
        QUATERNION = 0,
        EULERANGLEXYZ = 1,
        EULERANGLEZYX = 2,
        EULERANGLEZXZ = 3,
        EULERANGLEZYZ = 4
    }

    //
    public enum PQDataType
    {
        PQ_WORKINGPART = 0x10,
        PQ_ROBOT = 0x20,
        PQ_TOOL = 0x30,
        PQ_SEAT = 0x40,
        PQ_PATH = 0x50,
        PQ_COORD = 0xB0,
        PQ_ACCESSORY_PART = 0xE1,
        PQ_MECHSTATE = 0xF0
    }

    //
    public enum PQPointInstruction
    {
         PQ_LINE = 0x1,
         PQ_CIRCLE = 0x2,
         PQ_SPLINE = 0x3,
         PQ_JOINT = 0x4,
         PQ_ABSJOINT = 0x5
    }

    //
    public enum PQPointStatus
    {
         PQ_NORMAL = 0x00000000,
         PQ_BEREACH = 0x00000002,
         PQ_NOTREACH = 0x00000004,
         PQ_OUTRANGE = 0x00000008,
         PQ_SINGULAR = 0x00000010,
         PQ_UNKNOW = 0x00000020,
         PQ_CONFIGDIFF = 0x00000040
     }
   
    public enum PQAnnotationType
    {
        PQ_UNKNOWN,
        PQ_ANNOTATION_TEXT, //only text
        PQ_ANNOTATION_LINE   //text + line
    }

    public enum PQCoordinateType
    {
        PQ_COORINATE_WORLD = 0,
        PQ_COORINATE_USER = 1,
        PQ_COORINATE_ROBOTBASE = 2,
        PQ_COORINATE_WKT = 3
    }

    public enum PQModelType
    {
        PQ_MODEL_STANFORD_TRIANGLE_MESH_PLY = 0,
        PQ_MODEL_STL = 1
    }

    public enum PQCalibrateType
    {
        CALIBRATE_ROBOT = 0,
        CALIBRATE_WORKPART = 1
    }
    

    public enum PQNormType
    {
        PQNormType_Norm,
        PQNormType_Average,
        PQNormType_TiltAngle,
    }



    #region PQKit callback interface
    [Guid("0637B701-1636-49B6-9DF3-06FD0474D930")]
    [TypeLibType(4288)]
    public interface IPQPlatformComponentCallBack
    {
        [DispId(1)]
        void Fire_Initialize_Result(int lResult);
        [DispId(2)]
        void Fire_RunCMD_Result(int lResult);
        [DispId(3)]
        void Fire_GetData_Result(int lResult);
        [DispId(4)]
        void Notify_Raise_Dockwindow(int i_nType);
        [DispId(5)]
        void Fire_Login_Result(int i_nLoginType);
        [DispId(6)]
        void Fire_Path_Generate_Result(int i_bSuccess, int i_nPathCount, int i_nIndex, uint i_ulPathID);
        [DispId(7)]
        void Fire_Element_Pickup(uint i_ulObjID, [MarshalAs(UnmanagedType.LPWStr)] string i_wsEntityName, int i_nEntityType);
        [DispId(8)]
        void Fire_RButton_Up(int i_lPosX,int i_lPosY);
        [DispId(9)]
        void Fire_LButton_Up(int i_lPosX, int i_lPosY);
        [DispId(10)]
        void Fire_Menu_Pop(uint i_ulObjID, int i_lPosX, int i_lPosY,out int o_nHandled);
        [DispId(11)]
        void Fire_Draw();
        [DispId(12)]
        void Fire_Element_Selection([MarshalAs(UnmanagedType.LPWStr)] string i_wObjNames, [MarshalAs(UnmanagedType.LPWStr)] string i_wFaceNames,  ref double i_dPointXYZ,  int i_nSize);
    }
    #endregion

    public class CPQKitCallbackBase : IPQPlatformComponentCallBack
    {
        public virtual void Fire_Initialize_Result(int lResult)
        {

        }

        public virtual void Fire_RunCMD_Result(int lResult)
        {

        }

        public virtual void Fire_GetData_Result(int lResult)
        {

        }

        public virtual void Notify_Raise_Dockwindow(int i_nType)
        {

        }

        public virtual void Fire_Login_Result(int i_nLoginType)
        {

        }

        public virtual void Fire_Path_Generate_Result(int i_bSuccess, int i_nPathCount, int i_nIndex, uint i_ulPathID)
        {

        }

        public virtual void Fire_Element_Pickup(uint i_ulObjID, [MarshalAs(UnmanagedType.LPWStr)] string i_wsEntityName, 
            int i_nEntityType)
        {

        }

        public virtual void Fire_RButton_Up(int i_lPosX, int i_lPosY)
        {

        }

        public virtual void Fire_LButton_Up(int i_lPosX, int i_lPosY)
        {

        }

        public virtual void Fire_Menu_Pop(uint i_ulObjID, int i_lPosX, int i_lPosY, out int o_nHandled)
        {
            o_nHandled = 0;
        }

        public virtual void Fire_Draw()
        {

        }

        public virtual void Fire_Element_Selection([MarshalAs(UnmanagedType.LPWStr)] string i_wObjNames,
            [MarshalAs(UnmanagedType.LPWStr)] string i_wFaceNames, ref double i_dPointXYZ, int i_nSize)
        {

        }


    }

    #region PQKit option interface
    [Guid("E57F0328-15E9-415B-8BA1-E578F7B34465")]
    [TypeLibType(4288)]
    public interface IPQComponentOption
    {
        [DispId(1)]
        void Set_defaultdock_embedded(bool i_bDefaultDockEmbedded);
        [DispId(2)]
        void Is_defaultdock_embedded(out bool o_bDefaultDockEmbedded);
        [DispId(3)]
        void Set_enable_cefcontext(bool i_bEnableCefContext);
        [DispId(4)]
        void Is_enable_cefcontext(out bool o_bEnableCefContext);
    }
    #endregion

    #region PQKit interface
    [ComConversionLoss]
    [Guid("25686FB1-DA0D-4B16-969B-D9613837CB4D")]
    [TypeLibType(4288)]
    public interface IPQPlatformComponent
    {
        [DispId(1)]
        [PreserveSig]
        uint pq_InitPlatformComponent(IPQPlatformComponentCallBack pCallBack, int hParentHwnd, [MarshalAs(UnmanagedType.BStr)] string bsName, [MarshalAs(UnmanagedType.BStr)] string bsPWD);
        [DispId(2)]
        [PreserveSig]
        uint pq_GetPlatformView(out int hView);
        [DispId(3)]
        [PreserveSig]
        uint pq_RunCommand([MarshalAs(UnmanagedType.BStr)] string bsCommandID, ulong wParam, long lParam, [MarshalAs(UnmanagedType.BStr)] string bsParam, object varParam, out long lResult);
        [DispId(4)]
        [PreserveSig]
        uint pq_GetAllDataObjectsByType(int i_lObjType, [MarshalAs(UnmanagedType.BStr)] out string o_sObjNames, [MarshalAs(UnmanagedType.BStr)] out string o_sObjIDs);
        [DispId(5)]
        [PreserveSig]
        uint Doc_get_obj_joint_count(uint i_ulObjID, out int o_nCount);
        [DispId(6)]
        [PreserveSig]
        uint Doc_get_obj_joints(uint i_ulObjID, out int i_nJointsCount, ref IntPtr o_dJoints);
        [DispId(7)]
        [PreserveSig]
        uint Doc_get_obj_links(uint i_ulObjID, out int o_nLinksCount, ref IntPtr o_dLinks);
        [DispId(8)]
        [PreserveSig]
        uint Doc_get_obj_velocity(uint i_ulObjID, out double o_dVelocity, out double o_dRAD);
        [DispId(9)]
        IntPtr Tool_get_tcp_posture(uint i_ulObjID, [MarshalAs(UnmanagedType.LPWStr)] string i_chTcpName, int i_nPostureType, out int o_nPostureCount);
        [DispId(10)]
        IntPtr Robot_get_end_posture(uint i_ulObjID, int i_nPostureType, out int o_nPostureArraySize);
        [DispId(11)]
        [PreserveSig]
        uint Doc_get_obj_name(uint i_ulObjID, [MarshalAs(UnmanagedType.BStr)] out string o_bsName);
        [DispId(12)]
        IntPtr Doc_get_obj_posture(uint i_ulObjID, int i_nPostureType, out int o_nPostureArraySize);
        [DispId(13)]
        [PreserveSig]
        uint Doc_set_obj_color(uint i_ulObjID, double i_dR, double i_dG, double i_dB);
        [DispId(14)]
        [PreserveSig]
        uint pq_CloseDocument([MarshalAs(UnmanagedType.BStr)]string i_bsDocName);
        [DispId(15)]
        ulong pq_GetModelTreeView();
        [DispId(16)]
        ulong pq_GetDebugTreeView();
        [DispId(17)]
        ulong pq_GetOutPutView();
        [DispId(18)]
        ulong pq_GetRobotControlView();
        [DispId(19)]
        [PreserveSig]
        uint PQAPIImportPointsToPath(uint i_ulRobotID, ref double i_dPosition, ref int i_nInstruct, ref double i_dVelocity, ref double i_dSpeedPercent, ref int i_nApproach, int i_nPointCount, string i_PathName, string i_GroupName, uint i_uCoordinateID, out uint o_PathID, int i_bToolEndPosture = 0);
        [DispId(20)]
        [PreserveSig]
        uint PQAPITransPosition([MarshalAs(UnmanagedType.LPArray, SizeConst = 16, SizeParamIndex = 0)] double[] i_dInputPosition, int i_nPostureType, out int o_nTargetPositionSize, ref IntPtr o_dTargetPosition);
        [DispId(21)]
        [PreserveSig]
        uint PQAPIDeletePathGroup(uint i_ulRobotID, [MarshalAs(UnmanagedType.BStr)] string bsName);
        [DispId(22)]
        [PreserveSig]
        uint PQAPIGetPointsID(uint i_uPathID, out object o_varIDArray);
        [DispId(23)]
        [PreserveSig]
        uint PQAPIGetPointInfo(uint i_uPointID, int i_nPosType, out int o_nPostureCount, ref IntPtr o_dPointPosture, out double o_dVelocity, out double o_dSpeedPercent, out int o_nInstruct, out int o_nApproach);
        [DispId(24)]
        [PreserveSig]
        uint PQAPIAddCustomEvent(ref uint i_uPointsID, int i_nPointCount, uint i_uExecuteObjID, int i_nEventPosition, [MarshalAs(UnmanagedType.BStr)] string i_bsEventName, [MarshalAs(UnmanagedType.BStr)] string i_bsContent);
        [DispId(25)]
        [PreserveSig]
        uint PQAPIPutViewBGC(double i_dFromR, double i_dFromG, double i_dFromB, double i_dToR, double i_dToG, double i_dToB);
        [DispId(26)]
        [PreserveSig]
        uint PQAPIGetObjPositionAttitudeType(uint i_ulObjID, out int o_nType);
        [DispId(27)]
        [PreserveSig]
        uint PQAPISetPathInverType(uint i_uPathID, int i_nType);
        [DispId(28)]
        [PreserveSig]
        uint PQAPISetActiveEngine(uint i_uEngineID);
        [DispId(29)]
        [PreserveSig]
        uint PQAPIGetActiveEngine(out uint o_uEngineID);
        [DispId(30)]
        [PreserveSig]
        uint PQAPIGetRobotJointsFromPoints(uint i_ulPointID, out object o_varJointsArray);
        [DispId(31)]
        [PreserveSig]
        uint PQAPICreateExternalLink(uint i_ulEngineID, uint i_ulGuideID, int i_nAngle, int i_bSyncPosition, uint i_ulPositionerID);
        [DispId(32)]
        [PreserveSig]
        uint PQAPIDeleteExternalLink(uint i_ulEngineID, uint i_ulExternalID, int i_bClearAll);
        [DispId(33)]
        [PreserveSig]
        uint PQAPIGetExternalJointsFromPoints(uint i_ulPointID, uint i_uExternalID, out object o_varJointsArray);
        [DispId(34)]
        [PreserveSig]
        uint PQAPIGetPathFromRobot(uint i_ulRobotID,[MarshalAs(UnmanagedType.BStr)]  out string o_sNames, [MarshalAs(UnmanagedType.BStr)] out string o_sIDs);
        [DispId(35)]
        [PreserveSig]
        uint PQAPIGetPathStatus(uint i_ulPathID, out int o_nStatus);
        [DispId(36)]
        [PreserveSig]
        uint PQAPIGetPointCustomEventInfo(uint i_uID, out int o_bHasCustomEvent, [MarshalAs(UnmanagedType.BStr)] out string o_sName, [MarshalAs(UnmanagedType.BStr)] out string o_sContent, [MarshalAs(UnmanagedType.BStr)] out string o_sPosition);
        [DispId(37)]
        [PreserveSig]
        uint PQAPIAddTransitPath(uint i_uPathAID, uint i_uPathBID, out uint o_PathID);
        [DispId(38)]
        [PreserveSig]
        uint PQAPIAddAbsJointPath(uint i_ulRobotID, ref double i_dRobotJoints, int i_nRJointsCount, ref double i_dGuideJoints, int i_nGJointsCount, ref double i_dPositionerJoints, int i_nPJointsCount, ref double i_dVelocity, ref double i_dSpeedPercent, ref int i_nApproach, int i_nPointCount, uint i_uPathID, out uint o_uPathID);
        [DispId(39)]
        [PreserveSig]
        uint PQAPIDeletePathGroupAll(uint i_ulRobotID);
        [DispId(40)]
        [PreserveSig]
        uint PQAPICreatePathCoordinateRelation(uint i_ulPathID, uint i_ulCoordinateID);
        [DispId(41)]
        [PreserveSig]
        uint PQAPIModifyExternalAxleBatch(ref uint i_ulPathIDs, int i_nPathCount, uint i_ulExternalID, ref double i_dExternalJoints, int i_nJointsCount);
        [DispId(42)]
        [PreserveSig]
        uint PQIKCalInverseKinematics6R(uint i_ulRobotID, ref double i_EndPosture, int i_nEndPostureCount, ref double io_pJointValues, int i_nJointValuesCount, int i_nAxis1Cfg, int i_nAxis2Cfg, int i_nAxis4Cfg, int i_nAxis6Cfg, out int o_pPtStatus);
        [DispId(43)]
        [PreserveSig]
        uint PQAPICreateBoxPart(double i_dLength, double i_dWidth, double i_dHeight, double i_dR, double i_dG, double i_dB, [MarshalAs(UnmanagedType.BStr)] string i_PartName, out uint o_PartID);
        [DispId(44)]
        [PreserveSig]
        uint PQAPICreateCatchEvent(uint i_uPartID, uint i_uPointID, int i_bPrePoint, int i_bCatch, [MarshalAs(UnmanagedType.BStr)] string i_EventName);
        [DispId(45)]
        [PreserveSig]
        uint PQAPIModifyPointPosture(uint i_ulPointID, ref double i_dPosture, int i_nPostureArraySize, int i_nPostureType = 1);
        [DispId(46)]
        [PreserveSig]
        uint PQAPICalibrateObj(uint i_ulObjID, ref double i_dSrcPosition, ref double i_dDesPosition, uint i_uCoordinateID, int i_nTolerancesType, double i_dTolerancesValue, PQCalibrateType i_calibrateType);
        [DispId(47)]
        [PreserveSig]
        uint PQAPIGetWorkPartVertexCount(uint i_ulPartID, out int o_nCount);
        [DispId(48)]
        [PreserveSig]
        uint PQAPIGetWorkPartVertex(uint i_ulPartID, uint i_uCoordinateID, int i_nCount, out double i_dSrcPosition);
        [DispId(49)]
        [PreserveSig]
        uint PQAPIModifyPathGroupIndex(uint i_ulRobotID, [MarshalAs(UnmanagedType.BStr)] string bsSrcName, [MarshalAs(UnmanagedType.BStr)] string bsTarName, int i_bBefore);
        [DispId(50)]
        [PreserveSig]
        uint PQAPIInverseKinematics(uint i_ulRobotID, ref double i_EndPosture, int i_nEndPostureCount, int i_nPostureType, ref double io_pJointValues, int i_nJointValuesCount, ref int i_nAxisCfg, int i_nAxisCfgCount, out int o_pPtStatus, int i_bToolEndPosture = 0);
        [DispId(51)]
        [PreserveSig]
        uint PQAPIInverseKinematicsFanuc(uint i_ulRobotID, ref double i_EndPosture, int i_nEndPostureCount, ref double io_pJointValues, int i_nJointValuesCount, ref int i_nAxisCfg, int i_nAxisCfgCount, out int o_pPtStatus);
        [DispId(52)]
        [PreserveSig]
        uint PQAPISetAxisConfig6R(uint i_uPathID, ref int i_nAxisCfg, int i_nAxisCfgCount);
        [DispId(53)]
        [PreserveSig]
        uint Robot_get_base_coordinate(uint i_ulRobotID, out uint o_uCoordinateID);
        [DispId(54)]
        [PreserveSig]
        uint Path_set_post_coordinate(uint i_ulPathID, uint i_ulCoorID);
        [DispId(55)]
        [PreserveSig]
        uint Path_change_order(uint i_ulSrcPathID, uint i_ulTarPathID, int i_bAhead);
        [DispId(56)]
        [PreserveSig]
        uint Path_feature_set_round(uint i_ulPathID, int i_nTime);
        [DispId(57)]
        [PreserveSig]
        uint Path_modify_external_axis(uint i_ulPathID, ref uint i_ulGuideID, int i_nGuideCount, ref double i_dGuideData, int i_nGuideDataCount);
        [DispId(58)]
        [PreserveSig]
        uint Path_add_entry_point(uint i_ulPathID, double i_dInOffset, double i_dOutOffset, int i_nInInstruction, int i_nOutInstruction);
        [DispId(59)]
        [PreserveSig]
        uint Path_insert_from_point(uint i_ulRobotID, int i_nPtCount, ref double i_dPosition, int i_nPosType, ref int i_nInstruct, ref double i_dVelocity, ref double i_dSpeedPercent, ref int i_nApproach, [MarshalAs(UnmanagedType.BStr)] string i_PathName, [MarshalAs(UnmanagedType.BStr)] string i_GroupName, uint i_uCoordinateID, int i_bToolEndPosture, out uint o_PathID, int i_bIsUpdate = 1);
        [DispId(60)]
        [PreserveSig]
        uint Path_get_group_path(uint i_ulRobotID, [MarshalAs(UnmanagedType.BStr)] string i_GroupName, out object o_sNames, out object o_sIDs);
        [DispId(61)]
        [PreserveSig]
        uint Path_get_point_count(uint i_ulPathID, out int o_nCount);
        [DispId(62)]
        [PreserveSig]
        uint Doc_get_pathgroup_name(uint i_ulRobotID, out object o_varNameArray);
        [DispId(63)]
        [PreserveSig]
        uint Doc_get_obj_bytype(int i_lType, out object o_sNames, out object o_sIDs);
        [DispId(64)]
        [PreserveSig]
        uint Doc_set_obj_visibility(uint i_ulObjID, int i_bShow);
        [DispId(65)]
        [PreserveSig]
        uint Math_trans_posture_to_rotationmatrix(ref double i_dPosture, int i_nPostureArraySize, int i_nPostureType, [MarshalAs(UnmanagedType.LPArray, SizeConst = 16, SizeParamIndex = 0)] double[] o_dTranslation);
        [DispId(66)]
        [PreserveSig]
        uint Path_get_pointgroup_info(uint i_ulPathID, out int o_nPtGroupCount, out object o_arrGroupPtCount);
        [DispId(67)]
        [PreserveSig]
        uint Doc_collide_obj_single(uint i_ulObjAID, uint i_ulObjBID, out int o_bCollide);
        [DispId(68)]
        [PreserveSig]
        uint Doc_collide_obj_multiple(ref uint i_ulObjAIDs, int i_nACount, ref uint i_ulObjBIDs, int i_nBCount, out object o_collideObjs);
        [DispId(69)]
        [PreserveSig]
        uint Doc_set_obj_joints(uint i_ulObjID, ref double i_dJoints, int i_nJointArraySize);
        [DispId(70)]
        [PreserveSig]
        uint Doc_set_obj_links(uint i_ulObjID, ref double i_dLinks, int i_nLinkArraySize);
        [DispId(71)]
        [PreserveSig]
        uint Doc_set_obj_velocity(uint i_ulObjID, double i_dVelocity, double i_dRAD);
        [DispId(72)]
        [PreserveSig]
        uint Tool_set_tcp_posture(uint i_ulObjID, [MarshalAs(UnmanagedType.LPWStr)] string i_chTcpName, ref double i_dTcpPosture, int i_nPostureArraySize, int i_nPostureType);
        [DispId(73)]
        [PreserveSig]
        uint Doc_set_obj_name(uint i_ulObjID, [MarshalAs(UnmanagedType.BStr)] string i_bsName);
        [DispId(74)]
        [PreserveSig]
        uint Doc_set_obj_posture(uint i_ulObjID, ref double i_dPosture, int i_nPostureArraySize, int i_nPostureType);
        [DispId(75)]
        [PreserveSig]
        uint Doc_get_name([MarshalAs(UnmanagedType.BStr)] out string o_sName);
        [DispId(76)]
        [PreserveSig]
        uint Point_modify_poture_batch(uint i_ulPathID, int i_nStartIndex, int i_nEndIndex, ref double i_dPosture, int i_nPostureArraySize, int i_nPostureType);
        [DispId(77)]
        [PreserveSig]
        uint PQAPISetOption(ref struct_PQKitOption i_stuOption);
        [DispId(78)]
        [PreserveSig]
        uint Path_get_generate_face(uint i_ulPathID, out int o_nFaceCount, IntPtr o_lpFacePtr);
        [DispId(79)]
        [PreserveSig]
        uint Path_insert_from_joint(uint i_ulRobotID, ref double i_dRobotJoints, int i_nRobotJointsSize, ref double i_dGuideJoints, int i_nGuideJointsSize, ref double i_dPositionerJoints, int i_nPositionerJointsSize, int i_nPointCount, ref int i_nInstruct, ref double i_dVelocity, ref double i_dSpeedPercent, ref int i_nApproach, [MarshalAs(UnmanagedType.BStr)] string i_PathName, [MarshalAs(UnmanagedType.BStr)] string i_GroupName, uint i_uCoordinateID, out uint o_PathID);
        [DispId(80)]
        [PreserveSig]
        uint Path_get_generate_shape(uint i_ulPathID, out int o_nShapeCount, IntPtr o_lpShapePtr);
        [DispId(81)]
        [PreserveSig]
        uint PQAPIFree(IntPtr i_ptrData);
        [DispId(82)]
        [PreserveSig]
        uint PQAPIFreeArray(IntPtr i_ptrDataArray);
        [DispId(83)]
        [PreserveSig]
        uint Robot_get_tool(uint i_ulRobotID, out uint o_ulToolID);
        [DispId(84)]
        [PreserveSig]
        IntPtr Tool_get_tcp(uint i_ulID, out int o_nTcpCount, [MarshalAs(UnmanagedType.LPWStr)] out string o_chTcpNames);
        [DispId(85)]
        [PreserveSig]
        uint Path_set_tcp(uint i_ulID,[MarshalAs(UnmanagedType.LPWStr)] string i_chTcpName);
        [DispId(86)]
        [PreserveSig]
        uint Path_get_tcp(uint i_ulID, [MarshalAs(UnmanagedType.LPWStr)] out string o_chTcpName);
        [DispId(87)]
        [PreserveSig]
        uint Path_get_post_coordinate(uint i_ulPathID, out uint o_ulCoorID);
        [DispId(88)]
        [PreserveSig]
        uint Path_get_relation_coordinate(uint i_ulPathID, out uint o_ulCoorID);
        [DispId(89)]
        [PreserveSig]
        uint Doc_get_coordinate_posture(uint i_ulCoorID, int i_nPostureType, out int o_nPostureArraySize, ref IntPtr o_dPosture);
        [DispId(90)]
        [PreserveSig]
        uint Doc_add_sim_collide_data(ref uint i_vCollisionCheckIDS, int i_nCheckIDSCount, ref uint i_vCollisionWithIDS, int i_nWithIDSCount);
        [DispId(91)]
        [PreserveSig]
        uint Doc_clear_sim_collide_data();
        [DispId(92)]
        [PreserveSig]
        uint Doc_set_dockwindow_visible(int i_nDockType, int i_bVisible);
        [DispId(93)]
        [PreserveSig]
        uint pq_CloseComponent();
        [DispId(94)]
        [PreserveSig]
        uint Point_get_posture_batch(uint i_ulPathID, int i_nStartIndex, int i_nCount, int i_nPostureType, uint i_uCoordinateID, out int o_nPostureArraySize,ref IntPtr o_dPosture);
        [DispId(95)]
        [PreserveSig]
        uint Doc_obj_rotate_local_X(uint i_ulObjID, double i_dRAD);
        [DispId(96)]
        [PreserveSig]
        uint Doc_obj_rotate_local_Y(uint i_ulObjID, double i_dRAD);
        [DispId(97)]
        [PreserveSig]
        uint Doc_obj_rotate_local_Z(uint i_ulObjID, double i_dRAD);
        [DispId(98)]
        [PreserveSig]
        uint Doc_create_coordinate(uint i_ulBaseCoordinateID, int i_nPostureType, ref double i_dPosture, int i_nPostureSize,[MarshalAs(UnmanagedType.LPWStr)] string i_chName);
        [DispId(99)]
        [PreserveSig]
        uint Doc_get_current_selected_obj(out int o_nSelectedCount, ref IntPtr o_ulSelectedObjIDs);
        [DispId(100)]
        [PreserveSig]
        uint Path_add_send_event(ref uint i_uPointsID, int i_nPointCount, uint i_uExecuteObjID, int i_nEventPosition,[MarshalAs(UnmanagedType.LPWStr)] string i_bsEventName);
        [DispId(101)]
        [PreserveSig]
        uint Path_add_waittime_event(ref uint i_uPointsID, int i_nPointCount, uint i_uExecuteObjID, int i_nEventPosition, double i_dWaitTime,[MarshalAs(UnmanagedType.LPWStr)] string i_bsEventName);
        [DispId(102)]
        [PreserveSig]
        uint Path_add_wait_event(ref uint i_uPointsID, int i_nPointCount, uint i_uExecuteObjID, int i_nEventPosition, [MarshalAs(UnmanagedType.LPWStr)] string i_bsEventName, [MarshalAs(UnmanagedType.LPWStr)] string i_bsWaitEventName);
        [DispId(103)]
        [PreserveSig]
        uint App_get_option(out IPQComponentOption o_ptrOption);
        [DispId(104)]
        [PreserveSig]
        uint Part_check_edge_in_face(uint i_ulFacePartID, uint i_ulEdgePartID, out bool o_bIn);
        [DispId(105)]
        [PreserveSig]
        uint Part_get_extreme_point_of_edge(uint uEdgeID, out int o_nPointCount, ref IntPtr o_dStartAndEndPoint);
        [DispId(106)]
        [PreserveSig]
        uint Part_get_face(uint i_uPartID, out int o_nFaceCount, ref IntPtr o_vFaceId);
        [DispId(107)]
        [PreserveSig]
        uint Part_get_edge(uint i_uPartID, eLoopType i_EedgeType, out int o_nEdgeCount, ref IntPtr o_uEdgeId);
        [DispId(108)]
        [PreserveSig]
        uint Part_get_face_normal(uint i_uFacePartID, out double o_dx, out double o_dy, out double o_dz);
        [DispId(109)]
        [PreserveSig]
        uint Part_get_face_of_edge(uint i_ulEdgeID, out int o_nFaceCount, ref IntPtr o_ulFaceIds);
        [DispId(110)]
        [PreserveSig]
        uint Doc_get_objtype_by_id(uint i_ulObjID, out int o_IobjType);
        [DispId(111)]
        [PreserveSig]
        uint Path_get_robot(uint i_ulPathID, out uint o_uRobotID);
        [DispId(112)]
        [PreserveSig]
        uint Robot_get_fl_in_point(uint i_ulPointID, PQPostureType i_nPostureType, out int o_nPostureArraySize, ref IntPtr o_dPosture);
        [DispId(113)]
        [PreserveSig]
        uint Doc_open_robx([MarshalAs(UnmanagedType.LPWStr)] string i_whRobxPath);
        [DispId(114)]
        [PreserveSig]
        uint Doc_save_robx();
        [DispId(115)]
        [PreserveSig]
        uint Doc_saveas_robx([MarshalAs(UnmanagedType.LPWStr)] string i_whRobxPath);
        [DispId(116)]
        [PreserveSig]
        uint Path_set_velocity_batch(ref uint i_uPointsID, int i_nPathIDArraySize, double i_dInitWorkingVel, double i_dJointWorkingVel, double i_dJointSpeedPercent);
        [DispId(117)]
        [PreserveSig]
        uint Path_get_velocity(uint i_ulPathID, out double i_dInitWorkingVel, out double i_dJointWorkingVel, out double i_dJointSpeedPercent);
        [DispId(118)]
        [PreserveSig]
        uint Point_get_parent_path(uint i_uPointID, out uint o_uPathID);
        [DispId(119)] 
        [PreserveSig]
        uint Robot_insert_pos_point(uint i_ulRobotID, PQPointInstruction i_eInstruction, [MarshalAs(UnmanagedType.LPWStr)] string i_whPathName, out uint o_PathID);
        [DispId(120)] 
        [PreserveSig]
        uint Part_insert_pos_point(uint i_ulPartID, [MarshalAs(UnmanagedType.LPWStr)] string i_whPathName, out uint o_PathID);
        [DispId(121)] 
        [PreserveSig]
        uint Doc_get_collide_result(double i_dMInDistance, ref IntPtr o_ulObjAIDs, out int o_nObjCount);
	      [DispId(122)] 
	      [PreserveSig]
        uint Doc_set_collide_data(ref uint i_vCollisionCheckIDS, int i_nCheckIDSCount, ref uint i_vCollisionWithIDS, int i_nWithIDSCount);
	      [DispId(123)] 
	      [PreserveSig]
        uint Doc_clear_collide_data();
	      [DispId(124)] 
	      [PreserveSig]
        uint Doc_update_view();
        [DispId(125)]
        [PreserveSig]
        uint Doc_insert_annotation([MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] i_dBasePoint, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] i_dTextPoint, uint i_ulCoordinateID,[MarshalAs(UnmanagedType.LPWStr)] string i_whText, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] i_dTextColor, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] i_dBaseColor, PQAnnotationType i_eType, double i_dFontSize,int i_bFrame, out uint o_AnnotationID, uint i_uObjID);
        [DispId(126)]
        [PreserveSig]
        uint Annotation_modify_text(uint i_ulAnnotationId,[MarshalAs(UnmanagedType.LPWStr)] string i_whEditText);
        [DispId(127)] 
        [PreserveSig]
        uint Doc_get_coordinate(PQCoordinateType i_eType, out int o_nCount, [MarshalAs(UnmanagedType.LPWStr)] out string o_whCoordinateNames, ref IntPtr o_nCoordinateNameLengthArray, ref IntPtr o_uCoordinateIDs);
        [DispId(128)] 
        [PreserveSig]
        uint Doc_start_module([MarshalAs(UnmanagedType.LPWStr)] string i_whModuleID);
        [DispId(129)] 
        [PreserveSig]
        uint Doc_end_module([MarshalAs(UnmanagedType.LPWStr)] string i_whModuleID);
        [DispId(130)] 
        [PreserveSig]
        uint View_draw_point([MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] i_PtPosition,  uint i_ulCoordinateID,  int i_lPtSize,  int i_lPtColor, [MarshalAs(UnmanagedType.LPWStr)] string i_wsText,  int i_lTextColor);
        [DispId(131)]
        [PreserveSig]
        uint Part_get_vertex_position(uint i_ulPartID,[MarshalAs(UnmanagedType.LPWStr)] string i_whVertextName, uint i_ulCoordinateID, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] o_dPosition);
        [DispId(132)]
        [PreserveSig]
        uint Path_set_transition(uint i_ulPointIDStart, double i_dXStart, double i_dYStart, double i_dZStart,
            uint i_ulPointIDEnd, double i_dXEnd, double i_dYEnd, double i_dZEnd);
        [DispId(133)] 
        [PreserveSig]
        uint Math_is_three_point_collinear(ref double i_dPoint1, ref double i_dPoint2, ref double i_dPoint3, out int o_bIsColliner);
        [DispId(134)] 
        [PreserveSig]
        uint Math_is_calibrate_point_less_distance(ref double i_dSrcPosition, ref double i_dDesPosition, double i_dSafetyValue, out int o_bIsLess);
        [DispId(135)] 
        [PreserveSig]
        uint Part_get_ray_surface_intersetion([MarshalAs(UnmanagedType.LPWStr)] string i_wSurfaceName, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] i_dPointCoor, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] i_dPointVec, ref IntPtr o_dIntersetionPoint,out int o_nArrySize);
        [DispId(136)] 
        [PreserveSig]
        uint Part_cheak_point_on_surface([MarshalAs(UnmanagedType.LPWStr)] string i_wSurfaceName, [MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] i_dPointCoor, double i_dTol, out int o_bPointonSuraface);
        [DispId(137)] 
        [PreserveSig]
        uint Robot_set_custom_post(uint i_ulRobotID, [MarshalAs(UnmanagedType.LPWStr)] string i_whPostFile);
        [DispId(138)] 
        [PreserveSig]
        uint Part_get_path(uint i_ulPartID, out int o_nCount, [MarshalAs(UnmanagedType.LPWStr)] out string o_whPathNames, ref IntPtr o_uPathIDs);
        [DispId(139)]
        [PreserveSig]
        uint Math_trans_vector_to_posture([MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] i_dPoint, 
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] i_dNormalVector,
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 3, SizeParamIndex = 0)] double[] i_dTangentVector, 
            int i_nPostureType, IntPtr o_dPosture, out int o_nPostureArraySize);
        [DispId(140)]
        [PreserveSig]
        uint Path_Reverse(ref uint i_ulPathIDs, int i_nPathIDArraySize);
        [DispId(141)]
        [PreserveSig]
        uint Path_Rotate(ref uint i_ulPathIDs, int i_nPathIDArraySize, double i_dX, double i_dY, double i_dZ);
        [DispId(142)]
        [PreserveSig]
        uint Path_Translation(ref uint i_ulPathIDs, int i_nPathIDArraySize, double i_dX, double i_dY, double i_dZ);
        [DispId(143)]
        [PreserveSig]
        uint Part_Multi_Point_Calibrate(uint i_ulPartID, [MarshalAs(UnmanagedType.LPArray, SizeConst = 15, SizeParamIndex = 0)] double[] i_dSrcPosition,
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 15, SizeParamIndex = 0)] double[] i_dDesPosition, uint i_uCoordinateID);
        [DispId(144)]
        [PreserveSig]
        uint Doc_set_floor_color(double i_dR, double i_dG, double i_dB);
        [DispId(145)]
        [PreserveSig]
        uint Part_export_ply(uint i_ulObjID, uint i_ulObjVisualID, [MarshalAs(UnmanagedType.LPWStr)] string i_wsFileName);
        [DispId(146)]
        [PreserveSig]
        uint Doc_setup_workingpart(uint i_ulWorkingPart);
        [DispId(147)]
        [PreserveSig]
        uint Path_add_spray_event(uint i_uPointsID,int i_nPointCount,uint i_uExecuteObjID,int i_nEventPosition, [MarshalAs(UnmanagedType.LPWStr)] string i_bsEventName,uint i_ulNozzleID,uint i_ulAssistorID);
        [DispId(148)]
        [PreserveSig]
        uint Doc_get_nozzle(uint i_ulRobotID, out int o_nSize, [MarshalAs(UnmanagedType.LPWStr)] out string o_sNames, ref IntPtr o_sIDs);
        [DispId(149)]
        [PreserveSig]
        uint Doc_get_assistor(uint i_ulNozzleID, out int o_nSize, [MarshalAs(UnmanagedType.LPWStr)] out string o_sNames, ref IntPtr o_sIDs);
        [DispId(150)]
        [PreserveSig]
        uint Doc_set_coordinate_posture(uint i_ulCoorID, int i_nPostureType, ref double i_dPosture, int i_nPostureSize,int i_bUpdatePost);
        [DispId(151)]
        [PreserveSig]
        uint Doc_catch_obj(uint i_uEngineID, uint i_ulPartID);
        [DispId(152)]
        [PreserveSig]
        uint Doc_release_obj(uint i_uEngineID, uint i_ulPartID);
        [DispId(153)]
        [PreserveSig]
        uint Doc_get_selected_annotation_position(out int o_nPointCount, ref IntPtr o_dBasePoint, ref IntPtr o_dTextPoint, ref IntPtr o_ulAnnotationIDs);
        [DispId(154)]
        [PreserveSig]
        uint Path_add_SynSetting_event(uint i_uPointsID, int i_nPointCount, uint i_uExecuteObjID, int i_nEventPosition,
            string i_bsEventName, ref uint i_uSynSettingEngineID, int i_nSynSettingEngineCount);
        [DispId(155)]
        [PreserveSig]
        uint Doc_synergism_set(ref uint i_ulObjIDs, int i_nCount, [MarshalAs(UnmanagedType.LPWStr)] string i_whSystemName);
        [DispId(156)]
        [PreserveSig]
        uint Doc_set_synergism_instruction(uint i_ulPathID, uint i_ulObjIDSyn, [MarshalAs(UnmanagedType.LPWStr)] string i_whSystemName);
        [DispId(157)]
        [PreserveSig]
        uint Doc_set_synergism_velocity(uint i_ulPathID, uint i_ulObjIDSyn);
        [DispId(158)]
        [PreserveSig]
        uint Kit_init_post_option_struct(ref struct_PQPostOption i_stPostOption);
        [DispId(159)]
        [PreserveSig]
        uint Robot_post_file(uint i_ulRobotID, [MarshalAs(UnmanagedType.LPWStr)] string i_whPostFilePath, [MarshalAs(UnmanagedType.LPWStr)] string i_whPostFileName, [MarshalAs(UnmanagedType.LPWStr)] string i_whUseTCP, ref struct_PQPostOption i_stPostOption);
        [DispId(160)]
        [PreserveSig]
        uint Path_add_positioning(uint i_ulRobotID, [MarshalAs(UnmanagedType.LPWStr)] string i_wSurfaceName, ref double i_dPoint, int i_dPointsize, PQNormType i_pqNormaltype, double i_dTiltAngle, double i_dHeight, double i_dOffset, double i_dDepth, [MarshalAs(UnmanagedType.LPWStr)] string i_whGroupName, PQPointInstruction i_eInstruction, ref IntPtr o_ulPathID);
        [DispId(161)]
        [PreserveSig]
        uint Path_insert_from_point_with_sameproperty(uint i_ulRobotID, int i_nPtCount, ref double i_dPosition, int i_nPosType, int i_nInstruct, double i_dVelocity, double i_dSpeedPercent, int i_nApproach, [MarshalAs(UnmanagedType.BStr)] string i_PathName, [MarshalAs(UnmanagedType.BStr)] string i_GroupName, uint i_uCoordinateID, bool i_bToolEndPosture, bool i_bIsUpdate, ref IntPtr o_PathID);
        [DispId(162)]
        [PreserveSig]
        uint Path_get_point_id(uint i_ulPathID, out int o_nPointsCount, ref IntPtr o_ulPointIDs);
        [DispId(163)]
        [PreserveSig]
        uint  Point_get_posture_in_path_tcp_coordinate(uint i_ulPointID, int i_nPostureType, out int o_nPostureCount, ref IntPtr o_dPointPosture);
        [DispId(164)]
        [PreserveSig]
        uint Point_get_posture_in_path_tcp_coordinate_unsafe(uint i_ulPointID, int i_nPostureType, ref double i_dPointPosture, int i_nPostureArraySize);
        [DispId(165)]
        [PreserveSig]
        uint Path_modify_line_color(uint i_ulPathID, long i_lColor);
        [DispId(166)]
        [PreserveSig]
        uint Path_show_name(uint i_ulPathID, int i_bShow);
        [DispId(167)]
        [PreserveSig]
        uint Path_modify_line_width(uint i_ulPathID, double i_dLineWidth);
        [DispId(168)]
        [PreserveSig]
        uint Point_modify_velocity_and_approch(uint i_ulPointID, double i_dVelocity, int i_nApproach);
        [DispId(169)]
        [PreserveSig]
        uint Robot_Get_All_Inverse_Kinematics( uint i_ulRobotID,  ref double i_EndPosture,  int i_nPostureArraySize,  int i_nPostureType,  int b_IsEndPosture, ref IntPtr io_pJointValues, out int o_nJointValuesCount, ref IntPtr o_nAxisCfg, out int o_nAxisCfgCount);
        [DispId(170)]
        [PreserveSig]
        uint Part_get_extreme_point_of_edge_by_name([MarshalAs(UnmanagedType.LPWStr)] string i_wEdgeName,  int i_nPostureType, out int o_nStartPointCount, out int o_nEndPointCount, ref IntPtr o_dStartPoint, ref IntPtr o_dEndPoint);
        [DispId(171)]
        [PreserveSig]
        uint Robot_inverse_kinematics_with_external_axis( uint i_ulRobotID,  int i_nCalInveType,  ref double i_EndPosture,  int i_nPostureArraySize,  int i_nPostureType,  int b_IsEndPosture,  ref double io_pJointValues,  int o_nJointValuesCount,  ref double io_ExtJointRadValues,  int i_nExtJointRadValuesCount,  ref double io_GuideRadValues,  int i_nGuideValuesCount, out int o_pPtStatus);
        [DispId(172)]
        [PreserveSig]
        uint Doc_import_accessory_part_with_posture([MarshalAs(UnmanagedType.LPWStr)] string i_wsAbsPath, [MarshalAs(UnmanagedType.LPWStr)] string i_wsFileType,  ref double i_dPosture,  int i_nPostureArraySize,  int i_nPostureType,  uint i_ulCoorID, out uint o_ulObjId);
        [DispId(173)]
        [PreserveSig]
        uint Doc_init_gp3reconstructionparm( ref struct_GP3ReconParm i_sGp3ReconPram);
        [DispId(174)]
        [PreserveSig]
        uint Doc_pointcloud_gp3reconstruction( uint i_ulObjID,  ref struct_GP3ReconParm i_sGp3ReconPram, out uint o_ulMeshObjID);
        [DispId(175)]
        [PreserveSig]
        uint Robot_get_joints_count( uint i_ulRobotID, out int o_nJointsCount);
        [DispId(176)]
        [PreserveSig]
        uint Robot_get_type( uint i_ulRobotID, [MarshalAs(UnmanagedType.LPWStr)] out string o_wsRobotType);
        [DispId(177)]
        [PreserveSig]
        uint Doc_set_save_entityId_flag( int i_bIsSava);
        [DispId(178)]
        [PreserveSig]
        uint Doc_Export_Model([MarshalAs(UnmanagedType.LPWStr)] string i_sFilePath,  ref uint i_uObjIDs,  int i_nObjCount);
        [DispId(179)]
        [PreserveSig]
        uint Doc_Draw_Line( ref double i_dLineStart,  ref double i_dLineEnd,  double i_dRadius,  ref double i_dColorRGB);

    }
    #endregion

    #region PQKit Utility interface
    [Guid("70D66320-0D8B-477B-B1C9-0AE07C04FC30")]
    [TypeLibType(4288)]
    public interface IPQComponentUtility
    {
        [DispId(1)]
        void License_get_hardkey_info([MarshalAs(UnmanagedType.LPWStr)] out string o_wsKeyInfo, [MarshalAs(UnmanagedType.LPWStr)] out string o_wsStatusMsg);
        [DispId(2)]
        void App_set_language([MarshalAs(UnmanagedType.LPWStr)] string i_chLang);
    }
    #endregion
}
