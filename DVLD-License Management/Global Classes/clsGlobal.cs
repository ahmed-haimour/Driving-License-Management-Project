using DVLD_Buisness;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_License_Management.Global_Classes
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPasswordUsingRegistry(string Username, string Password)
            {
                bool isSave = false;

                string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_UserName&Password";  

                string UsernameValue = "Username";
                string UsernameData = Username;

                string PasswordValue = "Password";
                string PasswordData = Password;

                try
                {
                    Registry.SetValue(keyPath, UsernameValue, UsernameData, RegistryValueKind.String);
                    Registry.SetValue(keyPath, PasswordValue, PasswordData, RegistryValueKind.String);
                    isSave = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                return isSave;
            }

        public static bool GetStoredCredentialUsingRegistry(ref string Username, ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_UserName&Password";

            string UsernameValue = "Username";
            string PasswordValue = "Password";

            try
            {
                // Read the value from the Registry
                Username = Registry.GetValue(keyPath, UsernameValue, null) as string;
                Password = Registry.GetValue(keyPath, PasswordValue, null) as string;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Error occurred: {ex.Message}");
                return false;
            }
        }

        public static bool SaveToEventLog(string Message, EventLogEntryType EventLogType)
        {
            string sourceName = "DVLD";
            bool isSave = false;

            try
            {
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                EventLog.WriteEntry(sourceName, Message, EventLogType);
                isSave = true;
            }
            catch (Exception ex)
            {
                isSave = false;
            }
            
            return isSave;
        }

        static string ComputeHash(string input)
        {
            //SHA is Secutred Hash Algorithm.
            // Create an instance of the SHA-256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));


                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        //public static bool RememberUsernameAndPassword(string Username, string Password)
        //{
        //    try
        //    {

        //        //this will get the current project directory folder.
        //        string currentDirectory = System.IO.Directory.GetCurrentDirectory();

        //        // Define the path to the text file where you want to save the data
        //        string filePath = currentDirectory + "\\data.txt";

        //        if (Username == "" && File.Exists(filePath))
        //        {
        //            File.Delete(filePath);
        //            return true;
        //        }

        //        // concatonate username and passwrod withe seperator.
        //        string dataToSave = Username + "#//#" + Password;

        //        using (StreamWriter writer = new StreamWriter(filePath))
        //        {
        //            // Write the data to the file
        //            writer.WriteLine(dataToSave);

        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}");
        //        return false;
        //    }
        //}



        //public static bool GetStoredCredential(ref string Username, ref string Password)
        //{
        //    //this will get the stored username and password and will return true if found and false if not found.
        //    try
        //    {
        //        //gets the current project's directory
        //        string currentDirectory = System.IO.Directory.GetCurrentDirectory();

        //        // Path for the file that contains the credential.
        //        string filePath = currentDirectory + "\\data.txt";

        //        // Check if the file exists before attempting to read it
        //        if (File.Exists(filePath))
        //        {
        //            // Create a StreamReader to read from the file
        //            using (StreamReader reader = new StreamReader(filePath))
        //            {
        //                // Read data line by line until the end of the file
        //                string line;
        //                while ((line = reader.ReadLine()) != null)
        //                {
        //                    Console.WriteLine(line); // Output each line of data to the console
        //                    string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

        //                    Username = result[0];
        //                    Password = result[1];
        //                }
        //                return true;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}");
        //        return false;
        //    }

        //}
    }
}
