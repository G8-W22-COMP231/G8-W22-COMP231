using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetTracApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracApp.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        //unit test 1
        [TestMethod()]
        public void GetIncomeDataFromDB()
        {
            // arrange
            BudgetTracDBEntities db = new BudgetTracDBEntities();
            List<Income> allIncome = db.Incomes.ToList();
            int expectedCount = 2;

            // act
            int testCount = allIncome.Count;

            // assert
            Assert.AreEqual(expectedCount, testCount);
        }

        //unit test 2
        [TestMethod()]
        public void GetExpenseDataFromDB()
        {
            BudgetTracDBEntities db = new BudgetTracDBEntities();
            List<Expense> allExpense = db.Expenses.ToList();
            int expectedCount = 4;

            // act
            int testCount = allExpense.Count;

            // assert
            Assert.AreEqual(expectedCount, testCount);
        }

        //unit test 3
        [TestMethod()]
        public void GetReminderDataFromDB()
        {
            BudgetTracDBEntities db = new BudgetTracDBEntities();
            List<Reminder> allReminder = db.Reminders.ToList();
            int expectedCount = 1;

            // act
            int testCount = allReminder.Count;

            // assert
            Assert.AreEqual(expectedCount, testCount);
        }

        //unit test 4
        [TestMethod()]
        public void GetCurrentMonthIncomeDataFromDB()
        {
            BudgetTracDBEntities db = new BudgetTracDBEntities();
            List<Income> allIncome = db.Incomes.ToList();
            int expectedCount = 1;

            // act
            var currentMonthIncomes = from i in db.Incomes where i.Date.Value.Month == DateTime.Today.Month select i;
            int testCount = currentMonthIncomes.ToList().Count;

            // assert
            Assert.AreEqual(expectedCount, testCount);
        }

        //unit test 5
        [TestMethod()]
        public void GetCurrentMonthExpenseDataFromDB()
        {
            BudgetTracDBEntities db = new BudgetTracDBEntities();
            List<Expense> allExpense = db.Expenses.ToList();
            int expectedCount = 2;

            // act
            var currentMonthExpeneses = from i in db.Expenses where i.Date.Value.Month == DateTime.Today.Month select i;
            int testCount = currentMonthExpeneses.ToList().Count;

            // assert
            Assert.AreEqual(expectedCount, testCount);
        }
    }
}