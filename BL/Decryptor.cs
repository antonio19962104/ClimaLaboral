using System;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;


	/// <summary>
	/// This class is used to decrypt strings using one of the 4 
	/// symmetric algorithms - 3DES, DES, RC2 or Rijndael.
	/// </summary>
	/// <remarks>
	///  When creating an
	/// instance of this class, you must pass the algorithm to use in the 
	/// constructor: see <see cref="EncryptionAlgorithm"/>.  Optionally, if the encrypted string ws hex formatted,
	/// you can reformat the input string before decrypting.
	/// 
	/// This class is based on principles from the Patterns and Practices guide: 
	/// How To: Create an Encryption Library 
	/// by J.D. Meier, Alex Mackman, Michael Dunner, and Srinath Vasireddy 
	/// Microsoft Corporation
	///</remarks>
	[DebuggerStepThrough] 
	internal class Decryptor
	{
		private DecryptTransformer transformer;
		private byte[] initVec;
		private bool _FormatAsHex = true;

		/// <summary>
		/// The constructor
		/// </summary>
		/// <param name="algId">The Algorithm to use</param>
		public Decryptor(EncryptionAlgorithm algId)
		{
			//Create a Transformer with the proper algorithm
			transformer = new DecryptTransformer(algId);
		}
		/// <summary>
		/// Takes an encrypted string and returns an unencrypted
		/// string, given the string to decrypt, the algorithm,
		///  key and intial vector.
		/// </summary>
		/// <param name="StringToDecrypt">An encrypted string</param>
		/// <param name="key">The key used in encryption</param>
		/// <param name="IV">The Initial Vector used in encryption</param>
		/// <returns></returns>
		public string Decrypt(string StringToDecrypt, string key, string IV)
		{
			try {
				//Set up the memory stream for the decrypted data.
				MemoryStream memStreamDecryptedData = new MemoryStream();
				//Create byte array from string to decrypt
				byte[] bytesCipherString = CreateByteArray(StringToDecrypt);
				byte[] bytesKey = System.Text.Encoding.UTF8.GetBytes(key);
				initVec = System.Text.Encoding.UTF8.GetBytes(IV);

				transformer.IV = initVec;
				ICryptoTransform transform = 
					transformer.GetCryptoServiceProvider(bytesKey);

				CryptoStream cs_tmp = new CryptoStream(memStreamDecryptedData, 
					transform, CryptoStreamMode.Write);

				CryptoStream cs_dec = new CryptoStream(cs_tmp, 
					new FromBase64Transform(), CryptoStreamMode.Write);

				cs_dec.Write(bytesCipherString, 0, bytesCipherString.Length);
			
				cs_dec.FlushFinalBlock();
				cs_dec.Close();

				byte[] bytesRoundtrippedText = memStreamDecryptedData.ToArray();

				// now we have our round-tripped text in a byte array
				// turn it into string for output
				return System.Text.Encoding.UTF8.GetString(bytesRoundtrippedText);
			}
			catch(Exception ex) {
				throw new Exception("Error while writing encrypted data to the stream: \n" 
					+ ex.Message);
			}
		}

		/// <summary>
		/// Creates a byte array for decryption from the input string.
		/// If the string has been formatted as hex, FormatAsHex should
		/// set to be true and the string will be converted from hex
		/// before creating the byte array. 
		/// </summary>
		/// <param name="StringToDecrypt">The encrypted string</param>
		/// <returns>A byte array from the encrypted string</returns>
		private byte[] CreateByteArray(string StringToDecrypt)
		{
			//Put the input string into the byte array.
			byte[] bytesCipherString;
			if (_FormatAsHex)
			{
				bytesCipherString = new byte[StringToDecrypt.Length / 2];
				for(int x = 0; x < StringToDecrypt.Length / 2; x++)
				{
					int i = (Convert.ToInt32(StringToDecrypt.Substring(x * 2, 2), 16));
					bytesCipherString[x] = (byte)i;
				}
			}
			else
			{
				bytesCipherString = System.Text.Encoding.UTF8.GetBytes(StringToDecrypt);
			}
			return bytesCipherString;

		}

		/// <summary>
		/// If true (default), input string will be treated as hex and decoded before decryption
		/// </summary>
		public bool FormatAsHex
		{
			get
			{
				return _FormatAsHex;
			}
			set
			{
				_FormatAsHex = value;
			}
		}
	}
