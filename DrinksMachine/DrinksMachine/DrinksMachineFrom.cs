using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrinksMachine
{
    public partial class DrinksMachineForm : Form
    {
        private Coke coke;
        private Pepsi pepsi;
        private Soda soda;
        private CustomerChange change;

        public DrinksMachineForm()
        {
            InitializeComponent();

            coke = new Coke(25, 5);
            pepsi = new Pepsi(36, 15);
            soda = new Soda(45, 3);

            change = new CustomerChange(100, 10, 5, 25);

            tbPenny.Text = change.Penny.ToString();
            tbNickel.Text = change.Nickel.ToString();
            tbDime.Text = change.Dime.ToString();
            tbQuarter.Text = change.Quarter.ToString();

            tbCoke.Text = coke.OrderQuantity.ToString();
            tbPepsi.Text = pepsi.OrderQuantity.ToString();
            tbSoda.Text = soda.OrderQuantity.ToString();

            lbCoke.Text = coke.ToString();
            lbPepsi.Text = pepsi.ToString();
            lbSoda.Text = soda.ToString();

            lbTotal.Text = (getTotalCharge() / 100) + "/" + (getTotalCharge() % 100);
        }

        private void btGetDrinks_Click(object sender, EventArgs e)
        {
            // 1.1 Validate that the input for each coin type is not negative.
            if (tbPenny.Text.Equals("") || (int.Parse(tbPenny.Text) < 0))
            {
                MessageBox.Show("Please enter valid number for Penny.");
                return;
            }
            else if (tbNickel.Text.Equals("") || (int.Parse(tbNickel.Text) < 0))
            {
                MessageBox.Show("Please enter valid number for Nickel.");
                return;
            }
            else if (tbDime.Text.Equals("") || (int.Parse(tbDime.Text) < 0))
            {
                MessageBox.Show("Please enter valid number for Dime.");
                return;
            }
            else if (tbQuarter.Text.Equals("") || (int.Parse(tbQuarter.Text) < 0))
            {
                MessageBox.Show("Please enter valid number for Quarter.");
                return;
            }
            else if (tbCoke.Text.Equals("") || (int.Parse(tbCoke.Text) < 0))
            {
                MessageBox.Show("Please enter valid number for Coke.");
                return;
            }
            else if (tbPepsi.Text.Equals("") || (int.Parse(tbPepsi.Text) < 0))
            {
                MessageBox.Show("Please enter valid number for Pepsi.");
                return;
            }
            else if (tbSoda.Text.Equals("") || (int.Parse(tbSoda.Text) < 0))
            {
                MessageBox.Show("Please enter valid number for Soda.");
                return;
            }

            // 1.2 the total is greater than zero.
            if (getTotalCharge() <= 0)
            {
                MessageBox.Show("Please add at least one order to purchase.");
                return;
            }

            // 5.2 The user must select at the minimum one drink.
            if ((coke.OrderQuantity + pepsi.OrderQuantity + soda.OrderQuantity) == 0)
            {
                MessageBox.Show("Please select at the minimum one drink.");
                return;
            }

            // 7. If the machine does not have enough drinks, it should display an error message that says: “Drink is sold out, your purchase cannot be processed”. 
            if ((coke.AvailableQuantity + pepsi.AvailableQuantity + soda.AvailableQuantity) == 0)
            {
                MessageBox.Show("Drink is sold out, your purchase cannot be processed.");
                return;
            }
            
            if (coke.OrderQuantity > coke.AvailableQuantity)
            {
                MessageBox.Show("Less than " + coke.OrderQuantity + " Coke is/are available.");
                return;
            }
            else if (pepsi.OrderQuantity > pepsi.AvailableQuantity)
            {
                MessageBox.Show("Less than " + pepsi.OrderQuantity + " Pepsi is/are available.");
                return;
            }
            else if (soda.OrderQuantity > soda.AvailableQuantity)
            {
                MessageBox.Show("Less than " + soda.OrderQuantity + " Soda is/are available.");
                return;
            }

            // 8. If the machine does not have sufficient change, it should display an error message that says: “Not sufficient change in the inventory”.
            if (!hasSufficientChange())
                return;

            // 6. The drink quantity must be subtracted and reflect the correct number after the purchase in the inventory.
            coke.AvailableQuantity -= coke.OrderQuantity;
            pepsi.AvailableQuantity -= pepsi.OrderQuantity;
            soda.AvailableQuantity -= soda.OrderQuantity;

            // 5.1 After pressing “Ok”, the application will clear the coins and products information to start a new purchase.
            MessageBox.Show("Resume of Order: Coke [" + coke.OrderQuantity + "], Pepsi [" + pepsi.OrderQuantity + "], Soda [" + soda.OrderQuantity + "] Total Cost: " + getTotalCharge());

            change.Penny = 0;
            change.Nickel = 0;
            change.Dime = 0;
            change.Quarter = 0;
            coke.OrderQuantity = 0;
            pepsi.OrderQuantity = 0;
            soda.OrderQuantity = 0;

            tbPenny.Text = "";
            tbNickel.Text = "";
            tbDime.Text = "";
            tbQuarter.Text = "";
            tbCoke.Text = "";
            tbPepsi.Text = "";
            tbSoda.Text = "";

            lbTotal.Text = (getTotalCharge() / 100) + "/" + (getTotalCharge() % 100);

            // 2. Return selected drink(s) and remaining change if any
            lbCoke.Text = coke.ToString();
            lbPepsi.Text = pepsi.ToString();
            lbSoda.Text = soda.ToString();

            // 3. Validate there are available drinks in the machine, if not, the purchase input should be disabled.
            if (coke.AvailableQuantity <= 0)
            {
                tbCoke.Enabled = false;
            }

            if (pepsi.AvailableQuantity <= 0)
            {
                tbPepsi.Enabled = false;
            }

            if (soda.AvailableQuantity <= 0)
            {
                tbSoda.Enabled = false;
            }
        }

        private void tbPenny_TextChanged(object sender, EventArgs e)
        {
            if (tbPenny.Text.Equals(""))
            {
                change.Penny = 0;
                return;
            }

            if (!isValid(tbPenny.Text))
            {
                tbPenny.Text = change.Penny.ToString();
                return;
            }

            change.Penny = int.Parse(tbPenny.Text);

            hasSufficientChange();
        }

        private void tbNickel_TextChanged(object sender, EventArgs e)
        {
            if (tbNickel.Text.Equals(""))
            {
                change.Nickel = 0;
                return;
            }

            if (!isValid(tbNickel.Text))
            {
                tbNickel.Text = change.Nickel.ToString();
                return;
            }

            change.Nickel = int.Parse(tbNickel.Text);

            hasSufficientChange();
        }

        private void tbDime_TextChanged(object sender, EventArgs e)
        {
            if (tbDime.Text.Equals(""))
            {
                change.Dime = 0;
                return;
            }

            if (!isValid(tbDime.Text))
            {
                tbDime.Text = change.Dime.ToString();
                return;
            }

            change.Dime = int.Parse(tbDime.Text);

            hasSufficientChange();
        }

        private void tbQuarter_TextChanged(object sender, EventArgs e)
        {
            if (tbQuarter.Text.Equals(""))
            {
                change.Quarter = 0;
                return;
            }

            if (!isValid(tbQuarter.Text))
            {
                tbQuarter.Text = change.Quarter.ToString();
                return;
            }

            change.Quarter = int.Parse(tbQuarter.Text);

            hasSufficientChange();
        }

        private void tbCoke_TextChanged(object sender, EventArgs e)
        {
            if (tbCoke.Text.Equals(""))
            {
                coke.OrderQuantity = 0;
                return;
            }

            if (!isValid(tbCoke.Text))
            {
                tbCoke.Text = coke.OrderQuantity.ToString();
                return;
            }

            coke.OrderQuantity = int.Parse(tbCoke.Text);

            // 4. Each time the user changes the number of purchases for a drink, the total is going to be updated.The inputs can’t accept a value less than 0.
            lbTotal.Text = getTotalString();

            hasSufficientChange();
        }

        private void tbPepsi_TextChanged(object sender, EventArgs e)
        {
            if (tbPepsi.Text.Equals(""))
            {
                pepsi.OrderQuantity = 0;
                return;
            }

            if (!isValid(tbPepsi.Text))
            {
                tbPepsi.Text = pepsi.OrderQuantity.ToString();
                return;
            }

            pepsi.OrderQuantity = int.Parse(tbPepsi.Text);

            lbTotal.Text = getTotalString();

            hasSufficientChange();
        }

        private void tbSoda_TextChanged(object sender, EventArgs e)
        {
            if (tbSoda.Text.Equals(""))
            {
                soda.OrderQuantity = 0;
                return;
            }

            if (!isValid(tbSoda.Text))
            {
                tbSoda.Text = soda.OrderQuantity.ToString();
                return;
            }

            soda.OrderQuantity = int.Parse(tbSoda.Text);

            lbTotal.Text = getTotalString();

            hasSufficientChange();   
        }

        private string getTotalString()
        {
            return (getTotalCharge() / 100) + "/" + (getTotalCharge() % 100);
        }

        private bool isValid(String inputString)
        {
            try
            {
                int input = int.Parse(inputString);

                if (input < 0)
                {
                    MessageBox.Show("Number of coins should zero or greater than zero. Please enter valid number.");
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                MessageBox.Show("Invalid input. Please enter valid number.");
            }

            return false;
        }

        private int getAvailableChange()
        {
            return change.Penny + change.Nickel * 5 + change.Dime * 10 + change.Quarter * 25;
        }

        private int getTotalCharge()
        {
            return coke.Cost * coke.OrderQuantity + pepsi.Cost * pepsi.OrderQuantity + soda.Cost * soda.OrderQuantity;
        }

        private bool hasSufficientChange()
        {
            if (getTotalCharge() > getAvailableChange())
            {
                MessageBox.Show("Not sufficient change in the inventory.");
                return false;
            }   
            
            return true;
        }
    }
}
