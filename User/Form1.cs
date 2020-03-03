using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace User
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void GetBtn_Click(object sender, EventArgs e)
        {
            Requester requester = new Requester();
            var result = requester.GetAll();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Test request for get");
            sb.AppendLine($"LotNo := {result.LotNo}");
            sb.AppendLine($"PackageName := {result.Package}");
            sb.AppendLine($"DeviceName := {result.Device}");
            sb.AppendLine($"FlowName := {result.FlowName}");

            MessageBox.Show(sb.ToString());
        }

        private void PostBtn_Click(object sender, EventArgs e)
        {
            ValidateDataDto setupData = new ValidateDataDto();
            setupData.lotNo = "2003A4086V";
            setupData.machineName = "FT-M-019";

            Requester requester = new Requester();
            var result = requester.Validate(setupData);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Test request for validation");
            sb.AppendLine($"LotNo := {result.Source.lotNo}");
            sb.AppendLine($"MachineName := {result.Source.machineName}");
            sb.AppendLine($"IsError := {result.IsPass}");
            sb.AppendLine($"Message := {result.Message}");

            MessageBox.Show(sb.ToString());
        }
    }
}
