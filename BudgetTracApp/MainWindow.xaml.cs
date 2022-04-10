using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BudgetTracApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string IncomeRadioBTNVale = "Yes";
        public string IncomeTypeCMBValue = "Income";
        public DateTime IncomeDate = DateTime.Today;
        public double IncomeTaxValue = 0.0f;
        public int IncomeUpdateIndex = 0;

        public string ExpenseTypeCMBValue = "Utility";
        public DateTime ExpenseDate = DateTime.Today;
        public int ExpenseUpdateIndex = 0;

        public string ReminderTypeCMBValue = "Utility";
        public DateTime ReminderDate = DateTime.Today;
        public int ReminderUpdateIndex = 0;

        public MainWindow()
        {
            InitializeComponent();

            BudgetTracDBEntities db = new BudgetTracDBEntities();

            var incomes = from i in db.Incomes orderby i.Date descending select i;
            var reminders = from i in db.Reminders orderby i.Date ascending select i;
            var expenses = from i in db.Expenses orderby i.Date descending select i;

            /*
            foreach (Income item in incomes)
            {
                Console.WriteLine(item.ID + " " + item.Description + " " + item.Amount + " " + item.Type + " " + item.Date + " " + item.Taxable + " " + item.TaxPercentage);
            }

            Console.WriteLine("");

            

            foreach (Expense item in expenses)
            {
                Console.WriteLine(item.ID + " " + item.Description + " " + item.Amount + " " + item.Type + " " + item.Date);
            }

            Console.WriteLine("");

            

            foreach (Reminder item in reminders)
            {
                Console.WriteLine(item.ID + " " + item.Description + " " + item.Amount + " " + item.Type + " " + item.Date);
            }

            Console.WriteLine("");*/

            this.HomeGrid.Visibility = Visibility.Visible;
            this.IncomeGrid.Visibility = Visibility.Hidden;
            this.ExpenesGrid.Visibility = Visibility.Hidden;
            this.ReminderGrid.Visibility = Visibility.Hidden;
            this.AllGrid.Visibility = Visibility.Hidden;

            this.IncomeList.ItemsSource = incomes.ToList();
            this.ExpenseList.ItemsSource = expenses.ToList();
            this.ReminderList.ItemsSource = reminders.ToList();
        }

        #region NAVBAR REGOIN
        void OnHomeBTNClick(object sender, RoutedEventArgs e)
        {
            this.HomeGrid.Visibility = Visibility.Visible;
            this.IncomeGrid.Visibility = Visibility.Hidden;
            this.ExpenesGrid.Visibility = Visibility.Hidden;
            this.ReminderGrid.Visibility = Visibility.Hidden;
            this.AllGrid.Visibility = Visibility.Hidden;
        }

        void OnIncomeBTNClick(object sender, RoutedEventArgs e)
        {
            this.IncomeHeaderTXT.Text = "Income Page";

            this.HomeGrid.Visibility = Visibility.Hidden;
            this.IncomeGrid.Visibility = Visibility.Visible;
            this.ExpenesGrid.Visibility = Visibility.Hidden;
            this.ReminderGrid.Visibility = Visibility.Hidden;
            this.AllGrid.Visibility = Visibility.Hidden;

            this.IncomeListPageGrid.Visibility = Visibility.Visible;
            this.AddIncomePageGrid.Visibility = Visibility.Hidden;
            this.EditIncomePageGrid.Visibility = Visibility.Hidden;
        }

        void OnExpensBTNClick(object sender, RoutedEventArgs e)
        {
            this.HomeGrid.Visibility = Visibility.Hidden;
            this.IncomeGrid.Visibility = Visibility.Hidden;
            this.ExpenesGrid.Visibility = Visibility.Visible;
            this.ReminderGrid.Visibility = Visibility.Hidden;
            this.AllGrid.Visibility = Visibility.Hidden;

            this.ExpenseListPageGrid.Visibility = Visibility.Visible;
            this.AddExpensePageGrid.Visibility = Visibility.Hidden;
            this.EditExpensePageGrid.Visibility = Visibility.Hidden;
        }

        void OnReminderBTNClick(object sender, RoutedEventArgs e)
        {
            this.HomeGrid.Visibility = Visibility.Hidden;
            this.IncomeGrid.Visibility = Visibility.Hidden;
            this.ExpenesGrid.Visibility = Visibility.Hidden;
            this.ReminderGrid.Visibility = Visibility.Visible;
            this.AllGrid.Visibility = Visibility.Hidden;

            this.ReminderListPageGrid.Visibility = Visibility.Visible;
            this.AddReminderPageGrid.Visibility = Visibility.Hidden;
            this.EditReminderPageGrid.Visibility = Visibility.Hidden;
        }

        void OnAllBTNClick(object sender, RoutedEventArgs e)
        {
            this.HomeGrid.Visibility = Visibility.Hidden;
            this.IncomeGrid.Visibility = Visibility.Hidden;
            this.ExpenesGrid.Visibility = Visibility.Hidden;
            this.ReminderGrid.Visibility = Visibility.Hidden;
            this.AllGrid.Visibility = Visibility.Visible;

            this.IncomeAllDataPageNoFilterGrid.Visibility = Visibility.Visible;
            this.ExpenseAllDataPageNoFilterGrid.Visibility = Visibility.Hidden;

            BudgetTracDBEntities db = new BudgetTracDBEntities();

            var incomes = from i in db.Incomes orderby i.Date descending select i;
            var expenses = from i in db.Expenses orderby i.Date descending select i;

            double totalIncome = 0.0f;
            double lastMonthIncome = 0.0f;
            double currentMonthIncome = 0.0f;

            foreach(Income i in incomes)
            {
                totalIncome += i.Amount.Value;

                if (i.Date.Value.Month == DateTime.Today.Month)
                    currentMonthIncome += i.Amount.Value;

                else if (i.Date.Value.Month == DateTime.Today.AddMonths(-1).Month)
                    lastMonthIncome += i.Amount.Value;
            }

            double totalExpensee = 0.0f;
            double lastMonthExpense = 0.0f;
            double currentMonthExpense = 0.0f;

            foreach (Expense i in expenses)
            {
                totalExpensee += i.Amount.Value;

                if (i.Date.Value.Month == DateTime.Today.Month)
                    currentMonthExpense += i.Amount.Value;

                else if (i.Date.Value.Month == DateTime.Today.AddMonths(-1).Month)
                    lastMonthExpense += i.Amount.Value;
            }

            this.currentMonthIncomeAllPageTXT.Text = currentMonthIncome.ToString();
            this.lastMonthIncomeALLPageTXT.Text = lastMonthIncome.ToString();
            this.allIncomeAllPageTXT.Text = totalIncome.ToString();
        }

        #endregion


        #region INCOME PAGE

        void OnAddIncomeBTNClick(object sender, RoutedEventArgs e)
        {
            this.IncomeHeaderTXT.Text = "Add Income Page";

            this.IncomeRadioYesBtn.IsChecked = true;
            this.IncomeTypeCMB.SelectedIndex = 0;

            this.IncomeDateCalander.DisplayDateStart = DateTime.MinValue;
            this.IncomeDateCalander.DisplayDateEnd = DateTime.Today;

            this.IncomeListPageGrid.Visibility = Visibility.Hidden;
            this.AddIncomePageGrid.Visibility = Visibility.Visible;
            this.EditIncomePageGrid.Visibility = Visibility.Hidden;
        }

        void OnEditIncomeBTNClick(object sender, RoutedEventArgs e)
        {
            this.IncomeHeaderTXT.Text = "Edit Income Page";

            this.IncomeEditDateCalander.DisplayDateStart = DateTime.MinValue;
            this.IncomeEditDateCalander.DisplayDateEnd = DateTime.Today;

            this.IncomeListPageGrid.Visibility = Visibility.Hidden;
            this.AddIncomePageGrid.Visibility = Visibility.Hidden;
            this.EditIncomePageGrid.Visibility = Visibility.Visible;
        }

        void OnDeleteIncomeBTNClick(object sender, RoutedEventArgs e)
        {
            BudgetTracDBEntities db = new BudgetTracDBEntities();

            var oldIncome = from i in db.Incomes where i.ID == this.IncomeUpdateIndex select i;

            Income newIncome = oldIncome.FirstOrDefault();

            //Console.WriteLine(this.IncomeDescriptionTXT.Text.ToString() + " " + Convert.ToDouble(this.IncomeAmountTXT.Text) + " " + this.IncomeTypeCMBValue + " " + Convert.ToDouble(this.IncomeTaxAmountTXT.Text) + " " + this.IncomeDate.ToString());

            if (newIncome != null)
            {
                db.Incomes.Remove(newIncome);
                db.SaveChanges();
            }

            var incomes = from i in db.Incomes orderby i.Date descending select i;

            this.IncomeList.ItemsSource = incomes.ToList();
        }

        void OnAddIncomeConfirmBTNClick(object sender, RoutedEventArgs e)
        {
            if (this.IncomeDescriptionTXT.Text == "" || this.IncomeAmountTXT.Text == "")
                return;

            BudgetTracDBEntities db = new BudgetTracDBEntities();

            //Console.WriteLine(this.IncomeDescriptionTXT.Text.ToString() + " " + Convert.ToDouble(this.IncomeAmountTXT.Text) + " " + this.IncomeTypeCMBValue + " " + Convert.ToDouble(this.IncomeTaxAmountTXT.Text) + " " + this.IncomeDate.ToString());

            Income newIncome = new Income()
            {
                Description = this.IncomeDescriptionTXT.Text.ToString(),
                Amount = Convert.ToDouble(this.IncomeAmountTXT.Text),
                Type = this.IncomeTypeCMBValue,
                Taxable = this.IncomeRadioBTNVale,
                TaxPercentage = Convert.ToDouble(this.IncomeTaxAmountTXT.Text),
                Date = this.IncomeDate
            };

            db.Incomes.Add(newIncome);
            db.SaveChanges();
            
            this.IncomeHeaderTXT.Text = "Income Page";

            this.IncomeListPageGrid.Visibility = Visibility.Visible;
            this.AddIncomePageGrid.Visibility = Visibility.Hidden;
            this.EditIncomePageGrid.Visibility = Visibility.Hidden;

            this.IncomeDescriptionTXT.Clear();
            this.IncomeAmountTXT.Clear();
            this.IncomeRadioYesBtn.IsChecked = true;
            this.IncomeTypeCMB.SelectedIndex = 0;
            this.IncomeAmountTXT.Clear();

            var incomes = from i in db.Incomes orderby i.Date descending select i;

            this.IncomeList.ItemsSource = incomes.ToList();
        }

        void OnEditIncomeConfirmBTNClick(object sender, RoutedEventArgs e)
        {
            if (this.IncomeEditDescriptionTXT.Text == "" || this.IncomeEditAmountTXT.Text == "")
                return;

            BudgetTracDBEntities db = new BudgetTracDBEntities();

            var oldIncome = from i in db.Incomes where i.ID == this.IncomeUpdateIndex select i;

            Income newIncome = oldIncome.FirstOrDefault();

            //Console.WriteLine(this.IncomeDescriptionTXT.Text.ToString() + " " + Convert.ToDouble(this.IncomeAmountTXT.Text) + " " + this.IncomeTypeCMBValue + " " + Convert.ToDouble(this.IncomeTaxAmountTXT.Text) + " " + this.IncomeDate.ToString());

            if(newIncome != null)
            {
                newIncome.Description = this.IncomeEditDescriptionTXT.Text.ToString();
                newIncome.Amount = Convert.ToDouble(this.IncomeEditAmountTXT.Text);
                newIncome.Type = this.IncomeTypeCMBValue;
                newIncome.Taxable = this.IncomeRadioBTNVale;
                newIncome.TaxPercentage = Convert.ToDouble(this.IncomeEditTaxAmountTXT.Text);
                newIncome.Date = this.IncomeDate;

                db.SaveChanges();
            }

            //db.SaveChanges();

            this.IncomeHeaderTXT.Text = "Income Page";

            this.IncomeListPageGrid.Visibility = Visibility.Visible;
            this.AddIncomePageGrid.Visibility = Visibility.Hidden;
            this.EditIncomePageGrid.Visibility = Visibility.Hidden;

            this.IncomeEditDescriptionTXT.Clear();
            this.IncomeEditAmountTXT.Clear();
            this.IncomeEditRadioYesBtn.IsChecked = true;
            this.IncomeEditTypeCMB.SelectedIndex = 0;
            this.IncomeEditTaxAmountTXT.Clear();

            var incomes = from i in db.Incomes orderby i.Date descending select i;

            this.IncomeList.ItemsSource = incomes.ToList();
        }

        void OnCancelIncomeBTNClick(object sender, RoutedEventArgs e)
        {
            BudgetTracDBEntities db = new BudgetTracDBEntities();

            this.IncomeHeaderTXT.Text = "Income Page";

            this.IncomeListPageGrid.Visibility = Visibility.Visible;
            this.AddIncomePageGrid.Visibility = Visibility.Hidden;
            this.EditIncomePageGrid.Visibility = Visibility.Hidden;

            this.IncomeDescriptionTXT.Clear();
            this.IncomeAmountTXT.Clear();
            this.IncomeRadioYesBtn.IsChecked = true;
            this.IncomeTypeCMB.SelectedIndex = 0;
            this.IncomeAmountTXT.Clear();

            var incomes = from i in db.Incomes orderby i.Date descending select i;

            this.IncomeList.ItemsSource = incomes.ToList();
        }

        void IncomeOnRadioButtonChange(object sender, RoutedEventArgs e)
        {
            if (this.IncomeRadioYesBtn.IsChecked == true)
            {
                this.IncomeRadioBTNVale = "Yes";

                this.IncomeTaxAmountTXT.IsEnabled = true;
            }
            else if (this.IncomeRadionoBtn.IsChecked == true)
            {
                this.IncomeRadioBTNVale = "No";

                this.IncomeTaxAmountTXT.IsEnabled = false;
                this.IncomeTaxAmountTXT.Text = "0.0";
            }
        }

        void IncomeEditOnRadioButtonChange(object sender, RoutedEventArgs e)
        {
            if (this.IncomeEditRadioYesBtn.IsChecked == true)
            {
                this.IncomeRadioBTNVale = "Yes";

                this.IncomeEditTaxAmountTXT.IsEnabled = true;
            }
            else if (this.IncomeEditRadionoBtn.IsChecked == true)
            {
                this.IncomeRadioBTNVale = "No";

                this.IncomeEditTaxAmountTXT.IsEnabled = false;
                this.IncomeEditTaxAmountTXT.Text = "0.0";
            }
        }

        void OnIncomeTypeSelectionChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.IncomeTypeCMBValue = (string)((ComboBoxItem)IncomeTypeCMB.SelectedItem).Content;
        }

        void OnIncomeEditTypeSelectionChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.IncomeTypeCMBValue = (string)((ComboBoxItem)IncomeEditTypeCMB.SelectedItem).Content;
        }

        void OnIncomeDateChange(object sender, SelectionChangedEventArgs e)
        {
            this.IncomeDate = this.IncomeDateCalander.SelectedDate.Value;
        }

        void OnIncomeEditDateChange(object sender, SelectionChangedEventArgs e)
        {
            this.IncomeDate = this.IncomeEditDateCalander.SelectedDate.Value;
        }

        private void IncomeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IncomeList.SelectedIndex < 0)
                return;

            if(this.IncomeList.SelectedItem.GetType() == typeof(Income))
            {
                
                Income i = (Income)IncomeList.SelectedItem;

                this.IncomeEditDescriptionTXT.Text = i.Description;
                this.IncomeEditAmountTXT.Text = i.Amount.ToString();

                if (i.Type == "Income")
                    this.IncomeEditTypeCMB.SelectedIndex = 0;

                else if(i.Type == "Saving")
                    this.IncomeEditTypeCMB.SelectedIndex = 1;

                else if (i.Type == "Gift")
                    this.IncomeEditTypeCMB.SelectedIndex = 2;

                if (i.Taxable == "Yes")
                    this.IncomeEditRadioYesBtn.IsChecked = true;
                else if(i.Taxable == "No")
                    this.IncomeEditRadionoBtn.IsChecked = true;

                this.IncomeEditTaxAmountTXT.Text = i.TaxPercentage.ToString();

                this.IncomeEditDateCalander.SelectedDate = i.Date;

                this.IncomeUpdateIndex = i.ID;
            }
        }

        #endregion

        #region EXPENSE PAGE

        void OnAddExpenseBTNClick(object sender, RoutedEventArgs e)
        {
            this.ExpenseHeaderTXT.Text = "Add Expense Page";

            this.ExpenseTypeCMB.SelectedIndex = 0;

            this.ExpenseDateCalander.DisplayDateStart = DateTime.MinValue;
            this.ExpenseDateCalander.DisplayDateEnd = DateTime.Today;

            this.ExpenseListPageGrid.Visibility = Visibility.Hidden;
            this.AddExpensePageGrid.Visibility = Visibility.Visible;
            this.EditExpensePageGrid.Visibility = Visibility.Hidden;
        }

        void OnEditExpenseBTNClick(object sender, RoutedEventArgs e)
        {
            this.ExpenseHeaderTXT.Text = "Edit Expense Page";

            this.ExpenseEditDateCalander.DisplayDateStart = DateTime.MinValue;
            this.ExpenseEditDateCalander.DisplayDateEnd = DateTime.Today;

            this.ExpenseListPageGrid.Visibility = Visibility.Hidden;
            this.AddExpensePageGrid.Visibility = Visibility.Hidden;
            this.EditExpensePageGrid.Visibility = Visibility.Visible;
        }

        void OnDeleteExpenseBTNClick(object sender, RoutedEventArgs e)
        {
            BudgetTracDBEntities db = new BudgetTracDBEntities();

            var oldExpense = from i in db.Expenses where i.ID == this.ExpenseUpdateIndex select i;

            Expense newExpense = oldExpense.FirstOrDefault();

            //Console.WriteLine(this.IncomeDescriptionTXT.Text.ToString() + " " + Convert.ToDouble(this.IncomeAmountTXT.Text) + " " + this.IncomeTypeCMBValue + " " + Convert.ToDouble(this.IncomeTaxAmountTXT.Text) + " " + this.IncomeDate.ToString());

            if (newExpense != null)
            {
                db.Expenses.Remove(newExpense);
                db.SaveChanges();
            }

            var expenses = from i in db.Expenses orderby i.Date descending select i;
            this.ExpenseList.ItemsSource = expenses.ToList();
        }

        void OnAddExpenseConfirmBTNClick(object sender, RoutedEventArgs e)
        {
            if (this.ExpenseDescriptionTXT.Text == "" || this.ExpenseAmountTXT.Text == "")
                return;

            BudgetTracDBEntities db = new BudgetTracDBEntities();

            //Console.WriteLine(this.IncomeDescriptionTXT.Text.ToString() + " " + Convert.ToDouble(this.IncomeAmountTXT.Text) + " " + this.IncomeTypeCMBValue + " " + Convert.ToDouble(this.IncomeTaxAmountTXT.Text) + " " + this.IncomeDate.ToString());

            Expense newExpense = new Expense()
            {
                Description = this.ExpenseDescriptionTXT.Text.ToString(),
                Amount = Convert.ToDouble(this.ExpenseAmountTXT.Text),
                Type = this.ExpenseTypeCMBValue,
                Date = this.ExpenseDate
            };

            db.Expenses.Add(newExpense);
            db.SaveChanges();

            this.ExpenseHeaderTXT.Text = "Expense Page";

            this.ExpenseListPageGrid.Visibility = Visibility.Visible;
            this.AddExpensePageGrid.Visibility = Visibility.Hidden;
            this.EditExpensePageGrid.Visibility = Visibility.Hidden;

            this.ExpenseDescriptionTXT.Clear();
            this.ExpenseAmountTXT.Clear();
            this.ExpenseTypeCMB.SelectedIndex = 0;

            var expenses = from i in db.Expenses orderby i.Date descending select i;
            this.ExpenseList.ItemsSource = expenses.ToList();
        }

        void OnEditExpenseConfirmBTNClick(object sender, RoutedEventArgs e)
        {
            if (this.ExpenseEditDescriptionTXT.Text == "" || this.ExpenseEditAmountTXT.Text == "")
                return;

            BudgetTracDBEntities db = new BudgetTracDBEntities();

            var oldExpense = from i in db.Expenses where i.ID == this.ExpenseUpdateIndex select i;

            Expense newExpense = oldExpense.FirstOrDefault();

            //Console.WriteLine(this.IncomeDescriptionTXT.Text.ToString() + " " + Convert.ToDouble(this.IncomeAmountTXT.Text) + " " + this.IncomeTypeCMBValue + " " + Convert.ToDouble(this.IncomeTaxAmountTXT.Text) + " " + this.IncomeDate.ToString());

            if (newExpense != null)
            {
                newExpense.Description = this.ExpenseEditDescriptionTXT.Text.ToString();
                newExpense.Amount = Convert.ToDouble(this.ExpenseEditAmountTXT.Text);
                newExpense.Type = this.ExpenseTypeCMBValue;
                newExpense.Date = this.ExpenseDate;

                db.SaveChanges();
            }

            //db.SaveChanges();

            this.ExpenseHeaderTXT.Text = "Expense Page";

            this.ExpenseListPageGrid.Visibility = Visibility.Visible;
            this.AddExpensePageGrid.Visibility = Visibility.Hidden;
            this.EditExpensePageGrid.Visibility = Visibility.Hidden;

            this.ExpenseDescriptionTXT.Clear();
            this.ExpenseAmountTXT.Clear();
            this.ExpenseTypeCMB.SelectedIndex = 0;

            var expenses = from i in db.Expenses orderby i.Date descending select i;
            this.ExpenseList.ItemsSource = expenses.ToList();
        }

        void OnCancelExpenseBTNClick(object sender, RoutedEventArgs e)
        {
            BudgetTracDBEntities db = new BudgetTracDBEntities();

            this.ExpenseHeaderTXT.Text = "Expense Page";

            this.ExpenseListPageGrid.Visibility = Visibility.Visible;
            this.AddExpensePageGrid.Visibility = Visibility.Hidden;
            this.EditExpensePageGrid.Visibility = Visibility.Hidden;

            this.ExpenseDescriptionTXT.Clear();
            this.ExpenseAmountTXT.Clear();
            this.ExpenseTypeCMB.SelectedIndex = 0;

            var expenses = from i in db.Expenses orderby i.Date descending select i;
            this.ExpenseList.ItemsSource = expenses.ToList();
        }

        void OnExpenseEditTypeSelectionChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.ExpenseTypeCMBValue = (string)((ComboBoxItem)ExpenseEditTypeCMB.SelectedItem).Content;
        }

        void OnExpenseTypeSelectionChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.ExpenseTypeCMBValue = (string)((ComboBoxItem)ExpenseTypeCMB.SelectedItem).Content;
        }

        void OnExpenseDateChange(object sender, SelectionChangedEventArgs e)
        {
            this.ExpenseDate = this.ExpenseDateCalander.SelectedDate.Value;
        }

        void OnExpenseEditDateChange(object sender, SelectionChangedEventArgs e)
        {
            this.ExpenseDate = this.ExpenseEditDateCalander.SelectedDate.Value;
        }

        private void ExpenseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ExpenseList.SelectedIndex < 0)
                return;

            if (this.ExpenseList.SelectedItem.GetType() == typeof(Expense))
            {

                Expense i = (Expense)ExpenseList.SelectedItem;

                this.ExpenseEditDescriptionTXT.Text = i.Description;
                this.ExpenseEditAmountTXT.Text = i.Amount.ToString();

                if (i.Type == "Utility")
                    this.ExpenseEditTypeCMB.SelectedIndex = 0;

                else if (i.Type == "Grocery")
                    this.ExpenseEditTypeCMB.SelectedIndex = 1;

                else if (i.Type == "Food")
                    this.ExpenseEditTypeCMB.SelectedIndex = 2;

                else if (i.Type == "Entertainment")
                    this.ExpenseEditTypeCMB.SelectedIndex = 3;

                else if (i.Type == "Study")
                    this.ExpenseEditTypeCMB.SelectedIndex = 4;

                else if (i.Type == "Transportation")
                    this.ExpenseEditTypeCMB.SelectedIndex = 5;

                else if (i.Type == "Other")
                    this.ExpenseEditTypeCMB.SelectedIndex = 6;

                this.ExpenseEditDateCalander.SelectedDate = i.Date;

                this.ExpenseUpdateIndex = i.ID;
            }
        }

        #endregion

        #region REMINDER PAGE

        void OnAddReminderBTNClick(object sender, RoutedEventArgs e)
        {
            this.ReminderHeaderTXT.Text = "Add Reminder Page";

            this.ReminderTypeCMB.SelectedIndex = 0;

            this.ReminderDateCalander.DisplayDateStart = DateTime.Today.AddDays(+1);
            this.ReminderDateCalander.DisplayDateEnd = DateTime.MaxValue;

            this.ReminderListPageGrid.Visibility = Visibility.Hidden;
            this.AddReminderPageGrid.Visibility = Visibility.Visible;
            this.EditReminderPageGrid.Visibility = Visibility.Hidden;
        }

        void OnEditReminderBTNClick(object sender, RoutedEventArgs e)
        {
            this.ReminderHeaderTXT.Text = "Edit Reminder Page";

            this.ReminderEditDateCalander.DisplayDateStart = DateTime.Today.AddDays(+1);
            this.ReminderEditDateCalander.DisplayDateEnd = DateTime.MaxValue;

            this.ReminderListPageGrid.Visibility = Visibility.Hidden;
            this.AddReminderPageGrid.Visibility = Visibility.Hidden;
            this.EditReminderPageGrid.Visibility = Visibility.Visible;
        }

        void OnDeleteReminderBTNClick(object sender, RoutedEventArgs e)
        {
            BudgetTracDBEntities db = new BudgetTracDBEntities();

            var oldReminder = from i in db.Reminders where i.ID == this.ReminderUpdateIndex select i;

            Reminder newReminder = oldReminder.FirstOrDefault();

            //Console.WriteLine(this.IncomeDescriptionTXT.Text.ToString() + " " + Convert.ToDouble(this.IncomeAmountTXT.Text) + " " + this.IncomeTypeCMBValue + " " + Convert.ToDouble(this.IncomeTaxAmountTXT.Text) + " " + this.IncomeDate.ToString());

            if (newReminder != null)
            {
                db.Reminders.Remove(newReminder);
                db.SaveChanges();
            }

            var reminders = from i in db.Reminders orderby i.Date ascending select i;
            this.ReminderList.ItemsSource = reminders.ToList();
        }

        void OnAddReminderConfirmBTNClick(object sender, RoutedEventArgs e)
        {
            if (this.ReminderDescriptionTXT.Text == "" || this.ReminderAmountTXT.Text == "")
                return;

            BudgetTracDBEntities db = new BudgetTracDBEntities();

            //Console.WriteLine(this.IncomeDescriptionTXT.Text.ToString() + " " + Convert.ToDouble(this.IncomeAmountTXT.Text) + " " + this.IncomeTypeCMBValue + " " + Convert.ToDouble(this.IncomeTaxAmountTXT.Text) + " " + this.IncomeDate.ToString());

            Reminder newReminder = new Reminder()
            {
                Description = this.ReminderDescriptionTXT.Text.ToString(),
                Amount = Convert.ToDouble(this.ReminderAmountTXT.Text),
                Type = this.ReminderTypeCMBValue,
                Date = this.ReminderDate
            };

            db.Reminders.Add(newReminder);
            db.SaveChanges();

            this.ReminderHeaderTXT.Text = "Reminder Page";

            this.ReminderListPageGrid.Visibility = Visibility.Visible;
            this.AddReminderPageGrid.Visibility = Visibility.Hidden;
            this.EditReminderPageGrid.Visibility = Visibility.Hidden;

            this.ReminderDescriptionTXT.Clear();
            this.ReminderAmountTXT.Clear();
            this.ReminderTypeCMB.SelectedIndex = 0;

            var reminders = from i in db.Reminders orderby i.Date ascending select i;
            this.ReminderList.ItemsSource = reminders.ToList();
        }

        void OnEditReminderConfirmBTNClick(object sender, RoutedEventArgs e)
        {
            if (this.ReminderEditDescriptionTXT.Text == "" || this.ReminderEditAmountTXT.Text == "")
                return;

            BudgetTracDBEntities db = new BudgetTracDBEntities();

            var oldReminder = from i in db.Reminders where i.ID == this.ReminderUpdateIndex select i;

            Reminder newReminder = oldReminder.FirstOrDefault();

            //Console.WriteLine(this.IncomeDescriptionTXT.Text.ToString() + " " + Convert.ToDouble(this.IncomeAmountTXT.Text) + " " + this.IncomeTypeCMBValue + " " + Convert.ToDouble(this.IncomeTaxAmountTXT.Text) + " " + this.IncomeDate.ToString());

            if (newReminder != null)
            {
                newReminder.Description = this.ReminderEditDescriptionTXT.Text.ToString();
                newReminder.Amount = Convert.ToDouble(this.ReminderEditAmountTXT.Text);
                newReminder.Type = this.ReminderTypeCMBValue;
                newReminder.Date = this.ReminderDate;

                db.SaveChanges();
            }

            //db.SaveChanges();

            this.ReminderHeaderTXT.Text = "Reminder Page";

            this.ReminderListPageGrid.Visibility = Visibility.Visible;
            this.AddReminderPageGrid.Visibility = Visibility.Hidden;
            this.EditReminderPageGrid.Visibility = Visibility.Hidden;

            this.ReminderEditDescriptionTXT.Clear();
            this.ReminderEditAmountTXT.Clear();
            this.ReminderEditTypeCMB.SelectedIndex = 0;

            var reminders = from i in db.Reminders orderby i.Date ascending select i;
            this.ReminderList.ItemsSource = reminders.ToList();
        }

        void OnCancelReminderBTNClick(object sender, RoutedEventArgs e)
        {
            BudgetTracDBEntities db = new BudgetTracDBEntities();

            this.ReminderHeaderTXT.Text = "Reminder Page";

            this.ReminderListPageGrid.Visibility = Visibility.Visible;
            this.AddReminderPageGrid.Visibility = Visibility.Hidden;
            this.EditReminderPageGrid.Visibility = Visibility.Hidden;

            this.ReminderDescriptionTXT.Clear();
            this.ReminderAmountTXT.Clear();
            this.ReminderTypeCMB.SelectedIndex = 0;

            var reminders = from i in db.Reminders orderby i.Date ascending select i;
            this.ReminderList.ItemsSource = reminders.ToList();
        }

        void OnReminderEditTypeSelectionChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.ReminderTypeCMBValue = (string)((ComboBoxItem)ReminderEditTypeCMB.SelectedItem).Content;
        }

        void OnReminderTypeSelectionChange(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.ReminderTypeCMBValue = (string)((ComboBoxItem)ReminderTypeCMB.SelectedItem).Content;
        }

        void OnReminderDateChange(object sender, SelectionChangedEventArgs e)
        {
            this.ReminderDate = this.ReminderDateCalander.SelectedDate.Value;
        }

        void OnReminderEditDateChange(object sender, SelectionChangedEventArgs e)
        {
            this.ReminderDate = this.ReminderEditDateCalander.SelectedDate.Value;
        }

        private void ReminderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ReminderList.SelectedIndex < 0)
                return;

            if (this.ReminderList.SelectedItem.GetType() == typeof(Reminder))
            {

                Reminder i = (Reminder)ReminderList.SelectedItem;

                this.ReminderEditDescriptionTXT.Text = i.Description;
                this.ReminderEditAmountTXT.Text = i.Amount.ToString();

                if (i.Type == "Utility")
                    this.ReminderEditTypeCMB.SelectedIndex = 0;

                else if (i.Type == "Grocery")
                    this.ReminderEditTypeCMB.SelectedIndex = 1;

                else if (i.Type == "Food")
                    this.ReminderEditTypeCMB.SelectedIndex = 2;

                else if (i.Type == "Entertainment")
                    this.ReminderEditTypeCMB.SelectedIndex = 3;

                else if (i.Type == "Study")
                    this.ReminderEditTypeCMB.SelectedIndex = 4;

                else if (i.Type == "Transportation")
                    this.ReminderEditTypeCMB.SelectedIndex = 5;

                else if (i.Type == "Other")
                    this.ReminderEditTypeCMB.SelectedIndex = 6;

                this.ReminderEditDateCalander.SelectedDate = i.Date;

                this.ReminderUpdateIndex = i.ID;
            }
        }

        #endregion

        #region All DATA PAGE
        void OnIncomeAllDataBTNClick(object sender, RoutedEventArgs e)
        {
            this.IncomeAllDataPageNoFilterGrid.Visibility = Visibility.Visible;
            this.ExpenseAllDataPageNoFilterGrid.Visibility = Visibility.Hidden;

            BudgetTracDBEntities db = new BudgetTracDBEntities();

            var incomes = from i in db.Incomes orderby i.Date descending select i;

            double totalIncome = 0.0f;
            double lastMonthIncome = 0.0f;
            double currentMonthIncome = 0.0f;

            foreach (Income i in incomes)
            {
                totalIncome += i.Amount.Value;

                if (i.Date.Value.Month == DateTime.Today.Month)
                    currentMonthIncome += i.Amount.Value;

                else if (i.Date.Value.Month == DateTime.Today.AddMonths(-1).Month)
                    lastMonthIncome += i.Amount.Value;
            }

            this.currentMonthIncomeAllPageTXT.Text = currentMonthIncome.ToString();
            this.lastMonthIncomeALLPageTXT.Text = lastMonthIncome.ToString();
            this.allIncomeAllPageTXT.Text = totalIncome.ToString();
        }

        void OnExpenseAllDataBTNClick(object sender, RoutedEventArgs e)
        {
            this.IncomeAllDataPageNoFilterGrid.Visibility = Visibility.Hidden;
            this.ExpenseAllDataPageNoFilterGrid.Visibility = Visibility.Visible;

            BudgetTracDBEntities db = new BudgetTracDBEntities();

            var expenses = from i in db.Expenses orderby i.Date descending select i;

            double totalExpense = 0.0f;
            double lastMonthExpense = 0.0f;
            double currentMonthExpense = 0.0f;

            foreach (Expense i in expenses)
            {
                totalExpense += i.Amount.Value;

                if (i.Date.Value.Month == DateTime.Today.Month)
                    currentMonthExpense += i.Amount.Value;

                else if (i.Date.Value.Month == DateTime.Today.AddMonths(-1).Month)
                    lastMonthExpense += i.Amount.Value;
            }

            this.currentMonthExpenseAllPageTXT.Text = currentMonthExpense.ToString();
            this.lastMonthExpenseALLPageTXT.Text = lastMonthExpense.ToString();
            this.allExpenseAllPageTXT.Text = totalExpense.ToString();
        }

        void OnFilterExpenseAllDataBTNClick(object sender, RoutedEventArgs e)
        {

        }

        void OnFilterIncomeAllDataBTNClick(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}
