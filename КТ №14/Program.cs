using System;
using System.Collections.Generic;

namespace КТ14
{
    public record Person(string Name, int Age);
    
    public record struct PersonStruct(string Name, int Age);

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Демонстрация работы Person (record) и PersonStruct (record struct) ===\n");

            List<Person> people = new List<Person>();

            try
            {
                Console.WriteLine("--- Создание списка персонажей ---");
                while (true)
                {
                    Console.Write("Хотите добавить персонажа? (да/нет): ");
                    string? answer = Console.ReadLine()?.Trim().ToLower();
                    if (answer != "да" && answer != "yes")
                    {
                        break;
                    }

                    Console.Write("Введите имя: ");
                    string name ;
                    name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                        throw new ArgumentException("Имя не может быть пустым.");

                    Console.Write("Введите возраст: ");
                    if (!int.TryParse(Console.ReadLine(), out int age) || age < 0)
                        throw new FormatException("Некорректный возраст.");

                    people.Add(new Person(name, age));
                    Console.WriteLine($"Персонаж успешно добавлен! Всего в списке: {people.Count}\n");
                }

                if (people.Count == 0)
                {
                    Console.WriteLine("Список пуст. Добавим персонажа по умолчанию для демонстрации.");
                    people.Add(new Person("Иван", 20));
                }

                Console.WriteLine("\n--- Список всех персонажей ---");
                for (int i = 0; i < people.Count; i++)
                {
                    Console.WriteLine($"[{i}] Имя: {people[i].Name}, Возраст: {people[i].Age}");
                }

                Console.WriteLine("\n--- Сравнение персонажей ---");
                Console.Write("Введите номер (индекс) первого персонажа: ");
                if (!int.TryParse(Console.ReadLine(), out int idx1) || idx1 < 0 || idx1 >= people.Count)
                    throw new ArgumentException("Неверный номер персонажа.");

                Console.Write("Введите номер (индекс) второго персонажа: ");
                if (!int.TryParse(Console.ReadLine(), out int idx2) || idx2 < 0 || idx2 >= people.Count)
                    throw new ArgumentException("Неверный номер персонажа.");

                Person person1 = people[idx1];
                Person person2 = people[idx2];

                Console.WriteLine($"\nСравниваем: {person1} и {person2}");
                bool areEqual = (person1 == person2);
                Console.WriteLine($"new Person(\"{person1.Name}\", {person1.Age}) == new Person(\"{person2.Name}\", {person2.Age}) -> {areEqual}");

                Console.WriteLine("\n--- Изменение существующего персонажа (with) ---");
                Console.Write($"Хотите изменить возраст персонажа [{idx1}] ({person1.Name})? (да/нет): ");
                string? changeAnswer = Console.ReadLine()?.Trim().ToLower();
                if (changeAnswer == "да" || changeAnswer == "yes")
                {
                    Console.Write("Введите новый возраст: ");
                    if (!int.TryParse(Console.ReadLine(), out int newAge) || newAge < 0)
                        throw new FormatException("Некорректный возраст.");

                    Person personCopy = person1 with { Age = newAge };
                    people[idx1] = personCopy;
                    Console.WriteLine($"person with {{ Age = {newAge} }}");
                    Console.WriteLine($"Оригинал остался с Age == {person1.Age}: {person1}");
                    Console.WriteLine($"Новый объект (копия), Age == {personCopy.Age}: {personCopy}");
                }

                Console.WriteLine("\n--- Деконструкция Person ---");
                var (deconstrName, deconstrAge) = people[idx1];
                Console.WriteLine($"var (name, age) = person;");
                Console.WriteLine($"name и age получают значения свойств: name = \"{deconstrName}\", age = {deconstrAge}");

                Console.WriteLine("\n--- Работа с PersonStruct (мутабельность) ---");
                PersonStruct personStruct = new PersonStruct(people[idx1].Name, people[idx1].Age);
                Console.WriteLine($"Исходный PersonStruct: {personStruct}");

                personStruct.Age = 30;
                Console.WriteLine("personStruct.Age = 30; напрямую");
                Console.WriteLine($"Компилируется и работает (в отличие от Person.Age): {personStruct}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Ошибка]: {ex.Message}");
            }

            Console.WriteLine("\nРабота программы завершена. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}