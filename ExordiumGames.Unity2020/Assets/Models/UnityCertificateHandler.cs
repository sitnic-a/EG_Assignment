using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Assets.Models
{
    public class UnityCertificateHandler : CertificateHandler
    {
        public string PUB_KEY { get; set; }

        public UnityCertificateHandler()
        {
            
        }
        public UnityCertificateHandler(string publicKey)
        {
            PUB_KEY = publicKey;
        }
        protected override bool ValidateCertificate(byte[] certificateData)
        {
            //string publicCertificateKey = PUB_KEY.ToString();
            //byte[] newCertificate = Encoding.UTF8.GetBytes(publicCertificateKey);
            //X509Certificate2 certificate = new X509Certificate2(newCertificate, "eShopCertv2020");
            

            //X509Certificate2 output = certs[0];
            return true;
            //X509Certificate2 certificate = new X509Certificate2(certificateData, "eShopCertv2020");
            //string pk = certificate.GetPublicKeyString();

            //if (pk.ToLower().Equals(PUB_KEY.ToLower()))
            //    return true;
            //else
            //    return false;
        }
    }
}
