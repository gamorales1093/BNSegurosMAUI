using System;
namespace BNSegurosMAUI.Utils
{
    public interface IDeviceUtils
    {
        /*
         * DEVICE INFO STRING FORMAT
         * "OS: [OS type]; OSVersion: [OS version identifier]; Model: [device model]; ID: [device ID]"
         * example: "OS: iOS; OSVersion: 11.2.1; Model: iPhone SE; ID: 8C1F142-93A840091-A354D78B7FB9B6E"
         */
        string GetDeviceInfoString();
    }
}
