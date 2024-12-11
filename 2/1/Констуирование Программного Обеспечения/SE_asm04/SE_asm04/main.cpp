// DataSerializer.cpp
#include <iostream>
#include <fstream>
#include <vector>
#include <cstdint>
#include <cstring>

// Определение структуры данных
struct Data {
    long longValue;               // Переменная типа long
    uint8_t unsignedCharValue;    // Беззнаковый целочисленный литерал (1 байт)

    // Сериализация данных в бинарный формат
    void serialize(std::ofstream& outFile) const {
        outFile.write(reinterpret_cast<const char*>(&longValue), sizeof(longValue));
        outFile.write(reinterpret_cast<const char*>(&unsignedCharValue), sizeof(unsignedCharValue));
    }
};

// Главная функция
int main() {
    setlocale(LC_ALL, "RUS");
    // Инициализация данных
    Data data = { 123456789L, 0x7F };

    // Открытие файла для записи
    std::ofstream outFile("data.bin", std::ios::binary);
    if (!outFile) {
        std::cerr << "Ошибка открытия файла для записи!" << std::endl;
        return -1;
    }

    // Сериализация данных
    data.serialize(outFile);
    outFile.close();

    std::cout << "Сериализация завершена. Данные записаны в data.bin" << std::endl;
    return 0;
}