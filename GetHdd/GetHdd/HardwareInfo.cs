using System;
using System.Collections.Generic;
using System.Text;
using System.Management;


class HardwareInfo
{
    public static string GetProcessorID()
    {
        string sProcessorID = "";
        string sQuery = "SELECT ProcessorId FROM Win32_Processor";
        ManagementObjectSearcher oManagementObjectSearcher = new ManagementObjectSearcher(sQuery);
        ManagementObjectCollection oCollection = oManagementObjectSearcher.Get();
        foreach (ManagementObject oManagementObject in oCollection)
        {
            sProcessorID = (string)oManagementObject["ProcessorId"];
        }
        // 這樣的話會return 最後一個processor id..
        return (sProcessorID);
    }

    public static string GetHDDSignature()
    {
        
        string sSignatureID = "";
        string sQuery = "SELECT * FROM Win32_LogicalDisk";
        ManagementObjectSearcher oManagementObjectSearcher = new ManagementObjectSearcher(sQuery);
        ManagementObjectCollection oCollection = oManagementObjectSearcher.Get();
        foreach (ManagementObject oManagementObject in oCollection)
        {
            if(oManagementObject["VolumeSerialNumber"] != null)
            {
                //string sDeviceID = oManagementObject["DeviceID"].ToString(); // C:
                sSignatureID = oManagementObject["VolumeSerialNumber"].ToString();
                break;
            }
                
        }
        // return 第一個Signature id..
        return (sSignatureID);
        
    }  
}
