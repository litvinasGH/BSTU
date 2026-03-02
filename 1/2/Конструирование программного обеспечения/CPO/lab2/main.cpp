#include <iostream>

using namespace std;
int function(int x, int y) { return x + y; };

int main()
{
	//Задание 1
	//Инициализация констант
	const int n = 4,
		X = 9 + n,
		Y = 10 + n,
		Z = 11 + n;
	const float S = 1.0 + n;

	//Задание 2
	//1 байт
	bool tr = true,//01
		 fl = false;//00

	//Задания 5-6
	//1 байт 
	char eng = 'a',//61
		 ru = 'а';//e0

	//Задания 7-8
	//2 байта
	wchar_t f_eng = L'K',//4b 00
		 f_ru = L'К';//1a 04

	//Задание 9
	//2 байта
	short s_p = X, // 13(10) = D(16) //0d 00
		 s_m = -X; // -13(10) = FFF3(16) //f3 ff

	//Задание 10
	short s_max = SHRT_MAX,//32767(10) = 7FFF(16)
		s_min = SHRT_MIN;//-32768(10) = 8000(16)
	
	//Задание 11
	unsigned short us_min = 0,// 0(10) = 0(16)
		 us_max = USHRT_MAX;//65535(10) = FFFF(16)

	//Задание 12
	//4 байта
	int int_p = Y,// 14(10) = E(16) //0e 00 00 00
		 int_m = -Y;// -14(10) = FFFFFF8E(16) //f2 ff ff ff//?

	//Задание 13
	int i_max = INT_MAX, //2147483647(10) = 7FFFFFFF(16)
		 i_min = INT_MIN; //-2147483648(10) = 80000000(16)

	//Задание 14
	unsigned int ui_max = UINT_MAX, //4294967295(10) = ffffffff(16)
		 ui_min = 0; //0(10) = 0(16)

	//Задание 15
	//4 байт
	long long_p = Z, // 15(10) = F(16)//0f 00 00 00
		 long_m = -Z; // -15(10) -> BIN(11100011) -> BIN(00011101) -> HEX(FF FF FF F1)//f1 ff ff ff

	//Задание 16
	long l_max = LONG_MAX, //2147483647(10) = 7FFFFFFF(16)
		 l_min = LONG_MIN; //-2147483648(10) = 80000000(16)

	//Задание 17
	unsigned long min_u_l = 0, // 0(10) = 0(16)
		 max_u_l = ULONG_MAX; //4294967295(10) = ffffffff(16)

	//Задание 18
	//IEEE 754
	float f_p = S ,//5.000 
		 f_m = -S; //FFFB.000

	//Задание 19
	float s = S;
	double q1 = s / 0; // #INF
	cout << q1 << endl;// inf
	double q2 = -s / 0; // -#INF
	cout << q2 << endl;// - inf
	double q3 = sqrt(-5.0); // -#IND
	cout << q3 << endl;// - nan(ind)


	char name = 'v';
	wchar_t second_name = L'k';
	short sh = 18;
	int in = 15;
	float flt = 16.5;
	double db = 79.05;



	char* to_name = &name; //44 f6 0f 36 54 00 00 00
	wchar_t* to_second_name = &second_name; //64 f6 0f 36 54 00 00 00
	short* to_sh = &sh; // 84 f6 0f 36 54 00 00 00
	int* to_in = &in; //a4 f6 0f 36 54 00 00 00
	float* to_fl = &flt; //c4 f6 0f 36 54 00 00 00
	double* to_db = &db;//e8 f6 0f 36 54 00 00 00



	to_name += 3; //97 f2 cf 43 63 00 00 00
	to_second_name += 3; //ba f2 cf 43 63 00 00 00
	to_sh += 3; // da f2 cf 43 63 00 00 00
	to_in += 3; // 00 f3 cf 43 63 00 00 00
	to_fl += 3; //20 f3 cf 43 63 00 00 00
	to_db += 3;//50 f3 cf 43 63 00 00 00


	int(*f) (int, int) = function;//74 14 bf 16 f6 7f 00 00

	return 0;
}
