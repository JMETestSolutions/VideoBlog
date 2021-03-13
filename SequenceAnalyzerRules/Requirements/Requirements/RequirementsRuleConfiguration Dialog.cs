using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JMETestSolutions.SequenceAnalyzerRules
{
    public partial class RequirementsRuleConfiguration_Dialog : Form
    {
        private bool bListAll;

        public RequirementsRuleConfiguration_Dialog(bool ListAll)
        {
            InitializeComponent();

            this.bListAll = ListAll;
            this.checkBoxListAll.Checked = ListAll;

        }

        public bool ListAll
        {
            get { return this.bListAll; }
        }

        private void RequirementsRuleConfiguration_Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
  
            //Set our Configuration Variables if the user pressed OK
            if (this.DialogResult == DialogResult.OK)
            {
                this.bListAll = checkBoxListAll.Checked;
            }
        }
    }
}
