using System;
using EdDSAJwtBearer;
namespace EdDSAJwtBearerKeysGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var EdDSAKeys = EdDSATokenHandler.CreateDerEncodedKeys();
            Console.WriteLine("Private key:");
            Console.WriteLine(EdDSAKeys.Private);
            Console.WriteLine("Public key:");
            Console.WriteLine(EdDSAKeys.Public);
            Console.WriteLine("Presiona cualquier tecla para continuar");
            Console.ReadKey();
        }
    }
}
