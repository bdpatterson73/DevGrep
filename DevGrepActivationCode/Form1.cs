using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevGrepActivationCode
{
    public partial class Form1 : Form
    {
        private string publicKey = @"<RSAKeyValue><Modulus>mu2wAtvcKagN9NXg69Vaa6L0qpEY/2mxPt1opPGFgi3rBPp3aKMOkFxcVhtZAAL1jLJibODa9Omgv16jBwpq31vOSgBlnkXHSdVdbybEsIYZ1aJUlPKQpVFE4ukaCk0xJEwgCkk7Lj70RI+T99BJhc8IJf21RSSHx/WIYw/mZOk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        private string privateKey = @"<RSAKeyValue><Modulus>mu2wAtvcKagN9NXg69Vaa6L0qpEY/2mxPt1opPGFgi3rBPp3aKMOkFxcVhtZAAL1jLJibODa9Omgv16jBwpq31vOSgBlnkXHSdVdbybEsIYZ1aJUlPKQpVFE4ukaCk0xJEwgCkk7Lj70RI+T99BJhc8IJf21RSSHx/WIYw/mZOk=</Modulus><Exponent>AQAB</Exponent><P>1GdX2oFsNRhIhK+TscuwcnINlC/yVx1ug84RpbiYrbA78MYkSr2w9bWUOtHrONJoPBMSReCNJbg8acPxubOSgw==</P><Q>urpVr+of3iEVZxFc99NptIGfzeda/PbDTuyLrBE9Qcd4XsSVuYTt7lxxCV14oZ9JeOy7tsjgs3oHucmR0x6fIw==</Q><DP>kfDn9mTS2rBys2iITLOt6xTeq6E+HAlG9B1VFs79aOocZVVfflNUEZ/P15KA0qwPnqdj+MtPcElkiS9vGa9+zQ==</DP><DQ>fFVSdP4aFhhFXgmrs8Dr46tkMwBYD5Rn3AeZwQsBlOky9zdC0vL2Uv7urWO1zTh4/bH0E/OR2y/oMQdMuBztrQ==</DQ><InverseQ>CvqB63m7Ug/qMESoptG6zgn/Ew7QVwnypbY9FDP6EaMv9QSOcyV7XeFD1lIoQPL2rnCjG89m2/xdtnOp3lzSyA==</InverseQ><D>Y+vAhVd/m25D8hCvS+sCskEbQ5bpZHGqWz05bIhI7zjmRwOzV80ya3DmLLLep99At0mlqUssbKDxh/tlcfkKC5siCJceffYVCn5U7E+SmnUrivJDk5ln8qU+ULvSgrOgEdX6Bxc0qGCZYvEhITyBws43a2rhVC3GLYqUHuvPb3E=</D></RSAKeyValue>";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string plainText = "Brian Patterson was here";

            string encrypted = Encrypt(plainText);
            Console.WriteLine("Encrypted: "+encrypted);
            string decrypt = Decrypt(encrypted);
            Console.WriteLine("Decrypted: " + decrypt);
        }

         private string Decrypt(string encryptedBase64)
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

         private string Encrypt(string plainText)
        {
            // Variables
            CspParameters cspParams = null;
            RSACryptoServiceProvider rsaProvider = null;
            StreamReader publicKeyFile = null;
            StreamReader plainFile = null;
            FileStream encryptedFile = null;

            byte[] plainBytes = null;
            byte[] encryptedBytes = null;

            try
            {
                // Select target CSP
                cspParams = new CspParameters();
                cspParams.ProviderType = 1; // PROV_RSA_FULL 
                //cspParams.ProviderName; // CSP name
                rsaProvider = new RSACryptoServiceProvider(cspParams);


                // Import public key
                rsaProvider.FromXmlString(publicKey);

                // Encrypt plain text
                plainBytes = Encoding.Unicode.GetBytes(plainText);
                encryptedBytes = rsaProvider.Encrypt(plainBytes, false);

                return Convert.ToBase64String(encryptedBytes);
            }
            catch (Exception ex)
            {
                // Any errors? Show them
                Console.WriteLine("Exception encrypting file! More info:");
                Console.WriteLine(ex.Message);
            }
            finally
            {
               
            }
             return "";
        }

         private void button1_Click(object sender, EventArgs e)
         {
             txtCompanyName.Text = "";
             txtDateRegistered.Text = DateTime.Now.ToShortDateString();
             txtDevGrepMajorVersion.Text = "";
             txtEmailAddress.Text = "";
             txtLicenseCount.Text = "1";
             txtRegisteredUser.Text = "";
             txtInstallationCode.Text = Guid.NewGuid().ToString();

         }

         private void button2_Click(object sender, EventArgs e)
         {
             string base64Encrtypted = GetBase64Encrypted();
             richTextBox1.Text = base64Encrtypted;
         } // Encrypt

        private void SaveLicenseDetails()
        {
            string licDetails = txtRegisteredUser.Text.Trim() + "," + txtDateRegistered.Text.Trim() + "," +
                                txtEmailAddress.Text.Trim() + "," + txtLicenseCount.Text.Trim() + "," +
                                txtCompanyName.Text.Trim() + "," + txtDevGrepMajorVersion.Text.Trim() + "," +
                                txtInstallationCode.Text + "," +
                                txtTimeoutDate.Text + Environment.NewLine;
            File.AppendAllText("LicenseLog.txt", licDetails);
        }
        private string GetBase64Encrypted()
        {
            //Guid installationCode = Guid.NewGuid();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("RegisteredUser|" + Encrypt(txtRegisteredUser.Text.Trim()));
            sb.AppendLine("DateRegistered|" + Encrypt(txtDateRegistered.Text.Trim()));
            sb.AppendLine("EmailAddress|" + Encrypt(txtEmailAddress.Text.Trim()));
            sb.AppendLine("LicenseCount|" + Encrypt(txtLicenseCount.Text.Trim()));
            sb.AppendLine("CompanyName|" + Encrypt(txtCompanyName.Text.Trim()));
            sb.AppendLine("DevGrepMajorVersion|" + Encrypt(txtDevGrepMajorVersion.Text.Trim()));
            sb.AppendLine("ID|" + Encrypt(txtInstallationCode.Text.Trim()));
            sb.AppendLine("TO|" + Encrypt(txtTimeoutDate.Text.Trim()));
            string plain = sb.ToString();
            byte[] myBytes = System.Text.Encoding.ASCII.GetBytes(plain); //System.Text.Encoding.ASCII.GetString (ascii)
            return Convert.ToBase64String( myBytes);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string base64Encrtypted = GetBase64Encrypted();
            richTextBox1.Text = base64Encrtypted;

            string compressedName = txtRegisteredUser.Text.Trim().Replace(" ", "");
            string fileName = compressedName + "_DevGrep" + txtDevGrepMajorVersion.Text.Trim() + ".license";
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.AddExtension = true;
            sfd.CheckFileExists = false;
            sfd.CheckPathExists = true;
            sfd.CreatePrompt = false;
            sfd.DereferenceLinks = false;
            sfd.OverwritePrompt = true;
            sfd.RestoreDirectory = false;
            sfd.ValidateNames = true;
            sfd.FileName = fileName;
            sfd.DefaultExt = ".license";
            sfd.InitialDirectory = "C:\\";
            sfd.Filter = "license files (*.license)|*.license";
            sfd.FilterIndex = 1;
            sfd.Title = "Save License File";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                SaveLicenseDetails();
                File.WriteAllText(sfd.FileName, base64Encrtypted);
            }
 
        }
    }
}
