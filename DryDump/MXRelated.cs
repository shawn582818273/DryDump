namespace DryDump
{
    public class MXRelated
    {
        //public static ActUtlTypeLib.ActUtlTypeClass lpcom_ReferencesUtlType=new ActUtlTypeLib.ActUtlTypeClass();
        public bool MXOpen(ActUtlTypeLib.ActUtlTypeClass lpcom_ReferencesUtlType,int LogicalStationNumber)
        {
            lpcom_ReferencesUtlType.ActLogicalStationNumber = 2;
            int a = lpcom_ReferencesUtlType.Open();
            if (a == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void MXClose(ActUtlTypeLib.ActUtlTypeClass lpcom_ReferencesUtlType)
        {
            lpcom_ReferencesUtlType.Close();
        }
        public int MXReadMemory(ActUtlTypeLib.ActUtlTypeClass lpcom_ReferencesUtlType,string szDeviceName,int iNumberOfData,out short[] arrDeviceValue)
        {
            arrDeviceValue = new short[iNumberOfData];
            int iReturnCode = lpcom_ReferencesUtlType.ReadDeviceBlock2(szDeviceName, iNumberOfData, out arrDeviceValue[0]);
            return iReturnCode;
        }
        public int MXWriteMemory(ActUtlTypeLib.ActUtlTypeClass lpcom_ReferencesUtlType, string szDeviceName, int iNumberOfData, short[] arrDeviceValue)
        {
            int iReturnCode = lpcom_ReferencesUtlType.WriteDeviceBlock2(szDeviceName, iNumberOfData, ref arrDeviceValue[0]);
            return iReturnCode;
        }

    }
}
