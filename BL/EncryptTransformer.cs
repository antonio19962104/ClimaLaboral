using System;
using System.Diagnostics;
using System.Security.Cryptography;


	/// <summary>
	/// The Encryption algorithm to use
	/// </summary>
	internal enum EncryptionAlgorithm {
		/// <summary>
		/// Use <see cref="DES"/> 
		/// </summary>
		Des = 1, 
		/// <summary>
		/// Use <see cref="RC2"/>
		/// </summary>
		Rc2, 
		/// <summary>
		/// Use <see cref="Rijndael"/>
		/// </summary>
		Rijndael, 
		/// <summary>
		/// Use <see cref="TripleDES"/>
		/// </summary>
		TripleDes};
	
	/// <summary>
	/// This class is used to return a Crypto Provider of the type
	/// specified.
	/// 
	/// </summary>
	/// <remarks>
	/// This class is based on the principles from Patterns and Practices guide: 
	/// How To: Create an Encryption Library 
	/// by J.D. Meier, Alex Mackman, Michael Dunner, and Srinath Vasireddy 
	/// Microsoft Corporation
	/// </remarks>
	[DebuggerStepThrough] 
	internal class EncryptTransformer
	{
		/// <summary>
		/// The Algorithn to use
		/// </summary>
		private EncryptionAlgorithm algorithmID;
		/// <summary>
		/// The Initial vector to use
		/// </summary>
		private byte[] initVec;
		/// <summary>
		/// The encryption key to use
		/// </summary>
		private byte[] encKey;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="algId">The Algorithm to use</param>
		internal EncryptTransformer(EncryptionAlgorithm algId)
		{
			//Save the algorithm being used.
			algorithmID = algId;
		}
		/// <summary>
		/// Returns the proper Service Provider. Sets the key and IV of
		/// the provider. If no key and/or IV were passed, a random key and/or
		/// IV are created. They can then be retrieved from the <see cref="Encryptor.Key"/>
		/// and <see cref="Encryptor.IV"/> properties. (Do not call <see cref="Encryptor.GetKey()"/> 
		/// or <see cref="Encryptor.GetIV()"/> as
		/// these create <i>new</i> keys).
		/// </summary>
		/// <param name="bytesKey"></param>
		/// <returns></returns>
		internal ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey)
		{
			// Pick the provider.
			try
			{
				switch (algorithmID)
				{
					case EncryptionAlgorithm.Des:
					{
						DES des = new DESCryptoServiceProvider();
						des.Mode = CipherMode.CBC;

						// See if a key was provided
						if (null == bytesKey)
						{
							encKey = des.Key;
						}
						else
						{
							des.Key = bytesKey;
							encKey = des.Key;
						}
						// See if the client provided an initialization vector
						if (null == initVec)
						{ // Have the algorithm create one
							initVec = des.IV;
						}
						else
						{ //No, give it to the algorithm
							des.IV = initVec;
						}
						return des.CreateEncryptor();
					}
					case EncryptionAlgorithm.TripleDes:
					{
						TripleDES des3 = new TripleDESCryptoServiceProvider();
						des3.Mode = CipherMode.CBC;
						// See if a key was provided
						if (null == bytesKey)
						{
							encKey = des3.Key;
						}
						else
						{
						
							des3.Key = bytesKey;
							encKey = des3.Key;
						
						
						}
						// See if the client provided an IV
						if (null == initVec)
						{ //Yes, have the alg create one
							initVec = des3.IV;
						}
						else
						{ //No, give it to the alg.
							des3.IV = initVec;
						}
						return des3.CreateEncryptor();
					}
					case EncryptionAlgorithm.Rc2:
					{
						RC2 rc2 = new RC2CryptoServiceProvider();
						rc2.Mode = CipherMode.CBC;
						// Test to see if a key was provided
						if (null == bytesKey)
						{
							encKey = rc2.Key;
						}
						else
						{
							rc2.Key = bytesKey;
							encKey = rc2.Key;
						}
						// See if the client provided an IV
						if (null == initVec)
						{ //Yes, have the alg create one
							initVec = rc2.IV;
						}
						else
						{ //No, give it to the alg.
							rc2.IV = initVec;
						}
						return rc2.CreateEncryptor();
					}
					case EncryptionAlgorithm.Rijndael:
					{
						Rijndael rijndael = new RijndaelManaged();
						rijndael.Mode = CipherMode.CBC;
						// Test to see if a key was provided
						if(null == bytesKey)
						{
							encKey = rijndael.Key;
						}
						else
						{
							rijndael.Key = bytesKey;
							encKey = rijndael.Key;
						}
						// See if the client provided an IV
						if(null == initVec)
						{ //Yes, have the alg create one
							initVec = rijndael.IV;
						}
						else
						{ //No, give it to the alg.
							rijndael.IV = initVec;
						}
						return rijndael.CreateEncryptor();
					} 
					default:
					{
						throw new CryptographicException("Algorithm ID '" + 
							algorithmID + 
							"' not supported.");
					}
				
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

		/// <summary>
		///  The IV as an array of bytes
		/// </summary>
		internal byte[] IV
		{
			get{return initVec;}
			set{initVec = value;}
		}

		/// <summary>
		/// THe key as an array of bytes
		/// </summary>
		internal byte[] Key
		{
			get{return encKey;}
		}
	}

