using System;
using System.Security.Cryptography;
class Program
{
    static string data_path;
    static string target_hash;
    static public int number = 0;
    static string[] list;
    static void Main(string[] args)
    {
        string local_data_path = "data\\wordlist.txt";
        string current_path = Directory.GetCurrentDirectory();

        try
        {
            data_path = Path.Combine(current_path, local_data_path);
            try
            {
                list = File.ReadAllLines(data_path);
                int character;
                int key_action;
                Console.Write("Enter target hash: ");
                target_hash = Console.ReadLine().ToUpper();
                Console.Clear();
                for (key_action = 0; key_action == 0;)
                {
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("[1] Normal mode");
                    Console.WriteLine("[2] Visual mode");
                    Console.WriteLine("[3] Optimized mode");
                    char readkey = Console.ReadKey().KeyChar;
                    int number;
                    if (int.TryParse(readkey.ToString(), out number))
                    {
                        if ((number < 4) && (number > 0))
                        {
                            key_action = number;
                        }
                    }
                }
                Console.Clear();
                switch (key_action)
                {
                    case 1:
                        long ammount_of_tries = 0;
                        for (int i = 0; i < list.Length; i++)
                        {
                            for (int j = 0; j < list.Length; j++)
                            {
                                ammount_of_tries++;
                                string targetword = list[i] + list[j];
                                string result = CreateMD5(targetword);
                                if (result == target_hash)
                                {
                                    Console.Clear();
                                    Console.WriteLine(targetword + "resulted in " + result);
                                    Console.WriteLine("The targets were: " + list[i] + " , " + list[j]);
                                    Console.WriteLine(ammount_of_tries + "were needed to find the correct combination");
                                    Console.ReadKey();
                                    break;
                                }
                                if(ammount_of_tries % 5000000 == 0)
                                {
                                    string whatYouWant = ammount_of_tries.ToString("#,##0");
                                    Console.WriteLine(whatYouWant + " tries    " + result);
                                }
                            }
                        }
                        break;
                    case 2:
                        long two_ammount_of_tries = 0;
                        for (int i = 0; i < list.Length; i++)
                        {
                            for (int j = 0; j < list.Length; j++)
                            {
                                two_ammount_of_tries++;
                                string targetword = list[i] + list[j];
                                string result = CreateMD5(targetword);
                                if (result == target_hash)
                                {
                                    Console.Clear();
                                    Console.WriteLine(targetword + "resulted in " + result);
                                    Console.WriteLine("The targets were: " + list[i] + " , " + list[j]);
                                    Console.WriteLine(two_ammount_of_tries + "were needed to find the correct combination");
                                    Console.ReadKey();
                                    break;
                                }
                                string whatYouWant = two_ammount_of_tries.ToString("#,##0");
                                Console.WriteLine(whatYouWant + " tries    " + result);
                            }
                        }
                        break;
                    case 3:
                        Console.WriteLine("Working...");
                        for (int i = 0; i < list.Length; i++)
                        {
                            for (int j = 0; j < list.Length; j++)
                            {
                                string targetword = list[i] + list[j];
                                string result = CreateMD5(targetword);
                                if (result == target_hash)
                                {
                                    Console.Clear();
                                    Console.WriteLine(targetword + "resulted in " + result);
                                    Console.WriteLine("The targets were: " + list[i] + " , " + list[j]);
                                    Console.ReadKey();
                                    break;
                                }
                            }
                        }
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Internal error");
                        Console.WriteLine("key_action invalid");
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Could not acces \"wordlist.txt\"");
                Console.WriteLine("System: " + ex.Message);
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Could not find \"wordlist.txt\"");
            Console.WriteLine("System: " + ex.Message);
        }

    }
    public static string CreateMD5(string input)
    {
        using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
    }
}