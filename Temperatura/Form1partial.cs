using System.Windows.Forms;
using System;
namespace Temperatura
{
    public partial class Form1 : Form

    {
        private void cOM1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM1";
            cOM1ToolStripMenuItem.Checked = true;
            cOM2ToolStripMenuItem.Checked = false;
            cOM3ToolStripMenuItem.Checked = false;
            cOM4ToolStripMenuItem.Checked = false;
            cOM5ToolStripMenuItem.Checked = false;
            cOM6ToolStripMenuItem.Checked = false;
            cOM7ToolStripMenuItem.Checked = false;

        }

        private void cOM2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM2";
            cOM2ToolStripMenuItem.Checked = true;
            cOM1ToolStripMenuItem.Checked = false;
            cOM3ToolStripMenuItem.Checked = false;
            cOM4ToolStripMenuItem.Checked = false;
            cOM5ToolStripMenuItem.Checked = false;
            cOM6ToolStripMenuItem.Checked = false;
            cOM7ToolStripMenuItem.Checked = false;
        }

        private void cOM3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM3";
            cOM3ToolStripMenuItem.Checked = true;
            cOM2ToolStripMenuItem.Checked = false;
            cOM1ToolStripMenuItem.Checked = false;
            cOM4ToolStripMenuItem.Checked = false;
            cOM5ToolStripMenuItem.Checked = false;
            cOM6ToolStripMenuItem.Checked = false;
            cOM7ToolStripMenuItem.Checked = false;
        }

        private void cOM4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM4";
            cOM4ToolStripMenuItem.Checked = true;
            cOM2ToolStripMenuItem.Checked = false;
            cOM3ToolStripMenuItem.Checked = false;
            cOM1ToolStripMenuItem.Checked = false;
            cOM5ToolStripMenuItem.Checked = false;
            cOM6ToolStripMenuItem.Checked = false;
            cOM7ToolStripMenuItem.Checked = false;
        }

        private void cOM5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM5";
            cOM5ToolStripMenuItem.Checked = true;
            cOM2ToolStripMenuItem.Checked = false;
            cOM3ToolStripMenuItem.Checked = false;
            cOM4ToolStripMenuItem.Checked = false;
            cOM1ToolStripMenuItem.Checked = false;
            cOM6ToolStripMenuItem.Checked = false;
            cOM7ToolStripMenuItem.Checked = false;
        }

        private void cOM6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM6";
            cOM6ToolStripMenuItem.Checked = true;
            cOM2ToolStripMenuItem.Checked = false;
            cOM3ToolStripMenuItem.Checked = false;
            cOM4ToolStripMenuItem.Checked = false;
            cOM5ToolStripMenuItem.Checked = false;
            cOM1ToolStripMenuItem.Checked = false;
            cOM7ToolStripMenuItem.Checked = false;
        }

        private void cOM7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM7";
            cOM7ToolStripMenuItem.Checked = true;
            cOM2ToolStripMenuItem.Checked = false;
            cOM3ToolStripMenuItem.Checked = false;
            cOM4ToolStripMenuItem.Checked = false;
            cOM5ToolStripMenuItem.Checked = false;
            cOM6ToolStripMenuItem.Checked = false;
            cOM1ToolStripMenuItem.Checked = false;
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            odznaczWszystkieInterwaly();            
            sToolStripMenuItem.Checked = true;
            zmienInterwal("a");
        }
                

        private void sToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            odznaczWszystkieInterwaly();
            sToolStripMenuItem1.Checked = true;
            zmienInterwal("b");
        }

        private void mToolStripMenuItem_Click(object sender, EventArgs e)
        {
            odznaczWszystkieInterwaly();
            mToolStripMenuItem.Checked = true;
            zmienInterwal("c");
        }

        private void mToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            odznaczWszystkieInterwaly();
            mToolStripMenuItem1.Checked = true;
            zmienInterwal("d");
        }

        private void mToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            odznaczWszystkieInterwaly();
            mToolStripMenuItem2.Checked = true;
            zmienInterwal("e");
        }

        private void mToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            odznaczWszystkieInterwaly();
            mToolStripMenuItem3.Checked = true;
            zmienInterwal("f");
        }

        private void hToolStripMenuItem_Click(object sender, EventArgs e)
        {
            odznaczWszystkieInterwaly();
            hToolStripMenuItem.Checked = true;
            zmienInterwal("g");
        }

        private void odznaczWszystkieInterwaly()
        {
            sToolStripMenuItem.Checked = false;
            sToolStripMenuItem1.Checked = false;
            mToolStripMenuItem.Checked = false;
            mToolStripMenuItem1.Checked = false;
            mToolStripMenuItem2.Checked = false;
            mToolStripMenuItem3.Checked = false;
            hToolStripMenuItem.Checked = false;

        }

    }
}