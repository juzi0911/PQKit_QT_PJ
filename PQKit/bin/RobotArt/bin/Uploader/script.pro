<?xml version="1.0" encoding="UTF-8"?>
<AuboProgram name="script" installation="default" installationRelativePath="default.ins" directory="/root/arcs_ws/program" createdIn="0.29.1-alpha.1" lastSaveIn="0.29.1-alpha.1" robotSerialNumber="1105102AJ61001" crcValue="225635">
    <kinematics status="NOT_INITIALIZED" validChecksum="false">
        <theta value="3.14159265358979,-1.5707963267949,0,-1.5707963267949,0,0"/>
        <a value="0.0010004,0,0.647,0.601445091,0.000451803,-0.000785351"/>
        <d value="0.1632,0.201467966,-0.000167966,0.000167966,0.10373501,0.094884533"/>
        <alpha value="0,-1.5703531067949,3.13960138358979,3.13793245358979,-1.5694211267949,1.5774796767949"/>
        <jointChecksum value="0,0,0,0,0,0"/>
    </kinematics>
    <WaypointModel/>
    <TimerModel/>
    <VariableModel/>
    <SubProgModel/>
    <InitVariablesNode title="初始变量" id="105044e3-1fdf-4f1b-8736-0b2eb3588be1">
        <datamodel/>
    </InitVariablesNode>
    <MainProgramNode title="机器人编程" id="afad984f-8f57-4f65-bf61-25cc495861fa">
        <datamodel>
            <Bool key="addBeforeStartSequence" value="false"/>
            <Bool key="loopForever" value="true"/>
        </datamodel>
        <children>
            <ScriptNode title="{PQPostFile}" id="e0ff9115-83d8-40cf-8166-f6f10c08b75e">
                <datamodel>
                    <Expression key="expression"/>
                    <String key="file_path" value="{PQPostFile}"/>
                    <Bool key="use_file" value="true"/>
                </datamodel>
            </ScriptNode>
        </children>
    </MainProgramNode>
</AuboProgram>
