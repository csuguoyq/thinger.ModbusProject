
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thinger.ModbusRTULib
{
    public class ModbusRTU
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ModbusRTU()
        {
            serialPort = new SerialPort();
        }
        //串口通信对象
        private SerialPort serialPort;
        public int ReadTimeOut { get; set; } = 2000;
        public int WriteTimeOut { get; set; } = 2000;
        public int SleepTime { get; set; } = 10;
        public int ReceiveTimeOut { get; set; } = 5000;
        private bool dtrEnable = false;
        public bool DtrEnable
        {
            get { return dtrEnable; }
            set { dtrEnable = value;
                this.serialPort.DtrEnable = dtrEnable;
            }
        }
        private bool rtsEnable = false;
        public bool RtsEnable
        {
            get { return rtsEnable; }
            set {  rtsEnable = value;
                this.serialPort.RtsEnable = rtsEnable;
            }
        }
        /// <summary>
        /// 连接串口
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <returns></returns>
        public bool Connect(string portName,int baudRate=9600,Parity parity=Parity.None, int dataBits=8, StopBits stopBits=StopBits.None)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
                return false;
            }
            serialPort.BaudRate=baudRate;
            serialPort.PortName=portName;
            serialPort.Parity=parity;
            serialPort.DataBits=dataBits;
            serialPort.StopBits=stopBits;

            serialPort.ReadTimeout=ReadTimeOut;
            serialPort.WriteTimeout=WriteTimeOut;
            try
            {
                serialPort.Open();
            }
            catch (Exception ex) {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if(serialPort != null && serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
        #region 01H读取输出线圈
        /// <summary>
        /// 读取输出线圈
        /// </summary>
        /// <param name="slaveId">站地址</param>
        /// <param name="start">起始线圈地址</param>
        /// <param name="length">长度</param>
        /// <returns>返回数据</returns>
        public byte[] ReadOutputCoils(byte slaveId,ushort start,ushort length)
        {
            //第一步：拼接报文
            List<byte> SendCommand = new List<byte>();
            //从站地址
            SendCommand.Add(slaveId);
            //功能码
            SendCommand.Add(0x01);
            //开始地址
            SendCommand.Add((byte)(start / 256));
            SendCommand.Add((byte)(start % 256));
            //数量
            SendCommand.Add((byte)(length % 256));
            SendCommand.Add((byte)(length % 256));
            //CRC
            byte[] crc = Crc16(SendCommand.ToArray(),SendCommand.Count);
            SendCommand.AddRange(crc);
            //第二步：发送报文
            //第三步：接收报文
            byte[] receive = null;
            int byteLength = length%8 ==0 ? length % 8 : length%8 + 1;
            if (SendAndReceive(SendCommand.ToArray(),ref receive)) {
                //第四步：验证报文
                if (CheckCRC(receive) && receive.Length == 5 + byteLength)
                {
                    if (receive[0] == slaveId && receive[1] == 0x01 && receive[2] == byteLength)
                    {
                        // 第五步：解析报文
                        byte[] result = new byte[byteLength];
                        Array.Copy(receive,3,result,0,byteLength);
                        return result;
                    }
                }
            }
            return null;
        }
        #endregion
        #region 02H读取输入线圈
        /// <summary>
        /// 读取输入线圈
        /// </summary>
        /// <param name="slaveId">站地址</param>
        /// <param name="start">起始线圈地址</param>
        /// <param name="length">长度</param>
        /// <returns>返回数据</returns>
        public byte[] ReadInputCoils(byte slaveId, ushort start, ushort length)
        {
            //第一步：拼接报文
            List<byte> SendCommand = new List<byte>();
            //从站地址
            SendCommand.Add(slaveId);
            //功能码
            SendCommand.Add(0x02);
            //开始地址
            SendCommand.Add((byte)(start / 256));
            SendCommand.Add((byte)(start % 256));
            //数量
            SendCommand.Add((byte)(length % 256));
            SendCommand.Add((byte)(length % 256));
            //CRC
            byte[] crc = Crc16(SendCommand.ToArray(), SendCommand.Count);
            SendCommand.AddRange(crc);
            //第二步：发送报文
            //第三步：接收报文
            byte[] receive = null;
            int byteLength = length % 8 == 0 ? length % 8 : length % 8 + 1;
            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {
                //第四步：验证报文
                if (CheckCRC(receive) && receive.Length == 5 + byteLength)
                {
                    if (receive[0] == slaveId && receive[1] == 0x02 && receive[2] == byteLength)
                    {
                        // 第五步：解析报文
                        byte[] result = new byte[byteLength];
                        Array.Copy(receive, 3, result, 0, byteLength);
                        return result;
                    }
                }
            }
            return null;
        }
        #endregion
        #region 03H读取输出寄存器
        public byte[] ReadOutputRegisters(byte slaveId, ushort start, ushort length)
        {
            //拼接报文
            List<byte> SendCommand = new List<byte>();
            //从站地址
            SendCommand.Add(slaveId);
            //功能码
            SendCommand.Add(0x03);
            //开始地址
            SendCommand.Add((byte)(start / 256));
            SendCommand.Add((byte)(start % 256));
            //数量
            SendCommand.Add((byte)(length % 256));
            SendCommand.Add((byte)(length % 256));
            byte[] crc = Crc16(SendCommand.ToArray(), SendCommand.Count);
            SendCommand.AddRange(crc);
            // 第二步：发送报文
            //第三步：接收报文
            byte[] receive = null;
            int byteLength = length * 2;
            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {
                //第四步：验证报文
                if (CheckCRC(receive) && receive.Length == 5 + byteLength)
                {
                    if (receive[0] == slaveId && receive[1] == 0x03 && receive[2] == byteLength)
                    {
                        // 第五步：解析报文
                        byte[] result = new byte[byteLength];
                        Array.Copy(receive, 3, result, 0, byteLength);
                        return result;
                    }
                }
            }
            return null;
        }
        #endregion
        #region 04H读取输入寄存器
        public byte[] ReadInputRegisters(byte slaveId, ushort start, ushort length)
        {
            //拼接报文
            List<byte> SendCommand = new List<byte>();
            //从站地址
            SendCommand.Add(slaveId);
            //功能码
            SendCommand.Add(0x04);
            //开始地址
            SendCommand.Add((byte)(start / 256));
            SendCommand.Add((byte)(start % 256));
            //数量
            SendCommand.Add((byte)(length % 256));
            SendCommand.Add((byte)(length % 256));
            byte[] crc = Crc16(SendCommand.ToArray(), SendCommand.Count);
            SendCommand.AddRange(crc);
            //第二步：发送报文
            //第三步：接收报文
            byte[] receive = null;
            int byteLength = length * 2;
            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {
                //第四步：验证报文
                if (CheckCRC(receive) && receive.Length == 5 + byteLength)
                {
                    if (receive[0] == slaveId && receive[1] == 0x04 && receive[2] == byteLength)
                    {
                        // 第五步：解析报文
                        byte[] result = new byte[byteLength];
                        Array.Copy(receive, 3, result, 0, byteLength);
                        return result;
                    }
                }
            }
            return null;
        }
        #endregion
        #region  05H预置单线圈
        public bool PreSetSingleCoil(byte slaveId, ushort start, bool value)
        {
            //拼接报文
            List<byte> SendCommand = new List<byte>();
            //从站地址
            SendCommand.Add(slaveId);
            //功能码
            SendCommand.Add(0x05);
            //开始地址
            SendCommand.Add((byte)(start / 256));
            SendCommand.Add((byte)(start % 256));
            //数量
            SendCommand.Add(value?(byte)0xFF:(byte)0x00);
            SendCommand.Add(0x00);
            byte[] crc = Crc16(SendCommand.ToArray(), SendCommand.Count);
            SendCommand.AddRange(crc);
            //第二步：发送报文
            //第三步：接收报文
            byte[] receive = null;
            int byteLength = 8;
            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {
                //第四步：验证报文
                if (CheckCRC(receive) && receive.Length == 8)
                {
                    return ByteArrayEquals(SendCommand.ToArray(), receive);
                }
            }
            return false;
        }
        #endregion
        #region  06H预置单寄存器
        /// <summary>
        /// 预置单寄存器
        /// </summary>
        /// <param name="slaveId">站地址</param>
        /// <param name="start">寄存器地址</param>
        /// <param name="value">字节数组（2个字节）</param>
        /// <returns></returns>
        public bool PreSetSingleRegister(byte slaveId, ushort start, byte[] value)
        {
            //拼接报文
            List<byte> SendCommand = new List<byte>();
            //从站地址
            SendCommand.Add(slaveId);
            //功能码
            SendCommand.Add(0x06);
            //开始地址
            SendCommand.Add((byte)(start / 256));
            SendCommand.Add((byte)(start % 256));
            //数量
            SendCommand.AddRange(value);
            SendCommand.Add(0x00);
            byte[] crc = Crc16(SendCommand.ToArray(), SendCommand.Count);
            SendCommand.AddRange(crc);
            //第二步：发送报文
            //第三步：接收报文
            byte[] receive = null;
            int byteLength = 8;
            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {
                //第四步：验证报文
                if (CheckCRC(receive) && receive.Length == 8)
                {
                    return ByteArrayEquals(SendCommand.ToArray(), receive);
                }
            }
            return false;
        }
        /// <summary>
        /// 预置单寄存器
        /// </summary>
        /// <param name="slaveId">站地址</param>
        /// <param name="start">寄存器地址</param>
        /// <param name="value">short类型</param>
        /// <returns></returns>
        public bool PreSetSingleRegister(byte slaveId, ushort start, short value)
        {
            return PreSetSingleRegister(slaveId, start, BitConverter.GetBytes(value).Reverse().ToArray());
        }
        /// <summary>
        /// 预置单寄存器
        /// </summary>
        /// <param name="slaveId">站地址</param>
        /// <param name="start">寄存器地址</param>
        /// <param name="value">ushort类型</param>
        /// <returns></returns>
        public bool PreSetSingleRegister(byte slaveId, ushort start, ushort value)
        {
            return PreSetSingleRegister(slaveId, start, BitConverter.GetBytes(value).Reverse().ToArray());
        }
        #endregion
        #region 预置多线圈
        public bool PreSetMutiCoils(byte slaveId, ushort start, bool[] value)
        {
            //拼接报文
            List<byte> SendCommand = new List<byte>();
            byte[] setArray = GetByteArrayFromBoolArray(value);
            //从站地址
            SendCommand.Add(slaveId);
            //功能码
            SendCommand.Add(0x0F);
            //开始地址
            SendCommand.Add((byte)(start / 256));
            SendCommand.Add((byte)(start % 256));
            SendCommand.Add((byte)(value.Length / 256));
            SendCommand.Add((byte)(value.Length % 256));
            //数量
            SendCommand.Add((byte)setArray.Length);
            SendCommand.AddRange(setArray);
            byte[] crc = Crc16(SendCommand.ToArray(), SendCommand.Count);
            SendCommand.AddRange(crc);
            //第二步：发送报文
            //第三步：接收报文
            byte[] receive = null;
            int byteLength = 8;
            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {
                //第四步：验证报文
                if (CheckCRC(receive) && receive.Length == 8)
                {
                    //验证发送和接收的前6个字节是否相同
                    for(int i = 0;i<6;i++)
                    {
                        if (SendCommand[i] != receive[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        #endregion
        #region 10H预置多线圈
        /// <summary>
        /// 预置多线圈
        /// </summary>
        /// <param name="slaveId"></param>
        /// <param name="start"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public bool PreSetMutiRegisters(byte slaveId, ushort start, byte[] values)
        {
            //拼接报文
            if(values==null || values.Length == 0 || values.Length %2 == 1)
            {
                return false;
            }
            List<byte> SendCommand = new List<byte>();
            int RegisterLength = values.Length / 2;
            //从站地址
            SendCommand.Add(slaveId);
            //功能码
            SendCommand.Add(0x10);
            //开始地址
            SendCommand.Add((byte)(start / 256));
            SendCommand.Add((byte)(start % 256));
            SendCommand.Add((byte)(RegisterLength / 256));
            SendCommand.Add((byte)(RegisterLength % 256));
            //数量
            SendCommand.Add((byte)values.Length);
            SendCommand.AddRange(values);
            byte[] crc = Crc16(SendCommand.ToArray(), SendCommand.Count);
            SendCommand.AddRange(crc);
            //第二步：发送报文
            //第三步：接收报文
            byte[] receive = null;
            int byteLength = 8;
            if (SendAndReceive(SendCommand.ToArray(), ref receive))
            {
                //第四步：验证报文
                if (CheckCRC(receive) && receive.Length == 8)
                {
                    //验证发送和接收的前6个字节是否相同
                    for (int i = 0; i < 6; i++)
                    {
                        if (SendCommand[i] != receive[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
        #endregion
        /// <summary>
        ///  发送并接受方法
        /// </summary>
        /// <param name="send"></param>
        /// <param name="receive"></param>
        /// <returns></returns>
        private bool SendAndReceive(byte[] send,ref byte[] receive)
        {
            try
            {
                //发送报文
                this.serialPort.Write(send, 0, send.Length);
                //定义一个Buffer
                byte[] buffer = new byte[1024];
                //定义一个内存
                MemoryStream stream = new MemoryStream();
                DateTime start = DateTime.Now;
                //这么处理的原因是为了一次性读不完整
                //循环读取缓冲区的数据，如果大于0，就读出来，放到内存里，如果等于0，说明读完了
                //如果每次都读不到，要设置超时时间
                while (true)
                {
                    Thread.Sleep(SleepTime);
                    if (this.serialPort.BytesToRead > 0)
                    {
                        int count = this.serialPort.Read(buffer, 0, buffer.Length);
                        stream.Write(buffer, 0, count);
                    }
                    else
                    {
                        if (stream.Length > 0)
                        {
                            break;
                        }
                        else if ((DateTime.Now - start).TotalMilliseconds > this.ReadTimeOut)
                        {
                            return false;
                        }
                    }

                }
                receive = stream.ToArray();
                return true;
            } catch(Exception)
            {
                return false;
            }
        } 

        #region CRC校验
        // 1. CRC 低字节查找表
        private static readonly byte[] aucCRCLo = {
    0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7,
    0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
    0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9,
    0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
    0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
    0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
    0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D,
    0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38,
    0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF,
    0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
    0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1,
    0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
    0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB,
    0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
    0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
    0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
    0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97,
    0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
    0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89,
    0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
    0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83,
    0x41, 0x81, 0x80, 0x40
};

        // 2. CRC 高字节查找表
        private static readonly byte[] aucCRCHi = {
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
    0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
    0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40
};

        /// <summary>
        /// 计算 CRC16
        /// </summary>
        private byte[] Crc16(byte[] pucFrame, int usLen)
        {
            int i = 0;
            byte[] res = new byte[2] { 0xFF, 0xFF };
            ushort iIndex;
            while (usLen-- > 0)
            {
                iIndex = (ushort)(res[0] ^ pucFrame[i++]);
                res[0] = (byte)(res[1] ^ aucCRCHi[iIndex]);
                res[1] = aucCRCLo[iIndex];
            }
            return res;
        }

        /// <summary>
        /// 校验 CRC
        /// </summary>
        private bool CheckCRC(byte[] value)
        {
            if (value == null) return false;

            if (value.Length <= 2) return false;

            int length = value.Length;
            byte[] buf = new byte[length - 2];
            Array.Copy(value, 0, buf, 0, buf.Length);

            byte[] CRCbuf = Crc16(buf, buf.Length);
            if (CRCbuf[0] == value[length - 2] && CRCbuf[1] == value[length - 1])
            {
                return true;
            }
            return false;
        }
        #endregion
        #region 数组比较
        private bool ByteArrayEquals(byte[] b1, byte[] b2)
        {
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i])
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region 将布尔数组转字节数组
        private byte[] GetByteArrayFromBoolArray(bool[] value)
        {
            int byteLength = value.Length % 8 == 0 ? value.Length / 8 : value.Length / 8 +1;
            byte[] result = new byte[byteLength];
            for(int i = 0; i<result.Length;i++)
            {
                //获取每个字节的值
                int total = value.Length < 8 * (i + 1)? value.Length-8*i:8;
                for(int j = 0;j<total;j++)
                {
                    result[i] = SetBitValue(result[i], j, value[8*i+j]);
                }


            }
            return result;
        }
        /// <summary>
        /// 将每个字节的位置位后者复位
        /// </summary>
        /// <param name="src"></param>
        /// <param name="bit"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private byte SetBitValue(byte src, int bit, bool value)
        {
            return value ? (byte)(src | (byte)Math.Pow(2, bit)) : (byte)(src & ~(byte)Math.Pow(2, bit));
        }
        #endregion

    }
}
