#include <iostream>

int getSum(int x, int y);
int getMul(int x, int y);

int main(int argc, char* argv[])
{
    int parm1 = 5;
    for (int i = 0; i < 5; i++) {
        int parm2 = parm1 + i;
        int result_getSum = getSum(parm1, parm2);
        std::cout << "i = " << i;
        std::cout << " getSum(" << parm1 << "," << parm2 << ") = " << result_getSum << std::endl;
    }
    int result_getMul = getMul(2, 3);
    std::cout << result_getMul << std::endl;

    system("pause");
    return 0;
}