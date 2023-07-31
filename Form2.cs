using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp4
{
    public partial class Form2 : Form
    {
        private IWebDriver driver;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            driver = new FirefoxDriver();

            // Navigate to the initial URL
            /*driver.Navigate().GoToUrl("https://www.example.com");*/

            // Start tracking URL changes in a separate thread
            var urlTrackerThread = new System.Threading.Thread(TrackURLChanges);
            urlTrackerThread.Start();
        }
        private void TrackURLChanges()
        {
            // Get the initial URL
            string currentUrl = driver.Url;

            while (true)
            {
                // Check if the URL has changed
                if (driver.Url != currentUrl)
                {
                    // Update the current URL
                    currentUrl = driver.Url;

                    // Do something with the new URL
                    /*  Console.WriteLine("URL changed: " + currentUrl);*/
                    
                  listBox1.Items.Add(currentUrl);
                }

                // Sleep for a short period to avoid excessive CPU usage
                System.Threading.Thread.Sleep(100);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Clean up resources
            driver.Quit();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
