using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;   

namespace DbCore.Otp
{
    /// <summary>
    /// Class for OTP Authenticator.
    /// </summary>
    public class OtpAuthenticator
    {
        private readonly int _intervalLength = 30;
        private readonly int _pinLength = 6;
        private readonly string _issuer = "IMB";

        /// <summary>
        /// Gets or sets the secret key of the user.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="username"></param>
        public OtpAuthenticator(string key, string username)
        {
            Key = key;
            Username = username;
        }

        /// <summary>
        /// Creates an url that needs to be encoded to a QR code.
        /// </summary>
        public string GetOtpUrl()
        {
            var keyBase = Base32.ToBase32String(Key);
            var url = HttpUtility.UrlEncode($"otpauth://totp/{Username}?secret={keyBase}&issuer={_issuer}");

            return url;
        }

        /// <summary>
        /// Generates a pin for the <see cref="Key"/> using the <see cref="CurrentInterval"/>.
        /// </summary>
        public string GetPin()
        {
            return GetPin(CurrentInterval);
        }

        /// <summary>
        /// Generates a pin for the <see cref="Key"/>.
        /// </summary>
        /// <param name="counter">Counter for which to generate the pin.</param>
        public string GetPin(long counter)
        {
            int intSize = 4;
            var counterBytes = BitConverter.GetBytes(counter);

            if (BitConverter.IsLittleEndian)
            {
                //spec requires bytes in big-endian order
                Array.Reverse(counterBytes);
            }

            var hash = new HMACSHA1(Encoding.ASCII.GetBytes(Key)).ComputeHash(counterBytes);
            var offset = hash[hash.Length - 1] & 0xF;

            var selectedBytes = new byte[intSize];
            Buffer.BlockCopy(hash, offset, selectedBytes, 0, intSize);

            if (BitConverter.IsLittleEndian)
            {
                //spec interprets bytes in big-endian order
                Array.Reverse(selectedBytes);
            }

            var selectedInteger = BitConverter.ToInt32(selectedBytes, 0);

            //remove the most significant bit for interoperability per spec
            var truncatedHash = selectedInteger & 0x7FFFFFFF;

            //generate number of digits for given pin length
            var pin = truncatedHash % (int)Math.Pow(10, _pinLength);

            return pin.ToString(CultureInfo.InvariantCulture).PadLeft(_pinLength, '0');
        }

        #region Helpers

        /// <summary>
        /// Gets the current number of intervals since the Unix Epoch.
        /// </summary>
        private long CurrentInterval
        {
            get
            {
                var totalSeconds = (long)Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
                return totalSeconds / _intervalLength;
            }
        }

        #endregion
    }
}
