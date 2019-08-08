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

namespace ChangeMachineWF
{
    public partial class Form1 : Form
    {
        Coins machineCoins;
        Coins change;
        public Form1()
        {
            InitializeComponent();
            machineCoins = new Coins();
            change = new Coins();

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            if (File.Exists("Coins.data"))
            {
                machineCoins = (Coins)Persistence.Deserialize(machineCoins.GetType());
            }

            updateTotals();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            double.TryParse(Change.Text, out double changeValue);
            change.Clear();

            if (changeValue > 0)
            {
               if(!ChangeCalc(changeValue))
                {
                    MessageBox.Show("Não há moedas suficientes.");
                    machineCoins.Add(Coin.One, change.One);
                    machineCoins.Add(Coin.FiftyCents, change.FiftyCents);
                    machineCoins.Add(Coin.TwentyFiveCents, change.TwentyFiveCents);
                    machineCoins.Add(Coin.TenCents, change.TenCents);
                    machineCoins.Add(Coin.FiveCents, change.FiveCents);
                    machineCoins.Add(Coin.OneCent, change.OneCent);

                    change.Clear();

                    updateTotals();

                }

                updateTotals();
            }
        }

        private bool ChangeCalc(double changeValue)
        {
            bool hasValidCoins = true;

            while ( (changeValue > 0) && machineCoins.hasCoins && hasValidCoins)
            {
                changeValue = Math.Round(changeValue, 2);

                if ((changeValue - 1) >= 0)
                {
                    if (machineCoins.Bleed(Coin.One, 1))
                    {
                        changeValue -= 1;
                        
                        change.Add(Coin.One, 1);
                        continue;
                    }
                }

                if ((changeValue - 0.5) >= 0)
                {
                    if (machineCoins.Bleed(Coin.FiftyCents, 1))
                    {
                        changeValue -= 0.5;
                        change.Add(Coin.FiftyCents, 1);
                        continue;
                    }
                }

                if ((changeValue - 0.25) >= 0)
                {
                    if (machineCoins.Bleed(Coin.TwentyFiveCents, 1))
                    {
                        changeValue -= 0.25;
                        change.Add(Coin.TwentyFiveCents, 1);
                        continue;
                    }
                }

                if ((changeValue - 0.10) >= 0)
                {
                    if (machineCoins.Bleed(Coin.TenCents, 1))
                    {
                        changeValue -= 0.1;
                        change.Add(Coin.TenCents, 1);
                        continue;
                    }
                }

                if ((changeValue - 0.05) >= 0)
                {
                    if (machineCoins.Bleed(Coin.FiveCents, 1))
                    {
                        changeValue -= 0.05;
                        change.Add(Coin.FiveCents, 1);
                        continue;
                    }
                }

                if ((changeValue - 0.01) >= 0)
                {
                    if (machineCoins.Bleed(Coin.OneCent, 1))
                    {
                        changeValue -= 0.01;
                        change.Add(Coin.OneCent, 1);
                        continue;
                    }
                    else
                    {
                        hasValidCoins = false;
                    }
                }
            }

            return changeValue > 0 ? false : true;
        }

        private void UpdateCoins_Click(object sender, EventArgs e)
        {
            if (rbAdd.Checked)
            {
                AddCoins();
            }

            if (rbRemove.Checked)
            {
                BleedCoins();
            }

            updateTotals();
        }

        private void BleedCoins()
        {
            int excludedCoins = 0;

            int.TryParse(UpdateCoins100.Text, out excludedCoins);
            if (!machineCoins.Bleed(Coin.One, excludedCoins))
            {
                NoCoinsBleedMessage(Coin.One);
            }

            int.TryParse(UpdateCoins050.Text, out excludedCoins);
            if (!machineCoins.Bleed(Coin.FiftyCents, excludedCoins))
            {
                NoCoinsBleedMessage(Coin.FiftyCents);
            }

            int.TryParse(UpdateCoins025.Text, out excludedCoins);
            if (!machineCoins.Bleed(Coin.TwentyFiveCents, excludedCoins))
            {
                NoCoinsBleedMessage(Coin.TwentyFiveCents);
            }

            int.TryParse(UpdateCoins010.Text, out excludedCoins);
            if (!machineCoins.Bleed(Coin.TenCents, excludedCoins))
            {
                NoCoinsBleedMessage(Coin.TenCents);
            }

            int.TryParse(UpdateCoins005.Text, out excludedCoins);
            if (!machineCoins.Bleed(Coin.FiveCents, excludedCoins))
            {
                NoCoinsBleedMessage(Coin.FiveCents);
            }

            int.TryParse(UpdateCoins001.Text, out excludedCoins);
            if (!machineCoins.Bleed(Coin.OneCent, excludedCoins))
            {
                NoCoinsBleedMessage(Coin.OneCent);
            }

        }

        private void NoCoinsBleedMessage(Coin c)
        {
            string[] coinLabel = new string[] { "R$ 1,00", "R$ 0,50", "R$ 0,25", "R$ 0,10", "R$ 0,05", "R$ 0,01" };

            MessageBox.Show($"Não há moedas de { coinLabel[(int)c]  } suficientes. Não foram retiradas moedas deste valor.");
        }

        private void AddCoins()
        {
            int newCoins = 0;

            int.TryParse(UpdateCoins100.Text, out newCoins);
            machineCoins.Add(Coin.One, newCoins);

            int.TryParse(UpdateCoins050.Text, out newCoins);
            machineCoins.Add(Coin.FiftyCents, newCoins);

            int.TryParse(UpdateCoins025.Text, out newCoins);
            machineCoins.Add(Coin.TwentyFiveCents, newCoins);

            int.TryParse(UpdateCoins010.Text, out newCoins);
            machineCoins.Add(Coin.TenCents, newCoins);

            int.TryParse(UpdateCoins005.Text, out newCoins);
            machineCoins.Add(Coin.FiveCents, newCoins);

            int.TryParse(UpdateCoins001.Text, out newCoins);
            machineCoins.Add(Coin.OneCent, newCoins);
        }

        private void updateTotals()
        {
            currentCoins100.Text = machineCoins.One.ToString();
            currentCoins050.Text = machineCoins.FiftyCents.ToString();
            currentCoins025.Text = machineCoins.TwentyFiveCents.ToString();
            currentCoins010.Text = machineCoins.TenCents.ToString();
            currentCoins005.Text = machineCoins.FiveCents.ToString();
            currentCoins001.Text = machineCoins.OneCent.ToString();

            lbCurrentCoins100.Text = currentCoins100.Text;
            lbCurrentCoins050.Text = currentCoins050.Text;
            lbCurrentCoins025.Text = currentCoins025.Text;
            lbCurrentCoins010.Text = currentCoins010.Text;
            lbCurrentCoins005.Text = currentCoins005.Text;
            lbCurrentCoins001.Text = currentCoins001.Text;

            Change100.Text = change.One.ToString();
            Change050.Text = change.FiftyCents.ToString();
            Change025.Text = change.TwentyFiveCents.ToString();
            Change010.Text = change.TenCents.ToString();
            Change005.Text = change.FiveCents.ToString();
            Change001.Text = change.OneCent.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Persistence.Serialize<Coins>(machineCoins);

        }
    }
}
