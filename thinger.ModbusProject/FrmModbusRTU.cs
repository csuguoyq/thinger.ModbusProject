using Microsoft.VisualBasic;
using System.ComponentModel;
using System.IO.Ports;
using thinger.DataConvertLib;
using thinger.ModbusRTULib;

namespace thinger.ModbusProject
{
    public partial class FrmModbusRTU : Form
    {
        public FrmModbusRTU()
        {
            InitializeComponent();
            InitParam();
        }
        /// <summary>
        /// ModbusRTU通信对象
        /// </summary>
        private ModbusRTU modbus = new ModbusRTU();
        /// <summary>
        /// 当前连接状态
        /// </summary>
        private bool IsConnected = false;
        private void InitParam()
        {
            //获取本机的串口列表
            string[] portList = SerialPort.GetPortNames();

            if (portList.Length > 0)
            {
                this.cmb_Port.Items.AddRange(portList);
            }
            this.cmb_Port.SelectedIndex = 0;

            //波特率
            this.cmb_BaudRate.Items.AddRange(new string[] { "2400", "4800", "9600", "19200", "38400" });
            this.cmb_BaudRate.SelectedIndex = 2;

            //校验位
            this.cmb_Parity.DataSource = Enum.GetNames(typeof(Parity));
            this.cmb_Parity.SelectedIndex = 0;

            //停止位
            this.cmb_StopBits.DataSource = Enum.GetNames(typeof(StopBits));
            this.cmb_StopBits.SelectedIndex = 1;

            //数据位
            this.cmb_DataBits.Items.AddRange(new string[] { "7", "8" });
            this.cmb_DataBits.SelectedIndex = 1;

            //大小端
            this.cmb_DataFormat.DataSource = Enum.GetNames(typeof(DataFormat));
            this.cmb_DataFormat.SelectedIndex = 0;

            //存储区
            this.cmb_StoreArea.DataSource = Enum.GetNames(typeof(StoreArea));
            this.cmb_StoreArea.SelectedIndex = 0;

            //数据类型
            this.cmb_DataType.DataSource = Enum.GetNames(typeof(DataType));
            this.cmb_DataType.SelectedIndex = 0;

            //动态修改ListView第二列宽度
            this.lsv_ReadInfo.Columns[1].Width = this.lsv_ReadInfo.Width - this.lsv_ReadInfo.Columns[0].Width - 20;
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (IsConnected)
            {
                AddLog(1, "Modbus已经连接！");
                return;
            }

            Parity parity = (Parity)Enum.Parse(typeof(Parity), this.cmb_Parity.Text, true);
            StopBits stopBits = (StopBits)Enum.Parse(typeof(StopBits), this.cmb_StopBits.Text, true);

            IsConnected = modbus.Connect(this.cmb_Port.Text, Convert.ToInt32(this.cmb_BaudRate.Text), parity,
                Convert.ToInt32(this.cmb_DataBits.Text), stopBits);

            if (IsConnected)
            {
                AddLog(0, "Modbus连接成功");
            }
            else
            {
                AddLog(2, "Modbus连接失败");
            }
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            modbus.DisConnect();
            IsConnected = false;
            AddLog(0, "Modbus断开连接");
        }
        private void AddLog(int level, string info)
        {
            ListViewItem lst = new ListViewItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), level);

            lst.SubItems.Add(info);

            //让最新的数据在最上面
            this.lsv_ReadInfo.Items.Insert(0, lst);

        }
    }
}
