
#include <iostream>
#include <fstream>
#include <cstdint>


struct Data {
    long longValue;
    uint8_t unsignedCharValue;

    
    void deserialize(std::ifstream& inFile) {
        inFile.read(reinterpret_cast<char*>(&longValue), sizeof(longValue));
        inFile.read(reinterpret_cast<char*>(&unsignedCharValue), sizeof(unsignedCharValue));
    }
};


void generateAsm(const Data& data, const std::string& asmFileName) {
    std::ofstream asmFile(asmFileName);
    if (!asmFile) {
        std::cerr << "������ �������� ����� ��� ������������� ����!" << std::endl;
        return;
    }

    asmFile << ".586P\n.MODEL FLAT, STDCALL\nincludelib kernel32.lib\n\n" <<
        "ExitProcess PROTO :DWORD\n" <<
        ".stack 4096\n\n";
    asmFile << ".data\n";
    asmFile << "longValue dq " << data.longValue << "\n";          
    asmFile << "unsignedCharValue db " << static_cast<int>(data.unsignedCharValue) << "\n"; 
    asmFile << "\n\n.CODE\nmain PROC\nSTART:\npush -1\ncall ExitProcess\nmain ENDP\nend main";

    asmFile.close();

    std::cout << "������������ ��� ������������ � " << asmFileName << std::endl;
}


int main() {
    setlocale(LC_ALL, "RUS");
    
    std::ifstream inFile("../SE_asm04/data.bin", std::ios::binary);
    if (!inFile) {
        std::cerr << "������ �������� ����� ��� ������!" << std::endl;
        return -1;
    }

    
    Data data;
    data.deserialize(inFile);
    inFile.close();

    
    std::cout << "����������������� ������:\n";
    std::cout << "longValue: " << data.longValue << "\n";
    std::cout << "unsignedCharValue: " << static_cast<int>(data.unsignedCharValue) << "\n";

    
    generateAsm(data, "data.asm");

    return 0;
}