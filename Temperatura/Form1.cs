using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace Temperatura
{
	public partial class Form1 : Form
	{
		string RxString;
		float temperatura = 0;
		Graphics g;
		Pen newpen = new Pen(Color.Navy); Font newfont = new Font("Arial", 10);
		float a = 0, b = 0, c = 0 , d = 0, f = 0;
		float tempb = 0, tempc = 0, tempd = 0, tempf = 0;
		Point[] pointarray = new Point[5];
		bool initDraw = true; //rysuj na początku wykres, później już nie
		bool wykresStworzony = false;   //do odświeżenia okna po powrocie z paska zadań
		bool powrotPoMinimalizacji = false;
        string tempInterwal = "b";
		//SqlConnection cn = new SqlConnection(@"Data Source=C:\Users\Adam\Documents\Visual Studio 2008\Projects\Temperatura\Temperatura\ZapisTemperatury.sdf");
		//SqlCommand cmd = new SqlCommand();
		//SqlDataReader dr;
        //string excelData;

		public Form1()
		{
			InitializeComponent();

		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (g != null)
			{
				g.Dispose();                                
			}
			try
			{                
				serialPort1.Open();
				if (serialPort1.IsOpen)
				{
					button1.Enabled = false;
					button2.Enabled = true;
                    InterwalNaStart("a");//na początek żada szybkich wyników
				}
			}
			catch
			{
				MessageBox.Show("Wystąpił błąd. Upewnij się czy urządzenie jest podłączne i wybrano odpowiedni, nie zajęty port.");
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				if (serialPort1.IsOpen)
				{
					serialPort1.Close();
				}
			}
			catch (System.UnauthorizedAccessException ex)
			{
				MessageBox.Show(ex.Message);
				//LogError(e);  // call a custom error logging procedurethrow e;      // re-throw the error
			}

			button1.Enabled = true;
			button2.Enabled = false;
		}
		
		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				if (serialPort1.IsOpen)
					serialPort1.Close();
			}
			catch (System.UnauthorizedAccessException ex)            
			{                
				MessageBox.Show(ex.Message);                
			}
		}

		private void DisplayText(object sender, EventArgs e)
		{
			if (string.Compare("-127.00", RxString) == 0)
			{
				label1.ForeColor = Color.Red;
				label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
				label1.Text = "Błąd odczytu";                
			}
			else
			{
				try
				{
					temperatura = Convert.ToSingle(RxString.Replace(".", ","));
				}

				catch
				{
					temperatura = 0;
				}

				label1.Font = new System.Drawing.Font("Showcard Gothic", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
				if (temperatura < 15.0)
					label1.ForeColor = Color.Blue;
				else if ((temperatura >= 15.0) && (temperatura < 26.0))
					label1.ForeColor = Color.Lime;
				else if ((temperatura >= 26.0) && (temperatura < 40.0))
					label1.ForeColor = Color.OrangeRed;
				else if ((temperatura >= 40.0))
					label1.ForeColor = Color.Red;
				label1.Text = RxString + "°C";
				//g = new Graphics();
				przesunTemperatury();
				PaintChart();
                //Excel arkusz = new Excel();
                Excel.excelOperations(temperatura);
				//insertRecord();
			}
		}
		private void przesunTemperatury()
		{
			//trzyma w zakresie wykresu [0,50]
			if (temperatura < 0)
				temperatura = 0;
			else if (temperatura > 50)
				temperatura = 50;

			//przesunięcie wartości
			a = tempb;
			b = tempc;
			tempb = b;
			c = tempd;
			tempc = c;
			d = tempf;
			tempd = d;
			f = temperatura * 2;
			tempf = f; //temp spelnia role zapasowej kopii, na podstawowych sa wykonywane operacje(poniżej)

			a = a * 3;
			a = a / 2;
			a = 150 - a;
			
			b = b * 3;
			b = b / 2;
			b = 150 - b;
			
			c = c * 3;
			c = c / 2;
			c = 150 - c;
			
			d = d * 3;
			d = d / 2;
			d = 150 - d;
			
			f = f * 3;
			f = f / 2;
			f = 150 - f;
		}
		private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			RxString = serialPort1.ReadLine();
			RxString = RxString.ToString().TrimEnd('\r','\n');
			this.Invoke(new EventHandler(DisplayText));
		}
        
		protected override void OnPaint(PaintEventArgs e)
		{
			if (wykresStworzony && powrotPoMinimalizacji)
			{
				powrotPoMinimalizacji = false;
				PaintChart();
			}
			else if (initDraw || !wykresStworzony)
			{
				g = this.CreateGraphics();
				//g.Clear(Color.Blue);
				//Set point of origin to be 50,50 on the form
				g.TranslateTransform(60, 80);
				g.DrawLine(newpen, 1, 1, 1, 150);
				g.DrawLine(newpen, 1, 150, 150, 150);
				//Draw X and y axis
				g.DrawLine(newpen, -3, 0, 3, 0);
				g.DrawLine(newpen, -3, 30, 3, 30);
				g.DrawLine(newpen, -3, 60, 3, 60);
				g.DrawLine(newpen, -3, 90, 3, 90);
				g.DrawLine(newpen, -3, 120, 3, 120);
				g.DrawLine(newpen, 30, 147, 30, 153);
				g.DrawLine(newpen, 60, 147, 60, 153);
				g.DrawLine(newpen, 90, 147, 90, 153);
				g.DrawLine(newpen, 120, 147, 120, 153);
				g.DrawLine(newpen, 150, 147, 150, 153);

				//label Y axis
				g.DrawString("50", newfont, new SolidBrush(Color.Black), -25, -5);
				g.DrawString("40", newfont, new SolidBrush(Color.Black), -25, 25);
				g.DrawString("30", newfont, new SolidBrush(Color.Black), -25, 55);
				g.DrawString("20", newfont, new SolidBrush(Color.Black), -25, 85);
				g.DrawString("10", newfont, new SolidBrush(Color.Black), -25, 115);
				//Label X axis
				g.DrawString("0", newfont, new SolidBrush(Color.Black), -10, 145);
				g.DrawString("t1", newfont, new SolidBrush(Color.Black), 25, 155);
				g.DrawString("t2", newfont, new SolidBrush(Color.Black), 55, 155);
				g.DrawString("t3", newfont, new SolidBrush(Color.Black), 85, 155);
				g.DrawString("t4", newfont, new SolidBrush(Color.Black), 115, 155);
				g.DrawString("t5", newfont, new SolidBrush(Color.Black), 145, 155);
				initDraw = false;
			}
		}

		private void PaintChart()
		{
            if (!wykresStworzony)
            {
                zmienInterwal(tempInterwal);//powrot do wybranej opcji                
            }
			g = this.CreateGraphics();
			g.Clear(Color.DodgerBlue);
			//Set point of origin to be 50,50 on the form
			g.TranslateTransform(60, 80);
			g.DrawLine(newpen, 1, 1, 1, 150);
			g.DrawLine(newpen, 1, 150, 150, 150);
			//Draw X and y axis
			g.DrawLine(newpen, -3, 0, 3, 0);
			g.DrawLine(newpen, -3, 30, 3, 30);
			g.DrawLine(newpen, -3, 60, 3, 60);
			g.DrawLine(newpen, -3, 90, 3, 90);
			g.DrawLine(newpen, -3, 120, 3, 120);
			g.DrawLine(newpen, 30, 147, 30, 153);
			g.DrawLine(newpen, 60, 147, 60, 153);
			g.DrawLine(newpen, 90, 147, 90, 153);
			g.DrawLine(newpen, 120, 147, 120, 153);
			g.DrawLine(newpen, 150, 147, 150, 153);
			
			//label Y axis
			g.DrawString("50", newfont, new SolidBrush(Color.Black), -25, -5);
			g.DrawString("40", newfont, new SolidBrush(Color.Black), -25, 25);
			g.DrawString("30", newfont, new SolidBrush(Color.Black), -25, 55);
			g.DrawString("20", newfont, new SolidBrush(Color.Black), -25, 85);
			g.DrawString("10", newfont, new SolidBrush(Color.Black), -25, 115);
			//Label X axis
			g.DrawString("0", newfont, new SolidBrush(Color.Black), -10, 145);
			g.DrawString("t1", newfont, new SolidBrush(Color.Black), 25, 155);
			g.DrawString("t2", newfont, new SolidBrush(Color.Black), 55, 155);
			g.DrawString("t3", newfont, new SolidBrush(Color.Black), 85, 155);
			g.DrawString("t4", newfont, new SolidBrush(Color.Black), 115, 155);
			g.DrawString("t5", newfont, new SolidBrush(Color.Black), 145, 155);
			//Plot points
			g.DrawEllipse(new Pen(Color.Blue), 30 - 2, a - 2, 4, 4);
			g.DrawEllipse(new Pen(Color.Red), 60 - 2, b - 2, 4, 4);
			g.DrawEllipse(new Pen(Color.Green), 90 - 2, c - 2, 4, 4);
			g.DrawEllipse(new Pen(Color.Black), 120 - 2, d - 2, 4, 4);
			g.DrawEllipse(new Pen(Color.Gold), 150 - 2, f - 2, 4, 4);
			//Join the points
			pointarray[0] = new Point(30, Convert.ToInt32(a));
			pointarray[1] = new Point(60, Convert.ToInt32(b));
			pointarray[2] = new Point(90, Convert.ToInt32(c));
			pointarray[3] = new Point(120, Convert.ToInt32(d));
			pointarray[4] = new Point(150, Convert.ToInt32(f));
			g.DrawLines(new Pen(Color.Yellow), pointarray);
			g.Dispose();
			wykresStworzony = true;
		}
        	           		
		private void Form1_Resize(object sender, EventArgs e)
		{
			if (WindowState == FormWindowState.Normal)
			{
				 powrotPoMinimalizacji = true;
				
			}
			
		}
        		
		private void Form1_Activated(object sender, EventArgs e)
		{
			powrotPoMinimalizacji = true;
		}

		private void serialPort1_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
		{
			MessageBox.Show("Błąd portu szeregowego!");
		}

		private void serialPort1_PinChanged(object sender, System.IO.Ports.SerialPinChangedEventArgs e)
		{
			MessageBox.Show("Błąd portu szeregowego!");
		}
		private void serialPort1_Error(System.UnauthorizedAccessException e)
		{
			MessageBox.Show("Błąd portu szeregowego!");
		}

        private void zmienInterwal(string odstepPomiarow)
        {            
            
            // If the port is closed, don't try to send a data
            if (!serialPort1.IsOpen)
            {
                tempInterwal = odstepPomiarow;
                return;
            }

            // Send the one character buffer.
            serialPort1.Write(odstepPomiarow);
        }
        private void InterwalNaStart(string odstepPomiarow)
        {
            if (!serialPort1.IsOpen)
            {
                return;
            }

            // Send the one character buffer.
            serialPort1.Write(odstepPomiarow);
        }
		/*private void insertRecord()
		{            
			cmd.Connection = cn;
			cn.Open();
            MessageBox.Show("Próba zapisu");
			cmd.CommandText = @"INSERT INTO Tabela (ID, Temperatura, Data) VALUES (@ID, @Temperatura, @Data)";
            cmd.Parameters.AddWithValue("@ID", 11);
            cmd.Parameters.AddWithValue("@Temperatura", 11);
            cmd.Parameters.AddWithValue("@Data", 11);
            //cmd.Parameters.AddWithValue("@Temperatura", temperatura);
			//cmd.Parameters.AddWithValue("@Data", DateTime.Now.Date);    //DateReleasedDate.Value
			//cmd.CommandText = "INSERT INTO Tabela (Temperatura,Data) VALUES ('"+ temperatura +"','"++"')";
            
            try
            {
                cmd.ExecuteNonQuery();
                //cmd.Clone();
                cn.Close();
                MessageBox.Show("Login");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid");
            }           
	

		}*/
		
	}
	   
}

