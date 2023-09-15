using CobotApplication;

namespace RobotInterfaceGUI
{
    public partial class RobotInterfaceGUI : Form
    {
        CobotApplicationProgram program;
        public RobotInterfaceGUI()
        {
            InitializeComponent();

        }

        private void btn_URRun_Click(object sender, EventArgs e)
        {
            Dictionary<string, int> pickingInfos = new Dictionary<string, int>();
            int pickCount1 = 0;
            int pickCount2 = 0;
            bool validation1 = int.TryParse(tbx_PickCount1.Text, out pickCount1);
            bool validation2 = int.TryParse(tbx_PickCount2.Text, out pickCount2);

            #region [Number Validation Check]
            if (cmb_Product1.SelectedIndex == 0 && cmb_Product2.SelectedIndex == 0)
                return;

            if (cmb_Product1.SelectedIndex != 0 && validation1 == false)
            {
                MessageBox.Show("Target 1의 Pick 개수가 잘못 되었습니다.", "오류");
                return;
            }
            else if (cmb_Product2.SelectedIndex != 0 && validation2 == false)
            {
                MessageBox.Show("Target 2의 Pick 개수가 잘못 되었습니다.", "오류");
                return;
            }
            #endregion

            #region [Target Number Check]
            
            
            if(cmb_Product1.SelectedIndex == 0)
            {
                pickingInfos.Add("Target", 2);
                pickingInfos.Add("PickCount", pickCount2);
                pickingInfos.Add("Product", cmb_Product2.SelectedIndex);
                program.OnClick_btn_URRun(pickingInfos);

            }
            else if (cmb_Product2.SelectedIndex == 0)
            {
                pickingInfos.Add("Target", 1);
                pickingInfos.Add("PickCount", pickCount1);
                pickingInfos.Add("Product", cmb_Product1.SelectedIndex);
                program.OnClick_btn_URRun(pickingInfos);

            }
            else
            {
                pickingInfos.Add("Target", 1);
                pickingInfos.Add("PickCount", pickCount1);
                pickingInfos.Add("Product", cmb_Product1.SelectedIndex);
                program.OnClick_btn_URRun(pickingInfos);
                pickingInfos.Clear();

                pickingInfos.Add("Target", 2);
                pickingInfos.Add("PickCount", pickCount2);
                pickingInfos.Add("Product", cmb_Product2.SelectedIndex);
                program.OnClick_btn_URRun(pickingInfos);
                pickingInfos.Clear();
            }
            #endregion



        }

        private void RobotInterfaceGUI_Load(object sender, EventArgs e)
        {
            program = new CobotApplicationProgram();
            program.RunWinformProgram();


        }


    }
}