using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Diagnostics;


	/// <summary>
	/// The Encryptor class handles encryption of a string using one of the 4 
	/// symmetric algorithms - 3DES, DES, RC2 or Rijndael. 
	/// </summary>
	/// <remarks>
	/// When creating an
	/// instance of this class, you must pass the algorithm to use in the 
	/// constructor: see <see cref="EncryptionAlgorithm"/>. Optionally, you can set the output to be formatted as
	/// hex through the FormatAsHex property.
	/// 
	/// This class is based on principles from the Patterns and Practices guide: 
	/// How To: Create an Encryption Library 
	/// by J.D. Meier, Alex Mackman, Michael Dunner, and Srinath Vasireddy 
	/// Microsoft Corporation</remarks>
	//[DebuggerStepThrough] 
	internal class Encryptor
	{
		private EncryptTransformer transformer;
		private byte[] initVec;
		private byte[] encKey;
		private bool _FormatAsHex = true;

		/// <summary>
		/// Class constructor
		/// </summary>
		/// <param name="algId">The encryption algorithm to use</param>
		public Encryptor(EncryptionAlgorithm algId)
		{
			transformer = new EncryptTransformer(algId);
		}
		/// <summary>
		/// Encrypts a string using the algorithm supplied in
		/// the class constructor using the provided key and
		/// initial vector.
		/// </summary>
		/// <remarks>Note that the length and composition of the key and IV
		/// are algorithm dependent. For example, 3DES requires a 16 or 24 byte key,
		/// whereas DES requires an 8 byte key. See <see cref="GetKey()"/>
		/// and <see cref="GetIV()"/></remarks>
		/// <param name="StringToEncrypt">The string to be encrypted</param>
		/// <param name="key">The key to use for encryption. The same key will
		/// be used for decryption.</param>
		/// <param name="IV">THe Initial Vector to use. The same IV will be
		/// used for decryption</param>
		/// <returns>An encrypted string. If FormatAsHex has been set,
		/// the string will be hex encoded.</returns>
		public string Encrypt(string StringToEncrypt, string key, string IV)
		{
			string cipherText = "";
			try
			{
				byte[] abPlainText = System.Text.Encoding.UTF8.GetBytes(StringToEncrypt);
				byte[] bytesKey = System.Text.Encoding.UTF8.GetBytes(key);
				initVec = System.Text.Encoding.UTF8.GetBytes(IV);

				MemoryStream memStreamEncryptedData = new MemoryStream();

				transformer.IV = initVec;
				ICryptoTransform transform = 
					transformer.GetCryptoServiceProvider(bytesKey);

				CryptoStream encStream;
				CryptoStream cs_base64 = new CryptoStream(memStreamEncryptedData, new ToBase64Transform(), 
					CryptoStreamMode.Write);

				encStream = new CryptoStream(cs_base64, transform, CryptoStreamMode.Write);
			
				//Encrypt the data, write it to the memory stream.

				encStream.Write(abPlainText, 0, abPlainText.Length);
			
				encKey = transformer.Key;
				initVec = transformer.IV;
				encStream.FlushFinalBlock();
				encStream.Close();

				// Get encrypted string from memory stream
				cipherText = CreateCipherString(memStreamEncryptedData.ToArray());

			}
			catch(Exception ex)
			{
				throw new Exception("Error while writing encrypted data to the stream: \n" 
					+ ex.Message);
			}
			return cipherText;

		}


		/// <summary>
		/// Creates a string from the encrypted byte array. If
		/// FormatAsHex is true, a hex formattted string will
		/// be returned. Formatting as hex can make storage in a
		/// database simpler as only legal characters will be output.
		/// </summary>
		/// <param name="BytesToConvert">The array of encrypted bytes</param>
		/// <returns>The encrypted string.</returns>
		private string CreateCipherString(byte[] BytesToConvert)
		{
			string cipherText;
			//Get the data back from the byte array, and into a string
			if (_FormatAsHex)
			{
				StringBuilder ret = new StringBuilder();
				foreach(byte b in BytesToConvert)
				{
					//Format as hex
					ret.AppendFormat("{0:X2}", b);
				}
				cipherText = ret.ToString();
			}
			else
			{
				cipherText = System.Text.Encoding.UTF8.GetString(BytesToConvert);
			}
			return cipherText;

		}

		/// <summary>
		/// A new key which can be used for encryption.
		/// </summary>
		/// <remarks>This method allows the creation of valid keys
		/// for the selcted algorithm. The format and length of a key
		/// is dependent on the algorithm, so use this method to get
		/// a valid key and then use this key in the Encrypt / Decrypt
		/// methods.</remarks>
		/// <returns>A new, valid, algorithm specific key as a string.</returns>
		public string GetKey()
		{
			ICryptoTransform transform = 
				transformer.GetCryptoServiceProvider(null);
			byte[] bytesKey = transformer.Key;
			
			string newKey = System.Text.Encoding.UTF8.GetString(bytesKey);
			return newKey;
		}

		/// <summary>
		/// A new Initial Vector to use for encryption
		/// </summary>
		/// <remarks>This method allows the creation of valid Initial
		/// Vectors for the selcted algorithm. The format and length of an IV
		/// is dependent on the algorithm, so use this method to get
		/// a valid IV and then use this key in the Encrypt / Decrypt
		/// methods.</remarks>
		/// <returns>A new, valid, algorithm dependent IV as a string</returns>
		public string GetIV()
		{
			ICryptoTransform transform = 
				transformer.GetCryptoServiceProvider(null);
			byte[] bytesIV = transformer.IV;
			
			string newIV = System.Text.Encoding.ASCII.GetString(bytesIV);
			return newIV;
		}
	
		/// <summary>
		/// The current Initial Vector used in Encryption
		/// </summary>
		public string IV
		{
			get{return System.Text.Encoding.UTF8.GetString(initVec);}
		}

		/// <summary>
		/// The current Key being used in Encryption
		/// </summary>
		public string Key
		{
			get{return System.Text.Encoding.UTF8.GetString(encKey);}
		}

		/// <summary>
		/// If true (default) output will be formatted as hex.
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

