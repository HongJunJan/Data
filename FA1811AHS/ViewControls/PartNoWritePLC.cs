using ConnPLCcommand;
using FA1811AHS.Repository;
using FA1811AHS.Service;
using FA1811AHS.Shared;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FA1811AHS
{
    public partial class PartNoWritePLC : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IPartNoJoinTrayAndLaserService partNoJoinTrayAndLaserService = new PartNoJoinTrayAndLaserService();
        private IRecChangeService recChangeService = new RecChangeService();
        private List<PartNoJoinTrayAndLaser> PartNoJoinTrayAndLasers = new List<PartNoJoinTrayAndLaser>();
        private PartNoJoinTrayAndLaser partNoJoinTrayAndLaser = null;

        public PartNoWritePLC()
        {
            InitializeComponent();
            ButtonProperty.SetProperty(btnSelectPartNo, true);
            ButtonProperty.SetProperty(btnPLCWrite, true);
            DefaultPartNo();
        }

        /// <summary>
        /// 預設值，取得料號資料 TO DataGridView控制器
        /// </summary>
        private void DefaultPartNo()
        {
            try
            {
                DataGridviewProperty.SetProperty(dataGridView1);
                DataGridviewProperty.SetProperty(dataGridView2);
                ResponseModel responseModel = partNoJoinTrayAndLaserService.GetAllData();
                PartNoJoinTrayAndLasers = responseModel.PartNoJoinTrayAndLaserList;
                dataGridView1.ColumnCount = 1;
                dataGridView1.Columns[0].Name = "PartNo";
                foreach (var item in PartNoJoinTrayAndLasers) { dataGridView1.Rows.Add(item.PartNo); }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 關閉離開
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripbtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectPartNo_Click(object sender, EventArgs e)
        {
            List<PartNoJoinTrayAndLaser> dataList = PartNoJoinTrayAndLasers
                .Where(c => c.PartNo.Contains(txtSelectPartNo.Text)).ToList();
            dataGridView1.Rows.Clear();
            foreach (var item in dataList) { dataGridView1.Rows.Add(item.PartNo); }
        }

        /// <summary>
        /// DataGridView選取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblPartNo.Text = string.Empty;
            dataGridView2.ColumnCount = 2;
            dataGridView2.Columns[0].Name = "項目";
            dataGridView2.Columns[1].Name = "參數";
            dataGridView2.Rows.Clear();
            if (e.RowIndex > -1)
            {
                string pn = dataGridView1.CurrentRow.Cells[0].Value.ToString().Trim();
                partNoJoinTrayAndLaser = PartNoJoinTrayAndLasers.Where(x => x.PartNo == pn).FirstOrDefault();
                lblPartNo.Text = partNoJoinTrayAndLaser.PartNo;
                List<string[]> list = new List<string[]>
                {
                   new string[] { "PartNo", partNoJoinTrayAndLaser.PartNo },
                   new string[] { "PieceSizeX", partNoJoinTrayAndLaser.PieceSizeX },
                   new string[] { "PieceSizeY", partNoJoinTrayAndLaser.PieceSizeY },
                   new string[] { "TrayNo", partNoJoinTrayAndLaser.TrayNo.ToString() },
                   new string[] { "DivideNoX", partNoJoinTrayAndLaser.DivideNoX.ToString() },
                   new string[] { "DivideNoY", partNoJoinTrayAndLaser.DivideNoY.ToString() },
                   new string[] { "DividePitchX", partNoJoinTrayAndLaser.DividePitchX },
                   new string[] { "DividePitchY", partNoJoinTrayAndLaser.DividePitchY },
                   new string[] { "PieceCenterX", partNoJoinTrayAndLaser.PieceCenterX },
                   new string[] { "PieceCenterY", partNoJoinTrayAndLaser.PieceCenterY },
                   new string[] { "TrayThickness", partNoJoinTrayAndLaser.TrayThickness },
                   new string[] { "TrayCenter", partNoJoinTrayAndLaser.TrayCenter },
                   new string[] { "TrayLength", partNoJoinTrayAndLaser.TrayLength },
                   new string[] { "TrayOffsetX", partNoJoinTrayAndLaser.TrayOffsetX },
                   new string[] { "TrayOffsetY", partNoJoinTrayAndLaser.TrayOffsetY },
                   new string[] { "PositionX2D", partNoJoinTrayAndLaser.PositionX2D },
                   new string[] { "PositionY2D", partNoJoinTrayAndLaser.PositionY2D }
            };

                foreach (string[] rowArray in list) { dataGridView2.Rows.Add(rowArray); }
                dataGridView2.ClearSelection();
            }
        }

        /// <summary>
        /// 傳送資料 TO PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPLCWrite_Click(object sender, EventArgs e)
        {
            try
            {
                // 使用者異動紀錄
                recChangeService.AddData(new RecChange
                {
                    NowTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd HH:ss:mm")),
                    Message = "PLC傳送資料",
                    UserName = GlobalParameter.UserName
                });

                if (string.IsNullOrEmpty(lblPartNo.Text)) { MessageBox.Show("未選取一筆"); return; }
                if (GlobalData.PLC初始連線是否成功 && !GlobalData.PLC線路異常)
                {
                    // DWORD
                    List<string> data = new List<string>();
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PieceSizeX, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PieceSizeY, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PieceSizeT, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PositionX2D, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PositionY2D, data);
                    PLCMethod.SetDefultParameter("0", 20, data);
                    PLCMethod.SetParameterSplit(partNoJoinTrayAndLaser.TrayNo.ToString(), data);
                    PLCMethod.SetParameterSplit(partNoJoinTrayAndLaser.DivideNoX.ToString(), data);
                    PLCMethod.SetParameterSplit(partNoJoinTrayAndLaser.DivideNoY.ToString(), data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.DividePitchX, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.DividePitchY, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PieceCenterX, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.PieceCenterY, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.TrayThickness, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.TrayCenter, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.TrayLength, data);
                    PLCMethod.SetDefultParameter("0", 10, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.TrayOffsetX, data);
                    PLCMethod.SetParameterSplitForPoint(partNoJoinTrayAndLaser.TrayOffsetY, data);
                    PLCMethod.SetDefultParameter("0", 6, data);
                    PLCcommand.PLC_Write(PLCcommand.Cmd_Mode.Mode_Write_Multiple,
                        PLCcommand.PLC_IO.DM, DmTable.DM_8300DWORD_板寬X, 0, data.Count, data);
                }
                else
                {
                    MessageBox.Show("PLC斷線異常");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}