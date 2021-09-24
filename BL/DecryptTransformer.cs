using System;
using System.Diagnostics;
using System.Security.Cryptography;


	/// <summary>
	/// Creates the proper Service Provider for the EncryptionAlgorithm
	/// </summary>
	[DebuggerStepThrough] 
	internal class DecryptTransformer
	{
		private EncryptionAlgorithm algorithmID;
		private byte[] initVec;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="deCryptId">The EncryptionAlgorithm</param>
		internal DecryptTransformer(EncryptionAlgorithm deCryptId)
		{
			algorithmID = deCryptId;
		}
		/// <summary>
		/// Returns the proper Service Provider. Sets the key and IV of
		/// the provider. If no key and IV were passed, a random key and/or
		/// IV are created (not very useful for decryption!).
		/// </summary>
		/// <param name="bytesKey">A byte array of the key</param>
		/// <returns>A CryptoServiceProvider for the EncryptionAlgorithm</returns>
		internal ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey)
		{
			// Pick the provider.
			switch (algorithmID)
			{
				case EncryptionAlgorithm.Des:
				{
					DES des = new DESCryptoServiceProvider();
					des.Mode = CipherMode.CBC;
					des.Key = bytesKey;
					des.IV = initVec;
					return des.CreateDecryptor();
				}
				case EncryptionAlgorithm.TripleDes:
				{
					TripleDES des3 = new TripleDESCryptoServiceProvider();
					des3.Mode = CipherMode.CBC;
					return des3.CreateDecryptor(bytesKey, initVec);
				}
				case EncryptionAlgorithm.Rc2:
				{
					RC2 rc2 = new RC2CryptoServiceProvider();
					rc2.Mode = CipherMode.CBC;
					return rc2.CreateDecryptor(bytesKey, initVec);
				}
				case EncryptionAlgorithm.Rijndael:
				{
					Rijndael rijndael = new RijndaelManaged();
					rijndael.Mode = CipherMode.CBC;
					return rijndael.CreateDecryptor(bytesKey, initVec);
				} 
				default:
				{
					throw new CryptographicException("Algorithm ID '" + 
						algorithmID + 
						"' not supported.");
				}
			}
		} //end GetCryptoServiceProvider
		/// <summary>
		/// The IV as an array of bytes
		/// </summary>
		internal byte[] IV
		{
			set{initVec = value;}
		}

	}
