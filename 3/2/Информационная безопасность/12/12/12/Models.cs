namespace EdsaLabWpf
{
    public enum SignatureAlgorithmKind
    {
        RSA,
        ElGamal,
        Schnorr
    }

    public class KeyInfo
    {
        public string PublicKeyText { get; set; }

        public string PrivateKeyText { get; set; }

        public KeyInfo(
            string publicKey,
            string privateKey)
        {
            PublicKeyText = publicKey;
            PrivateKeyText = privateKey;
        }
    }
}