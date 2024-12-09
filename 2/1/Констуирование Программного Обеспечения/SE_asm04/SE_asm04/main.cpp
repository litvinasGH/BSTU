// DataSerializer.cpp
#include <iostream>
#include <fstream>
#include <vector>
#include <cstdint>
#include <cstring>

// ����������� ��������� ������
struct Data {
    long longValue;               // ���������� ���� long
    uint8_t unsignedCharValue;    // ����������� ������������� ������� (1 ����)

    // ������������ ������ � �������� ������
    void serialize(std::ofstream& outFile) const {
        outFile.write(reinterpret_cast<const char*>(&longValue), sizeof(longValue));
        outFile.write(reinterpret_cast<const char*>(&unsignedCharValue), sizeof(unsignedCharValue));
    }
};

// ������� �������
int main() {
    setlocale(LC_ALL, "RUS");
    // ������������� ������
    Data data = { 123456789L, 0x7F };

    // �������� ����� ��� ������
    std::ofstream outFile("data.bin", std::ios::binary);
    if (!outFile) {
        std::cerr << "������ �������� ����� ��� ������!" << std::endl;
        return -1;
    }

    // ������������ ������
    data.serialize(outFile);
    outFile.close();

    std::cout << "������������ ���������. ������ �������� � data.bin" << std::endl;
    return 0;
}