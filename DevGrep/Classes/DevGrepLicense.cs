using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevGrep.Classes.Config;

namespace DevGrep.Classes
{
    internal class DevGrepLicense
    {
        private bool _isLicensed;
        private static string privateKey = @"<RSAKeyValue><Modulus>mu2wAtvcKagN9NXg69Vaa6L0qpEY/2mxPt1opPGFgi3rBPp3aKMOkFxcVhtZAAL1jLJibODa9Omgv16jBwpq31vOSgBlnkXHSdVdbybEsIYZ1aJUlPKQpVFE4ukaCk0xJEwgCkk7Lj70RI+T99BJhc8IJf21RSSHx/WIYw/mZOk=</Modulus><Exponent>AQAB</Exponent><P>1GdX2oFsNRhIhK+TscuwcnINlC/yVx1ug84RpbiYrbA78MYkSr2w9bWUOtHrONJoPBMSReCNJbg8acPxubOSgw==</P><Q>urpVr+of3iEVZxFc99NptIGfzeda/PbDTuyLrBE9Qcd4XsSVuYTt7lxxCV14oZ9JeOy7tsjgs3oHucmR0x6fIw==</Q><DP>kfDn9mTS2rBys2iITLOt6xTeq6E+HAlG9B1VFs79aOocZVVfflNUEZ/P15KA0qwPnqdj+MtPcElkiS9vGa9+zQ==</DP><DQ>fFVSdP4aFhhFXgmrs8Dr46tkMwBYD5Rn3AeZwQsBlOky9zdC0vL2Uv7urWO1zTh4/bH0E/OR2y/oMQdMuBztrQ==</DQ><InverseQ>CvqB63m7Ug/qMESoptG6zgn/Ew7QVwnypbY9FDP6EaMv9QSOcyV7XeFD1lIoQPL2rnCjG89m2/xdtnOp3lzSyA==</InverseQ><D>Y+vAhVd/m25D8hCvS+sCskEbQ5bpZHGqWz05bIhI7zjmRwOzV80ya3DmLLLep99At0mlqUssbKDxh/tlcfkKC5siCJceffYVCn5U7E+SmnUrivJDk5ln8qU+ULvSgrOgEdX6Bxc0qGCZYvEhITyBws43a2rhVC3GLYqUHuvPb3E=</D></RSAKeyValue>";
        private DevGrepLicenseData _licenseData;
       
        internal DevGrepLicense()
        {
            _isLicensed = false;

            // Attempt to load the license to see if one exists and it is valid.
            LoadLicenseFile();
        }

        /// <summary>
        /// Allow a user to pass in encrypted license data. We will decrypt it and see if it is valid.
        /// </summary>
        /// <param name="encryptedLicenseData"></param>
        internal DevGrepLicense(string encryptedLicenseData)
        {
            byte[] ldBytes = Convert.FromBase64String(encryptedLicenseData);
            // Convert back to ASCII
            string semiNormal = System.Text.Encoding.ASCII.GetString(ldBytes);

            DevGrepLicenseData licenseData = DevGrepLicenseData.Parse(semiNormal);
            if (licenseData != null)
            {
                if (licenseData.DevGrepMajorVersion == CurrentMajorVersion)
                {
                    if (!licenseData.HasTimedOut)
                    {
                        _isLicensed = true;
                        _licenseData = licenseData;
                        SaveLicenseData(encryptedLicenseData);
                      
                    }
                    else
                    {
                        _isLicensed = false;
                        _licenseData = licenseData;
                      
                    }
                   
                }

               

            } 
        }

        internal bool IsLicensed
        {
            get { return _isLicensed; }
        }

        private void LoadLicenseFile()
        {
            string crap = LicenseFileNamePath;
            if (File.Exists(crap))
            {
                string licenseContents = File.ReadAllText(crap);
                byte[] ldBytes = Convert.FromBase64String(licenseContents);
                // Convert back to ASCII
                string semiNormal = System.Text.Encoding.ASCII.GetString(ldBytes);
                if (!ValidLicense(semiNormal))
                {
                    try
                    {
                        File.Delete(LicenseFileNamePath);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            else
            {
                // No license file...
                _isLicensed = false;
            }
        }

        private bool ValidLicense(string decryptedText)
        {
            DevGrepLicenseData licenseData = DevGrepLicenseData.Parse(decryptedText);
            if (licenseData != null)
            {
             if (licenseData.DevGrepMajorVersion == CurrentMajorVersion)
             {
                 if (!licenseData.HasTimedOut)
                 {
                     _isLicensed = true;
                     _licenseData = licenseData;
                     return true;
                 }
                 else
                 {
                     _isLicensed = false;
                     _licenseData = licenseData;
                     return false;
                 }
             }
            }
            return false;
        }

        private void SaveLicenseData(string encryptedLicenseString)
        {
            if (!Directory.Exists(LicenseFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(LicenseFolderPath);
                }
                catch (Exception)
                {

                    MessageBox.Show("Could not create a folder to store the license file. Ensure you" +
                                    Environment.NewLine + "have the proper permissions." + Environment.NewLine +
                                    LicenseFolderPath);
                    return;
                }
            }
            try
            {
                File.WriteAllText(LicenseFileNamePath, encryptedLicenseString);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not save your license file. Ensure you" +
                                    Environment.NewLine + "have the proper permissions." + Environment.NewLine +
                                    LicenseFolderPath);
                
            }
        }

        internal int CurrentMajorVersion
        {
            get
            {
                // Get the version of the current assembly.
                Assembly assem = Assembly.GetExecutingAssembly();
                AssemblyName assemName = assem.GetName();
                Version ver = assemName.Version;
                return ver.Major;
            }
        }

        internal DevGrepLicenseData LicenseData
        {
            get { return _licenseData; }
        }
        internal string LicenseFileNamePath
        {
            get
            {
                var ad = new ApplicationData();
                string configNamePath = Path.Combine(ad.FolderPath,  "DevGrep"+CurrentMajorVersion+".license");
                return configNamePath;
            }
        }

        internal string LicenseFolderPath
        {
            get
            {
                var ad = new ApplicationData();
                return ad.FolderPath;
                
            }
        }

        internal static  string Decrypt(string encryptedBase64)
        {
            // Variables
            CspParameters cspParams = null;
            RSACryptoServiceProvider rsaProvider = null;

            string plainText = "";
            byte[] plainBytes = null;

            // Convert the base64 string back to a byte array....
            byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);

            try
            {
                // Select target CSP
                cspParams = new CspParameters();
                cspParams.ProviderType = 1; // PROV_RSA_FULL 
                //cspParams.ProviderName; // CSP name
                rsaProvider = new RSACryptoServiceProvider(cspParams);



                // Import private/public key pair
                rsaProvider.FromXmlString(privateKey);

                // Decrypt text
                plainBytes = rsaProvider.Decrypt(encryptedBytes, false);

                plainText = Encoding.Unicode.GetString(plainBytes);
                return plainText;
            }
            catch (Exception ex)
            {
                // Any errors? Show them
                Console.WriteLine("Exception decrypting file! More info:");
                Console.WriteLine(ex.Message);
            }
            finally
            {

            }
            return "";
        } // Decrypt
    }

    internal class DevGrepLicenseData
    {
        private string _registerdUser;
        private string _dateRegistered;
        private string _emailAddress;
        private string _licenseCount;
        private string _companyName;
        private string _installationID;
        private string _timeOutDate;
        private int _devGrepMajorVersion;

        internal DevGrepLicenseData()
        {
            _registerdUser = "";
            _dateRegistered = "";
            _emailAddress = "";
            _licenseCount = "";
            _companyName = "";
            _installationID = "";
            _timeOutDate = "";
            _devGrepMajorVersion = 0;
        }

        internal static DevGrepLicenseData Parse(string licenseData)
        {
            bool isTOPresent = false;
            DevGrepLicenseData newLicense = new DevGrepLicenseData();

            string[] indLines = licenseData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in indLines)
            {
                // Split these lines now to find the data we are looking for..
                string[] sp = s.Split('|');
                if (sp != null && sp.Length == 2)
                {
                  switch (sp[0].ToUpper())
                  {
                      case "REGISTEREDUSER":
                          newLicense.RegisteredUser = DevGrepLicense.Decrypt( sp[1]);
                          break;
                      case "DATEREGISTERED":
                          newLicense.DateRegistered = DevGrepLicense.Decrypt(sp[1]);
                          break;
                      case "EMAILADDRESS":
                          newLicense.EmailAddress = DevGrepLicense.Decrypt(sp[1]);
                          break;
                      case "LICENSECOUNT":
                          newLicense.LicenseCount = DevGrepLicense.Decrypt(sp[1]);
                          break;
                      case "COMPANYNAME":
                          newLicense.CompanyName = DevGrepLicense.Decrypt(sp[1]);
                          break;
                      case "DEVGREPMAJORVERSION":
                          newLicense.DevGrepMajorVersion  = Convert.ToInt32( DevGrepLicense.Decrypt(sp[1]));
                          break;
                      case "ID":
                          newLicense.InstallationID = DevGrepLicense.Decrypt(sp[1]);
                          break;
                      case "TO":
                          newLicense.TO = DevGrepLicense.Decrypt(sp[1]);
                          isTOPresent = true;
                          break;
                  }
                }
            }
            if (!string.IsNullOrEmpty(newLicense.RegisteredUser) && !string.IsNullOrEmpty(newLicense.DateRegistered) &&
                !string.IsNullOrEmpty(newLicense.InstallationID) && isTOPresent)
            {
                //TODO Check the TO member (timeout date)....  Return null if the date has passed.
                return newLicense;
            }
              
            return null;
        }

        internal DateTime? TimeOutDate
        {
            get
            {
                if (!string.IsNullOrEmpty(_timeOutDate))
                {
                    DateTime dtTried;
                    bool isValid = DateTime.TryParse(_timeOutDate + " 1:00:00 AM", out dtTried);
                    if (isValid)
                        return dtTried;
                }
                return null;
            }
        }
        internal bool HasTimedOut
        {
            get
            {
                if (ContainsTimeout)
                {
                    if (TimeOutDate != null)
                    return DateTime.Now > TimeOutDate;
                    return false;
                }
                return false;
            }
        }
        internal bool ContainsTimeout
        {
            get
            {
                if (string.IsNullOrEmpty(_timeOutDate))
                    return false;
                return true;
            }
        }
        internal string TO
        {
            get { return _timeOutDate; }
            set { _timeOutDate = value; }
        }

        internal string InstallationID
        {
            get { return _installationID; }
            set { _installationID = value; }
        }
        internal int DevGrepMajorVersion
        {
            get { return _devGrepMajorVersion; }
            set { _devGrepMajorVersion = value; }
        }
        internal string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
        internal string LicenseCount
        {
            get { return _licenseCount; }
            set { _licenseCount = value; }
        }
        internal string EmailAddress
        {
            get { return _emailAddress; }
            set { _emailAddress = value; }
        }
        internal string DateRegistered
        {
            get { return _dateRegistered; }
            set { _dateRegistered = value; }
        }

        internal string RegisteredUser
        {
            get { return _registerdUser; }
            set { _registerdUser=value; }
        }
    }
}
