
#include <iostream>
#include <fstream>
#include <vector>
#include <cstdint>
#include <cstring>

enum
{
    lv = 0,
    uc = 1
};


struct Data {
    enum
    {
        lv = 0,
        uc = 1
    };
    char type;
    union
    {
        long longValue;
        uint8_t unsignedCharValue;
    }data; 

    
    void serialize(std::ofstream& outFile) const {
        switch (type)
        {
        case lv:
            outFile.write(reinterpret_cast<const char*>(type), sizeof(char));
            outFile.write(reinterpret_cast<const char*>(data.longValue), sizeof(data.longValue));
            break;
        case uc:
            outFile.write(reinterpret_cast<const char*>(type), sizeof(char));
            outFile.write(reinterpret_cast<const char*>(data.unsignedCharValue), sizeof(data.unsignedCharValue));
            break;
        default:
            std::cout << "Не поддерживается";
            break;
        }
        /*outFile.write(reinterpret_cast<const char*>(&longValue), sizeof(longValue));
        outFile.write(reinterpret_cast<const char*>(&unsignedCharValue), sizeof(unsignedCharValue));*/
    }
};


int main() {
    setlocale(LC_ALL, "RUS");
    
    Data data = {};
    data.type = lv;
    data.data.longValue = 7837L;

    
    std::ofstream outFile("data.bin", std::ios::binary);
    if (!outFile) {
        std::cerr << "Ошибка открытия файла для записи!" << std::endl;
        return -1;
    }

    
    data.serialize(outFile);
    outFile.close();

    std::cout << "Сериализация завершена. Данные записаны в data.bin" << std::endl;
    return 0;
}