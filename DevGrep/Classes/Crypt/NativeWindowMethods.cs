using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DevGrep.Classes.Crypt
{
    /// <summary>
    /// Class NativeWindowMethods
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal class NativeWindowMethods
    {
        // Fields
        /// <summary>
        /// The CER t_ CLOS e_ STOR e_ CHEC k_ FLAG
        /// </summary>
        internal const int CERT_CLOSE_STORE_CHECK_FLAG = 2;

        /// <summary>
        /// The CER t_ CLOS e_ STOR e_ FORC e_ FLAG
        /// </summary>
        internal const int CERT_CLOSE_STORE_FORCE_FLAG = 1;

        /// <summary>
        /// The CER t_ COMPAR e_ ANY
        /// </summary>
        internal const int CERT_COMPARE_ANY = 0;

        /// <summary>
        /// The CER t_ COMPAR e_ ATTR
        /// </summary>
        internal const int CERT_COMPARE_ATTR = 3;

        /// <summary>
        /// The CER t_ COMPAR e_ CT l_ USAGE
        /// </summary>
        internal const int CERT_COMPARE_CTL_USAGE = 10;

        /// <summary>
        /// The CER t_ COMPAR e_ ENHKE y_ USAGE
        /// </summary>
        internal const int CERT_COMPARE_ENHKEY_USAGE = 10;

        /// <summary>
        /// The CER t_ COMPAR e_ HASH
        /// </summary>
        internal const int CERT_COMPARE_HASH = 1;

        /// <summary>
        /// The CER t_ COMPAR e_ KE y_ SPEC
        /// </summary>
        internal const int CERT_COMPARE_KEY_SPEC = 9;

        /// <summary>
        /// The CER t_ COMPAR e_ MASK
        /// </summary>
        internal const int CERT_COMPARE_MASK = 0xffff;

        /// <summary>
        /// The CER t_ COMPAR e_ M D5_ HASH
        /// </summary>
        internal const int CERT_COMPARE_MD5_HASH = 4;

        /// <summary>
        /// The CER t_ COMPAR e_ NAME
        /// </summary>
        internal const int CERT_COMPARE_NAME = 2;

        /// <summary>
        /// The CER t_ COMPAR e_ NAM e_ ST r_ A
        /// </summary>
        internal const int CERT_COMPARE_NAME_STR_A = 7;

        /// <summary>
        /// The CER t_ COMPAR e_ NAM e_ ST r_ W
        /// </summary>
        internal const int CERT_COMPARE_NAME_STR_W = 8;

        /// <summary>
        /// The CER t_ COMPAR e_ PROPERTY
        /// </summary>
        internal const int CERT_COMPARE_PROPERTY = 5;

        /// <summary>
        /// The CER t_ COMPAR e_ PUBLI c_ KEY
        /// </summary>
        internal const int CERT_COMPARE_PUBLIC_KEY = 6;

        /// <summary>
        /// The CER t_ COMPAR e_ SH a1_ HASH
        /// </summary>
        internal const int CERT_COMPARE_SHA1_HASH = 1;

        /// <summary>
        /// The CER t_ COMPAR e_ SHIFT
        /// </summary>
        internal const int CERT_COMPARE_SHIFT = 0x10;

        /// <summary>
        /// The CER t_ FIN d_ ANY
        /// </summary>
        internal const uint CERT_FIND_ANY = 0;

        /// <summary>
        /// The CER t_ FIN d_ ISSUE r_ STR
        /// </summary>
        internal const uint CERT_FIND_ISSUER_STR = 0x80004;

        /// <summary>
        /// The CER t_ FIN d_ ISSUE r_ ST r_ W
        /// </summary>
        internal const uint CERT_FIND_ISSUER_STR_W = 0x80004;

        /// <summary>
        /// The CER t_ INF o_ EXTENSIO n_ FLAG
        /// </summary>
        private const uint CERT_INFO_EXTENSION_FLAG = 11;

        /// <summary>
        /// The CER t_ INF o_ ISSUE r_ FLAG
        /// </summary>
        private const uint CERT_INFO_ISSUER_FLAG = 4;

        /// <summary>
        /// The CER t_ INF o_ ISSUE r_ UNIQU e_ I d_ FLAG
        /// </summary>
        private const uint CERT_INFO_ISSUER_UNIQUE_ID_FLAG = 9;

        /// <summary>
        /// The CER t_ INF o_ NO t_ AFTE r_ FLAG
        /// </summary>
        private const uint CERT_INFO_NOT_AFTER_FLAG = 6;

        /// <summary>
        /// The CER t_ INF o_ NO t_ BEFOR e_ FLAG
        /// </summary>
        private const uint CERT_INFO_NOT_BEFORE_FLAG = 5;

        /// <summary>
        /// The CER t_ INF o_ SERIA l_ NUMBE r_ FLAG
        /// </summary>
        private const uint CERT_INFO_SERIAL_NUMBER_FLAG = 2;

        /// <summary>
        /// The CER t_ INF o_ SIGNATUR e_ ALGORITH m_ FLAG
        /// </summary>
        private const uint CERT_INFO_SIGNATURE_ALGORITHM_FLAG = 3;

        /// <summary>
        /// The CER t_ INF o_ SUBJEC t_ FLAG
        /// </summary>
        private const uint CERT_INFO_SUBJECT_FLAG = 7;

        /// <summary>
        /// The CER t_ INF o_ SUBJEC t_ PUBLI c_ KE y_ INF o_ FLAG
        /// </summary>
        private const uint CERT_INFO_SUBJECT_PUBLIC_KEY_INFO_FLAG = 8;

        /// <summary>
        /// The CER t_ INF o_ SUBJEC t_ UNIQU e_ I d_ FLAG
        /// </summary>
        private const uint CERT_INFO_SUBJECT_UNIQUE_ID_FLAG = 10;

        /// <summary>
        /// The CER t_ INF o_ VERSIO n_ FLAG
        /// </summary>
        private const uint CERT_INFO_VERSION_FLAG = 1;

        /// <summary>
        /// The CER t_ STOR e_ N o_ CR l_ FLAG
        /// </summary>
        internal const int CERT_STORE_NO_CRL_FLAG = 0x10000;

        /// <summary>
        /// The CER t_ STOR e_ N o_ ISSUE r_ FLAG
        /// </summary>
        internal const int CERT_STORE_NO_ISSUER_FLAG = 0x20000;

        /// <summary>
        /// The CER t_ STOR e_ REVOCATIO n_ FLAG
        /// </summary>
        internal const int CERT_STORE_REVOCATION_FLAG = 4;

        /// <summary>
        /// The CER t_ STOR e_ SIGNATUR e_ FLAG
        /// </summary>
        internal const int CERT_STORE_SIGNATURE_FLAG = 1;

        /// <summary>
        /// The CER t_ STOR e_ TIM e_ VALIDIT y_ FLAG
        /// </summary>
        internal const int CERT_STORE_TIME_VALIDITY_FLAG = 2;

        /// <summary>
        /// The CER t_ v1
        /// </summary>
        private const uint CERT_V1 = 0;

        /// <summary>
        /// The CER t_ v2
        /// </summary>
        private const uint CERT_V2 = 1;

        /// <summary>
        /// The CER t_ v3
        /// </summary>
        private const uint CERT_V3 = 2;

        /// <summary>
        /// The CRYP t_ DECOD e_ NOCOP y_ FLAG
        /// </summary>
        internal const int CRYPT_DECODE_NOCOPY_FLAG = 1;

        /// <summary>
        /// The CRYP t_ ENCOD e_ DECOD e_ NONE
        /// </summary>
        internal const int CRYPT_ENCODE_DECODE_NONE = 0;

        /// <summary>
        /// The DRIV e_ FIXED
        /// </summary>
        internal const int DRIVE_FIXED = 3;

        /// <summary>
        /// The MA x_ LANA
        /// </summary>
        internal const int MAX_LANA = 0xfe;

        /// <summary>
        /// The M y_ ENCODIN g_ TYPE
        /// </summary>
        internal const uint MY_ENCODING_TYPE = 0x10001;

        /// <summary>
        /// The NCBASTAT
        /// </summary>
        internal const byte NCBASTAT = 0x33;

        /// <summary>
        /// The NCBENUM
        /// </summary>
        internal const byte NCBENUM = 0x37;

        /// <summary>
        /// The NCBNAMSZ
        /// </summary>
        internal const int NCBNAMSZ = 0x10;

        /// <summary>
        /// The NCBRESET
        /// </summary>
        internal const byte NCBRESET = 50;

        /// <summary>
        /// The NR c_ GOODRET
        /// </summary>
        internal const byte NRC_GOODRET = 0;

        /// <summary>
        /// The PKC S_7_ AS n_ ENCODING
        /// </summary>
        internal const uint PKCS_7_ASN_ENCODING = 0x10000;

        /// <summary>
        /// The PKC s_ ATTRIBUTE
        /// </summary>
        internal const char PKCS_ATTRIBUTE = '"';

        /// <summary>
        /// The PKC s_ CONTEN t_ INF o_ SEQUENC e_ O f_ ANY
        /// </summary>
        internal const char PKCS_CONTENT_INFO_SEQUENCE_OF_ANY = '#';

        /// <summary>
        /// The PKC s_ TIM e_ REQUEST
        /// </summary>
        internal const char PKCS_TIME_REQUEST = '\x0018';

        /// <summary>
        /// The PKC s_ UT c_ TIME
        /// </summary>
        internal const char PKCS_UTC_TIME = '\x0017';

        /// <summary>
        /// The RS a_ CS p_ PUBLICKEYBLOB
        /// </summary>
        internal const char RSA_CSP_PUBLICKEYBLOB = '\x0019';

        /// <summary>
        /// The X509_ ALTERNAT e_ NAME
        /// </summary>
        internal const char X509_ALTERNATE_NAME = '\x0012';

        /// <summary>
        /// The X509_ AN y_ STRING
        /// </summary>
        internal const char X509_ANY_STRING = '\x0006';

        /// <summary>
        /// The X509_ AS n_ ENCODING
        /// </summary>
        internal const uint X509_ASN_ENCODING = 1;

        /// <summary>
        /// The X509_ AUTHORIT y_ KE y_ ID
        /// </summary>
        internal const char X509_AUTHORITY_KEY_ID = '\t';

        /// <summary>
        /// The X509_ BASI c_ CONSTRAINTS
        /// </summary>
        internal const char X509_BASIC_CONSTRAINTS = '\x0013';

        /// <summary>
        /// The X509_ BASI c_ CONSTRAINT s2
        /// </summary>
        internal const char X509_BASIC_CONSTRAINTS2 = '\x0015';

        /// <summary>
        /// The X509_ BITS
        /// </summary>
        internal const char X509_BITS = '&';

        /// <summary>
        /// The X509_ CERT
        /// </summary>
        internal const char X509_CERT = '\x0001';

        /// <summary>
        /// The X509_ CER t_ CR l_ T o_ B e_ SIGNED
        /// </summary>
        internal const char X509_CERT_CRL_TO_BE_SIGNED = '\x0003';

        /// <summary>
        /// The X509_ CER t_ POLICIES
        /// </summary>
        internal const char X509_CERT_POLICIES = '\x0016';

        /// <summary>
        /// The X509_ CER t_ REQUES t_ T o_ B e_ SIGNED
        /// </summary>
        internal const char X509_CERT_REQUEST_TO_BE_SIGNED = '\x0004';

        /// <summary>
        /// The X509_ CER t_ T o_ B e_ SIGNED
        /// </summary>
        internal const char X509_CERT_TO_BE_SIGNED = '\x0002';

        /// <summary>
        /// The X509_ CHOIC e_ O f_ TIME
        /// </summary>
        internal const char X509_CHOICE_OF_TIME = '0';

        /// <summary>
        /// The X509_ ENUMERATED
        /// </summary>
        internal const char X509_ENUMERATED = ')';

        /// <summary>
        /// The X509_ EXTENSIONS
        /// </summary>
        internal const char X509_EXTENSIONS = '\x0005';

        /// <summary>
        /// The X509_ INTEGER
        /// </summary>
        internal const char X509_INTEGER = '\'';

        /// <summary>
        /// The X509_ KE y_ ATTRIBUTES
        /// </summary>
        internal const char X509_KEY_ATTRIBUTES = '\x0010';

        /// <summary>
        /// The X509_ KE y_ USAGE
        /// </summary>
        internal const char X509_KEY_USAGE = '\x0014';

        /// <summary>
        /// The X509_ KE y_ USAG e_ RESTRICTION
        /// </summary>
        internal const char X509_KEY_USAGE_RESTRICTION = '\x0011';

        /// <summary>
        /// The X509_ KEYGE n_ REQUES t_ T o_ B e_ SIGNED
        /// </summary>
        internal const char X509_KEYGEN_REQUEST_TO_BE_SIGNED = '!';

        /// <summary>
        /// The X509_ MULT i_ BYT e_ INTEGER
        /// </summary>
        internal const char X509_MULTI_BYTE_INTEGER = '(';

        /// <summary>
        /// The X509_ NAME
        /// </summary>
        internal const char X509_NAME = '\a';

        /// <summary>
        /// The X509_ NAM e_ VALUE
        /// </summary>
        internal const char X509_NAME_VALUE = '\x0006';

        /// <summary>
        /// The X509_ OCTE t_ STRING
        /// </summary>
        internal const char X509_OCTET_STRING = '%';

        /// <summary>
        /// The X509_ PUBLI c_ KE y_ INFO
        /// </summary>
        internal const char X509_PUBLIC_KEY_INFO = '\b';

        /// <summary>
        /// The X509_ UNICOD e_ AN y_ STRING
        /// </summary>
        internal const char X509_UNICODE_ANY_STRING = '$';

        /// <summary>
        /// The X509_ UNICOD e_ NAME
        /// </summary>
        internal const char X509_UNICODE_NAME = ' ';

        /// <summary>
        /// The X509_ UNICOD e_ NAM e_ VALUE
        /// </summary>
        internal const char X509_UNICODE_NAME_VALUE = '$';

        /// <summary>
        /// The public key
        /// </summary>
        internal static byte[] PublicKey = new byte[]
                                               {
                                                   0x30, 0x48, 2, 0x41, 0, 0xee, 0x24, 0xf5, 0xd4, 50, 0x5b, 0x1d, 0x77,
                                                   0xf7, 0x59, 5,
                                                   0x52, 0x73, 0xec, 0x6f, 0x23, 0xa3, 0xf4, 0x3a, 0x61, 0x1c, 0xd4, 0,
                                                   0x3d, 0x87, 0x45, 0x19,
                                                   100, 0x3d, 0x52, 0x91, 0x1f, 0x9a, 0xf6, 180, 0x15, 0xde, 0x8a, 0x44,
                                                   0x75, 0xae, 0x35, 0x1a,
                                                   0xe5, 0x99, 0xe8, 0xb6, 0x12, 150, 0xe4, 0x31, 0x33, 0xba, 0x94, 240,
                                                   0x69, 0x37, 0xff, 0x92,
                                                   0xef, 0x1f, 130, 0xc6, 0x3b, 2, 3, 1, 0, 1
                                               };

        /// <summary>
        /// The public key size
        /// </summary>
        internal static int PublicKeySize = PublicKey.GetLength(0);

        // Methods
        /// <summary>
        /// Certs the close store.
        /// </summary>
        /// <param name="hCertStore">The h cert store.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("crypt32", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern bool CertCloseStore([In] IntPtr hCertStore, uint dwFlags);

        /// <summary>
        /// Certs the find certificate in store.
        /// </summary>
        /// <param name="hCertStore">The h cert store.</param>
        /// <param name="dwCertEncodingType">Type of the dw cert encoding.</param>
        /// <param name="dwFindFlags">The dw find flags.</param>
        /// <param name="dwFindType">Type of the dw find.</param>
        /// <param name="pvFindPara">The pv find para.</param>
        /// <param name="pPrevCertContext">The p prev cert context.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("crypt32", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr CertFindCertificateInStore([In] IntPtr hCertStore, [In] uint dwCertEncodingType,
                                                                 [In] uint dwFindFlags, [In] uint dwFindType,
                                                                 [In, MarshalAs(UnmanagedType.BStr)] string pvFindPara,
                                                                 [In] IntPtr pPrevCertContext);

        /// <summary>
        /// Certs the find extension.
        /// </summary>
        /// <param name="pszObjId">The PSZ obj id.</param>
        /// <param name="cExtensions">The c extensions.</param>
        /// <param name="rgExtensions">The rg extensions.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("crypt32", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern IntPtr CertFindExtension(string pszObjId, [In] uint cExtensions, [In] IntPtr rgExtensions);

        /// <summary>
        /// Certs the free certificate context.
        /// </summary>
        /// <param name="pCertContext">The p cert context.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("crypt32", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern bool CertFreeCertificateContext([In] IntPtr pCertContext);

        /// <summary>
        /// Certs the get issuer certificate from store.
        /// </summary>
        /// <param name="hCertStore">The h cert store.</param>
        /// <param name="pSubjectContext">The p subject context.</param>
        /// <param name="pPrevIssuerContext">The p prev issuer context.</param>
        /// <param name="pdwFlags">The PDW flags.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("crypt32", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern IntPtr CertGetIssuerCertificateFromStore([In] IntPtr hCertStore,
                                                                        [In] IntPtr pSubjectContext,
                                                                        [In] IntPtr pPrevIssuerContext,
                                                                        [In, Out] ref uint pdwFlags);

        /// <summary>
        /// Certs the open system store A.
        /// </summary>
        /// <param name="hProv">The h prov.</param>
        /// <param name="szSubsystemProtocol">The sz subsystem protocol.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("crypt32", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern IntPtr CertOpenSystemStoreA(uint hProv, string szSubsystemProtocol);

        /// <summary>
        /// Certs the verify subject certificate context.
        /// </summary>
        /// <param name="pSubject">The p subject.</param>
        /// <param name="pIssuer">The p issuer.</param>
        /// <param name="pdwFlags">The PDW flags.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("crypt32", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern bool CertVerifySubjectCertificateContext([In] IntPtr pSubject, [In] IntPtr pIssuer,
                                                                        [In, Out] ref uint pdwFlags);

        /// <summary>
        /// Crypts the decode object.
        /// </summary>
        /// <param name="dwCertEncodingType">Type of the dw cert encoding.</param>
        /// <param name="lpszStructType">Type of the LPSZ struct.</param>
        /// <param name="pbEncoded">The pb encoded.</param>
        /// <param name="cbEncoded">The cb encoded.</param>
        /// <param name="dwFlags">The dw flags.</param>
        /// <param name="pvStructInfo">The pv struct info.</param>
        /// <param name="pcbStructInfo">The PCB struct info.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        [DllImport("crypt32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern bool CryptDecodeObject([In] uint dwCertEncodingType, [In] char lpszStructType,
                                                      [In] IntPtr pbEncoded, [In] uint cbEncoded, [In] uint dwFlags,
                                                      [In, Out] IntPtr pvStructInfo, [In, Out] ref uint pcbStructInfo);

        /// <summary>
        /// Gets the drive type A.
        /// </summary>
        /// <param name="nDrive">The n drive.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern int GetDriveTypeA([In, MarshalAs(UnmanagedType.LPStr)] string nDrive);

        /// <summary>
        /// Gets the volume information A.
        /// </summary>
        /// <param name="lpRootPathName">Name of the lp root path.</param>
        /// <param name="lpVolumneNameBuffer">The lp volumne name buffer.</param>
        /// <param name="nVolumeNameSize">Size of the n volume name.</param>
        /// <param name="lpVolumeSerialNumber">The lp volume serial number.</param>
        /// <param name="lpMaximumComponentLength">Length of the lp maximum component.</param>
        /// <param name="lpFileSystemFlags">The lp file system flags.</param>
        /// <param name="lpFileSystemNameBuffer">The lp file system name buffer.</param>
        /// <param name="nFileSystemNameSize">Size of the n file system name.</param>
        /// <returns>System.Int32.</returns>
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern int GetVolumeInformationA([In] string lpRootPathName,
                                                         [In, MarshalAs(UnmanagedType.LPStr)] string lpVolumneNameBuffer,
                                                         [In] uint nVolumeNameSize,
                                                         [In, Out] ref uint lpVolumeSerialNumber,
                                                         [In, Out] uint lpMaximumComponentLength,
                                                         [In, Out] uint lpFileSystemFlags,
                                                         [In, MarshalAs(UnmanagedType.LPStr)] string
                                                             lpFileSystemNameBuffer, [In] uint nFileSystemNameSize);

        /// <summary>
        /// Netbioses the specified PNCB.
        /// </summary>
        /// <param name="pncb">The PNCB.</param>
        /// <returns>System.Byte.</returns>
        [DllImport("netapi32", CharSet = CharSet.Ansi, ExactSpelling = true)]
        internal static extern byte Netbios([In, Out] ref NCB pncb);

        // Nested Types

        #region Nested type: ADAPTER_STATUS

        /// <summary>
        /// Struct ADAPTER_STATUS
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct ADAPTER_STATUS
        {
            /// <summary>
            /// The adapter_address
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] adapter_address;

            /// <summary>
            /// The rev_major
            /// </summary>
            public byte rev_major;

            /// <summary>
            /// The reserved0
            /// </summary>
            public byte reserved0;

            /// <summary>
            /// The adapter_type
            /// </summary>
            public byte adapter_type;

            /// <summary>
            /// The rev_minor
            /// </summary>
            public byte rev_minor;

            /// <summary>
            /// The duration
            /// </summary>
            public ushort duration;

            /// <summary>
            /// The frmr_recv
            /// </summary>
            public ushort frmr_recv;

            /// <summary>
            /// The frmr_xmit
            /// </summary>
            public ushort frmr_xmit;

            /// <summary>
            /// The iframe_recv_err
            /// </summary>
            public ushort iframe_recv_err;

            /// <summary>
            /// The xmit_aborts
            /// </summary>
            public ushort xmit_aborts;

            /// <summary>
            /// The xmit_success
            /// </summary>
            public uint xmit_success;

            /// <summary>
            /// The recv_success
            /// </summary>
            public uint recv_success;

            /// <summary>
            /// The iframe_xmit_err
            /// </summary>
            public ushort iframe_xmit_err;

            /// <summary>
            /// The recv_buff_unavail
            /// </summary>
            public ushort recv_buff_unavail;

            /// <summary>
            /// The t1_timeouts
            /// </summary>
            public ushort t1_timeouts;

            /// <summary>
            /// The ti_timeouts
            /// </summary>
            public ushort ti_timeouts;

            /// <summary>
            /// The reserved1
            /// </summary>
            public uint reserved1;

            /// <summary>
            /// The free_ncbs
            /// </summary>
            public ushort free_ncbs;

            /// <summary>
            /// The max_cfg_ncbs
            /// </summary>
            public ushort max_cfg_ncbs;

            /// <summary>
            /// The max_ncbs
            /// </summary>
            public ushort max_ncbs;

            /// <summary>
            /// The xmit_buf_unavail
            /// </summary>
            public ushort xmit_buf_unavail;

            /// <summary>
            /// The max_dgram_size
            /// </summary>
            public ushort max_dgram_size;

            /// <summary>
            /// The pending_sess
            /// </summary>
            public ushort pending_sess;

            /// <summary>
            /// The max_cfg_sess
            /// </summary>
            public ushort max_cfg_sess;

            /// <summary>
            /// The max_sess
            /// </summary>
            public ushort max_sess;

            /// <summary>
            /// The max_sess_pkt_size
            /// </summary>
            public ushort max_sess_pkt_size;

            /// <summary>
            /// The name_count
            /// </summary>
            public ushort name_count;

            /// <summary>
            /// Resets this instance.
            /// </summary>
            public void Reset()
            {
                adapter_address = new byte[6];
                rev_major = reserved0 = adapter_type = (rev_minor = 0);
                xmit_success = recv_success = reserved1 = 0;
                duration =
                    frmr_recv =
                    frmr_xmit =
                    iframe_recv_err =
                    xmit_aborts =
                    iframe_xmit_err =
                    recv_buff_unavail =
                    t1_timeouts =
                    ti_timeouts =
                    free_ncbs =
                    max_cfg_ncbs =
                    max_ncbs =
                    xmit_buf_unavail =
                    max_dgram_size = pending_sess = max_cfg_sess = max_sess = max_sess_pkt_size = (name_count = 0);
            }
        }

        #endregion

        #region Nested type: ASTAT

        /// <summary>
        /// Struct ASTAT
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct ASTAT
        {
            /// <summary>
            /// The adapt
            /// </summary>
            public ADAPTER_STATUS adapt;

            /// <summary>
            /// The name buff
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
            public NAME_BUFFER[] NameBuff;

            /// <summary>
            /// Resets this instance.
            /// </summary>
            public void Reset()
            {
                adapt.Reset();
                NameBuff = new NAME_BUFFER[30];
                for (int i = 0; i < 30; i++)
                {
                    NameBuff[i].Reset();
                }
            }
        }

        #endregion

        #region Nested type: CERT_CONTEXT

        /// <summary>
        /// Struct CERT_CONTEXT
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CERT_CONTEXT
        {
            /// <summary>
            /// The dw cert encoding type
            /// </summary>
            public uint dwCertEncodingType;

            /// <summary>
            /// The pb cert encoded
            /// </summary>
            public IntPtr pbCertEncoded;

            /// <summary>
            /// The cb cert encoded
            /// </summary>
            public uint cbCertEncoded;

            /// <summary>
            /// The p cert info
            /// </summary>
            public IntPtr pCertInfo;
        }

        #endregion

        #region Nested type: CERT_EXTENSION

        /// <summary>
        /// Struct CERT_EXTENSION
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CERT_EXTENSION
        {
            /// <summary>
            /// The PSZ obj id
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string pszObjId;

            /// <summary>
            /// The f critical
            /// </summary>
            public bool fCritical;

            /// <summary>
            /// The value
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CRYPTOAPI_BLOB Value;
        }

        #endregion

        #region Nested type: CERT_INFO

        /// <summary>
        /// Struct CERT_INFO
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CERT_INFO
        {
            /// <summary>
            /// The dw version
            /// </summary>
            public uint dwVersion;

            /// <summary>
            /// The serial number
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CRYPTOAPI_BLOB SerialNumber;

            /// <summary>
            /// The signature algorithm
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CRYPT_ALGORITHM_IDENTIFIER SignatureAlgorithm;

            /// <summary>
            /// The issuer
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CRYPTOAPI_BLOB Issuer;

            /// <summary>
            /// The not before
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public FILETIME NotBefore;

            /// <summary>
            /// The not after
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public FILETIME NotAfter;

            /// <summary>
            /// The subject
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CRYPTOAPI_BLOB Subject;

            /// <summary>
            /// The subject public key info
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CERT_PUBLIC_KEY_INFO SubjectPublicKeyInfo;

            /// <summary>
            /// The issuer unique id
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CRYPT_BIT_BLOB IssuerUniqueId;

            /// <summary>
            /// The subject unique id
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CRYPT_BIT_BLOB SubjectUniqueId;

            /// <summary>
            /// The c extension
            /// </summary>
            public uint cExtension;

            /// <summary>
            /// The rg extension
            /// </summary>
            public IntPtr rgExtension;
        }

        #endregion

        #region Nested type: CERT_NAME_VALUE

        /// <summary>
        /// Struct CERT_NAME_VALUE
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CERT_NAME_VALUE
        {
            /// <summary>
            /// The dw value type
            /// </summary>
            public uint dwValueType;

            /// <summary>
            /// The value
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CRYPTOAPI_BLOB Value;
        }

        #endregion

        #region Nested type: CERT_PUBLIC_KEY_INFO

        /// <summary>
        /// Struct CERT_PUBLIC_KEY_INFO
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CERT_PUBLIC_KEY_INFO
        {
            /// <summary>
            /// The algorithm
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CRYPT_ALGORITHM_IDENTIFIER Algorithm;

            /// <summary>
            /// The public key
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CRYPT_BIT_BLOB PublicKey;
        }

        #endregion

        #region Nested type: CRYPTOAPI_BLOB

        /// <summary>
        /// Struct CRYPTOAPI_BLOB
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPTOAPI_BLOB
        {
            /// <summary>
            /// The cb data
            /// </summary>
            public uint cbData;

            /// <summary>
            /// The pb data
            /// </summary>
            public IntPtr pbData;
        }

        #endregion

        #region Nested type: CRYPT_ALGORITHM_IDENTIFIER

        /// <summary>
        /// Struct CRYPT_ALGORITHM_IDENTIFIER
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_ALGORITHM_IDENTIFIER
        {
            /// <summary>
            /// The PSZ obj id
            /// </summary>
            [MarshalAs(UnmanagedType.LPStr)]
            public string pszObjId;

            /// <summary>
            /// The parameters
            /// </summary>
            [MarshalAs(UnmanagedType.Struct)]
            public CRYPTOAPI_BLOB Parameters;
        }

        #endregion

        #region Nested type: CRYPT_BIT_BLOB

        /// <summary>
        /// Struct CRYPT_BIT_BLOB
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CRYPT_BIT_BLOB
        {
            /// <summary>
            /// The cb data
            /// </summary>
            public uint cbData;

            /// <summary>
            /// The pb data
            /// </summary>
            public IntPtr pbData;

            /// <summary>
            /// The c unused bits
            /// </summary>
            public uint cUnusedBits;
        }

        #endregion

        #region Nested type: FILETIME

        /// <summary>
        /// Struct FILETIME
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct FILETIME
        {
            /// <summary>
            /// The dw low date time
            /// </summary>
            public uint dwLowDateTime;

            /// <summary>
            /// The dw high date time
            /// </summary>
            public uint dwHighDateTime;
        }

        #endregion

        #region Nested type: LANA_ENUM

        /// <summary>
        /// Struct LANA_ENUM
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct LANA_ENUM
        {
            /// <summary>
            /// The length
            /// </summary>
            public byte length;

            /// <summary>
            /// The lana
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xff)]
            public byte[] lana;
        }

        #endregion

        #region Nested type: NAME_BUFFER

        /// <summary>
        /// Struct NAME_BUFFER
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct NAME_BUFFER
        {
            /// <summary>
            /// The name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x10)]
            public string name;

            /// <summary>
            /// The name_num
            /// </summary>
            public byte name_num;

            /// <summary>
            /// The name_flags
            /// </summary>
            public byte name_flags;

            /// <summary>
            /// Resets this instance.
            /// </summary>
            public void Reset()
            {
                name = string.Empty;
                name_flags = (name_num = 0);
            }
        }

        #endregion

        #region Nested type: NCB

        /// <summary>
        /// Struct NCB
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct NCB
        {
            /// <summary>
            /// The ncb_command
            /// </summary>
            public byte ncb_command;

            /// <summary>
            /// The ncb_retcode
            /// </summary>
            public byte ncb_retcode;

            /// <summary>
            /// The NCB_LSN
            /// </summary>
            public byte ncb_lsn;

            /// <summary>
            /// The ncb_num
            /// </summary>
            public byte ncb_num;

            /// <summary>
            /// The ncb_buffer
            /// </summary>
            public IntPtr ncb_buffer;

            /// <summary>
            /// The ncb_length
            /// </summary>
            public ushort ncb_length;

            /// <summary>
            /// The ncb_callname
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x10)]
            public string ncb_callname;

            /// <summary>
            /// The ncb_name
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x10)]
            public string ncb_name;

            /// <summary>
            /// The ncb_rto
            /// </summary>
            public byte ncb_rto;

            /// <summary>
            /// The ncb_sto
            /// </summary>
            public byte ncb_sto;

            /// <summary>
            /// The ncb_post
            /// </summary>
            public IntPtr ncb_post;

            /// <summary>
            /// The ncb_lana_num
            /// </summary>
            public byte ncb_lana_num;

            /// <summary>
            /// The NCB_CMD_CPLT
            /// </summary>
            public byte ncb_cmd_cplt;

            /// <summary>
            /// The ncb_reserve
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
            public byte[] ncb_reserve;

            /// <summary>
            /// The ncb_event
            /// </summary>
            public IntPtr ncb_event;

            /// <summary>
            /// Resets this instance.
            /// </summary>
            public void Reset()
            {
                ncb_command = ncb_retcode = ncb_lsn = (ncb_num = 0);
                ncb_rto = ncb_sto = ncb_lana_num = (ncb_cmd_cplt = 0);
                ncb_event = ncb_post = ncb_buffer = IntPtr.Zero;
                ncb_length = 0;
                ncb_callname = string.Empty;
                ncb_name = string.Empty;
                ncb_reserve = new byte[10];
            }
        }

        #endregion
    }
}
