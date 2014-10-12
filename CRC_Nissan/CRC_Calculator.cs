using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRC_Nissan
{
    public partial class CRC_Calculator : Form
    {
        public CRC_Calculator()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //txtPoly has the poly
            //txtBytes has the 7 input bytes
            //txtOutput is used for output CRC

            uint poly = uint.Parse(txtPoly.Text, System.Globalization.NumberStyles.HexNumber);
            byte CRCOutput = 0;
            byte[] input_bytes = new byte[8];

            char[] splitters = { ' ' };
            String[] Tokens = txtBytes.Text.Split(splitters);
            if (Tokens.Length < 7)
            {
                MessageBox.Show("You need to enter 7 bytes here separated by spaces");
                return;
            }

            for (int i = 0; i < 7; i++) 
                input_bytes[i] = byte.Parse(Tokens[i], System.Globalization.NumberStyles.HexNumber);

            input_bytes[7] = 0;

            //start with msb of each byte and process the same every time. 

            byte thisbit = 0;
            for (int b = 0; b < 8; b++)
            {
                for (int bit = 7; bit >= 0; bit--)
                {
                    thisbit = 0;
                    if ((input_bytes[b] & (1 << bit)) > 0) thisbit = 1;
                    if (CRCOutput > 127) 
                    {
                        CRCOutput = (byte)(((CRCOutput << 1) + thisbit) ^ poly);
                    }
                    else 
                    {
                        CRCOutput = (byte)((CRCOutput << 1) + thisbit);
                    }                    
                }
            }
            txtOutput.Text = CRCOutput.ToString("X2");
        }
    }
}
