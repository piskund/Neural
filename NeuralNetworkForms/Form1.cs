// -------------------------------------------------------------------------------------------------------------
//  Form1.cs created by DEP on 2018/12/01
// -------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NeuralNetwork;

namespace NeuralNetworkForms
{
    public partial class Form1 : Form
    {
        private readonly Network _net = new Network();

        public Form1()
        {
            InitializeComponent();
            _net.ErrorChanged += _net_ErrorChanged;
            _net.NetworkTrained += _net_NetworkTrained;
            SetDefaultValuesToComponents();
        }

        private void SetDefaultValuesToComponents()
        {
            lblError.Text = "";
            lblCompleted.Visible = false;
            btnTest.Enabled = false;
            btnTrain.Enabled = true;
        }

        private void _net_ErrorChanged(object sender, double e)
        {
            Invoke(new MethodInvoker(() => lblError.Text = $@"Error: {e}"));
        }

        private void _net_NetworkTrained(object sender, EventArgs e)
        {
            Invoke(new MethodInvoker(
                () =>
                {
                    lblError.Text += $" < {_net.Threshold} threshold.";
                    btnTest.Enabled = true;
                    lblCompleted.Visible = true;
                }
            ));
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            listBoxArgs.Items.Clear();
            listBoxXor.Items.Clear();
            listBoxXand.Items.Clear();
            var testResult = _net.Test();
            foreach (var kvp in testResult)
            {
                var netArg = string.Join(",", kvp.Key);
                listBoxArgs.Items.Add(netArg);
                listBoxXor.Items.Add(kvp.Value[0]);
                listBoxXand.Items.Add(kvp.Value[1]);
            }
            SetDefaultValuesToComponents();
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {
            btnTrain.Enabled = false;
            var t = new Thread(() => _net.Train());
            t.Start();
        }
    }
}