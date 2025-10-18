using System.Data;
using System.Text.Json;

namespace LB1;

class Program
{
    static void Main(string[] args)
    {
        List<Polynomial> pol = [];
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("1 – Додати багаточлен");
            Console.WriteLine("2 – Видалити багаточлен");
            Console.WriteLine("3 – Редагувати багаточлен");
            Console.WriteLine("4 – Показати всі багаточлени");
            Console.WriteLine("5 – Запит (вивести багаточлени зі ступенем >= заданого)");
            Console.WriteLine("6 – Зберегти весь список у JSON");
            Console.WriteLine("7 – Завантажити список із JSON");
            Console.WriteLine("0 – Вихід");
            Console.Write("Ваш вибір: ");
            int x = Convert.ToInt32(Console.ReadLine());
            switch (x)
            {
                case 1:
                    AddPolynomial(pol);
                    break;
                case 2:
                    RemovePolynomial(pol);
                    break;
                case 3:
                    EditPolynomial(pol);
                    break;
                case 4:
                    ShowPolynomials(pol);
                    break;
                case 5:
                    QueryPolynomials(pol);
                    break;
                case 6:
                    SaveToJson(pol,"polynomials.json");
                    break;
                case 7:
                    LoadFromJson(pol,"polynomials.json");
                    break;
                case 0:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Wrong input");
                    break;
            }
        }

        Console.ReadKey();
    }

    static void AddPolynomial(List<Polynomial> list)
    {
        Console.Write("Введіть ступінь багаточлена: ");
        int degree = Convert.ToInt32(Console.ReadLine());
        double[] coefficients = new double[degree + 1];

        for (int i = degree; i >= 0; i--)
        {
            Console.Write($"Коефіцієнт при x^{i}: ");
            coefficients[i] = double.Parse(Console.ReadLine());
        }

        list.Add(new Polynomial(degree, coefficients));
        Console.WriteLine("Багаточлен додано!");
    }

    static void RemovePolynomial(List<Polynomial> list)
    {
        ShowPolynomials(list);
        Console.Write("Введіть індекс для видалення: ");
        int index = int.Parse(Console.ReadLine());

        if (index >= 0 && index < list.Count)
        {
            list.RemoveAt(index);
            Console.WriteLine("Видалено!");
        }
        else
        {
            Console.WriteLine("Невірний індекс!");
        }
    }

    static void EditPolynomial(List<Polynomial> list)
    {
        ShowPolynomials(list);
        Console.Write("Введіть індекс для редагування: ");
        int index = int.Parse(Console.ReadLine());

        if (index >= 0 && index < list.Count)
        {
            Console.Write("Введіть новий ступінь: ");
            int degree = int.Parse(Console.ReadLine());

            double[] coefficients = new double[degree + 1];
            for (int i = degree; i >= 0; i--)
            {
                Console.Write($"Коефіцієнт при x^{i}: ");
                coefficients[i] = double.Parse(Console.ReadLine());
            }

            list[index] = new Polynomial(degree, coefficients);
            Console.WriteLine("Змінено!");
        }
        else
        {
            Console.WriteLine("Невірний індекс!");
        }
    }

    static void ShowPolynomials(List<Polynomial> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Список пустий!");
            return;
        }

        for (int i = 0; i < list.Count; i++)
        {
            Console.WriteLine($"{i}: {list[i]} (ступінь {list[i].Degree})");
        }
    }

    static void QueryPolynomials(List<Polynomial> list)
    {
        Console.Write("Введіть мінімальний ступінь: ");
        int minDegree = int.Parse(Console.ReadLine());

        foreach (var p in list)
        {
            if (p.Degree >= minDegree)
            {
                Console.WriteLine($"Ступінь {p.Degree}, Багаточлен: {p}");
            }
        }
    }

    static void SaveToJson(List<Polynomial> list, string filename)
    {
        string json = JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filename, json);
        Console.WriteLine($"Список збережено у файл {filename}");
    }
    
    static void LoadFromJson(List<Polynomial> list, string filename)
    {
        if (!File.Exists(filename))
        {
            Console.WriteLine($"Файл {filename} не знайдено!");
            return;
        }

        string json = File.ReadAllText(filename);
        var loaded = JsonSerializer.Deserialize<List<Polynomial>>(json);

        if (loaded != null)
        {
            list.Clear();
            list.AddRange(loaded);
            Console.WriteLine($"Дані успішно завантажено з {filename}!");
        }
        else
        {
            Console.WriteLine("Не вдалося завантажити дані!");
        }
    }
    
}