using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastSignalGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 1. Константы правил игры (не меняются)
            const int OxygenDecreasePerTurn = 8;     // Трата кислорода за ход
            const int MedicalKitHealingPower = 25;    // Лечение от аптечки
            const int RequiredScrapForRepair = 3;    // Нужно лома для ремонта
            const int RequiredEnergyForRepair = 20;  // Нужно энергии для ремонта

            // 2. Переменные состояния (меняются в процессе)
            string currentCompartmentName = "Центральный отсек";
            int currentHealth = 100;
            int currentOxygen = 100;
            int energyCount = 50;
            int scrapCount = 0;
            int medicalKitsCount = 1;

            bool isTransmitterRepaired = false; // Состояние передатчика
            bool isGameRunning = true;          // Флаг работы цикла игры

            while (isGameRunning)
            {
                Console.Clear();

                // Вывод экрана отсека
                Console.WriteLine($"=== ТЕКУЩИЙ ОТСЕК: {currentCompartmentName} ===");
                Console.WriteLine("--------------------------------------------------");

                // Вывод строки состояния героя (Интерфейс)
                Console.WriteLine($"Здоровье: {currentHealth}/100 HP | Кислород: {currentOxygen}/100 O2");
                Console.WriteLine($"Ресурсы:  Металлолом: {scrapCount}/{RequiredScrapForRepair} | Энергия: {energyCount}");
                Console.WriteLine($"Инвентарь: Аптечки: {medicalKitsCount} шт.");
                Console.WriteLine($"Передатчик: {(isTransmitterRepaired ? "ИСПРАВЕН" : "ПОЛОМАН")}");
                Console.WriteLine("--------------------------------------------------");

                // Меню действий
                Console.WriteLine("1 - Исследовать Инженерный отсек");
                Console.WriteLine("2 - Использовать аптечку");
                Console.WriteLine("3 - Починить передатчик");
                Console.WriteLine("4 - Отправить сигнал SOS");
                Console.WriteLine("0 - Выйти из игры");
                Console.Write("Ваш выбор: ");

                string userInput = Console.ReadLine();

                if (!int.TryParse(userInput, out int playerCommand))
                {
                    Console.WriteLine("\n[!] Ошибка: Введите число из меню!");
                    Console.WriteLine("Нажмите любую клавишу для повтора...");
                    Console.ReadKey();
                    continue; // Возврат в начало цикла, ход не теряется
                }
                switch (playerCommand)
                {
                    case 1: // Исследование
                        currentCompartmentName = "Инженерный отсек";
                        scrapCount += 1; // Нашли 1 металлолом
                        Console.WriteLine("\nВы нашли 1 ед. металлолома.");

                        currentOxygen -= OxygenDecreasePerTurn; // Расчет кислорода
                        break;

                    case 2: // Лечение
                        if (medicalKitsCount > 0)
                        {
                            medicalKitsCount--;
                            currentHealth += MedicalKitHealingPower;
                            if (currentHealth > 100) currentHealth = 100;
                            Console.WriteLine("\nВы использовали аптечку.");
                        }
                        currentOxygen -= OxygenDecreasePerTurn;
                        break;

                    case 3: // Ремонт (Сценарий 2)
                        if (scrapCount >= RequiredScrapForRepair && energyCount >= RequiredEnergyForRepair)
                        {
                            scrapCount -= RequiredScrapForRepair;
                            energyCount -= RequiredEnergyForRepair;
                            isTransmitterRepaired = true;
                            Console.WriteLine("\n[🛠️] Передатчик успешно починен!");
                        }
                        else
                        {
                            Console.WriteLine("\nНедостаточно ресурсов для ремонта!");
                        }
                        currentOxygen -= OxygenDecreasePerTurn;
                        break;

                    case 4: // Победа
                        if (isTransmitterRepaired)
                        {
                            Console.WriteLine("\n=== ПОБЕДА! СИГНАЛ SOS ОТПРАВЛЕН ===");
                            isGameRunning = false;
                        }
                        else
                        {
                            Console.WriteLine("\nПередатчик не работает. Сигнал не уходит.");
                            currentOxygen -= OxygenDecreasePerTurn;
                        }
                        break;

                    case 0:
                        isGameRunning = false;
                        break;
                }
                // Проверка лимитов жизнеобеспечения
                if (currentHealth <= 0 || currentOxygen <= 0)
                {
                    Console.WriteLine("\n=== ИГРА ОКОНЧЕНА: ПЕРСОНАЖ ПОГИБ ===");
                    isGameRunning = false;
                }

                if (isGameRunning)
                {
                    Console.WriteLine("\nНажмите клавишу для следующего хода...");
                    Console.ReadKey();
                }
            } // Конец цикла while

        }
        //тест git
    }
}
