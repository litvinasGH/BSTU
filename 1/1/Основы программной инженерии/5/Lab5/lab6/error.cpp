//err.cpp
#include "error.h"

void errorOutput(int code)
{
	switch (code){
	case 1:	MessageBox(NULL, TEXT("Неверный вариант. Надо ввести от 1 до 4. Возращение в меню"), TEXT("Неверный вариант =["), MB_OK | MB_ICONERROR);
		break;
	case 2: MessageBox(NULL, TEXT("В списке символов имеется неверный символ. Читайте условие. Возращение в меню"), TEXT("Неверный символ =[."), MB_OK | MB_ICONERROR); 
		break;
	default: MessageBox(NULL, TEXT("Неизвестная ошибка. Возращение в меню"), TEXT("Неизвестная ошибка =["), MB_OK | MB_ICONERROR);
		break;
	}
}