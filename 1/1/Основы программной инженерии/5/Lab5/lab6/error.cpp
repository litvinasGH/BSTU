//err.cpp
#include "error.h"

void errorOutput(int code)
{
	switch (code){
	case 1:	MessageBox(NULL, TEXT("�������� �������. ���� ������ �� 1 �� 4. ���������� � ����"), TEXT("�������� ������� =["), MB_OK | MB_ICONERROR);
		break;
	case 2: MessageBox(NULL, TEXT("� ������ �������� ������� �������� ������. ������� �������. ���������� � ����"), TEXT("�������� ������ =[."), MB_OK | MB_ICONERROR); 
		break;
	default: MessageBox(NULL, TEXT("����������� ������. ���������� � ����"), TEXT("����������� ������ =["), MB_OK | MB_ICONERROR);
		break;
	}
}