using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;

namespace BL
{
    /// <summary>
    /// Clase encargada de implementar la encriptacion dentro de los password de 
    /// los usuarios de Autosyst XXI
    /// </summary>
	//[DebuggerStepThrough] 
    public class Seguridad
    {
        readonly String _llave;
        readonly String _vectorInicial = "init vec";

        Encryptor _encryptor = new Encryptor(EncryptionAlgorithm.Des);
        Decryptor _decryptor = new Decryptor(EncryptionAlgorithm.Des);

		public Seguridad()
			:this("81726354")
		{
		}
			
		public Seguridad(string aLlave)
		{
			_llave = aLlave;
		}


        public String EncriptarCadena(String cadenaOriginal)
        {
            _encryptor.FormatAsHex = true;
            return _encryptor.Encrypt(cadenaOriginal, _llave, _vectorInicial);
        }

        public String DesencriptarCadena(String cadenaEncriptada)
        {
            _decryptor.FormatAsHex = true;
            return _decryptor.Decrypt(cadenaEncriptada, _llave, _vectorInicial);
        }

		/// <summary> Si el password no está encriptado lo compara en texto plano </summary>
		public bool EsPasswordValidoFailSafe(string aPasswordDesencriptado, string aPasswordEncriptado)
		{
			try
			{
				return EsPasswordValido(aPasswordDesencriptado, aPasswordEncriptado);
			}
			catch(CryptographicException)
			{ 
				return aPasswordDesencriptado.Trim() == aPasswordEncriptado.Trim();
			}
		}

		/// <summary> Si el password no está encriptado lanzará error </summary>
		public bool EsPasswordValido(string aPasswordDesencriptado, string aPasswordEncriptado)
		{
			try
			{
				Decryptor	decry			= new Decryptor(EncryptionAlgorithm.Des);
				string		desencriptado	= decry.Decrypt(aPasswordEncriptado, _llave, _vectorInicial);

				return (aPasswordDesencriptado.Trim() == desencriptado);
			}
			catch (Exception)
			{
				
				throw new CryptographicException("El password aun está en formato sin encriptación");
			}
		}
    }
}
