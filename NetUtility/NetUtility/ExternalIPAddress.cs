/******************************************************************
 *  Copyright (c) 2019Microsoft All Rights Reserved.
 *  CLR 版本： 4.0.30319.42000
 *  文件名：    ExternalIPAddress
 *  版本号：    v1.0.0.0
 *  创建人：    Administrator
 *  E-mail：    1306774926@qq.com
 *  创建时间：  2019-01-04 下午 3:05:53
 *  描述：
 *  
 *  
 *  ****************************************************************/
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NetUtility
{
    public class ExternalIPAddress
    {
        public IPAddress GetExternalIPAddress()
        {
            IPHostEntry myIPHostEntry = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress myIPAddress in myIPHostEntry.AddressList)
            {
                byte[] ipBytes = myIPAddress.GetAddressBytes();

                if (myIPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    if (!IsPrivateIP(myIPAddress))
                    {
                        return myIPAddress;
                    }
                }
            }

            return null;
        }


        private bool IsPrivateIP(IPAddress myIPAddress)
        {
            if (myIPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                byte[] ipBytes = myIPAddress.GetAddressBytes();

                // 10.0.0.0/24 
                if (ipBytes[0] == 10)
                {
                    return true;
                }
                // 172.16.0.0/16
                else if (ipBytes[0] == 172 && ipBytes[1] == 16)
                {
                    return true;
                }
                // 192.168.0.0/16
                else if (ipBytes[0] == 192 && ipBytes[1] == 168)
                {
                    return true;
                }
                // 169.254.0.0/16
                else if (ipBytes[0] == 169 && ipBytes[1] == 254)
                {
                    return true;
                }
            }

            return false;
        }


        private bool CompareIpAddress(IPAddress IPAddress1, IPAddress IPAddress2)
        {
            byte[] b1 = IPAddress1.GetAddressBytes();
            byte[] b2 = IPAddress2.GetAddressBytes();

            if (b1.Length == b2.Length)
            {
                for (int i = 0; i < b1.Length; ++i)
                {
                    if (b1[i] != b2[i])
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
