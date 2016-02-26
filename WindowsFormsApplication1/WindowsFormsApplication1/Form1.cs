using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Boolean pc_flag = false;
        Boolean converter_flag = false;
        string hex_dec_bın = "";
        static int callReturn = 0;
        int stack_count = 0;
        string tempPC;
        int labelcount = 0;
        int programCounter = 0;
        bool flagins = false;
        int insflag = 0;
        string IR = "";
        string R1 = "00";
        string R2 = "01";
        string R3 = "10";
        string R4 = "11";
        string ADD = "0"; 
        string SUB = "1";
        string AND = "4"; 
        string OR = "5";
        string XOR = "6"; 
        string SHL = "2";
        string SHR = "3"; 
        string INC = "15";
        string COMA = "7";  

        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.RowCount = 32;
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Width = 26;
            dataGridView1.Columns[0].Name = "Address";
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[1].Name = "Value";
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.RowCount = 16;
            dataGridView2.ColumnCount = 2;
            dataGridView2.Columns[0].Width = 63;
            dataGridView2.Columns[0].Name = "Address";
            dataGridView2.Columns[1].Width = 63;
            dataGridView2.Columns[1].Name = "Value";
        }


        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.RowCount = 32;
            dataGridView3.ColumnCount = 2;
            dataGridView3.Columns[0].Width = 63;
            dataGridView3.Columns[0].Name = "Address";
            dataGridView3.Columns[1].Width = 63;
            dataGridView3.Columns[1].Name = "Value";
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView4.ColumnCount = 3;
            dataGridView4.Columns[0].Name = "LabelName";
            dataGridView4.Columns[0].Width = 83;
            dataGridView4.Columns[1].Name = "Address";
            dataGridView4.Columns[1].Width = 83;
            dataGridView4.Columns[2].Name = "Value";
            dataGridView4.Columns[2].Width = 83;
            dataGridView4.RowCount = 32;
        }
        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            
        }

        private void openButton_Click(object sender, EventArgs e)
        {
           
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            dataGridView1_CellContentClick(null, null);
            dataGridView2_CellContentClick(null, null);
            dataGridView3_CellContentClick(null, null);
            dataGridView4_CellContentClick(null, null);
            dataGridView5_CellContentClick(null, null);

            string asd="0000";
            for (int j = 0; j < 16; j++)
            {
                dataGridView2.Rows[j].Cells[1].Value = asd; 
            }
            for (int j = 0; j < 32; j++)
            {
                dataGridView3.Rows[j].Cells[1].Value = asd;
            }
            for (int j = 0; j < 32; j++)
            {
                dataGridView1.Rows[j].Cells[1].Value = asd;
            }
            

            openFileDialog1.Title = "Please select the assembly file";
            openFileDialog1.Filter = "(*.basm)|*.basm|(*.asm)|*.asm";
            openFileDialog1.ShowDialog();
            //txtFilename.Text = openFileDialog1.FileName;
            if (openFileDialog1.FileName == "") return;
            lstInstructions.Items.Clear();
            using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
            {
                String line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    lstInstructions.Items.Add(line);
                }
            }


        }
        private string BinaryToDecimal(string binary)
        {
            int tempDec = 0;
            for (int i = 0; i < binary.Length; i++)
            {
                if (binary[binary.Length - i - 1] != '0')
                    tempDec += (int)Math.Pow(2, i);
            }
            string result = Convert.ToString(tempDec);
            return result;
        }
        
        private string HexToBinary(string hex)
        {
            string binary = Convert.ToString(Convert.ToInt32(hex, 16), 2);
            return binary;
        }

        private string DecimalToBinary(string dec)
        {
            byte binary = (byte)(Convert.ToInt16(dec) & 0x0F);
            String strHex = Convert.ToString(binary, 2).PadLeft(4, '0');
            return strHex;
            
        }
        
        private void microOperations(string[] str)
        {
            
            string exec = "";

            string[] registers = { "" }; ;
            if (str.Length > 1)
            {
                registers = str[1].Split(',');
            }
            if (str[0].Equals("ORG") && str[1].Equals("I"))
            {
                programCounter = int.Parse(str[str.Length - 1]);
                tempPC = DecimalToBinary(str[str.Length - 1]);
                tempPC = tempPC.PadLeft(5, '0');
                textBox2.Text = tempPC; // PC
            }
            //fetch
            string sb = str[0];
            if (!str[0].Equals("ORG"))
            {
                if (sb[1] != ':')
                {
                    dataGridView5.Items.Add("Fetch: T-0: IR <- M[PC]\n"); 
                 
                    dataGridView5.Items.Add( "Fetch: T-1: PC <- PC + 1\n\n");
                    
                    IR = Convert.ToString(dataGridView1.Rows[programCounter].Cells[1].Value);
                    textBox11.Text = IR; // IR
                }
            }

            if (str[0].Equals("ADD") || str[0].Equals("SUB") || str[0].Equals("AND") || str[0].Equals("OR") || str[0].Equals("XOR") || str[0].Equals("SHL") || str[0].Equals("SHR") || str[0].Equals("INC") || str[0].Equals("COMA"))
            {

                string a = IR.Substring(0, 1); //a=IR[13](a=1)               
                exec = "aD" + BinaryToDecimal(IR.Substring(1, 4));// D0,..,D15 <- IR[12..9]
                dataGridView5.Items.Add("Decode: T-2: a=IR[13] , a=1 ,  D" + BinaryToDecimal(IR.Substring(1, 4)) + " <- IR[12..9]\n\n");
               
            }
            if (str[0].Equals("LD") || str[0].Equals("ST") || str[0].Equals("IN") || str[0].Equals("OUT"))
            {
                string a_not = IR.Substring(0, 1);//a'=0   (a=IR[13])
                byte inp = 94;
                char outp = (char)inp;
                dataGridView5.Items.Add( "Decode: T-2: a'=0  X=IR[12] " + outp + " IR[11]\n");
                
                exec = "B" + BinaryToDecimal(IR.Substring(3, 2));//N0,..,N3 <- IR[10..9]
                dataGridView5.Items.Add( "Decode: T-2: a'X = B ,   N" + BinaryToDecimal(IR.Substring(3, 2)) + " <- IR[10..9]\n\n");
                
            }
            
        }



        private void instruntionMemory(string[] instruction)
        {
            string instructionFormat = "";
            string[] s = { "" }; ;
            if (instruction.Length > 1)
            {
                s = instruction[1].Split(',');
            }
            string s2 = "5555";

            if (instruction[0].Equals("ORG") || instruction[0].Equals("HLT"))
            {
                if (instruction[0].Equals("ORG"))
                {
                    instructionFormat += "00001";
                    instructionFormat += "0000";

                    if (instruction[instruction.Length - 1].Equals("B"))
                        instructionFormat += "1";
                    else
                        instructionFormat += "0";

                    instructionFormat += "0000";
                }
                else if (instruction[0].Equals("HLT"))
                {
                    instructionFormat += "00010";
                    instructionFormat += "000000000";
                }
            }
            else if (instruction[0].Equals("ADD") || instruction[0].Equals("SUB") || instruction[0].Equals("AND") || instruction[0].Equals("OR") || instruction[0].Equals("XOR"))
            {

                if (instruction[0].Equals("ADD"))
                {
                    instructionFormat += "10000";
                }
                else if (instruction[0].Equals("SUB"))
                {
                    instructionFormat += "10001";
                }
                else if (instruction[0].Equals("AND"))
                {
                    instructionFormat += "10100";
                }
                else if (instruction[0].Equals("OR"))
                {
                    instructionFormat += "10101";
                }
                else if (instruction[0].Equals("XOR"))
                {
                    instructionFormat += "10110";
                }

                if (s[0].Equals("R1")) { instructionFormat += R1; }
                else if (s[0].Equals("R2")) { instructionFormat += R2; }
                else if (s[0].Equals("R3")) { instructionFormat += R3; }
                else if (s[0].Equals("R4")) { instructionFormat += R4; }

                if (s[1].Equals("R1")) { instructionFormat += R1; }
                else if (s[1].Equals("R2")) { instructionFormat += R2; }
                else if (s[1].Equals("R3")) { instructionFormat += R3; }
                else if (s[1].Equals("R4")) { instructionFormat += R4; }

                if (instruction[instruction.Length - 1].Equals("B"))
                {
                    instructionFormat += "1";
                    try
                    {
                        instructionFormat += Convert.ToString(Convert.ToInt16(s[s.Length - 1]), 2).PadLeft(4, '0');
                    }
                    catch (Exception exp)
                    {
                        for (int j = 0; j < dataGridView4.RowCount; j++)
                        {
                            if (dataGridView4.Rows[j].Cells[0].Value != null && dataGridView4.Rows[j].Cells[0].Value.ToString() == s[s.Length - 1])
                                instructionFormat += Convert.ToString(Convert.ToInt16(dataGridView4.Rows[j].Cells[1].Value.ToString()), 2).PadLeft(4, '0');
                        }

                    }
                }
                else
                {
                    instructionFormat += "000";
                    if (s[2].Equals("R1")) { instructionFormat += R1; }
                    else if (s[2].Equals("R2")) { instructionFormat += R2; }
                    else if (s[2].Equals("R3")) { instructionFormat += R3; }
                    else if (s[2].Equals("R4")) { instructionFormat += R4; }
                }

            }
            else if (instruction[0].Equals("SHL") || instruction[0].Equals("SHR") || instruction[0].Equals("INC") || instruction[0].Equals("COMA"))
            {

                if (instruction[0].Equals("SHL"))
                {
                    instructionFormat += "10010";
                }
                else if (instruction[0].Equals("SHR"))
                {
                    instructionFormat += "10011";
                }
                else if (instruction[0].Equals("INC"))
                {
                    instructionFormat += "11111";
                }
                else if (instruction[0].Equals("COMA"))
                {
                    instructionFormat += "10111";
                }

                if (s[0].Equals("R1")) { instructionFormat += R1; }
                else if (s[0].Equals("R2")) { instructionFormat += R2; }
                else if (s[0].Equals("R3")) { instructionFormat += R3; }
                else if (s[0].Equals("R4")) { instructionFormat += R4; }

                if (s[1].Equals("R1")) { instructionFormat += R1; }
                else if (s[1].Equals("R2")) { instructionFormat += R2; }
                else if (s[1].Equals("R3")) { instructionFormat += R3; }
                else if (s[1].Equals("R4")) { instructionFormat += R4; }

                instructionFormat += "00000";


            }
            else if (instruction[0].Equals("LD") || instruction[0].Equals("ST"))
            {

                if (instruction[0].Equals("LD"))
                {
                    instructionFormat += "01100";

                    if (s[0].Equals("R1")) { instructionFormat += R1; }
                    else if (s[0].Equals("R2")) { instructionFormat += R2; }
                    else if (s[0].Equals("R3")) { instructionFormat += R3; }
                    else if (s[0].Equals("R4")) { instructionFormat += R4; }

                    instructionFormat += "000";

                }
                else if (instruction[0].Equals("ST"))
                {
                    instructionFormat += "01101";
                    instructionFormat += "00";

                    if (s[0].Equals("R1")) { instructionFormat += R1; }
                    else if (s[0].Equals("R2")) { instructionFormat += R2; }
                    else if (s[0].Equals("R3")) { instructionFormat += R3; }
                    else if (s[0].Equals("R4")) { instructionFormat += R4; }

                    instructionFormat += "0";

                }

                try
                {
                    instructionFormat += Convert.ToString(Convert.ToInt16(s[s.Length - 1]), 2).PadLeft(4, '0');
                }
                catch (Exception exp)
                {
                    for (int j = 0; j < dataGridView4.RowCount; j++)
                    {
                        if (dataGridView4.Rows[j].Cells[0].Value != null && dataGridView4.Rows[j].Cells[0].Value.ToString() == s[s.Length - 1])
                            instructionFormat += Convert.ToString(Convert.ToInt16(dataGridView4.Rows[j].Cells[1].Value.ToString()), 2).PadLeft(4, '0');

                    }
                }

            }
            else if (instruction[0].Equals("IN") || instruction[0].Equals("OUT"))
            {
                if (instruction[0].Equals("IN"))
                {
                    instructionFormat += "01110";

                    if (s[0].Equals("R1")) { instructionFormat += R1; }
                    else if (s[0].Equals("R2")) { instructionFormat += R2; }
                    else if (s[0].Equals("R3")) { instructionFormat += R3; }
                    else if (s[0].Equals("R4")) { instructionFormat += R4; }

                    instructionFormat += "00";
                }
                else if (instruction[0].Equals("OUT"))
                {
                    instructionFormat += "01111";
                    instructionFormat += "00";

                    if (s[0].Equals("R1")) { instructionFormat += R1; }
                    else if (s[0].Equals("R2")) { instructionFormat += R2; }
                    else if (s[0].Equals("R3")) { instructionFormat += R3; }
                    else if (s[0].Equals("R4")) { instructionFormat += R4; }
                }


                if (instruction[instruction.Length - 1].Equals("B"))
                {
                    instructionFormat += "1";
                    try
                    {
                        instructionFormat += Convert.ToString(Convert.ToInt16(s[s.Length - 1]), 2).PadLeft(4, '0');
                    }
                    catch (Exception exp)
                    {
                        for (int j = 0; j < dataGridView4.RowCount; j++)
                        {
                            if (dataGridView4.Rows[j].Cells[0].Value != null && dataGridView4.Rows[j].Cells[0].Value.ToString() == s[s.Length - 1])
                                instructionFormat += Convert.ToString(Convert.ToInt16(dataGridView4.Rows[j].Cells[1].Value.ToString()), 2).PadLeft(4, '0');

                        }
                    }
                }
                else
                {
                    instructionFormat += "0";
                    instructionFormat += "0000";
                }



            }
            else if (instruction[0].Equals("JMP"))
            {
                instructionFormat += "01001";

                if (s[0].Equals("R1")) { instructionFormat += R1; }
                else if (s[0].Equals("R2")) { instructionFormat += R2; }
                else if (s[0].Equals("R3")) { instructionFormat += R3; }
                else if (s[0].Equals("R4")) { instructionFormat += R4; }

                instructionFormat += "00";

                if (instruction[instruction.Length - 1].Equals("B"))
                    instructionFormat += "1";
                else
                    instructionFormat += "0";

                try
                {
                    instructionFormat += Convert.ToString(Convert.ToInt16(s[s.Length - 1]), 2).PadLeft(4, '0');
                }
                catch (Exception exp)
                {
                    for (int j = 0; j < dataGridView4.RowCount; j++)
                    {
                        if (dataGridView4.Rows[j].Cells[0].Value != null && dataGridView4.Rows[j].Cells[0].Value.ToString() == s[s.Length - 1])
                            instructionFormat += Convert.ToString(Convert.ToInt16(dataGridView4.Rows[j].Cells[1].Value.ToString()), 2).PadLeft(4, '0');

                    }
                }

            }
            else if (instruction[0].Equals("CALL"))
            {
                instructionFormat += "01010";
                instructionFormat += "0000";
                if (instruction[instruction.Length - 1].Equals("B"))
                    instructionFormat += "1";
                else
                    instructionFormat += "0";
                for (int i = 0; i < 4; i++)
                    instructionFormat += "0";

            }
            else if (instruction[0].Equals("RET"))
            {
                instructionFormat += "01011";
                instructionFormat += "000000000";
            }

            dataGridView1.Rows[programCounter].Cells[1].Value = instructionFormat;
            programCounter++;

        }
        

        private void saveButton_Click(object sender, EventArgs e)
        {

        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            
            if (lstInstructions.SelectedIndex != lstInstructions.Items.Count - 1)
                {
                    lstInstructions.SelectedIndex++;

                    if (pc_flag == false)
                    {
                        programCounter++;
                        tempPC = Convert.ToString(programCounter);
                        textBox2.Text = tempPC;
                    }
                }

             pc_flag = false;
             string s = lstInstructions.SelectedItem.ToString();
                s = s.Replace('\t', ' ');

                for (int j = 0; j < s.Length; j++)
                {
                    if (s.Substring(j, 1) == "#")
                    {
                        s = s.Substring(0, j - 1);
                        break;
                    }
                }

                s = s.Trim();

                if (!(s.Contains(':')))
                {
                  search(s);
                }
                 

              
            
        }


        private void search(string s){

            dataGridView5.Items.Clear();
            

                    string[] words_comm;
                    string[] words = s.Split(' ');
                    
                     microOperations(words);
                     stepButton.Enabled = true;
                    
                    

                    if (hex_dec_bın.Equals("BIN"))
                    {
                        hex_dec_bın = "DEC";
                        TypeOfNumbers();
                        converter_flag = true;
                    }

                    tempPC = Convert.ToString(programCounter);
                    textBox2.Text = tempPC;

                    if (words[0].Equals("LD")) {
                        words_comm = words[1].Split(',');
                        
                        int return_value=getValue(words_comm[1]);
                        string input = Convert.ToString(return_value);
                        addRegister(words_comm[0], input);

                        
                        
                    }

                    if (words[0].Equals("IN"))
                    {
                        string str = "";
                        string input = Microsoft.VisualBasic.Interaction.InputBox("Please enter the DECIMAL input to load", "INPUT", "Default", 0, 0);
                        if (hex_dec_bın.Equals("BIN")) { str = BinaryToDecimal(input); }
                        else { str = input;  }
                        textBox7.Text = str;
                        addRegister(words[1],  str);

                    }

                    if (words[0].Equals("OUT"))
                    {

                        int grid_index = Convert.ToInt16(words[1]);
                        textBox12.Text =Convert.ToString( dataGridView3.Rows[grid_index].Cells[1].Value);
                        
                        
                    }

                    if (words[0].Equals("SUB"))
                    {
                        words_comm = words[1].Split(',');
                        


                        int result = Convert.ToInt16(getRegister(words_comm[1])) - Convert.ToInt16(getRegister(words_comm[2]));
                        string input = Convert.ToString(result);
                            if (result < 0) { textBox13.Text = "1"; }
                            if (result == 0) { textBox9.Text = "1"; }
                            addRegister(words_comm[0], input);
                        

                    }

                    if (words[0].Equals("ADD"))
                    {
                        words_comm = words[1].Split(',');
                         

                        int result = Convert.ToInt16(getRegister(words_comm[1])) + Convert.ToInt16(words_comm[2]);
                        string input = Convert.ToString(result);
                             if (result > 15) { textBox14.Text = "1"; }
                      

                        addRegister(words_comm[0], input);
                       
                        
                    }

                    if (words[0].Equals("ST"))
                    {
                        words_comm = words[1].Split(',');
                       
                        int data_getIndex = getIndex(words_comm[1]);
                        dataGridView3.Rows[data_getIndex].Cells[1].Value = Convert.ToInt16(getRegister(words_comm[0]));

                        
                    }

                    if (words[0].Equals("JMP"))
                    {
                       
                        words_comm = words[1].Split(',');
                        returnLine(words_comm[1]);
                    }


                    if (words[0].Equals("HLT"))
                    {
                        nextButton.Enabled = false;
                       
                    }

                    if (words[0].Equals("CALL"))
                    {
                        
                        callReturn = lstInstructions.SelectedIndex;
                        dataGridView2.Rows[stack_count].Cells[1].Value = textBox2.Text;
                        stack_count++;
                        textBox8.Text = Convert.ToString(stack_count);
                        returnLine(words[1]);
                    }

                    if (words[0].Equals("RET"))
                    {
                        stack_count--;
                        textBox8.Text = Convert.ToString(stack_count);
                        dataGridView2.Rows[stack_count].Cells[1].Value = "0000";
                        
                        lstInstructions.SelectedIndex = callReturn+1;
                    }

                    if (words[0].Equals("SHR"))
                    {
                        words_comm = words[1].Split(',');
                        int convert = Convert.ToInt16(getRegister(words_comm[1]));
                        convert = convert >> 1;
                        string string_convert = Convert.ToString(convert);
                        addRegister(words_comm[0], string_convert);
                    }

                    if (words[0].Equals("SHL"))
                    {
                        words_comm = words[1].Split(',');
                        int convert = Convert.ToInt16(getRegister(words_comm[1]));
                        convert = convert << 1;
                        string string_convert = Convert.ToString(convert);
                        addRegister(words_comm[0], string_convert);
                    }


                    if (words[0].Equals("XOR"))
                    {
                        words_comm = words[1].Split(',');
                        int convert = Convert.ToInt16(getRegister(words_comm[1]));
                        int convert2 = Convert.ToInt16(getRegister(words_comm[2]));
                        int xor = convert ^ convert2;
                        string string_convert = Convert.ToString(xor);
                        addRegister(words_comm[0], string_convert);
                    }

                    if (words[0].Equals("AND"))
                    {
                        words_comm = words[1].Split(',');
                        int convert = Convert.ToInt16(getRegister(words_comm[1]));
                        int convert2 = Convert.ToInt16(getRegister(words_comm[2]));
                        int xor = convert & convert2;
                        string string_convert = Convert.ToString(xor);
                        addRegister(words_comm[0], string_convert);
                    }

                    if (words[0].Equals("OR"))
                    {
                        words_comm = words[1].Split(',');
                        int convert = Convert.ToInt16(getRegister(words_comm[1]));
                        int convert2 = Convert.ToInt16(getRegister(words_comm[2]));
                        int xor = convert | convert2;
                        string string_convert = Convert.ToString(xor);
                        addRegister(words_comm[0], string_convert);
                    }

                    if (words[0].Equals("INC"))
                    {
                        words_comm = words[1].Split(',');

                         
                        int convert = Convert.ToInt16(getRegister(words_comm[1]));
                        convert = convert + 1;
                        if (convert > 15) { textBox14.Text = "1"; }
                        string string_convert = Convert.ToString(convert);
                        addRegister(words_comm[0], string_convert);
                       

                    }

                    if (words[0].Equals("COMA"))
                    {
                        words_comm = words[1].Split(',');


                        int convert = Convert.ToInt16(getRegister(words_comm[1]));
                        convert = ~convert;
                        string string_convert = Convert.ToString(convert);
                        addRegister(words_comm[0], string_convert);

                    }



                    if (converter_flag == true)
                    {
                        hex_dec_bın = "BIN";
                        TypeOfNumbers();
                        converter_flag = false;
                    }
        
        
        
        }





        private void returnLine(string ser) { 
             string s;
             string[] split=null;

             for (int t = lstInstructions.SelectedIndex; t < lstInstructions.Items.Count; t++)
             {


                 if (lstInstructions.SelectedIndex != lstInstructions.Items.Count - 1)
                 {
                     lstInstructions.SelectedIndex++;
                 }

                 s = lstInstructions.SelectedItem.ToString();
                 
                 if (s.Contains(':'))
                 {
                     split = s.Split(':');
                     if (split[0].Equals(ser))
                     {


                         split[1] = split[1].Replace('\t', ' ');

                         for (int j = 0; j < split[1].Length; j++)
                         {
                             if (split[1].Substring(j, 1) == "#")
                             {
                                 split[1] = split[1].Substring(0, j - 1);
                                 break;
                             }
                         }


                         split[1] = split[1].Trim();
                         lstInstructions.SelectedIndex = t + 1;
                         break;
                     }
                 }

                
             }

                
                
                     search(split[1]);
                     
        
        
        }



        private string getRegister(string register) {

            if (register.Equals("R1")) { return textBox3.Text; }
            else if (register.Equals("R2")) {  return textBox4.Text;  }
            else if (register.Equals("R3")) { return textBox5.Text ;  }
            else  {  return textBox6.Text ;  }

        
        }

        private int getValue(string value) {
        
            int getvalue_count=0;
            while (!(dataGridView4.Rows[getvalue_count].Cells[0].Value.Equals(value)))
            {

            getvalue_count++;
        }
            int return_value=Convert.ToInt16(dataGridView4.Rows[getvalue_count].Cells[2].Value);
            return return_value;
        }

        private int getIndex(string value)
        {

            int getvalue_count = 0;
            while (!(dataGridView4.Rows[getvalue_count].Cells[0].Value.Equals(value)))
            {

                getvalue_count++;
            }
            int return_value = Convert.ToInt16(dataGridView4.Rows[getvalue_count].Cells[1].Value);
            return return_value;
        }

        private void addRegister(string register, string return_value)
        {
            if (register.Equals("R1")) {  textBox3.Text =return_value;  }
            else if (register.Equals("R2")) {  textBox4.Text = return_value;  }
            else if (register.Equals("R3")) {  textBox5.Text = return_value;  }
            else if (register.Equals("R4")) {  textBox6.Text =return_value;  }
        }

        private void TypeOfNumbers() { 
        


         if (hex_dec_bın.Equals("BIN")) {

             if ((!textBox1.Text.Equals("")) && (!textBox1.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox1.Text)); textBox1.Text = convert_numberType; }
             if ((!textBox2.Text.Equals("")) && (!textBox2.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox2.Text)); textBox2.Text = convert_numberType; }
             if ((!textBox3.Text.Equals("")) && (!textBox3.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox3.Text)); textBox3.Text = convert_numberType; }
             if ((!textBox4.Text.Equals("")) && (!textBox4.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox4.Text)); textBox4.Text = convert_numberType; }
             if ((!textBox5.Text.Equals("")) && (!textBox5.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox5.Text)); textBox5.Text = convert_numberType; }
             if ((!textBox6.Text.Equals("")) && (!textBox6.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox6.Text)); textBox6.Text = convert_numberType; }
             if ((!textBox7.Text.Equals("")) && (!textBox7.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox7.Text)); textBox7.Text = convert_numberType; }
             if ((!textBox8.Text.Equals("")) && (!textBox8.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox8.Text)); textBox8.Text = convert_numberType; }
             if ((!textBox9.Text.Equals("")) && (!textBox9.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox9.Text)); textBox9.Text = convert_numberType; }
             if ((!textBox12.Text.Equals("")) && (!textBox12.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox12.Text)); textBox12.Text = convert_numberType; }
             if ((!textBox13.Text.Equals("")) && (!textBox13.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox13.Text)); textBox13.Text = convert_numberType; }
             if ((!textBox14.Text.Equals("")) && (!textBox14.Text.Equals("0000"))) { string convert_numberType = DecimalToBinary(Convert.ToString(textBox14.Text)); textBox14.Text = convert_numberType; }


             for (int i = 0; i < dataGridView3.RowCount; i++)
             {
                 if ((!Convert.ToString(dataGridView3.Rows[i].Cells[1].Value).Equals("")) && (!Convert.ToString(dataGridView3.Rows[i].Cells[1].Value).Equals("0000")))
                 {
                     string convert_numberType = DecimalToBinary(Convert.ToString(dataGridView3.Rows[i].Cells[1].Value)); dataGridView3.Rows[i].Cells[1].Value = convert_numberType;
                 }
             }

             for (int k = 0; k < dataGridView2.RowCount; k++)
             {
                 if ((!Convert.ToString(dataGridView2.Rows[k].Cells[1].Value).Equals("")) && (!Convert.ToString(dataGridView2.Rows[k].Cells[1].Value).Equals("0000")))
                 {
                     string convert_numberType = DecimalToBinary(Convert.ToString(dataGridView2.Rows[k].Cells[1].Value)); dataGridView2.Rows[k].Cells[1].Value = convert_numberType;
                 }
             }

             for (int j = 0; j < dataGridView4.RowCount; j++)
             {
                 if ((!Convert.ToString(dataGridView4.Rows[j].Cells[1].Value).Equals("")) && (!Convert.ToString(dataGridView4.Rows[j].Cells[1].Value).Equals("0000")))
                 {
                     string convert_numberType = DecimalToBinary(Convert.ToString(dataGridView4.Rows[j].Cells[1].Value)); dataGridView4.Rows[j].Cells[1].Value = convert_numberType;
                 }
                 if ((!Convert.ToString(dataGridView4.Rows[j].Cells[2].Value).Equals("")) && (!Convert.ToString(dataGridView4.Rows[j].Cells[2].Value).Equals("0000")))
                 {
                     string convert_numberType2 = DecimalToBinary(Convert.ToString(dataGridView4.Rows[j].Cells[2].Value)); dataGridView4.Rows[j].Cells[2].Value = convert_numberType2;
                 }
             }

         }

        else if (hex_dec_bın.Equals("DEC")) {
           
            if ((!textBox1.Text.Equals("")) && (!textBox1.Text.Equals("0000"))) { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox1.Text)); textBox1.Text = convert_numberType; }
            if ((!textBox2.Text.Equals("")) && (!textBox2.Text.Equals("0000"))) { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox2.Text)); textBox2.Text = convert_numberType; }
            if ((!textBox3.Text.Equals("")) && (!textBox3.Text.Equals("0000"))) { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox3.Text)); textBox3.Text = convert_numberType; }
            if ((!textBox4.Text.Equals("")) && (!textBox4.Text.Equals("0000"))) { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox4.Text)); textBox4.Text = convert_numberType; }
            if ((!textBox5.Text.Equals("")) && (!textBox5.Text.Equals("0000"))) { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox5.Text)); textBox5.Text = convert_numberType; }
            if ((!textBox6.Text.Equals("")) && (!textBox6.Text.Equals("0000"))) { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox6.Text)); textBox6.Text = convert_numberType; }
            if ((!textBox7.Text.Equals("")) && (!textBox7.Text.Equals("0000")))  { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox7.Text)); textBox7.Text = convert_numberType; }
            if ((!textBox8.Text.Equals("")) && (!textBox8.Text.Equals("0000"))) { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox8.Text)); textBox8.Text = convert_numberType; }
            if ((!textBox9.Text.Equals("")) && (!textBox9.Text.Equals("0000"))) { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox9.Text)); textBox9.Text = convert_numberType; }
            if ((!textBox12.Text.Equals("")) && (!textBox12.Text.Equals("0000"))) { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox12.Text)); textBox12.Text = convert_numberType; }
            if ((!textBox13.Text.Equals("")) && (!textBox13.Text.Equals("0000"))) { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox13.Text)); textBox13.Text = convert_numberType; }
            if ((!textBox14.Text.Equals("")) && (!textBox14.Text.Equals("0000"))) { string convert_numberType = BinaryToDecimal(Convert.ToString(textBox14.Text)); textBox14.Text = convert_numberType; }


    
            for (int j = 0; j < dataGridView4.RowCount; j++)
            {
                if ((!Convert.ToString(dataGridView4.Rows[j].Cells[1].Value).Equals("")) && (!Convert.ToString(dataGridView4.Rows[j].Cells[1].Value).Equals("0000")))
                {
                string convert_numberType = BinaryToDecimal(Convert.ToString(dataGridView4.Rows[j].Cells[1].Value)); dataGridView4.Rows[j].Cells[1].Value = convert_numberType; 
                }
                if ((!Convert.ToString(dataGridView4.Rows[j].Cells[2].Value).Equals("")) && (!Convert.ToString(dataGridView4.Rows[j].Cells[2].Value).Equals("0000")))
                {
                string convert_numberType2 = BinaryToDecimal(Convert.ToString(dataGridView4.Rows[j].Cells[2].Value)); dataGridView4.Rows[j].Cells[2].Value = convert_numberType2;
            }
        }

            for (int k = 0; k < dataGridView2.RowCount; k++)
            {
                if ((!Convert.ToString(dataGridView2.Rows[k].Cells[1].Value).Equals("")) && (!Convert.ToString(dataGridView2.Rows[k].Cells[1].Value).Equals("0000")))
                {
                    string convert_numberType = BinaryToDecimal(Convert.ToString(dataGridView2.Rows[k].Cells[1].Value)); dataGridView2.Rows[k].Cells[1].Value = convert_numberType;
                }
            }


            for (int i = 0; i < dataGridView3.RowCount; i++)
            {
                if ((!Convert.ToString(dataGridView3.Rows[i].Cells[1].Value).Equals("")) && (!Convert.ToString(dataGridView3.Rows[i].Cells[1].Value).Equals("0000")))
                {
                    string convert_numberType = BinaryToDecimal(Convert.ToString(dataGridView3.Rows[i].Cells[1].Value)); dataGridView3.Rows[i].Cells[1].Value = convert_numberType;
                }
            }

        
        }

        
        
        
        
        
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                    if (comboBox1.SelectedItem == "Binary")
                    {
                        if (!(hex_dec_bın.Equals("BIN")))
                        {
                            hex_dec_bın = "BIN"; TypeOfNumbers();
                        }

                    }
                    
                    else if (comboBox1.SelectedItem == "Decimal")
                    {
                        if (!(hex_dec_bın.Equals("DEC")))
                        {
                        hex_dec_bın = "DEC"; TypeOfNumbers(); }
                    }
                        

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void debugButton_Click(object sender, EventArgs e)
        {
            Boolean flag_debug = false;
            string s;
            string[] words=null;
            int debug_index=0;
           

            for (int t = 0; t < lstInstructions.Items.Count; t++)
            {
                
                
                if (lstInstructions.SelectedIndex != lstInstructions.Items.Count - 1)
                {
                    lstInstructions.SelectedIndex++;
                }

                s = lstInstructions.SelectedItem.ToString();
                s = s.Replace('\t', ' ');

                for (int j = 0; j < s.Length; j++)
                {
                    if (s.Substring(j, 1) == "#")
                    {
                        s = s.Substring(0, j - 1);
                        break;
                    }
                }

                s = s.Trim();

                if (flag_debug == true)
                {
                    if (!(s.Contains("END")))
                    {
                        string[] doublecom = s.Split(':');
                        doublecom[0] = doublecom[0].Trim();
                        doublecom[1] = doublecom[1].Trim();

                        
                        dataGridView4.Rows[labelcount].Cells[0].Value = doublecom[0];
                        dataGridView4.Rows[labelcount].Cells[1].Value = debug_index;


                        string[] doublecomsplit = doublecom[1].Split(' ');  // doublecomsplit[0] da hex,dec olup olmadığı yazılı

                        
                        hex_dec_bın = "DEC";
                        
                        string str="";  // girilen değerin hex,dec,bin olup olmadığına bakıp bizim default olarak belirlediğimiz decimal'e çeviriyor
                        if (doublecomsplit[0].Equals("BIN")) { str=BinaryToDecimal(Convert.ToString(doublecomsplit[1])); }

                        else if (doublecomsplit[0].Equals("HEX")) { str = Convert.ToString(doublecomsplit[1]); }

                        else { str = Convert.ToString(doublecomsplit[1]); }

                        
                        dataGridView4.Rows[labelcount].Cells[2].Value = str;
                      
                        dataGridView3.Rows[debug_index].Cells[1].Value = str;

                        debug_index++;
                        labelcount++;
                    }
                }


                if ((!(s.Contains(':'))) && (!(s.Contains("END"))))
                {
                    words = s.Split(' ');

                    if (insflag > 0)
                    {
                        instruntionMemory(words);
                    }
                    if (words[0].Trim().Equals("ORG"))
                    {

                        
                        if (words[1].Trim().Equals("D"))
                        {
                            flag_debug = true;
                        }
                        debug_index = Convert.ToInt16(words[2]);
                    }
                }  
                
                

                 }
            programCounter = 0;
            lstInstructions.SelectedIndex = 0;
            insflag++;
            if (insflag < 2)
            {
                labelcount = 0;
                debugButton_Click(null, null);
            }
                }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            
            if (dataGridView5.SelectedIndex != dataGridView5.Items.Count - 1)
            {
                dataGridView5.SelectedIndex++;

            }

            else {stepButton.Enabled = false; }

            string st = dataGridView5.SelectedItem.ToString();
            if (st.Equals("Fetch: T-1: PC <- PC + 1\n\n"))
            {
                if (hex_dec_bın.Equals("BIN"))
                {
                    hex_dec_bın = "DEC";
                    TypeOfNumbers();
                    converter_flag = true;
                }
                programCounter++;
                tempPC = Convert.ToString(programCounter);
                textBox2.Text = tempPC;

                if (converter_flag == true)
                {
                    hex_dec_bın = "BIN";
                    TypeOfNumbers();
                    converter_flag = false;
                }

                pc_flag = true;
        
            }

        }

        

        private void fileToolStripMenuItem_Click(object sender, EventArgs e){}

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            StreamWriter file = new StreamWriter("T:\\instruction.mif");
            file.WriteLine("DEPTH = 32;                   -- The size of memory in words");
            file.WriteLine("WIDTH = 14;                    -- The size of data in bits");
            file.WriteLine("ADDRESS_RADIX = HEX;          -- The radix for address values");
            file.WriteLine("DATA_RADIX = BIN;             -- The radix for data values");
            file.WriteLine("CONTENT;                      -- start of (address : data pairs)");
            file.WriteLine("BEGIN;");
            for (int i = 0; i < 32; i++)
            {
                if (i < 16)
                    file.Write("0");
                if (dataGridView1.Rows[i].Cells[1].Value != null)
                    file.Write(String.Format("{0:X}", Convert.ToInt16(i)) + " : " + dataGridView1.Rows[i].Cells[1].Value.ToString());
                else
                    file.Write(String.Format("{0:X}", Convert.ToInt16(i)) + " : " + "00000000000000");
                file.WriteLine(";");

            }
            file.WriteLine("END;");
            file.Close();

            StreamWriter file2 = new StreamWriter("T:\\stack.mif");
            file2.WriteLine("DEPTH = 8;                   -- The size of memory in words");
            file2.WriteLine("WIDTH = 5;                    -- The size of data in bits");
            file2.WriteLine("ADDRESS_RADIX = HEX;          -- The radix for address values");
            file2.WriteLine("DATA_RADIX = BIN;             -- The radix for data values");
            file2.WriteLine("CONTENT;                      -- start of (address : data pairs)");
            file2.WriteLine("BEGIN;");
            for (int i = 0; i < 8; i++)
            {
                if (i < 16)
                    file2.Write("0");
                if (dataGridView2.Rows[i].Cells[1].Value != null)
                    file2.Write(String.Format("{0:X}", Convert.ToInt16(i)) + " : " + dataGridView2.Rows[i].Cells[1].Value.ToString());
                else
                    file2.Write(String.Format("{0:X}", Convert.ToInt16(i)) + " : " + "00000");
                file2.WriteLine(";");
            }
            file2.WriteLine("END;");
            file2.Close();

            StreamWriter file3 = new StreamWriter("T:\\data.mif");
            file3.WriteLine("DEPTH = 16;                   -- The size of memory in words");
            file3.WriteLine("WIDTH = 4;                    -- The size of data in bits");
            file3.WriteLine("ADDRESS_RADIX = HEX;          -- The radix for address values");
            file3.WriteLine("DATA_RADIX = BIN;             -- The radix for data values");
            file3.WriteLine("CONTENT;                      -- start of (address : data pairs)");
            file3.WriteLine("BEGIN;");
            for (int i = 0; i < 16; i++)
            {
                if (i < 16)
                    file3.Write("0");
                if (dataGridView3.Rows[i].Cells[1].Value != null)
                    file3.Write(String.Format("{0:X}", Convert.ToInt16(i)) + " : " + dataGridView3.Rows[i].Cells[1].Value.ToString());
                else
                    file3.Write(String.Format("{0:X}", Convert.ToInt16(i)) + " : " + "0000");
                file3.WriteLine(";");
            }
            file3.WriteLine("END;");
            file3.Close();


            MessageBox.Show("Successfully saved.");
        }

        private void lstInstructions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

        
    }
}
