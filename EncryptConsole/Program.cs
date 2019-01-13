using System;
using Yachne.Common.Encrypt;

namespace EncryptConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            string publicKey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCLTsQJB3UjLFx/Ok1Wi19nxbOCBt/+naYQ6w2Tq31jZlR/ZkORvQug1+ZVuwpnF0t9MmopJjX0X11+YFm1L2Nc+Ig6viQN51lQS5JukyyiXQ3zBZXu7fXtwPrJKvFnDlj6GjKl3qsUqjDxxMKCWoRGnk0/KCPloDsIgUCc1cS+dwIDAQAB";
            string privateKey = @"MIICXAIBAAKBgQCLTsQJB3UjLFx/Ok1Wi19nxbOCBt/+naYQ6w2Tq31jZlR/ZkORvQug1+ZVuwpnF0t9MmopJjX0X11+YFm1L2Nc+Ig6viQN51lQS5JukyyiXQ3zBZXu7fXtwPrJKvFnDlj6GjKl3qsUqjDxxMKCWoRGnk0/KCPloDsIgUCc1cS+dwIDAQABAoGAEm+FD/DVtqbrQscTbw2YvaHzRJTmVcrFLF3++Pjr6hijvAxrisDq5glMaTIMiWIS0mm2lOWCpGludQgJNyojSvl8Od9fqAqZI9fIj52Ho+5PMZ1l4IMniMgU4wMyn2mZqhsxSuiWuwWcWA8MNCLSx8JNV0jPv9AUJEjN5Bg9TPkCQQCOkIf7UbFliSA60aiZx4ezVHdWWxzENZnDb7GB/3JtjbXcQPrMrI08uLh1QixT+gNB7oxnk0xEPqjbtdLLHtD9AkEA+ibTgc9WoC5GSz5bHtCXUHY3ZPry9JcJLSnzFgVUhZtPbt8KRAk5wjOqd30sHFsE13PQpfgYq7ZZTVcqy0sRgwJAB1XQaVHuuravfddDwYXOqZ9y9HKDrGTFoJSioXmvPYvJC6gcP2OxcKpgc0gQV9HJUR8hAkNF7Uz8CzHzwpe3UQJBAMK5xpueEwjN/NpFyBjMt31jCOwKjWXozLPjm97gd1Mp+0OLTCp6JAQQw/oP7m6ES9iLxzfrUQkaAZo66I0n+pECQGZwBuHm2rqfpT8hAF8oz6GhXJ+op9TaRWeaQlYW5tZh3p5GfI9nvIyPY/Uw/wv7TtGvmzgCqdOZilz0CjbXS1s=";

            while (true)
            {
                Console.WriteLine("1: 加密; 2: 解密");
                string selection = Console.ReadLine();
                try
                {
                    if (selection.Equals("1") || string.IsNullOrEmpty(selection))
                    {
                        string plainText = Console.ReadLine();
                        Console.WriteLine(EncryptProvider.RSAEncrypt(plainText, publicKey));
                    }

                    if (selection.Equals("2"))
                    {
                        string encryptText = Console.ReadLine();
                        Console.WriteLine(EncryptProvider.RSADecrypt(encryptText, privateKey));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
           
        }
    }
}
