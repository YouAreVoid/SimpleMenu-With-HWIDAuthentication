using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Clipboard = System.Windows.Clipboard;

namespace SimpleMenu
{
    class Program
    {
        #region Simple Authentication System
        //DO NOT RENAME ANY OF THE STRINGS FROM K OR A UNLESS ITS ANOTHER LETTER (For example you rename K to P, That is fine) - This is due to string being stored in memory which can be used by crackers, this will make it harder to find the strings :) or Encrypt strings in memory if you know how.
        //string k = Key (hash key) - Change the string value to a random string of letters,numbers and special characters.
        //string A = Key Decrypted And Checked (Do not edit this except where the list of hwid's are)
        static string SimpleAuth()
        {
            WebClient client = new WebClient();
            string K = "RandomString"; // Change RandomString to anything you want, or https://www.random.org/strings/?num=10&len=10&digits=on&upperalpha=on&loweralpha=on&unique=on&format=html&rnd=new - Put two strings from that together.
            string cpu = "";
            string motherboard = "";
            string hwid = "";
            //Getting HWID Details
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
            mbsList = mbs.Get();
            foreach (ManagementObject mo in mbsList)
            {
                cpu = mo["ProcessorID"].ToString();
            }
            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            ManagementObjectCollection moc = mos.Get();
            foreach (ManagementObject mo in moc)
            {
                motherboard = (string)mo["SerialNumber"];
            }
            hwid = cpu + motherboard;
            byte[] data = UTF8Encoding.UTF8.GetBytes(hwid);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(K));
                //Encrypt data by hash key
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    hwid = Convert.ToBase64String(results, 0, results.Length);
                    string A = client.DownloadString("https://pastebin.com"); // List of HWID's that are whitelisted, Change this to your own list.
                    if (A.Contains(hwid))
                    {
                        MessageBox.Show("Authenticated!", "You'reVoid Authentication Servers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.Clear(); // Clears The screen.
                        Thread.Sleep(0); // This is put instead of Program.Main(); Because its being called from Program.Main(); (Line 92)
                    }
                    else
                    {
                        Console.Clear(); // Clears The screen.
                        Logo(); //Calls the Logo
                        Clipboard.SetText(hwid); // sets users hwid to clipboard.
                        Colorful.Console.WriteLine("Error Authenticating You, Please Contact An Administrator." + Environment.NewLine + "Your HWID was copied to your clipboard, Give Your HWID to the Admin" + Environment.NewLine + "Press Any Key to Exit :)", Color.Cyan); ; // Error message if HWID is not WhiteListed
                        Console.ReadLine();
                        Process.GetCurrentProcess().Kill(); // Best Kill method I know of.
                    }
                }
            }
            return hwid; // Returns The String HWID (do not remove this)
        }
        #endregion

        #region Logo
        static string Logo()
        {
            //logo- start
            string Return = "";
            Colorful.Console.WriteLine("██╗      ██████╗  ██████╗  ██████╗ ", Color.Cyan);
            Colorful.Console.WriteLine("██║     ██╔═══██╗██╔════╝ ██╔═══██╗", Color.Cyan);
            Colorful.Console.WriteLine("██║     ██║   ██║██║  ███╗██║   ██║", Color.Cyan);
            Colorful.Console.WriteLine("██║     ██║   ██║██║   ██║██║   ██║", Color.Cyan);
            Colorful.Console.WriteLine("███████╗╚██████╔╝╚██████╔╝╚██████╔╝", Color.Cyan);
            Colorful.Console.WriteLine("╚══════╝ ╚═════╝  ╚═════╝  ╚═════╝ ", Color.Cyan);
            Console.WriteLine("");
            return Return;
            //logo-end
        }
        //http://patorjk.com/software/taag/#p=display&f=ANSI%20Shadow&t=Logo to change the text of The Logo.
        #endregion

        #region Main
        [STAThread] // do not remove this.
        static void Main()
        {
            SimpleAuth(); // This calls the auth system before anything else starts
            Logo();
            Console.Title = "Simple Menu + Authentication, By You'reVoid";
            //options-start
            Colorful.Console.WriteLine("Welcome User, to Simple Menu + Authentication By You'reVoid!" + Environment.NewLine, Color.Cyan);
            Colorful.Console.WriteLine("[1] Option 1", Color.Cyan);
            Colorful.Console.WriteLine("[2] Option 2", Color.Cyan);
            Colorful.Console.WriteLine("[3] Option 3", Color.Cyan);
            Colorful.Console.WriteLine("[4] Option 4", Color.Cyan);
            Colorful.Console.WriteLine("[5] Option 5" + Environment.NewLine, Color.Cyan);
            Colorful.Console.Write("---> ", Color.Cyan);
            string Option = Console.ReadLine();
            //options-end
            if (Option == "1")
            {
                Console.Clear();
                SubMenu1();
            }
            else if (Option == "2")
            {
                Console.Clear();
                SubMenu2();
            }
            else if (Option == "3")
            {
                Console.Clear();
                SubMenu3();
            }
            else if (Option == "4")
            {
                Console.Clear();
                SubMenu4();
            }
            else if (Option == "5")
            {
                Console.Clear();
                SubMenu5();
            }
            else
            {
                //This is if they put a number which doesn't correspond to any of the options.
                Colorful.Console.WriteLine(Option + " was not an valid option." + Environment.NewLine + "Press Anything to Return to Main Menu.", Color.Cyan);
                Console.ReadLine();
                Console.Clear();
                Program.Main();
            }
        }
        #endregion

        #region SubMenus
        static void SubMenu1()
        {
            Console.Title = "Simple Menu Made By You'reVoid - SubMenu1";
            Logo();
            Colorful.Console.WriteLine("SubMenu1 Opened, Option 1 was chosen." + Environment.NewLine + "Press Anything to Return to Main Menu", Color.Cyan);
            Console.ReadLine();
            Console.Clear();
            Program.Main();
        }

        static void SubMenu2()
        {
            Console.Title = "Simple Menu Made By You'reVoid - SubMenu2";
            Logo();
            Colorful.Console.WriteLine("SubMenu2 Opened, Option 2 was chosen." + Environment.NewLine + "Press Anything to Return to Main Menu", Color.Cyan);
            Console.ReadLine();
            Console.Clear();
            Program.Main();
        }

        static void SubMenu3()
        {
            Console.Title = "Simple Menu Made By You'reVoid - SubMenu3";
            Logo();
            Colorful.Console.WriteLine("SubMenu3 Opened, Option 3 was chosen." + Environment.NewLine + "Press Anything to Return to Main Menu", Color.Cyan);
            Console.ReadLine();
            Console.Clear();
            Program.Main();
        }

        static void SubMenu4()
        {
            Console.Title = "Simple Menu Made By You'reVoid - SubMenu4";
            Logo();
            Colorful.Console.WriteLine("SubMenu4 Opened, Option 4 was chosen." + Environment.NewLine + "Press Anything to Return to Main Menu", Color.Cyan);
            Console.ReadLine();
            Console.Clear();
            Program.Main();
        }

        static void SubMenu5()
        {
            Console.Title = "Simple Menu Made By You'reVoid - SubMenu5";
            Logo();
            Colorful.Console.WriteLine("SubMenu5 Opened, Option 5 was chosen." + Environment.NewLine + "Press Anything to Return to Main Menu", Color.Cyan);
            Console.ReadLine();
            Console.Clear();
            Program.Main();
        }
        #endregion
    }
}
