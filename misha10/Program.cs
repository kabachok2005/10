using Newtonsoft.Json;
class Program
{
    static List<pol> users;
    static string path = Directory.GetCurrentDirectory();

    static void Main()
    {
        if (Directory.Exists("пользователи") == false)
        {
            Directory.CreateDirectory(path + "/пользователи");
        }
        if (File.Exists("пользователи\\пользователи.json") == false)
        {
            FileInfo fi = new FileInfo(path + "/пользователи" + "/пользователи.json");
            FileStream fs = fi.Create();
            fs.Close();
        }
        users = JsonConvert.DeserializeObject < List < pol»(File.ReadAllText("пользователи\\пользователи.json")) ?? new List<pol>();
        if (users.Count == 0)
        {
            pol s = new pol();
            s.log = "админ";
            s.pas = "админ";
            users.Add(s);
        }
        string us = Auth();
        int ra = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Роль: {us}");
            Console.WriteLine($"\nКоманды:\nF1 - Добавить пользователя | F2 - Удалить\nF3 - Обновить | F4 - Поиск | Escape - Сохранить\n");
            Console.WriteLine("Все пользователи:");
            foreach (pol pol in users)
            {
                Console.WriteLine(" " + pol.log);
            }
            Console.SetCursorPosition(0, 7 + ra);
            Console.WriteLine("->");
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.DownArrow)
            {
                if (ra < users.Count - 1)
                    ra++;
            }
            if (key == ConsoleKey.UpArrow)
            {
                if (ra > 0)
                    ra--;
            }
            if (key == ConsoleKey.Enter)
            {
                pol aw = users[ra];
                Console.WriteLine($"Логин: {aw.log}, пароль: {aw.pas}");
                Console.ReadKey();
            }
            if (key == ConsoleKey.F1)
                Add();
            if (key == ConsoleKey.F2)
            {
                users.RemoveAt(ra);
                ra = 0;
            }
            if (key == ConsoleKey.F3)
                Update(ra);
            if (key == ConsoleKey.F4)
                Search();
            if (key == ConsoleKey.Escape)
            {
                File.WriteAllText("пользователи//пользователи.json", JsonConvert.SerializeObject(users));
            }
        }
    }
    static string Auth()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Введите логин");
            string us = Console.ReadLine();
            Console.WriteLine("Введите пароль");
            string pas = null;
            ConsoleKeyInfo info;
            while (true)
            {
                info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Enter)
                {
                    break;
                }
                pas += info.KeyChar;
                Console.Write('*');
            }
            Console.WriteLine();
            foreach (pol pol in users)
            {
                if (us == pol.log && pas == pol.pas)
                {
                    return us;
                }
            }
            Console.WriteLine("Логин или пароль неверный");
        }
    }
    static void Add()
    {
        pol s = new pol();
        Console.Clear();
        Console.WriteLine("Введите логин");
        s.log = Console.ReadLine();
        Console.WriteLine("Введите пароль");
        s.pas = Console.ReadLine();
        users.Add(s);
    }

    static void Update(int pol)
    {
        pol s = new pol();
        Console.Clear();
        Console.WriteLine("Новый логин");
        s.log = Console.ReadLine();
        Console.WriteLine("Новый пароль");
        s.pas = Console.ReadLine();
        users[pol] = s;
    }

    static void Search()
    {
        int y = 0;
        bool search = true;
        while (search)
        {
            Console.Clear();
            Console.WriteLine("Искать по");
            Console.WriteLine(" Логину");
            Console.WriteLine(" Паролю");
            Console.SetCursorPosition(0, y + 1);
            Console.WriteLine("->");
            ConsoleKey key = Console.ReadKey().Key;
            if (key == ConsoleKey.UpArrow)
            {
                if (y > 0)
                    y--;
            }
            if (key == ConsoleKey.DownArrow)
            {
                if (y < 1)
                    y++;
            }
            if (key == ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("Введите значение для поиска");
                string val = Console.ReadLine();
                Console.WriteLine("Результат поиска:");
                if (y == 0)
                    foreach (pol pol in users)
                    {
                        if (pol.log.Contains(val))
                        {
                            Console.WriteLine(pol.log);
                        }
                    }
                if (y == 1)
                    foreach (pol pol in users)
                    {
                        if (pol.pas.Contains(val))
                        {
                            Console.WriteLine(pol.log);
                        }
                    }
            }
            Console.ReadKey();
            search = false;
        }
    }
}
class pol
{
    public string log;
    public string pas;
}